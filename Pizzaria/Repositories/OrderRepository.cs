using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Repositories
{
    public sealed class OrderRepository : RepositoryBase<Order>
    {
        public OrderRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        protected override DbSet<Order> Data => DbContext.PizzaOrders;
    }
}
