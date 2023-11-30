using ShugyopediaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Data.Interfaces
{
    public interface IAccountRecoveryRequestRepository
    {
        bool ValidRequest(string token);
        void AddRequest(AccountRecoveryRequest request);
        string GetRequestEmailByToken(string token);
        IQueryable<AccountRecoveryRequest> GetValidRequests();
        void DeleteAccountRecovery(AccountRecoveryRequest requestId);
        public void DeleteRequestByEmail(string email);
    }
}
