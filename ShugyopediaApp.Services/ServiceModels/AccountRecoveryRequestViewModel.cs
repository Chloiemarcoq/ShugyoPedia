using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.ServiceModels
{
    public class AccountRecoveryRequestViewModel
    {
        public int RequestId { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public DateTime RequestDate { get; set; }        
    }
}
