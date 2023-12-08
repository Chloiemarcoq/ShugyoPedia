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
using System.IO.Compression;
using System.Linq;
using System.Net.Mime;
using static ShugyopediaApp.Data.PathManager;

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
            Dictionary<string, string> fileDetails = _trainingService.DownloadResourceLogic(fileUrl);
            try
            {
                return File(System.IO.File.ReadAllBytes(fileDetails["fileDirectory"]), fileDetails["contentType"], fileDetails["fileName"]);
            }
            catch (Exception)
            {
                return Content("Error, file cannot be found", "text/plain");
            }
        }
        [HttpPost]
        public IActionResult DownloadResources(string[] checkboxes)
        {
            List<string> filePaths = new List<string>();
            foreach (var path in checkboxes)
            {
                string directory = PathManager.DirectoryPath.TopicResourcesDirectory;
                string url = PathManager.UrlPath.TopicResourcesUrl;
                filePaths.Add(Path.Combine(directory,path.Replace(url, "")));
            }
            if (filePaths != null && filePaths.Any())
            {
                // Set a unique name for the zip file
                string zipFileName = "ZippedFiles_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip";

                // Get the path to the temporary directory
                string tempDirectory = Path.Combine(Path.GetTempPath(), "ZippedFilesTemp");

                // Create the temporary directory if it doesn't exist
                Directory.CreateDirectory(tempDirectory);

                // Create a unique temporary directory for this zip operation
                string uniqueTempDirectory = Path.Combine(tempDirectory, Path.GetRandomFileName());
                Directory.CreateDirectory(uniqueTempDirectory);

                // Add each selected file to the temporary directory
                foreach (string filePath in filePaths)
                {
                    // Copy each file to the temporary directory
                    string fileName = Path.GetFileName(filePath);
                    string destFilePath = Path.Combine(uniqueTempDirectory, fileName);
                    System.IO.File.Copy(filePath, destFilePath);
                }

                // Create a zip file from the temporary directory
                string zipFilePath = Path.Combine(tempDirectory, zipFileName);
                ZipFile.CreateFromDirectory(uniqueTempDirectory, zipFilePath);

                // Read the zip file and return it as a FileStreamResult
                FileStream zipFileStream = new FileStream(zipFilePath, FileMode.Open, FileAccess.Read);
                return File(zipFileStream, "application/zip", zipFileName);
            }

            // If no files were selected, return an error or appropriate response
            return Content("No files selected for download.", "text/plain");
        }
    }
}
        //if (checkboxes != null && checkboxes.Length > 0)
        //{
        //    return Content(checkboxes.ToString(), "text/plain");//redirect dapat same ubos
        //}
        //return Content("No checkboxes selected", "text/plain");

