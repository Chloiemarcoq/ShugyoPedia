using ShugyopediaApp.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Services.Interfaces
{
    public interface IAccountRecoveryRequestService
    {
        List<AccountRecoveryRequestViewModel> GetValidRequests();
        void EmailSender(string receiverEmail);
        bool ValidRequest(string token);
        string GetRequestEmailByToken(string token);
    }
}
