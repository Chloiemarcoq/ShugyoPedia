using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Data.Models;
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
            var data = _trainingCategoryRepository
                .GetTrainingCategories()
                .Select(s => new TrainingCategoryViewModel{
                    CategoryId = s.CategoryId,
                    CategoryName = s.CategoryName,
                    CategoryIcon = s.CategoryIcon
                })
                .OrderBy(s => s.CategoryId)
                .ToList();
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
        public void AddTrainingCategory(TrainingCategoryViewModel trainingCategory, string user)
        {
            var model = new TrainingCategory();
            model.CategoryName = trainingCategory.CategoryName;
            model.CategoryIcon = trainingCategory.CategoryIcon;
            model.CreatedBy = user;
            model.CreatedTime = DateTime.Now;
            model.UpdatedBy = user;
            model.UpdatedTime = DateTime.Now;
            _trainingCategoryRepository.AddTrainingCategory(model);
        }
        public void EditTrainingCategory(TrainingCategoryViewModel trainingCategory, string user)
        {
            var model = new TrainingCategory();
            model.CategoryId = trainingCategory.CategoryId;
            model.CategoryName = trainingCategory.CategoryName;
            model.CategoryIcon = trainingCategory.CategoryIcon;
            model.UpdatedBy = user;
            model.UpdatedTime = DateTime.Now;
            _trainingCategoryRepository.EditTrainingCategory(model);
        }
    }
}
