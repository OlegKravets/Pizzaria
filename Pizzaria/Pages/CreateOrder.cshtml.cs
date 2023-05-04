using Microsoft.AspNetCore.Mvc;
using Pizzaria.Models;
using Pizzaria.Repositories;

namespace Pizzaria.Pages
{
    public class CreateOrderModel : PageModelBase
    {
        private readonly IRepository<Pizza> _pizzaRepository;
        private readonly IRepository<Order> _orderRepository;

        public CreateOrderModel(IRepository<Pizza> pizzaRepository, IRepository<Order> orderRepository)
        {
            _pizzaRepository = pizzaRepository;
            _orderRepository = orderRepository;
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

            SelectedPizza = _pizzaRepository.FirstOrDefault(p => p.Id == pizzaId);

            if (SelectedPizza is null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int? customerId = GetUserId();
            if (_orderRepository.IsEmpty && SelectedPizza is null)
            {
                return Page();
            }

            CurrentOrder.CurrentCustomerId = customerId.Value;
            CurrentOrder.PizzaId = SelectedPizza.Id;

            _orderRepository.AddEntity(CurrentOrder);

            await _orderRepository.SaveChanges();

            return RedirectToPage(PageNames.Index);
        }
    }
}
