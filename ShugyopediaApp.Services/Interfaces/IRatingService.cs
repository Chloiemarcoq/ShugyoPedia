using ShugyopediaApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.Interfaces
{
    
    public interface IRatingService
    {
        public float GetRatingAverageFromTraining(int trainingId);
    }
}
