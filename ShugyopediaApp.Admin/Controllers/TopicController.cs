using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShugyopediaApp.Admin.Controllers
{
    public class TopicController : Controller
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

		public IActionResult DeleteResource()
		{
			return RedirectToAction("Index");
		}

		public IActionResult DeleteTopic(Topic topic)
		{
			_topicService.DeleteTopic(topic.topicId);
            return RedirectToAction("Index");
        }
	}
}
