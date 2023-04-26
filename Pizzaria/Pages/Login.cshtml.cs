using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pizzaria.Data;
using Pizzaria.Models;

namespace Pizzaria.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public LoginModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin()
        {
            return ValidUser()
                ? RedirectToPage("Index")
                : Page();
        }

        private bool ValidUser()
        {
            var user = _dbContext.Customers.FirstOrDefault(c => c.UserName == UserName);
            if (user is not null && user.Password.Equals(Password, StringComparison.InvariantCulture))
            {
                HttpContext.Session.SetInt32($"{nameof(Customer)}.{nameof(Customer.Id)}", user?.Id ?? -1);
                return true;
            }

            return false;
        }
    }
}
