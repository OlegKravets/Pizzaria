using Microsoft.AspNetCore.Mvc.RazorPages;
using Pizzaria.Models;

namespace Pizzaria.Pages
{
    public class PageModelBase : PageModel
    {
        private const string UserIdKey = $"{nameof(Customer)}.{nameof(Customer.Id)}";

        public int? GetUserId() => HttpContext.Session.GetInt32(UserIdKey);

        public void SetUserId(int userId) => HttpContext.Session.SetInt32(UserIdKey, userId);

        public void RemoveUserId() => HttpContext.Session.Remove(UserIdKey);

        public bool HasLogin => GetUserId().HasValue;
    }
}
