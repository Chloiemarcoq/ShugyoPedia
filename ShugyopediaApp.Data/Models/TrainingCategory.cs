using System;
using System.Collections.Generic;

namespace ShugyopediaApp.Data.Models
{
    public partial class TrainingCategory
    {
        public TrainingCategory()
        {
            training = new HashSet<Training>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }

        public ICollection<Training> training { get; set; }
    }
}
