using Microsoft.AspNetCore.Mvc;
using Pizzaria.Models;
using Pizzaria.Repositories;

namespace Pizzaria.Pages
{
    public class IndexModel : PageModelBase
    {
        public bool HasOrders { get; private set; }

        public List<Pizza> PizzaList = new List<Pizza>();
        private readonly IRepository<Pizza> _pizzaRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Customer> _customerRepository;

        public IndexModel(
            IRepository<Pizza> pizzaRepository,
            IRepository<Order> orderRepository,
            IRepository<Customer> customerRepository)
        {
            _pizzaRepository = pizzaRepository;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public Customer Customer { get; set; }

        public void OnGet()
        {
            PizzaList = _pizzaRepository.GetAllEntities().ToList();
            GetCurrentUser();

            HasOrders = Customer is not null && _orderRepository.Any(o => o.CurrentCustomerId == Customer.Id);
        }

        public IActionResult OnPostLogout()
        {
            RemoveUserId();
            Customer = null;

            return RedirectToPage(PageNames.Index);
        }

        private void GetCurrentUser()
        {
            int? userId = GetUserId();
            if (userId.HasValue)
            {
                Customer = _customerRepository.FirstOrDefault(c => c.Id == userId.Value);
            }
        }
    }
}