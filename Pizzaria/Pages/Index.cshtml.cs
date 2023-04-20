using Microsoft.AspNetCore.Mvc.RazorPages;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private Customer _customer;

        public List<Pizza> PizzaList = new List<Pizza>();

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Customer Customer => _customer ??= _dbContext.Customers.FirstOrDefault();

        public void OnGet()
        {
            PizzaList = _dbContext.Pizzas.ToList();
            HttpContext.Session.SetInt32($"{nameof(Customer)}.{nameof(Customer.Id)}", Customer.Id);
        }
    }
}