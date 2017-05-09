using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using LibraryManagement.ViewModels;
using LibraryManagement.Components;
using LibraryManagement.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace LibraryManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Person> _userManager;
        private readonly CustomizeSignInManager<Person> _loginManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<Person> userManager,
            CustomizeSignInManager<Person> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _loginManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewBag.returnUrl = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _loginManager.PasswordSignInAsync(model.Identifier, model.Password, model.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("Logged in {userName}.", model.Identifier);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            else
            {
                _logger.LogWarning("Failed to log in {userName}.", model.Identifier);
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model,
            [FromServices]LibraryContext context,
            [FromServices]IOptions<IdentityOptions> options,
            [FromServices]IHostingEnvironment env,
            [FromServices]IOptions<AuthMessageSenderOptions> authOptions)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = Person.GenerateId(context, "user"),
                    Email = model.Email,
                    FullName = model.FullName,
                    Address = model.Address,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender
                };
                string Password = GeneratePassword(options.Value);
                var result = await _userManager.CreateAsync(user, Password);
                if (result.Succeeded)
                {
                    await new AuthMessageSender(authOptions).SendEmail(model.Email, "Password Initlize",
                        CreateMessage(user, "/Data/EmailHtmlRandomPass.html", new String[] { Password }, ModifyStrInitPassword, env),
                        CreateMessage(user, "/Data/EmailTxtRandomPass.txt", new String[] { Password }, ModifyStrInitPassword, env));
                    //await _loginManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User created a new account with password.");
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            //var userName = HttpContext.User.Identity.Name;
            //// clear all items from the cart
            //HttpContext.Session.Clear();
            await _loginManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("ChangePasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        public IActionResult ChangePasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model, 
            [FromServices]IOptions<AuthMessageSenderOptions> authOptions,
            [FromServices]IHostingEnvironment env)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction("ForgotPasswordConfirmation", new { email = model.Email });
                }
                // Send an email with this link
                string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { code = code }, protocol: HttpContext.Request.Scheme);
                await new AuthMessageSender(authOptions).SendEmail(model.Email, "Reset Password",
                    CreateMessage(user, "/Data/EmailHtmlResetPass.html", new String[] { callbackUrl }, ModifyStrResetPassword, env),
                    CreateMessage(user, "/Data/EmailTxtResetPass.txt", new String[] { callbackUrl }, ModifyStrResetPassword, env));
                return RedirectToAction("ForgotPasswordConfirmation", new { email = model.Email });
            }

            ModelState.AddModelError("", string.Format("We could not locate an account with email : {0}", model.Email));

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private string CreateMessage(Person user, string path, string[] strArr,Func<string, Person, string[], string> finalcontent, IHostingEnvironment env)
        {
            try
            {
                using (StreamReader reader = System.IO.File.OpenText(env.ContentRootPath + path))
                {
                    string fileContent = reader.ReadToEnd();
                    if (fileContent != null && fileContent != "")
                    {
                        return finalcontent(fileContent, user, strArr);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }


        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation(string email)
        {
            if (email == null)
                return View("Error");
            ViewBag.Email = email;
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            //TODO: Fix this?
            var resetPasswordViewModel = new ResetPasswordViewModel() { Code = code };
            return code == null ? View("Error") : View(resetPasswordViewModel);
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
                _logger.LogWarning("Error in creating user: {error}", error.Description);
            }
        }
        private async Task<Person> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        private string GeneratePassword(IdentityOptions validator)
        {
            if (validator == null)
                return null;

            bool requireNonLetterOrDigit = validator.Password.RequireDigit;
            bool requireDigit = validator.Password.RequireDigit;
            bool requireLowercase = validator.Password.RequireLowercase;
            bool requireUppercase = validator.Password.RequireUppercase;

            string randomPassword = string.Empty;

            int passwordLength = validator.Password.RequiredLength;

            Random random = new Random();
            while (randomPassword.Length != passwordLength)
            {
                int randomNumber = random.Next(48, 122);  // >= 48 && < 122 
                if (randomNumber == 95 || randomNumber == 96) continue;  // != 95, 96 _'

                char c = Convert.ToChar(randomNumber);

                if (requireDigit)
                    if (char.IsDigit(c))
                        requireDigit = false;

                if (requireLowercase)
                    if (char.IsLower(c))
                        requireLowercase = false;

                if (requireUppercase)
                    if (char.IsUpper(c))
                        requireUppercase = false;

                if (requireNonLetterOrDigit)
                    if (!char.IsLetterOrDigit(c))
                        requireNonLetterOrDigit = false;

                randomPassword += c;
            }

            if (requireDigit)
                randomPassword += Convert.ToChar(random.Next(48, 58));  // 0-9

            if (requireLowercase)
                randomPassword += Convert.ToChar(random.Next(97, 123));  // a-z

            if (requireLowercase)
                randomPassword += Convert.ToChar(random.Next(65, 91));  // A-Z

            if (requireNonLetterOrDigit)
                randomPassword += Convert.ToChar(random.Next(33, 48));  // symbols !"#$%&'()*+,-./

            return randomPassword;
        }

        private string ModifyStrResetPassword(string content, Person user, string[] strArray)
        {

            string fileContent = content.Replace("{{name}}", user.FullName);
            fileContent = fileContent.Replace("{{action_url}}", strArray[0]);
            return fileContent;
        }

        private string ModifyStrInitPassword(string content, Person user, string[] strArray)
        {
            string fileContent = content.Replace("{{name}}", user.FullName);
            fileContent = fileContent.Replace("{{password_init}}", strArray[0]);
            return fileContent;
        }
    }
}