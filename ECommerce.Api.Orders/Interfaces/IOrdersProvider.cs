namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Order>, string ErrorMessage)> GetOrdersAsync(int CustomerId);
        Task<(bool IsSuccess, Models.Order, string ErrorMessage)> GetOrderAsync(int id, int CustomerId);
        //Task<(bool IsSuccess, IEnumerable<Models.OrderItem>, string ErrorMessage)> GetOrderItemsAsync(int orderId);

    }
}
