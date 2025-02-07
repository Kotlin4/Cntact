namespace Cntact.Contracts
{
    public record GetContactsRequest(string? Search, string? SortItem, string? SortOrder);

}
