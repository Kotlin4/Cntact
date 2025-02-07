using Cntact.Contracts;
using Cntact.DataAccess;
using Cntact.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cntact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsDbContext _dbContext;

        public ContactsController(ContactsDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateContactRequest request, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = new Contacts(request.Number, request.FirstName, request.Name, request.LastName);

            await _dbContext.Contacts.AddAsync(contact, ct);
            await _dbContext.SaveChangesAsync(ct);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetContactsRequest request, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contactsQuery = _dbContext.Contacts
                .Where(n => string.IsNullOrEmpty(request.Search) ||
                    n.FirstName.ToLower().Contains(request.Search.ToLower()));

            if (request.SortOrder == "desc")
            {
                contactsQuery = contactsQuery.OrderByDescending(n => n.FirstName);
            }
            else
            {
                contactsQuery = contactsQuery.OrderBy(n => n.FirstName);
            }

            var contactDtos = await contactsQuery
                .Select(n => new ContactDto(n.Id, n.Number, n.FirstName, n.Name, n.LastName))
                .ToListAsync();

            return Ok(new GetContactsResponse(contactDtos));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteContactRequest request, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = _dbContext.Contacts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Number)) { query = query.Where(c => c.Number == request.Number); }
            if (!string.IsNullOrWhiteSpace(request.FirstName)) { query = query.Where(c => c.FirstName == request.FirstName); }
            if (!string.IsNullOrWhiteSpace(request.Name)) { query = query.Where(c => c.Name == request.Name); }
            if (!string.IsNullOrWhiteSpace(request.LastName)) { query = query.Where(c => c.LastName == request.LastName); }

            var contactsToDelete = await query.ToListAsync(ct);

            if (contactsToDelete.Count == 0)
            {
                return NotFound();
            }

            _dbContext.Contacts.RemoveRange(contactsToDelete);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
