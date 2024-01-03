using ShugyopediaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.ServiceModels
{
    public class TrainingViewModel
    {
        public int TrainingId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public float RateAverage { get; set; }
        public string TrainingName { get; set; }
        public string TrainingDescription { get; set; }
        public string TrainingImage { get; set; }
    }
}
