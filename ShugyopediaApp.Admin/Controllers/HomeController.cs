using ShugyopediaApp.Admin.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace ShugyopediaApp.Admin.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : ControllerBase<HomeController>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="configuration"></param>
        /// <param name="localizer"></param>
        /// <param name="mapper"></param>
        public HomeController(IHttpContextAccessor httpContextAccessor,
                              ILoggerFactory loggerFactory,
                              IConfiguration configuration,
                              IMapper mapper = null) : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {

        }

        /// <summary>
        /// Returns Home View.
        /// </summary>
        /// <returns> Home View </returns>
        /// 
        [AllowAnonymous]
        public IActionResult Index() // training category ni
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Training()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Topics()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Resources() 
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Ratings()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Users()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ForgetPasswordRequests() 
        {
            return View();
        }
    }
}
