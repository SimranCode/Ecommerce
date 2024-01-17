using ECommerce.Api.Search.Interface;
using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService orderService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService orderService , IProductsService productsService,ICustomersService customersService)
        {
            this.orderService = orderService;
            this.productsService = productsService;
            this.customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int customerid)
        {
            var ordersResult = await orderService.GetOrdersAsync(customerid);
            var productsResult = await productsService.GetProductsAsync();
            var customersresult = await customersService.GetCustomersAsync(customerid);

            if(ordersResult.IsSuccess)
            {
                foreach(var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess ? productsResult.products.Where(x => x.Id == item.ProductId).FirstOrDefault().Name : "Product Name is not available";
                    }
                }


                var output = new
                {
                    Customer = customersresult.IsSuccess ? customersresult.Customer : new { Name = "Customer details not available" },
                    Orders =  ordersResult.Orders
                };
                return (ordersResult.IsSuccess, output);
            }
            return (false, null);

        }
    }
}
