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
using System;
using System.Collections.Generic;

namespace ShugyopediaApp.Admin.Controllers
{
    public class TrainingController : ControllerBase<TrainingController>
    {
        private readonly ITrainingService _trainingService;
        public TrainingController(
            ITrainingService trainingService,
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IMapper mapper = null)
            : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _trainingService = trainingService;
        }

        [HttpGet]
        public IActionResult Index(string category)
        {
            List<TrainingViewModel> trainings = _trainingService.GetTrainings();
            ViewBag.category = category;
            return View(trainings);
        }
        [HttpGet]
        public IActionResult AddTraining()
        {
            AddTrainingViewModel categories = _trainingService.GetTrainingCategorySummary();
            return View(categories);
        }
        [HttpPost]
        public IActionResult AddTraining(AddTrainingViewModel training)
        {
            try
            {
                _trainingService.AddTraining(training, this.UserId);
                TempData["SuccessMessage"] = "Successfully Added";
            }
            catch (Exception ex) 
            {
                TempData["ErrorMessage"] = $"Error Adding: {ex.Message}";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
		public IActionResult RedirectEditTraining(TrainingViewModel training)
        {
            AddTrainingViewModel editTrainingData =  _trainingService.GetTrainingCategorySummary();
            editTrainingData.TrainingId = training.TrainingId;
            editTrainingData.TrainingName = training.TrainingName;
            editTrainingData.CategoryId = training.CategoryId;
            editTrainingData.CategoryName = training.CategoryName;
            editTrainingData.TrainingDescription = training.TrainingDescription;
            editTrainingData.TrainingImage = training.TrainingImage;
            return View("EditTraining", editTrainingData);
		}
        [HttpPost]
        public IActionResult EditTraining(AddTrainingViewModel training)
        {
            try
            {
                _trainingService.EditTraining(training, this.UserId);
                TempData["SuccessMessage"] = "Successfully Edited";
            }
            catch (Exception ex) 
            {
                TempData["ErrorMessage"] = $"Error Editing: {ex.Message}";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteTraining(Training training)
        {
            try
            {
                _trainingService.DeleteTraining(training.TrainingId);
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
