using ShugyopediaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Data.Interfaces
{
    public interface ITrainingRepository
    {
        IQueryable<Training> GetTrainings();
        void AddTraining(Training training);
        void EditTraining(Training training);
        void DeleteTraining(Training training);
    }
}
