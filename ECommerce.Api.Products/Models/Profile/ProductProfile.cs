namespace ECommerce.Api.Products.Models.Profile
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<Db.Product, Product>();
        }
    }
}
