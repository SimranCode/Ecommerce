using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interface
{
    public interface IProductsService
    {
        Task<(bool IsSuccess, IEnumerable<Product> products, string ErrorMessage)> GetProductsAsync();
    }
}
