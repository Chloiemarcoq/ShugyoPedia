using System;
using System.Collections.Generic;

namespace ShugyopediaApp.Data.Models
{
    public partial class Training
    {
        public Training()
        {
            Ratings = new HashSet<Rating>();
            Topics = new HashSet<Topic>();
        }

        public int TrainingId { get; set; }
        public int CategoryId { get; set; }
        public string TrainingName { get; set; }
        public string TrainingDescription { get; set; }
        public string TrainingImage { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }

        public virtual TrainingCategory Category { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
    }
}
