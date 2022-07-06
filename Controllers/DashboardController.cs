using Microsoft.AspNetCore.Mvc;

namespace Stock_keeping.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
