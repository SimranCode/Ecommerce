namespace ECommerce.Api.Orders.Models.Profile
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        { 
         CreateMap<Db.Order, Order>();
         CreateMap<Db.OrderItem, OrderItem>();
        }
    }
}
