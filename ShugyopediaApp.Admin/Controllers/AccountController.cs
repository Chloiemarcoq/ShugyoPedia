using ShugyopediaApp.Data.Models;
using ShugyopediaApp.Services.Interfaces;
using ShugyopediaApp.Services.Manager;
using ShugyopediaApp.Services.ServiceModels;
using ShugyopediaApp.Admin.Authentication;
using ShugyopediaApp.Admin.Models;
using ShugyopediaApp.Admin.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using static ShugyopediaApp.Resources.Constants.Enums;
using System.Net.Mail;
using System.Net;
using ShugyopediaApp.Data.Interfaces;
using ShugyopediaApp.Data.Repositories;

namespace ShugyopediaApp.Admin.Controllers
{
    public class AccountController : ControllerBase<AccountController>
    {
        private readonly SessionManager _sessionManager;
        private readonly SignInManager _signInManager;
        private readonly TokenValidationParametersFactory _tokenValidationParametersFactory;
        private readonly TokenProviderOptionsFactory _tokenProviderOptionsFactory;
        private readonly IConfiguration _appConfiguration;
        private readonly IUserService _userService;
        private readonly IAccountRecoveryRequestService _accountRecoveryRequestService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="localizer">The localizer.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="tokenValidationParametersFactory">The token validation parameters factory.</param>
        /// <param name="tokenProviderOptionsFactory">The token provider options factory.</param>
        public AccountController(
                            SignInManager signInManager,
                            IHttpContextAccessor httpContextAccessor,
                            ILoggerFactory loggerFactory,
                            IConfiguration configuration,
                            IMapper mapper,
                            IUserService userService,
                            IAccountRecoveryRequestService accountRecoveryRequestService,
                            TokenValidationParametersFactory tokenValidationParametersFactory,
                            TokenProviderOptionsFactory tokenProviderOptionsFactory) : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            this._sessionManager = new SessionManager(this._session);
            this._signInManager = signInManager;
            this._tokenProviderOptionsFactory = tokenProviderOptionsFactory;
            this._tokenValidationParametersFactory = tokenValidationParametersFactory;
            this._appConfiguration = configuration;
            this._userService = userService;
            _accountRecoveryRequestService = accountRecoveryRequestService;
        }

        /// <summary>
        /// Login Method
        /// </summary>
        /// <returns>Created response view</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            TempData["returnUrl"] = System.Net.WebUtility.UrlDecode(HttpContext.Request.Query["ReturnUrl"]);
            this._sessionManager.Clear();
            this._session.SetString("SessionId", System.Guid.NewGuid().ToString());
            return this.View();
        }

        /// <summary>
        /// Authenticate user and signs the user in when successful.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns> Created response view </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            this._session.SetString("HasSession", "Exist");

            User user = null;
            var loginResult = _userService.AuthenticateUser(model.UserId, model.Password, ref user);
            if (loginResult == LoginResult.Success)
            {
                // 認証OK
                await this._signInManager.SignInAsync(user);
                this._session.SetString("UserName", user.Name);
                return RedirectToAction("Index", "TrainingCategory");
            }
            else
            {
                // 認証NG
                TempData["ErrorMessage"] = "Incorrect UserId or Password";
                return View();
            }
        }


        /// <summary>
        /// Sign Out current account and return login view.
        /// </summary>
        /// <returns>Created response view</returns>
        [AllowAnonymous]
        public async Task<IActionResult> SignOutUser()
        {
            await this._signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult EmailCheckExist(ForgotPasswordViewModel email)
        {
            //if email exist else return view error
            if (_userService.UserExistsEmail(email.UserEmail))
            {
                return RedirectToAction("EmailSender", "Account", new { email = email.UserEmail});
            }
            else
            {
                TempData["ErrorMessage"] = "Email not associated to any account";
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpGet]
        [AllowAnonymous]
        //This saves the request to the database as well as send the email
        public IActionResult EmailSender(string email)
        {            
            _accountRecoveryRequestService.EmailSender(email);
            TempData["SuccessMessage"] = "Request sent, please check your email.";
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token)
        {
            if (_accountRecoveryRequestService.ValidRequest(token))
            {
                string email = _accountRecoveryRequestService.GetRequestEmailByToken(token);
                if (_userService.UserExistsEmail(email))
                {
                    UserViewModel model = new UserViewModel { UserEmail = email };
                    return View(model);
                }
            }
            //invalid token or expired request here
            TempData["ErrorMessage"] = "Token is invalid or expired.";
            return RedirectToAction("ForgotPassword", "Account");          
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ResetPassword(UserViewModel user)
        {
            _userService.ResetPassword(user);
            _accountRecoveryRequestService.DeleteRequestByEmail(user.UserEmail);
            TempData["ErrorMessage"] = "Password Reset Successful";
            return RedirectToAction("Login", "Account");
        }
    }
}
