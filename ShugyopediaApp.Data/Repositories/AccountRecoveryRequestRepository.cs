using Basecode.Data.Repositories;
using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShugyopediaApp.Data.Repositories
{
    public class AccountRecoveryRequestRepository : BaseRepository, IAccountRecoveryRequestRepository
    {
        public AccountRecoveryRequestRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public bool ValidRequest(string token)
        {
            var currentDateTime = DateTime.Now;
            var request = GetDbSet<AccountRecoveryRequest>()
                .FirstOrDefault(r => r.Token == token && r.DateExpiration > currentDateTime);
            return request != null;
        }
        public void AddRequest(AccountRecoveryRequest request)
        {
            this.GetDbSet<AccountRecoveryRequest>().Add(request);
            UnitOfWork.SaveChanges();
        }
        public string GetRequestEmailByToken(string token)
        {
            var request = GetDbSet<AccountRecoveryRequest>()
                            .FirstOrDefault(r => r.Token == token);

            return request?.UserEmail; // Returns the email associated with the token or null if not found
        }
        public IQueryable<AccountRecoveryRequest> GetValidRequests()
        {
            return this.GetDbSet<AccountRecoveryRequest>()
                .Where(r => r.DateExpiration > DateTime.Now);
        }

        public void DeleteAccountRecovery(AccountRecoveryRequest accountRecoveryRequest)
        {
            var accountRecoveryToDelete = this.GetDbSet<AccountRecoveryRequest>().Find(accountRecoveryRequest.RequestId);

            if (accountRecoveryToDelete != null)
            {
                this.GetDbSet<AccountRecoveryRequest>().Remove(accountRecoveryToDelete);
                UnitOfWork.SaveChanges();
            }
        }
        public void DeleteRequestByEmail(string email)
        {
            var accountRecoveryToDelete = this.GetDbSet<AccountRecoveryRequest>()
                .FirstOrDefault(r => r.UserEmail == email);

            if (accountRecoveryToDelete != null)
            {
                this.GetDbSet<AccountRecoveryRequest>().Remove(accountRecoveryToDelete);
                UnitOfWork.SaveChanges();
            }
        }
    }
}
