using Ecommerce.Api.Search.Model;

namespace Ecommerce.Api.Search.Interface
{
    public interface IProductsService
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)>
            GetProductsAsync();
    }
}
