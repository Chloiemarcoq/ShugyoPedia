using Microsoft.AspNetCore.Mvc;

namespace ShugyopediaApp.Site.Controllers
{
    public class TrainingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
