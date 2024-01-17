using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger , IMapper mapper) 
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (dbContext.OrderItems == null || !dbContext.OrderItems.Any())
            {

                dbContext.OrderItems.Add(new Db.OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 10 });
                dbContext.OrderItems.Add(new Db.OrderItem { Id = 5, OrderId = 1, ProductId = 2, Quantity = 2, UnitPrice = 20 });
                dbContext.OrderItems.Add(new Db.OrderItem { Id = 2, OrderId = 2, ProductId = 2, Quantity = 3, UnitPrice = 100 });
                dbContext.OrderItems.Add(new Db.OrderItem { Id = 3, OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 108 });
                dbContext.OrderItems.Add(new Db.OrderItem { Id = 4, OrderId = 4, ProductId = 4, Quantity = 4, UnitPrice = 900 });
                dbContext.SaveChanges();
            }

            if(dbContext.Orders == null || !dbContext.Orders.Any()) 
            {
            dbContext.Orders.Add( new Db.Order() { Id = 1, CustomerId = 1, Items = dbContext.OrderItems.Where(x => x.OrderId == 1).ToList(), OrderDate = new DateTime(2024, 01, 12, 11, 50, 45), Total = dbContext.OrderItems.Where(x => x.OrderId == 1).ToList().Count() });
            dbContext.Orders.Add( new Db.Order() { Id = 2,CustomerId = 2,Items = dbContext.OrderItems.Where(x=>x.OrderId == 2).ToList()  , OrderDate = new DateTime(2024,01,13,11,50,45), Total = dbContext.OrderItems.Where(x => x.OrderId == 2).ToList().Count() });
            dbContext.Orders.Add( new Db.Order() { Id = 3,CustomerId = 3,Items = dbContext.OrderItems.Where(x=>x.OrderId == 3).ToList()  , OrderDate = new DateTime(2024,01,14,11,50,45), Total = dbContext.OrderItems.Where(x => x.OrderId == 3).ToList().Count() });
            dbContext.Orders.Add( new Db.Order() { Id = 4, CustomerId = 4,Items = dbContext.OrderItems.Where(x=>x.OrderId == 4).ToList()  , OrderDate = new DateTime(2024,01,02,11,50,45), Total = dbContext.OrderItems.Where(x => x.OrderId == 4).ToList().Count() });
                dbContext.SaveChanges();

            }
        }

       
        public async Task<(bool IsSuccess, IEnumerable<Models.Order>, string ErrorMessage)> GetOrdersAsync(int CustomerId)
        {
            try
            {
                var orders = await dbContext.Orders.Where(x => x.CustomerId == CustomerId).ToListAsync();
                if(orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return(false, null, "NotFound");
            }
            catch (Exception  ex)
            {
                logger.LogError(ex.Message, ex.StackTrace);
                return (false, null, ex.Message);
                throw;
            }
            
        }

        public async Task<(bool IsSuccess, Models.Order, string ErrorMessage)> GetOrderAsync(int id, int CustomerId)
        {
            try
            {
                var order = await dbContext.Orders.Where(x => x.Id == id && x.CustomerId == CustomerId).FirstOrDefaultAsync();
                if (order != null)
                {
                    var result = mapper.Map<Db.Order, Models.Order>(order);
                    return (true, result, null);

                }
                return (false, null, "NotFound");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.StackTrace);
                return (false, null, ex.Message);
                throw;
            }
           
        }
    }
}
