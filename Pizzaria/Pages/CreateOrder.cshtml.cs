using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Pages
{
    public class CreateOrderModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateOrderModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order CurrentOrder { get; set; }

        [BindProperty]
        public Pizza SelectedPizza { get; set; }

        public IActionResult OnGet(int? pizzaId)
        {
            if (!pizzaId.HasValue)
            {
                return NotFound();
            }

            SelectedPizza = _context.Pizzas.FirstOrDefault(p => p.Id == pizzaId);

            if (SelectedPizza is null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int? customerId = HttpContext.Session.GetInt32($"{nameof(Customer)}.{nameof(Customer.Id)}");

            if (_context.PizzaOrders is null || SelectedPizza is null || !customerId.HasValue)
            {
                return Page();
            }

            CurrentOrder.CurrentCustomerId = customerId.Value;
            CurrentOrder.PizzaId = SelectedPizza.Id;

            _context.PizzaOrders.Add(CurrentOrder);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
