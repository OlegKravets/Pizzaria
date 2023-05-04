using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Repositories
{
    public sealed class PizzaRepository : RepositoryBase<Pizza>
    {
        public PizzaRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        protected override DbSet<Pizza> Data => DbContext.Pizzas;
    }
}
