using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Repositories
{
    public sealed class CustomerRepository : RepositoryBase<Customer>
    {
        public CustomerRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        protected override DbSet<Customer> Data => DbContext.Customers;
    }
}
