using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Models.Profile;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Ecommerce.Api.Projects.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task  GetProductsReturnAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnAllProducts))
                .Options;
            var dbcontext = new ProductsDbContext(options);

            CreateProducts(dbcontext);

            var profile = new ProductProfile();
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(mapperConfig);
            var productsProvider = new ProductsProvider(dbcontext, null, mapper);

            var products = await productsProvider.GetProductsAsync();
            Assert.True(products.IsSuccess);
            Assert.True(products.products.Any());
            Assert.Null(products.ErrorMessage);
        }

        [Fact]
        public async Task GetProductReturnValidProductById()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductReturnValidProductById))
                .Options;
            var dbcontext = new ProductsDbContext(options);

            CreateProducts(dbcontext);

            var profile = new ProductProfile();
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(mapperConfig);
            var productsProvider = new ProductsProvider(dbcontext, null, mapper);

            var products = await productsProvider.GetProductAsync(1);
            Assert.True(products.IsSuccess);
            Assert.NotNull(products.product);
            Assert.True(products.product.Id == 1);
            Assert.Null(products.ErrorMessage);
        }

        [Fact]
        public async Task GetProductReturnInValidProductById()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductReturnInValidProductById))
                .Options;
            var dbcontext = new ProductsDbContext(options);

            CreateProducts(dbcontext);

            var profile = new ProductProfile();
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(profile));
            var mapper = new Mapper(mapperConfig);
            var productsProvider = new ProductsProvider(dbcontext, null, mapper);

            var products = await productsProvider.GetProductAsync(-1);
            Assert.False(products.IsSuccess);
            Assert.Null(products.product);
            Assert.NotNull(products.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbcontext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbcontext.Products.Add(new Product
                {
                    Id = i,
                    Inventory = i + 10,
                    Name = Guid.NewGuid().ToString(),
                    Price = (decimal) (i*3.14)
                });
            }
            dbcontext.SaveChanges();
        }
    }
}