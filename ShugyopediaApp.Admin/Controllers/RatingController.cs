using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShugyopediaApp.Admin.Controllers
{
    public class RatingController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DeleteRating(Rating rating)
        {
            _ratingService.DeleteRating(rating.ratingId);
            return RedirectToAction("Index");
        }
    }
}
