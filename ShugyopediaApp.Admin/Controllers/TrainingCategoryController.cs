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
using System;
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
            try
            {
                _trainingCategoryService.AddTrainingCategory(trainingCategory, this.UserId);
                TempData["SuccessMessage"] = "Successfully Added";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error Adding: {ex.Message}";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RedirectEditTrainingCategory(TrainingCategoryViewModel trainingCategory)
        {
            return View("EditTrainingCategory", trainingCategory);
        }
        [HttpPost]
        public IActionResult EditTrainingCategory(TrainingCategoryViewModel trainingCategory)
		{
            try
            {
                _trainingCategoryService.EditTrainingCategory(trainingCategory, this.UserId);
                TempData["SuccessMessage"] = "Successfully Edited";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error Editing: {ex.Message}";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteTrainingCategory(TrainingCategory trainingCategory) 
        {
            try
            {
                _trainingCategoryService.DeleteTrainingCategory(trainingCategory.CategoryId);
                TempData["SuccessMessage"] = "Successfully Deleted";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error Deleting: {ex.Message}";
            }
            return RedirectToAction("Index");
        }
	}
}
