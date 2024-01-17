namespace ECommerce.Api.Customers.Models.Profile
{
    public class CustomerProfile : AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<Db.Customer, Customer>();
        }
    }
}
