using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using ShugyopediaApp.Data;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.ServiceModels;
using ShugyopediaApp.Site.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;

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
        [HttpGet]
        public IActionResult DownloadResource(string fileUrl)
        {
            Dictionary<string,string> fileDetails = _trainingService.DownloadResourceLogic(fileUrl);
            try
            {
                return File(System.IO.File.ReadAllBytes(fileDetails["fileDirectory"]), fileDetails["contentType"], fileDetails["fileName"]);
            }
            catch (Exception)
            {
                return Content("FUCK", "text/plain");
            }
        }
        [HttpPost]
        public IActionResult DownloadResources(string[] checkboxes)
        { 
            if (checkboxes != null && checkboxes.Length > 0)
            {
                // Concatenate the checkbox values into a single string
                string checkboxValues = string.Join(", ", checkboxes.Select(x => x.ToString()));

                // Return the checkbox values as plain text
                return Content(checkboxValues, "text/plain");
            }

            return Content("No checkboxes selected", "text/plain");
}
    }
}
