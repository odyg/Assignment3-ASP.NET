using Microsoft.AspNetCore.Mvc;

namespace Assignment3.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Home()
        {
            return View();
        }
    }
}
