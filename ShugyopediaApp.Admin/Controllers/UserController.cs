using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShugyopediaApp.Admin.Mvc;
using ShugyopediaApp.Data.Models;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.ServiceModels;
using ShugyopediaApp.Services.Services;
using System.Collections.Generic;

namespace ShugyopediaApp.Admin.Controllers
{
    public class UserController : Controller
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

		public IActionResult DeleteUser(User user)
		{
            _userService.DeleteUser(user.id);
            return RedirectToAction("Index");
		}
	}
}
