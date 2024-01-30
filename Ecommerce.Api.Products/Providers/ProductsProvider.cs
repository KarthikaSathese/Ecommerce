using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product()
                {
                    ID = 1,
                    Name = "Keyboard",
                    Price = 5000,
                    Inventory = 50
                });
                dbContext.Products.Add(new Db.Product()
                {
                    ID = 2,
                    Name = "Mouse",
                    Price = 1200,
                    Inventory = 100
                });
                dbContext.Products.Add(new Db.Product()
                {
                    ID = 3,
                    Name = "Headphone",
                    Price = 2500,
                    Inventory = 150
                });
                dbContext.Products.Add(new Db.Product()
                {
                    ID = 4,
                    Name = "GPU",
                    Price = 35000,
                    Inventory = 25
                });
                dbContext.Products.Add(new Db.Product()
                {
                    ID = 5,
                    Name = "CPU",
                    Price = 22000,
                    Inventory = 30
                });

                dbContext.SaveChanges();
            
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, 
            string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if (products!= null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync( p => p.ID == id);
                if (product != null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
