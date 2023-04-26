using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public bool HasOrders { get; private set; }

        public List<Pizza> PizzaList = new List<Pizza>();

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Customer Customer { get; set; }

        public void OnGet()
        {
            PizzaList = _dbContext.Pizzas.ToList();
            GetCurrentUser();

            HasOrders = Customer is not null && _dbContext.PizzaOrders.Any(o => o.CurrentCustomerId == Customer.Id);
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove($"{nameof(Customer)}.{nameof(Customer.Id)}");
            Customer = null;

            return RedirectToPage("Index");
        }

        private void GetCurrentUser()
        {
            int? userId = HttpContext.Session.GetInt32($"{nameof(Customer)}.{nameof(Customer.Id)}");
            if (userId.HasValue)
            {
                Customer = _dbContext.Customers.FirstOrDefault(c => c.Id == userId.Value);
            }
        }
    }
}