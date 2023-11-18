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
using System.Collections.Generic;

namespace ShugyopediaApp.Admin.Controllers
{
    public class TrainingCategoryController : ControllerBase<TrainingCategoryController>
    {
        private readonly ITrainingCategoryService _trainingCategoryService;
        public TrainingCategoryController(
            ITrainingCategoryService trainingCategoryService,
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IMapper mapper = null)
            : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _trainingCategoryService = trainingCategoryService;
        }
        public IActionResult Index()
        {
            List<TrainingCategoryViewModel> categories = _trainingCategoryService.GetTrainingCategories();
            return View(categories);
        }
        [HttpGet]
        public IActionResult AddTrainingCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddTrainingCategory(TrainingCategoryViewModel trainingCategory)
        {
            _trainingCategoryService.AddTrainingCategory(trainingCategory, this.UserId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(TrainingCategoryViewModel trainingCategory)
		{
			return View(trainingCategory);
		}
	}
}
