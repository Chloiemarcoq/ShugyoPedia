using ShugyopediaApp.Data.Models;
using ShugyopediaApp.Data;
using ShugyopediaApp.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Data.Repositories;
using ShugyopediaApp.Services.Interfaces;
using System.Net.Mail;
using System.Net;

namespace ShugyopediaApp.Services.Services
{
    public class AccountRecoveryRequestService : IAccountRecoveryRequestService
    {
        private readonly IAccountRecoveryRequestRepository _accountRecoveryRequestRepository;
        private readonly IUserService _userService;
        private readonly float _requestExpirationHours;
        public AccountRecoveryRequestService(
            IAccountRecoveryRequestRepository accountRecoveryRequestRepository,
            IUserService userService)
        {
            _accountRecoveryRequestRepository = accountRecoveryRequestRepository;
            _userService = userService;
            _requestExpirationHours = 2;
        }
        public List<AccountRecoveryRequestViewModel> GetValidRequests()
        {
            List<AccountRecoveryRequestViewModel> data = _accountRecoveryRequestRepository.GetValidRequests()
                .Select(s => new AccountRecoveryRequestViewModel
                {
                    RequestId = s.RequestId,
                    UserEmail = s.UserEmail,
                    UserId = _userService.GetUserByEmail(s.UserEmail).UserId,
                    RequestDate = s.DateExpiration.AddHours(-_requestExpirationHours)
                })
                .ToList();
            return data;
        }
        public void EmailSender(string receiverEmail)
        {

            string token = Guid.NewGuid().ToString();
            AccountRecoveryRequest request = new AccountRecoveryRequest
            {
                UserEmail = receiverEmail,
                Token = token,
                DateExpiration = DateTime.Now.AddHours(_requestExpirationHours)
            };
            _accountRecoveryRequestRepository.AddRequest(request);
            var senderMail = "dwight.eyac20@gmail.com";
            var pw = "vces kwbh hghn ousu";
            var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderMail, pw),
                EnableSsl = true
            };
            string htmlBody = @"<!DOCTYPE html>
    <html lang=""en"">
    <head>
        <meta charset=""UTF-8"">
        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
        <link href='https://fonts.googleapis.com/css?family=Poppins' rel='stylesheet'>
        <title>Change Password</title>
    </head>
    <style>
        /* Your existing CSS styles */
    </style>
    <body>
        <div class=""mid-container"">
            <div class=""hello"">
                <h1>Hello there!</h1>
            </div>
            <div class=""text1"">
                <p>We received a request to reset your password. If you didn't make this request, you can safely ignore this email.</p>
            </div>
            <div class=""btn-container"">
                <div class=""btn"">
                    <a href=""https://localhost:22519/Account/ResetPassword?token=" + token + @""" target=""_blank"">
                        <button>CHANGE YOUR PASSWORD</button>
                    </a>
                </div>
            </div>
            <div class=""text2"">
                <p>If you have any questions or need assistance, please contact our support team at customer.support@shugyopedia.com.</p>
            </div>
            <div class=""text3"">
                <p>Best Regards,</p>
            </div> 
            <div class=""text4"">
                <p>Shugyopedia</p>
            </div>  
        </div>
    </body>
    </html>";

            client.SendMailAsync(
                new MailMessage(from: senderMail,
                                to: receiverEmail,
                                subject: "Shugyopedia Account Recovery Request",
                                body: htmlBody)
                { IsBodyHtml = true });
        }
        public bool ValidRequest(string token)
        {
            return _accountRecoveryRequestRepository.ValidRequest(token);
        }
        public string GetRequestEmailByToken(string token)
        {
            return _accountRecoveryRequestRepository.GetRequestEmailByToken(token);
        }

        public void DeleteAccountRecovery(int requestId)
        {
            var model = new AccountRecoveryRequest();
            model.RequestId = requestId;
            _accountRecoveryRequestRepository.DeleteAccountRecovery(model);
        }
        public void DeleteRequestByEmail(string email)
        {
            _accountRecoveryRequestRepository.DeleteRequestByEmail(email);
        }
    }
}
