using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Site.Mvc;

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
        public IActionResult AllTrainings(string category)
        {
            var data = _trainingService.GetTrainingsFromCategory(category);
            return View("AllTrainings", data);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Training(string training)
        {
            var data = _trainingService.GetTrainingsFromCategory(training);
            return View("Training", data);
        }
    }
}
