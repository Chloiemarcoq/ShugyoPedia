using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShugyopediaApp.Admin.Mvc;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.Services;

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
    }
}
