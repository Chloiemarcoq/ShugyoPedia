using Microsoft.AspNetCore.Mvc;
using ShugyopediaApp.Data.Models;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.ServiceModels;
using System.Collections.Generic;
using System.Linq;

//used in trainings page only
namespace ShugyopediaApp.Admin.ViewComponents
{
    public class FilterCategory : ViewComponent
    {
        private readonly ITrainingCategoryService _trainingCategoryService;
        public FilterCategory(ITrainingCategoryService trainingCategoryService)
        {
            _trainingCategoryService = trainingCategoryService;
        }
        public IViewComponentResult Invoke()
        {
            List<string> categories = _trainingCategoryService.GetTrainingCategories()
                .Select(s => s.CategoryName)
                .ToList();
            return View(categories);
        }
    }
}
