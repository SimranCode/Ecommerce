using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interface
{
    public interface ICustomersService
    {
        Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> GetCustomersAsync(int id);
    }
}
