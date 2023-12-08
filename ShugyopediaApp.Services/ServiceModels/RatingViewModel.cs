using ShugyopediaApp.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.ServiceModels
{
    public class RatingViewModel
    {
        public string RatingReview { get; set; }
        public int RatingId { get; set; }
        public int TrainingId { get; set; }
        public string TrainingName { get; set; }
        public double Rate { get; set; }
        public string RaterName { get; set; }
        public string RaterEmail { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
