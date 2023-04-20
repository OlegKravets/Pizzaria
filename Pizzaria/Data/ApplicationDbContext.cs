using Microsoft.EntityFrameworkCore;
using Pizzaria.Models;

namespace Pizzaria.Data
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pizza>().HasData(new Pizza() { Id = 1, Name = "Pepperoni", BasePrice = 5 });
            modelBuilder.Entity<Pizza>().HasData(new Pizza() { Id = 2, Name = "Mushroom", BasePrice = 7 });
            modelBuilder.Entity<Pizza>().HasData(new Pizza() { Id = 3, Name = "Margarita", BasePrice = 6 });
            modelBuilder.Entity<Pizza>().HasData(new Pizza() { Id = 4, Name = "King", BasePrice = 10 });

            modelBuilder.Entity<Customer>().HasData(new Customer() { Name = "Oleh", Id = 1});
        }

        public DbSet<Order> PizzaOrders { get; set; }

        public DbSet<Pizza> Pizzas { get; set; }

        public DbSet<Customer> Customers { get; set; }
    }
}
