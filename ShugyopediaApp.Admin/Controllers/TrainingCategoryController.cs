using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShugyopediaApp.Admin.Controllers
{
    public class TrainingCategoryController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Add()
        {
            return View();
        }

		[AllowAnonymous]
		public IActionResult Edit()
		{
			return View();
		}
	}
}
