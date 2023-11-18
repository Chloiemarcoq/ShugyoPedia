using ShugyopediaApp.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.Interfaces
{
    public interface ITrainingCategoryService
    {
        List<TrainingCategoryViewModel> GetTrainingCategories();
        string GetCategoryNameById(int categoryId);
        void AddTrainingCategory(TrainingCategoryViewModel trainingCategory, string user);
    }
}
