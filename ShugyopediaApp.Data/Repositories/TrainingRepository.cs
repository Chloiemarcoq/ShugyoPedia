using Basecode.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Data.Repositories
{
    public class TrainingRepository : BaseRepository, ITrainingRepository
    {
        public TrainingRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public IQueryable<Training> GetTrainings()
        {
            return this.GetDbSet<Training>()
                .Include(t => t.Category)
                .Include(t => t.Ratings)
                .Include(t => t.Topics);
        }
        public void AddTraining(Training training)
        {
            this.GetDbSet<Training>().Add(training);
            UnitOfWork.SaveChanges();
        }
        public void EditTraining(Training training)
        {
            var recordFound = this.GetDbSet<Training>().Find(training.TrainingId);
            if (recordFound != null)
            {
                recordFound.TrainingName = training.TrainingName;
                recordFound.CategoryId = training.CategoryId;
                recordFound.TrainingDescription = training.TrainingDescription;
                recordFound.TrainingImage = (string.IsNullOrEmpty(training.TrainingImage)) ? recordFound.TrainingImage : training.TrainingImage;
                recordFound.UpdatedBy = training.UpdatedBy;
                recordFound.UpdatedTime = training.UpdatedTime;
                UnitOfWork.SaveChanges();
            }
        }
        public void DeleteTraining(Training training)
        {
            var trainingToDelete = this.GetDbSet<Training>().Find(training.TrainingId);

            if (trainingToDelete != null)
            {
                this.GetDbSet<Training>().Remove(trainingToDelete);
                UnitOfWork.SaveChanges();
            }
        }

    }
}
