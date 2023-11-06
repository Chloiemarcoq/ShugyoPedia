using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.Services;
using ShugyopediaApp.Site.Mvc;

namespace ShugyopediaApp.Site.Controllers
{
    public class TrainingCategoryController : ControllerBase<TrainingCategoryController>
    {
        private readonly ITrainingCategoryService _trainingCategoryService;
        private readonly ITrainingService _trainingService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="configuration"></param>
        /// <param name="localizer"></param>
        /// <param name="mapper"></param>
        public TrainingCategoryController(
            ITrainingCategoryService trainingCategoryService,
            ITrainingService trainingService,
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IMapper mapper = null) 
            : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _trainingCategoryService = trainingCategoryService;
            _trainingService = trainingService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var data = _trainingCategoryService.GetTrainingCategories();
            return View("Index", data);
        }
        [HttpGet]
        public IActionResult ShowTrainings(string category)
        {
            var data = _trainingService.GetTrainingsFromCategory(category);
            return View("ShowTrainings", data);
        }
    }
}
