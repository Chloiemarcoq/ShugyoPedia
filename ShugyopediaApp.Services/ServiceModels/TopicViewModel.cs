using ShugyopediaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.ServiceModels
{
    public class TopicViewModel
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public int TrainingId { get; set; }
        public string TrainingName { get; set; }
        public string ResourceFile { get; set; }        
    }
}
