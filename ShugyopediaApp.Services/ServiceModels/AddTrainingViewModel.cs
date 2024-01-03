using Microsoft.AspNetCore.Http;
using ShugyopediaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.ServiceModels
{
    public class AddTrainingViewModel
    {
        public List<TrainingCategoryViewModel> Categories { get; set; }

        [Required(ErrorMessage = "CategoryId is required.")]
        public int CategoryId { get; set; }
        public int TrainingId { get; set; }
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "TrainingName is required.")]
        public string TrainingName { get; set; }

        [Required(ErrorMessage = "TrainingDescription is required.")]
        public string TrainingDescription { get; set; }
        public IFormFile TrainingImageFile { get; set; }
        public string TrainingImage { get; set; }
    }
}
