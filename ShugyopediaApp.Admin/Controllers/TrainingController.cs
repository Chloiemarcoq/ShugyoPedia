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
        public IActionResult Index()
        {
            List<TrainingViewModel> trainings = _trainingService.GetTrainings();
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
            _trainingService.AddTraining(training, this.UserId);
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
            _trainingService.EditTraining(training, this.UserId);
            return RedirectToAction("Index");
        }
    }
}
