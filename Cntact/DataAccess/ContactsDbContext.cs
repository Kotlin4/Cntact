using Cntact.Models;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cntact.DataAccess
{
    public class ContactsDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ContactsDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Contacts> Contacts => Set<Contacts>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
        }
    }
}
