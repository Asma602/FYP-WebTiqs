using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FYP1.Models;
using FYP1.ViewModel;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace FYP1.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _applicationDbContext;
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
       static private string forgotAppPassword;
        private IHostingEnvironment hosting;
        private readonly ILogger<UserController> logger;

        public UserController(IHostingEnvironment hostingEnvironment,ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<UserController> _logger)
        {
            hosting = hostingEnvironment;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            logger = _logger;
        }

        public bool checkUser()
        {
            if (_userManager.GetUserId(HttpContext.User) == null)
            {
                return false;
            }
            return true;
        }
        public IActionResult Index()
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            List<applicationDatabase> a = new List<applicationDatabase>();
            string userID=this.HttpContext.Session.GetString("userID");
       
                List<userApplication> u = _applicationDbContext.userApplications.Where(e => e.Id == userID).ToList();
                List<appDatabase> appDb = new List<appDatabase>();
            //try
            //{
                foreach (var item in u)
                {
                    appDb.Add(_applicationDbContext.appDatabases.Where(e => e.appId == item.appId).FirstOrDefault());
                }

                foreach (var item in u)
                {
                    a.Add(new applicationDatabase { userApplications = item, appDatabases = appDb.Where(e => e.appId == item.appId).FirstOrDefault() });
                }
            //}
            //catch (NullReferenceException)
            //{
            //    return RedirectToAction("index", "home");
            //}
            ViewBag.userName = HttpContext.Session.GetString("userName");
            return View(a);
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(user model)
        {
            if (ModelState.IsValid)
            {
                var getUsers = _userManager.Users;
                var u1 = getUsers.Where(e => e.UserName == model.UserName).FirstOrDefault();
                if (u1 != null)
                {
                    ViewBag.errorMsg = "User already exists!";
                    return View();
                }

                var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var user1 = _userManager.Users;
                    var u = user1.Where(e => e.UserName == model.UserName).FirstOrDefault();

                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "User",
                     new { userId = u.Id, token = token }, Request.Scheme);
                    logger.Log(LogLevel.Warning, confirmationLink);
                    ViewBag.ErrorTitle = "Registration successful";
                    ViewBag.ErrorMessage = "Before you can Login, please confirm your " +
                            "email, by clicking on the confirmation link we have emailed you";
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("webtiqs@gmail.com"));
                    message.To.Add(new MailboxAddress(model.Email));
                    message.Subject = "Email Confirmation";
                    message.Body = new TextPart("plain")
                    {
                        Text = confirmationLink
                    };
                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("webtiqs@gmail.com", "Webtiqs_1");
                        client.Send(message);
                        client.Disconnect(true);

                    }
                    return View("Error");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View();
        }
        public async Task<IActionResult> Login(string returnUrl)
        {
            loginUser model = new loginUser
            {
                ReturnUrl = returnUrl,
                ExternalLogins= (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()

            };
            return View(model);
        }
       [HttpPost]
        public async Task<IActionResult> Login(loginUser user)
        {
            var adminEmail = "webtiqs@gmail.com";
            var adminPassword = "Admin-01";
            
            if (ModelState.IsValid)
            {
                var user2 = _userManager.Users;
                var u1 = user2.Where(e => e.UserName == user.UserName).FirstOrDefault();
                
                try
                {
                    var user3 = await _userManager.FindByEmailAsync(u1.Email);
                    if(user3.Email == adminEmail && 
                        await _userManager.CheckPasswordAsync(user3, adminPassword))
                    {
                        HttpContext.Session.SetString("admin", user3.UserName);
                        return View("~/Views/WebAdmin/Index.cshtml");
                    }
                    if (user3 != null && !user3.EmailConfirmed &&
                    (await _userManager.CheckPasswordAsync(user3, user.Password)))
                    {
                        ViewBag.ErrorMsg = "Email not confirmed yet !";
                        loginUser model1 = new loginUser
                        {
                            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()

                        };
                        return View(model1);
                    }

                }
                catch (Exception)
                {
                    ViewBag.ErrorMsg = "User doesn't exists! ";
                    return RedirectToAction("login");
                }
               

                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe,false);
                if (result.Succeeded)
                {
                    var user1 = _userManager.Users;
                    var u = user1.Where(e=>e.UserName==user.UserName).FirstOrDefault();
                    string userID = "userID";
                    HttpContext.Session.SetString(userID, u.Id);
                    HttpContext.Session.SetString("userName", u.UserName);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMsg = "Invalid Username or Password!";
                }
            }
            loginUser model = new loginUser
            {
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()

            };
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            try
            {
                var redirectUrl = Url.Action("ExternalLoginCallback", "User",
                                    new { ReturnUrl = returnUrl });
                var properties = _signInManager
                    .ConfigureExternalAuthenticationProperties(provider, redirectUrl);
                return new ChallengeResult(provider, properties);
            }
            catch(Exception)
            {
                return View("~/Views/User/login.cshtml");
            }

        }
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            try
            {
                returnUrl = returnUrl ?? Url.Content("~/");

                loginUser loginViewModel = new loginUser
                {
                    ReturnUrl = returnUrl,
                    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
                };

                if (remoteError != null)
                {
                    ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                    return View("Login", loginViewModel);
                }

                // Get the login information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    ModelState
                        .AddModelError(string.Empty, "Error loading external login information.");

                    return View("Login", loginViewModel);
                }

                var email3 = info.Principal.FindFirstValue(ClaimTypes.Email);
                

                if (email3 != null)
                {
                    // Find the user
                   var user = await _userManager.FindByEmailAsync(email3);

                    // If email is not confirmed, display login view with validation error
                    if (user != null && !user.EmailConfirmed)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action("ConfirmEmail", "User",
                         new { userId = user.Id, token = token }, Request.Scheme);
                        logger.Log(LogLevel.Warning, confirmationLink);

                        ViewBag.ErrorMsg = "Email not confirm yet go to mail and confirm email";
                        var message = new MimeMessage();
                        message.From.Add(new MailboxAddress("webtiqs@gmail.com"));
                        message.To.Add(new MailboxAddress(user.Email));
                        message.Subject = "Email Confirmation";
                        message.Body = new TextPart("plain")
                        {
                            Text = confirmationLink
                        };
                        using (var client = new SmtpClient())
                        {
                            client.Connect("smtp.gmail.com", 587, false);
                            client.Authenticate("webtiqs@gmail.com", "Webtiqs_1");
                            client.Send(message);
                            client.Disconnect(true);

                        }
                        return View("Login", loginViewModel);
                    }
                }
                // If the user already has a login (i.e if there is a record in AspNetUserLogins
                // table) then sign-in the user with this external login provider
                var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                    info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

                if (signInResult.Succeeded)
                {
                    var email1 = info.Principal.FindFirstValue(ClaimTypes.Email);
                    var u1 = await _userManager.FindByEmailAsync(email1);
                    string userID = "userID";
                    HttpContext.Session.SetString(userID, u1.Id);
                    return RedirectToAction("Index", "Home");
                }
                // If there is no record in AspNetUserLogins table, the user may not have
                // a local account
                else
                {
                    // Get the email claim value
                    var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                    if (email != null)
                    {
                        // Create a new user without password if we do not have a user already
                        var user = await _userManager.FindByEmailAsync(email);

                        if (user == null)
                        {
                            user = new aspUser
                            {
                                UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                                Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                            };

                            await _userManager.CreateAsync(user);
                        }

                        // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                        await _userManager.AddLoginAsync(user, info);
                        await _signInManager.SignInAsync(user, isPersistent: false);


                        var u1 = await _userManager.FindByEmailAsync(email);
                        string userID = "userID";
                        HttpContext.Session.SetString(userID, u1.Id);
                        return RedirectToAction("Index", "Home");
                    }

                   

                    return View("Error");
                }
            }
            catch(Exception)
            {
                return View("~/Views/User/login.cshtml");
            }
           
        }
        public async Task<ActionResult> Logout()
        {
            //if (HttpContext.Session.GetString("userName") != null)
            //{
                await _signInManager.SignOutAsync();
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "User");
            //}
            //else
            //{
            //    return View("~/Views/User/Login.cshtml");
            //}

        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
       
       
       
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                // ChangePasswordAsync changes the user password
                var result = await _userManager.ChangePasswordAsync(user,
                    model.CurrentPassword, model.newPassword);

                // The new password did not meet the complexity rules or
                // the current password is incorrect. Add these errors to
                // the ModelState and rerender ChangePassword view
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }
                ViewBag.successMsg = "Your password was changed successfully! ";
                ViewBag.loginAgain = "You need to login to continue";
                // Upon successfully changing the password refresh sign-in cookie
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("login","user");
            }

            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ApplicationForgot(string dbName)
        {
            var DBNAME= _applicationDbContext.appDatabases.Where(e => e.dbName == dbName).FirstOrDefault();
            var app = _applicationDbContext.userApplications.Where(e => e.appId == DBNAME.appId).FirstOrDefault();

            forgotAppPassword = app.appName;
            return View("ApplicationForgotPassword");
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ApplicationForgotPassword(String Email)
        {

            // Find the user by email
            var user = await _userManager.FindByEmailAsync(Email);
            // If the user is found AND Email is confirmed
            if (user != null)
            {
                // Generate the reset password token
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                // Build the password reset link

                var passwordResetLink = Url.Action("ResetApplicationPassword", "User",
                    new { email = forgotAppPassword, token = token }, Request.Scheme);
                // Log the password reset link
                logger.Log(LogLevel.Warning, passwordResetLink);

                // Send the user to Forgot Password Confirmation view
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("webtiqs@gmail.com"));
                message.To.Add(new MailboxAddress(Email));
                message.Subject = "Rest Password";
                message.Body = new TextPart("plain")
                {
                    Text = passwordResetLink
                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("webtiqs@gmail.com", "Webtiqs_1");
                    client.Send(message);
                    client.Disconnect(true);

                }
                return View("ForgotPasswordConfirmation");

            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(String Email)
        {

            // Find the user by email
            var user = await _userManager.FindByEmailAsync(Email);
                // If the user is found AND Email is confirmed
                if (user != null)
                {
                // Generate the reset password token
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                // Build the password reset link
               
                var passwordResetLink = Url.Action("ResetPassword", "User",
                    new { email= user.Email, token = token }, Request.Scheme);
                // Log the password reset link
                logger.Log(LogLevel.Warning, passwordResetLink);
               
                // Send the user to Forgot Password Confirmation view
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("webtiqs@gmail.com"));
                message.To.Add(new MailboxAddress(Email));
                message.Subject = "Rest Password";
                message.Body = new TextPart("plain")
                {
                    Text = passwordResetLink
                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("webtiqs@gmail.com", "Webtiqs_1");
                    client.Send(message);
                    client.Disconnect(true);

                }
                return View("ForgotPasswordConfirmation");
             
            }

            return View();
        }
        public ActionResult deleteApplication(string appName)
        {

            userApplication u= _applicationDbContext.userApplications.Where(e => e.appName == appName).FirstOrDefault();
            appDatabase db = _applicationDbContext.appDatabases.Where(d => d.appId == u.appId).FirstOrDefault();
            List<dbTables> dtable = _applicationDbContext.dbTables.Where(e => e.dbId == db.dbId).ToList();
            ManageDatabase mb = new ManageDatabase();
            mb.deleteApplication(db.dbName);
            foreach (var item in dtable)
            {
               removedata(item);
            }
            _applicationDbContext.appDatabases.Remove(db);
            _applicationDbContext.SaveChanges();
            _applicationDbContext.userApplications.Remove(u);
            _applicationDbContext.SaveChanges();

           
            List<applicationDatabase> a = new List<applicationDatabase>();
            string userID = this.HttpContext.Session.GetString("userID");

            List<userApplication> u1 = _applicationDbContext.userApplications.Where(e => e.Id == userID).ToList();
            List<appDatabase> appDb = new List<appDatabase>();
            
            foreach (var item in u1)
            {
                appDb.Add(_applicationDbContext.appDatabases.Where(e => e.appId == item.appId).FirstOrDefault());
            }

            foreach (var item in u1)
            {
                a.Add(new applicationDatabase { userApplications = item, appDatabases = appDb.Where(e => e.appId == item.appId).FirstOrDefault() });
            }

            ViewBag.userName = HttpContext.Session.GetString("userName");
            return PartialView("_listofApplication",a);
        }
        private void removedata(dbTables tables)
        {
            _applicationDbContext.dbTables.Remove(tables);
            _applicationDbContext.SaveChanges();
        }
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }
      

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // reset the user password
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    // Display validation errors. For example, password reset token already
                    // used to change the password or password complexity rules not met
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist
                return View("ResetPasswordConfirmation");
            }
            // Display validation errors if model state is not valid
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetApplicationPassword(string token, string email)
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public  IActionResult ResetApplicationPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var app = _applicationDbContext.userApplications.Where(e => e.appName == model.Email).FirstOrDefault();
                app.appPassword = model.Password;
                _applicationDbContext.SaveChanges();
                   
                

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist
                return View("ResetApplicationPasswordConfirmation");
            }
            // Display validation errors if model state is not valid
            return View(model);
        }
        [HttpPost]
        public IActionResult manageAppData(string appOldName, string appNewName, string appOldPassword, string appNewPassword, IFormFile appLogoChange)
        {
            var application = _applicationDbContext.userApplications.Where(a => a.appName == appOldName).FirstOrDefault();
            //if (application != null)
            //{
            //    try
            //    {
            var applogoad = appLogoChange;
            application.appName = appNewName;
            if (appLogoChange != null)
            {

                var extension = Path.GetExtension(appLogoChange.FileName);
                var name = Path.GetFileNameWithoutExtension(appLogoChange.FileName);
                string file = Path.GetRandomFileName() + extension;
                var path = Path.Combine(this.hosting.WebRootPath, "logo", file);
                var stream = new FileStream(path, FileMode.Create);
                appLogoChange.CopyToAsync(stream);
                application.appLogo = file;
            }

            if (appNewPassword != null)
            {
                if (application.appPassword == appOldPassword)
                {
                    application.appPassword = appNewPassword;
                }
            }
            _applicationDbContext.SaveChanges();
            //    }
            //    catch (Exception)
            //    {
            //        ViewBag.errorMsg = "Application can't be editted. Please try later.";
            //    }

            //}

            return RedirectToAction("index", "User");
        }
    



    }


}