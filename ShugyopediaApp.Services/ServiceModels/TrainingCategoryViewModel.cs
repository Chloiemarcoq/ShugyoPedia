using ShugyopediaApp.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.ServiceModels
{
    public class TrainingCategoryViewModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "CategoryName is required")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "CategoryIcon is required.")]
        public string CategoryIcon { get; set; }
        public ICollection<Training> Training { get; set; }
        //public string CreatedBy { get; set; }
        //public string CreatedTime { get; set; }
        //public string UpdatedBy { get; set; }
        //public string UpdatedTime { get; set; }

    }
}
