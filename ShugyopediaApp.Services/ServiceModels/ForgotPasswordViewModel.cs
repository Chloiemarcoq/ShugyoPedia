using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.ServiceModels
{
    public class ForgotPasswordViewModel
    {

        [Required(ErrorMessage = "Email is required.")]
        public string UserEmail { get; set; }
    }
}
