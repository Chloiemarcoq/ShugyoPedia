using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.ServiceModels
{
    public class AddTopicViewModel
    {
        public List<TrainingViewModel> Trainings { get; set; }
        public int TopicId { get; set; }

        [Required(ErrorMessage = "TopicName is required.")]
        public string TopicName { get; set; }

        [Required(ErrorMessage = "TrainingName is required.")]
        public int TrainingId { get; set; }
        public string TrainingName { get; set; }
        public string ResourceFile { get; set; }

        [Required(ErrorMessage = "ResourceFile is required.")]
        public IFormFile ResourceFileUpload { get; set; }
    }
}
