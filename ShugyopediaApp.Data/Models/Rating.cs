using System;
using System.Collections.Generic;

namespace ShugyopediaApp.Data.Models
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public int TrainingId { get; set; }
        public string RatingReview { get; set; }
        public double Rate { get; set; }
        public string RaterName { get; set; }
        public string RaterEmail { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Training Training { get; set; }
    }
}
