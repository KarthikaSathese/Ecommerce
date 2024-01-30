using AutoMapper;
using Ecommerce.Api.Orders.Db;
using Ecommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Db.Order()
                {
                    ID = 1,
                    CustomerID = 245,
                    OrderDate = DateTime.Today,
                    Total = 28998,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderID = 1, ProductID = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 1, ProductID = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 1, ProductID = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 2, ProductID = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 3, ProductID = 3, Quantity = 1,  UnitPrice = 100 }
                    }
                });
                dbContext.Orders.Add(new Db.Order()
                {
                    ID = 2,
                    CustomerID = 356,
                    OrderDate = DateTime.Today,
                    Total = 34587,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderID = 1, ProductID = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 1, ProductID = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 1, ProductID = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 2, ProductID = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 3, ProductID = 3, Quantity = 1,  UnitPrice = 100 }
                    }
                });
                dbContext.Orders.Add(new Db.Order()
                {
                    ID = 3,
                    CustomerID = 467,
                    OrderDate = DateTime.Today,
                    Total = 5670,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderID = 1, ProductID = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 1, ProductID = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 1, ProductID = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 2, ProductID = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 3, ProductID = 3, Quantity = 1,  UnitPrice = 100 }
                    }
                });
                dbContext.Orders.Add(new Db.Order()
                {
                    ID = 4,
                    CustomerID = 578,
                    OrderDate = DateTime.Today,
                    Total = 5340,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderID = 1, ProductID = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 1, ProductID = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 1, ProductID = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 2, ProductID = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderID = 3, ProductID = 3, Quantity = 1,  UnitPrice = 100 }
                    }
                });

                dbContext.SaveChanges();

            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders,
            string ErrorMessage)> GetOrdersAsync(int customerID)
        {
            try
            {
                var orders = await dbContext.Orders.Where(o=>o.CustomerID==customerID).Include(i=>i.Items).ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
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

