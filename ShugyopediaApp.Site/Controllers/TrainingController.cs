using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.ServiceModels;
using ShugyopediaApp.Site.Mvc;
using System.Collections.Generic;

namespace ShugyopediaApp.Site.Controllers
{
    public class TrainingController : ControllerBase<TrainingController>
    {
        private readonly ITrainingService _trainingService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="configuration"></param>
        /// <param name="localizer"></param>
        /// <param name="mapper"></param>
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
        [AllowAnonymous]
        public IActionResult AllTrainings(string categoryName)
        {
            List<TrainingViewModel> data = _trainingService.GetTrainingsFromCategory(categoryName);
            return View("AllTrainings", data);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Learn(string trainingName)
        {
            var data = _trainingService.GetTrainingTopicRatingDetails(trainingName);
            return View(data);
        }

        public IActionResult DownloadResource(LearnTrainingViewModel data)
        {

            string content = $"Category Name: {data.CategoryName}, Training Name: {data.TrainingName}, Description: {data.TrainingDescription}";
            return Content(content, "text/plain");
        }
    }
}
