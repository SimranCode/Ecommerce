namespace ECommerce.Api.Search.Interface
{
    public interface ISearchService
    {
        Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int customerid);
    }
}
