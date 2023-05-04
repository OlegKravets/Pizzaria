using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Models;
using Pizzaria.Repositories;

namespace Pizzaria.Pages
{
    [BindProperties(SupportsGet = true)]
    public class FinalModel : PageModelBase
    {
        private readonly IRepository<Order> _orderRepository;

        public FinalModel(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [BindProperty]
        public List<Order> Orders { get; set; } = new List<Order>();

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id.HasValue)
            {
                var order = _orderRepository.FirstOrDefault(o => o.Id == id.Value);
                if (order is not null)
                {
                    _orderRepository.RemoveEntity(order);
                    await _orderRepository.SaveChanges();
                }
            }

            return RedirectToPage(PageNames.Final);
        }

        public void OnGet()
        {
            int? customerId = GetUserId();

            if (customerId.HasValue)
            {
                Orders = _orderRepository
                    .Where(o => o.CurrentCustomerId == customerId)
                    .Include(o => o.Pizza)
                    .ToList();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int? customerId = GetUserId();

            if (customerId.HasValue)
            {
                _orderRepository.RemoveRange(_orderRepository.Where(o => o.CurrentCustomerId == customerId));
                await _orderRepository.SaveChanges();
            }

            return RedirectToPage(PageNames.ThankYou);
        }
    }
}
