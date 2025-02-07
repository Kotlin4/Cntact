namespace Cntact.Contracts
{
    public record ContactDto(Guid Id, string Number, string FirstName, string Name, string? LastName);
}
