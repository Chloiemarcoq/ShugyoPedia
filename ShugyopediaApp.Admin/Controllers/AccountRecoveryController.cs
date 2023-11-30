using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShugyopediaApp.Admin.Controllers
{
    public class AccountRecoveryController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DeleteAccountRecovery(AccountRecovery accountRecovery)
        {
            _accountRecoveryService.DeleteAccountRecovery(accountRecovery.RequestId);
            return RedirectToAction("Index");
        }
    }
}
