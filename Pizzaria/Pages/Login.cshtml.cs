using Microsoft.AspNetCore.Mvc;
using Pizzaria.Models;
using Pizzaria.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Pizzaria.Pages
{
    public class LoginModel : PageModelBase
    {
        private readonly IRepository<Customer> _customerRepository;

        public LoginModel(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [BindProperty]
        [Required]
        public string UserName { get; set; }

        [BindProperty]
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Pasword must be  between  6 and 20 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin()
        {
            return ValidUser()
                ? RedirectToPage(PageNames.Index)
                : Page();
        }

        private bool ValidUser()
        {
            var user = _customerRepository.FirstOrDefault(c => c.UserName == UserName);
            if (user is not null && user.Password.Equals(Password, StringComparison.InvariantCulture))
            {
                SetUserId(user?.Id ?? -1);
                return true;
            }

            return false;
        }
    }
}
