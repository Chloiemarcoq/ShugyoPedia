using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.ServiceModels
{
    public class LearnTrainingViewModel
    {
        public string CategoryName { get; set; }
        public float RateAverage { get; set; }
        public string TrainingName { get; set; }
        public string TrainingDescription { get; set; }
        public DateTime UpdatedTime { get; set; }
        public string CreatedBy { get; set; }
        public List<LearnRatingViewModel> Ratings { get; set; }
        public List<LearnTopicViewModel> Topics { get; set; }
    }
    public class LearnRatingViewModel
    {
        public string RatingReview { get; set; }
        public double Rate { get; set; }
        public string RaterName { get; set; }
        public string RaterEmail { get; set; }
    }
    public class LearnTopicViewModel
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public string ResourceFile { get; set; }
        public string ResourceFileType { get; set; }
        public bool IsChecked { get; set; }
    }
}
