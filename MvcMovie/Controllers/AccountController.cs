using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MvcMovie.Core.Services;
using MvcMovie.Core.ViewModels;

namespace MvcMovie.Controllers
{
    // [Authorize()]
    public class AccountController : Controller
    {
        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountController/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            var message = _localizer["Message"].Value;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Process login
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }


        [AllowAnonymous()]
        // GET: AccountController/Register
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous()]
        // GET: AccountController/ForgotPassword
        public async Task<ActionResult> ForgotPassword()
        {
            return View("ForgotPassword");
        }

        /*
        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */

        // private readonly IUserContext _userContext;
        private readonly AccountService _accountService;
        private readonly IStringLocalizer<AccountController> _localizer;

        // IUserContext userContext, 
        public AccountController(AccountService accountService, IStringLocalizer<AccountController> localizer)
        {
            // _userContext = userContext;
            _accountService = accountService;
            _localizer = localizer;
        }
    }
}
