
using System;
using System.Collections.Generic;
using System.Linq;
using FYP.Controllers;
using FYP1.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace FYP1.Controllers
{

    public class userInterfaceController : Controller
    {

        static Databases databases = new Databases();
        static private userApplication _uApplication;
        //private UserManager<IdentityUser> _userManager;
        //private IHostingEnvironment hosting;

        public bool checkUser()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return false;
            }
            return true;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("userName")==null)
            {
                return RedirectToAction("login", "user");
            }
            ViewBag.db = databases.name;
            insertRelation();
            return View(databases);
        }
       
        public void insertRelation()
        {
            foreach (var item in databases.relations)
            {
                var table = databases.tables.Where(e => e.tableName == item.fTableName).FirstOrDefault();
                List<Column> c = table.column.Where(e => e.dataType == "select").ToList();
                foreach (var i in c)
                {
                    table.column.Remove(i);
                }
            }
            foreach (var item in databases.relations)
            {
                var table = databases.tables.Where(e => e.tableName == item.fTableName).FirstOrDefault();
                table.column.Add(new Column { name = item.pTableName + "id", dataType = "select" });
            }
        }
        public void recieveDatabase(Databases d,userApplication uApplication)
        {
            
            databases = d;
            _uApplication = uApplication;

        }
        public IActionResult getTableAttributes(string nameTable)
        {
            if (checkUser() == false)
            {
                return RedirectToAction("login", "user");
            }
            try
            {
                List<Tables> tablesList = new List<Tables>();
                tablesList = databases.tables;
                var t = tablesList.Where(x => x.tableName == nameTable).FirstOrDefault();
                List<Column> tableAttrs = new List<Column>();
                tableAttrs = t.column;
                return PartialView("_PartialCreateForm", tableAttrs);
            }
            catch(Exception)
            {
                return RedirectToAction("index","userInterface");
            }

        }

        public IActionResult createSystem()
        {
            if (checkUser() == false)
            {
                return RedirectToAction("login", "user");
            }
            ManageDatabase m = new ManageDatabase();
          
            string connectionString = m.getConnectionString(databases.name);
            m.makeDatabaseConnection(connectionString);
            m.openConnection();
         
          
            m.closeConnection();
            return RedirectToAction("Index","User");
        }
        public ActionResult styleForm()
        {
            if (checkUser() == false)
            {
                return RedirectToAction("login", "user");
            }
            return PartialView("~/Views/userInterface/_styleSideBar.cshtml");
        }
        public IActionResult SaveForm()
        {
            if (checkUser() == false)
            {
                return RedirectToAction("login", "user");
            }
            return View(Index());
        }
    }
}