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
    }
}
