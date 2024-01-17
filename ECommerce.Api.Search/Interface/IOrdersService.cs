using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interface
{
    public interface IOrdersService
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders,string ErrorMessage)> GetOrdersAsync(int CustomerId);
    }
}
