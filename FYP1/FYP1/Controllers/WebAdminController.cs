using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FYP1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FYP1.Controllers
{
    public class WebAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public WebAdminController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public ActionResult Create()
        {
           
                return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminId,AdminUserName,password")] WebAdmin webAdmin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(webAdmin);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "webadmin");
            }
            return View("index");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Login(WebAdmin admin)
        {
            WebAdmin ad = _context.webAdmin.Where(a => (a.AdminUserName == admin.AdminUserName && a.password == admin.password)).FirstOrDefault();
            if (ad == null)
            {
                ViewBag.message = "Incorrect username or password!";
                return View("~/Views/User/Login.cshtml");
            }

            HttpContext.Session.SetString("username", ad.AdminUserName);
            ViewBag.username = HttpContext.Session.GetString("username");

            return View("~/Views/WebAdmin/Index.cshtml");


        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login","User");
        }
        // GET: Admins
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                ViewBag.username = HttpContext.Session.GetString("admin");
                return View();
            }
            return RedirectToAction("Login","User");
        }

        public IActionResult viewUsers()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Login", "User");
            }
            try
            {
                var users = _userManager.Users;
                List<user> usersList = new List<user>();
                foreach(var item in users)
                {
                    user u = new user();
                    u.UserName = item.UserName;
                    u.Email = item.Email;
                    usersList.Add(u);
                }
                return PartialView("_partialViewUsers",usersList);
            }
            catch (NullReferenceException)
            {
                ViewBag.errorMessage = "No User Exists!";
            }
            return View();

            
        }

        public IActionResult viewApps()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Login", "User");
            }
            List<userApplication> userApplications = new List<userApplication>();
            try
            {
                userApplications = _context.userApplications.ToList();
                return PartialView("_partialViewApps",userApplications);
            }
            catch(NullReferenceException)
            {
                ViewBag.errorMessage = "No apps created by any user";
            }
            return RedirectToAction("Index");

        }
        public IActionResult viewFeedbacks()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Login");
            }
            List<Query> contacts = new List<Query>();
            try
            {
                contacts = _context.query.ToList();
                return PartialView("_partialViewFeedbacks",contacts);
            }
            catch (NullReferenceException)
            {
                ViewBag.errorMessage = "No Feedback given by the user";
            }
            return RedirectToAction("Index");
        }

    }

}
