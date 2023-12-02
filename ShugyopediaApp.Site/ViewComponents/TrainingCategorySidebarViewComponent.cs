using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.ServiceModels;
using System.Collections.Generic;

namespace ShugyopediaApp.Site.Component
{
    public class TrainingCategorySidebar : ViewComponent
    {
        private readonly ITrainingCategoryService _trainingCategoryService;
        public TrainingCategorySidebar(ITrainingCategoryService trainingCategoryService)
        {
            _trainingCategoryService = trainingCategoryService;
        }
        public IViewComponentResult Invoke()
        {
            List<TrainingCategoryViewModel> data = _trainingCategoryService.GetTrainingCategories();
            return View(data);
        }
    }
}
