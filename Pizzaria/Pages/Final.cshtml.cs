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

        [BindProperty]
        public List<Order> Orders { get; set; } = new List<Order>();

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id.HasValue)
            {
                var order = _context.PizzaOrders.FirstOrDefault(o => o.Id == id.Value);
                if (order is not null)
                {
                    _context.PizzaOrders.Remove(order);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("Final");
        }

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
