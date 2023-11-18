using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Data.Repositories;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.Services
{
    
    public class TrainingCategoryService : ITrainingCategoryService
    {
        private readonly ITrainingCategoryRepository _trainingCategoryRepository;
        public TrainingCategoryService(ITrainingCategoryRepository trainingCategoryRepository) 
        {
            _trainingCategoryRepository = trainingCategoryRepository;
        }
        public List<TrainingCategoryViewModel> GetTrainingCategories() 
        {
            var data = _trainingCategoryRepository.GetTrainingCategories().Select(s => new TrainingCategoryViewModel{
                CategoryName = s.CategoryName,
                CategoryIcon = s.CategoryIcon}).ToList();
            return data;            
        }
        public string GetCategoryNameById(int categoryId)
        {
            var categoryName = _trainingCategoryRepository.GetTrainingCategories()
                .Where(t => t.CategoryId == categoryId)
                .Select(t => t.CategoryName)
                .FirstOrDefault();
            return categoryName;
        }

    }
}
