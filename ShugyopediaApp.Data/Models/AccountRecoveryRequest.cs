using System;
using System.Collections.Generic;

namespace ShugyopediaApp.Data.Models
{
    public partial class AccountRecoveryRequest
    {
        public int RequestId { get; set; }
        public string UserEmail { get; set; }
        public string Token { get; set; }
        public DateTime DateExpiration { get; set; }
    }
}
