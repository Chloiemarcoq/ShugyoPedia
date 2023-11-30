using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Services.ServiceModels;
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
        public void DeleteRating(string raterName);
        List<RatingViewModel> GetRatings();
    }
}
