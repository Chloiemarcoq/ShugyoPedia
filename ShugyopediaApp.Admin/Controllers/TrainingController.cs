using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShugyopediaApp.Admin.Mvc;
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
        [AllowAnonymous]
		public IActionResult Edit()
		{
			return View();
		}
	}
}
