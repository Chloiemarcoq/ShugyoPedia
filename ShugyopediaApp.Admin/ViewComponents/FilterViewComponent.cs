using Microsoft.AspNetCore.Mvc;
using ShugyopediaApp.Data.Models;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.ServiceModels;
using System;
using System.Collections.Generic;

// used in other pages (ratings, topics)
namespace ShugyopediaApp.Admin.ViewComponents
{
    public class Filter : ViewComponent
    {
        private readonly ITrainingCategoryService _trainingCategoryService;
        public Filter(ITrainingCategoryService trainingCategoryService)
        {
            _trainingCategoryService = trainingCategoryService;
        }
        public IViewComponentResult Invoke()
        {
            List<TrainingCategoryViewModel> categories = _trainingCategoryService.GetTrainingCategories();
            Dictionary<string, List<string>> filterData = new Dictionary<string, List<string>>();
            foreach (TrainingCategoryViewModel category in categories)
            {
                List<string> trainingNameList = new List<string>();
                foreach (Training training in category.Training)
                {                    
                    trainingNameList.Add(training.TrainingName);                    
                }
                filterData.Add(category.CategoryName, trainingNameList);
            }
            return View(filterData);
        }
    }
}
