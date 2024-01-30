namespace Ecommerce.Api.Search.Interface
{
    public interface ISearchService
    {
        Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerID);

    }
}
