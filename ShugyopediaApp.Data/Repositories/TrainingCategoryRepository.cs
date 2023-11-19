using Basecode.Data.Repositories;
using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Data.Repositories
{
    public class TrainingCategoryRepository : BaseRepository, ITrainingCategoryRepository
    {
        public TrainingCategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
            
        }
        public IQueryable<TrainingCategory> GetTrainingCategories() 
        {
            return this.GetDbSet<TrainingCategory>();
        }
        public void AddTrainingCategory(TrainingCategory trainingCategory)
        {
            this.GetDbSet<TrainingCategory>().Add(trainingCategory);
            UnitOfWork.SaveChanges();
        }
        public void EditTrainingCategory(TrainingCategory trainingCategory)
        {
            var recordFound = this.GetDbSet<TrainingCategory>().Find(trainingCategory.CategoryId);
            if (recordFound != null)
            {
                recordFound.CategoryName = trainingCategory.CategoryName;
                recordFound.CategoryIcon = trainingCategory.CategoryIcon;
                recordFound.UpdatedBy = trainingCategory.UpdatedBy;
                recordFound.UpdatedTime = trainingCategory.UpdatedTime;
                UnitOfWork.SaveChanges();
            }
        }
    }
}
