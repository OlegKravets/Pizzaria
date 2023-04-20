using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pizzaria.Models;
using Pizzaria.Data;
using Microsoft.EntityFrameworkCore;

namespace Pizzaria.Pages
{
    [BindProperties(SupportsGet = true)]
    public class FinalModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public FinalModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Order> Orders { get; set; } = new List<Order>();

        public void OnGet()
        {
            int? customerId = HttpContext.Session.GetInt32($"{nameof(Customer)}.{nameof(Customer.Id)}");

            if (customerId.HasValue)
            {
                Orders = _context.PizzaOrders
                    .Where(o => o.CurrentCustomerId == customerId)
                    .Include(o => o.Pizza)
                    .ToList();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int? customerId = HttpContext.Session.GetInt32($"{nameof(Customer)}.{nameof(Customer.Id)}");

            if (customerId.HasValue)
            {
                _context.PizzaOrders.RemoveRange(_context.PizzaOrders.Where(o => o.CurrentCustomerId == customerId));
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("ThankYou");
        }
    }
}
