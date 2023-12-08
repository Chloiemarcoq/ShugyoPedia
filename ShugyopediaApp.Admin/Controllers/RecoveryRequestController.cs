using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShugyopediaApp.Admin.Mvc;
using ShugyopediaApp.Data.Models;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.Services;
using System;

namespace ShugyopediaApp.Admin.Controllers
{
    public class RecoveryRequestController : ControllerBase<RecoveryRequestController>
    {
        private readonly IAccountRecoveryRequestService _accountRecoveryRequestService;
        public RecoveryRequestController(
            IAccountRecoveryRequestService accountRecoveryRequestService,
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IMapper mapper = null)
            : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _accountRecoveryRequestService = accountRecoveryRequestService;
        }
        public IActionResult Index()
        {            
            return View(_accountRecoveryRequestService.GetValidRequests());
        }
        public IActionResult DeleteAccountRecovery(AccountRecoveryRequest accountRecoveryRequest)
        {
            try
            {
                _accountRecoveryRequestService.DeleteAccountRecovery(accountRecoveryRequest.RequestId);
                TempData["SuccessMessage"] = "Successfully Deleted";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error Deleting: {ex.Message}";
            }
            return RedirectToAction("Index");
        }
    }
}
