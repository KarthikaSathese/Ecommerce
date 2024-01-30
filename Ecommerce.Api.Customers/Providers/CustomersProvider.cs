using AutoMapper;
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ecommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer()
                {
                    ID = 1,
                    Name = "Alan",
                    Address = "California",
                });
                dbContext.Customers.Add(new Db.Customer()
                {
                    ID = 2,
                    Name = "Britney",
                    Address = "Brooklyn",
                });
                dbContext.Customers.Add(new Db.Customer()
                {
                    ID = 3,
                    Name = "Cole",
                    Address = "Michigan",
                });
                dbContext.Customers.Add(new Db.Customer()
                {
                    ID = 4,
                    Name = "Dean",
                    Address = "Florida",
                });
                dbContext.Customers.Add(new Db.Customer()
                {
                    ID = 5,
                    Name = "Elizabeth",
                    Address = "Washington",
                });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers,
            string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                logger?.LogInformation("Querying Customers");
                var customers = await dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
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

        public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                logger?.LogInformation("Querying Customer");
                var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.ID == id);
                if (customer != null)
                {
                    logger?.LogInformation(" Customer Found");
                    var result = mapper.Map<Db.Customer, Models.Customer>(customer);
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

