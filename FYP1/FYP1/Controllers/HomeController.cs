using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FYP1.Models;
using FYP1.ViewModel;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;

namespace FYP1.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        static List<Relation> relations = new List<Relation>();
        static Databases databases = new Databases();
        static int cFlag = 0;
        static string tname;
        static int tableId = 1;
        static int tId ;
        static List<Column> columns = new List<Column>();
        static List<Tables> tabless = new List<Tables>();
        static int columnId = 1;
        private IHostingEnvironment hosting;
        private ApplicationDbContext _applicationDbContext;
        static userApplication uApplication = new userApplication();
        private string appName = ""; 
        public HomeController(IHostingEnvironment hostingEnvironment,  ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            hosting = hostingEnvironment;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }    
                
        
        public bool checkUser()
        {
            if (_userManager.GetUserId(HttpContext.User) == null)
            {
                return false;
            }
            return true;
        }
   
        public async Task<IActionResult> Index()
        {
            if (checkUser())
            {
                string userID = this.HttpContext.Session.GetString("userID");
                var user1 = await _userManager.FindByIdAsync(userID);
                aspUser u = new aspUser();
                if (user1 != null)
                {
                    u.Id = user1.Id;
                    u.Email = user1.Email;
                    u.UserName = user1.UserName;
                }
                return View(u);
               
            }
            ViewBag.loginErrorMsg = "Please login first!";
            return RedirectToAction("login","user");
        }
        public IActionResult createApplication()
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            return View();
        }
        [HttpPost]
        public IActionResult createApplication(string applicationName,string apPassword, IFormFile Logo)
        {
           
            if(applicationName==null || apPassword==null || Logo == null)
            {
                ViewBag.errorMsg = "Please fill in all the fields";
                return View("~/Views/Home/createApplication.cshtml");
            }
            if(apPassword.Length < 6)
            {
                ViewBag.lengthErrorMsg = "Password should be at least 6 characters long!";
                return View("~/Views/Home/createApplication.cshtml");
            }
            try {
                List<userApplication> appList = new List<userApplication>();
                appList = _applicationDbContext.userApplications.ToList();

                foreach (var item in appList)
                {
                    if (item.appName.Equals(applicationName))
                    {
                        ViewBag.appErrorMsg = "This application already exists!";
                        return View("~/Views/Home/createApplication.cshtml");
                    }
                }
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("createApplication", "home");
            }

            var extension = Path.GetExtension(Logo.FileName);
            var name = Path.GetFileNameWithoutExtension(Logo.FileName);
            string file = Path.GetRandomFileName() + extension;
            var path = Path.Combine(this.hosting.WebRootPath, "logo", file);
            var stream = new FileStream(path, FileMode.Create);
            Logo.CopyToAsync(stream);
      
            uApplication.appName = applicationName;
            uApplication.appPassword = apPassword;
            uApplication.appLogo = file;
            uApplication.Id = this.HttpContext.Session.GetString("userID");
            return RedirectToAction("createDatabase");
        }
        public ActionResult createDatabase()
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            return View();
        }
        [HttpPost]
        public ActionResult createDatabase(string databaseName)
        {
            List<appDatabase> dbList = new List<appDatabase>();
            dbList = _applicationDbContext.appDatabases.ToList();
            
            foreach(var item in dbList)
            {
                if (item.dbName.Equals(databaseName))
                {
                    ViewBag.dbErrorMsg = "This database already Exist!";
                    return View("~/Views/Home/createDatabase.cshtml");
                }
            }

            databases.id = 1;
            databases.name = databaseName;
            ViewBag.databaseName = databases.name;
            return RedirectToAction("createTables");
        }
        public ActionResult createTables(string databaseName) {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            ViewBag.databaseName = databases.name;
            return View(tabless);
        }
        public ActionResult partialTable()
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            return PartialView("_PartialTable");
        }
        public ActionResult addTableName(string tableName)
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            cFlag = 0;
            tname = tableName;
            tId = -1;
            ViewBag.databaseName = databases.name;
            return PartialView("_PartialColumns", columns);
        }
        public int checkTableExist(string tableName)
        {
            if (!checkUser())
            {
                 RedirectToAction("login", "user");
            }
            if (tableName == null)
            {
                ViewBag.errorMsg = "Table name can't be empty!";
                return -1;
            }
            foreach (var item in tabless)
            {
                if (item.tableName == tableName)
                    return 0;
            }
            return 1;
        }
        public ActionResult addColumns(string fieldName, string dataType, int required, int minLength, int maxLength, int fKey)
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            if (cFlag == 0)
            {
                columns.Add(new Column { id = columnId++, name = fieldName, dataType = dataType, required = required, minLength = minLength, maxLength = maxLength});
            }
            else
            {
                var c = tabless.Where(e => e.id == tId).FirstOrDefault();
                columns = c.column;
                columnId = c.column.Count();
                columns.Add(new Column { id = ++columnId, name = fieldName, dataType = dataType, required = required, minLength = minLength, maxLength = maxLength });
                c.column = (columns.ToList());
            }
            return PartialView("_PartialColumns", columns);
        }
        public int checkColumnExit(string columnName)
        {
            if (!checkUser())
            {
                 RedirectToAction("login", "user");
            }
            foreach (var item in columns)
            {
                if (item.name == columnName)
                    return 0;
            }
            return 1;
        }
        public int checkCExit(string columnName) {
            if (!checkUser())
            {
                 RedirectToAction("login", "user");
            }
            if (tId == -1)
            {
                return checkColumnExit(columnName);
            }
            else if (tabless.Count() > 0)
            {
                var t = tabless.Where(e => e.id == tId).FirstOrDefault();
                foreach (var item in t.column)
                {
                    if (item.name == columnName)
                        return 0;

                }
            }
            else
            {
                return checkColumnExit(columnName);


            }
           return 1;
        }
        public ActionResult saveTable()
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            if (cFlag == 0)
            {
                tabless.Add(new Tables { id = tableId++, tableName = tname, column = (columns.ToList()) });
            }
            else
            {
                columnId = 1;
            } 
            columns.Clear();
            columnId = 1;
            tId = 0;
            return PartialView("_slideBarPartialTables", tabless);
        }
        public ActionResult viewColumnPartial(int tableId)
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            cFlag = 1;
            tId = tableId;
            var t = tabless.Where(e => e.id == tableId).FirstOrDefault();
            return PartialView("_PartialColumns", t.column);
        }
        public ActionResult deleteColumn(int columnId)
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            var t = tabless.Where(e => e.id == tId).FirstOrDefault();
            var c = t.column.Where(e => e.id == columnId).FirstOrDefault();
            t.column.Remove(c);
            return PartialView("_PartialColumns", t.column);
        }
        public ActionResult updateColumn(int columnId)
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            var t = tabless.Where(e => e.id == tId).FirstOrDefault();
            var c = t.column.Where(e => e.id == columnId).FirstOrDefault();
            return PartialView("_PartialEditColumn", c);
        }
        public ActionResult editColumn(int id,string fieldName, string dataType, int required, int minLength, int maxLength, int fKey)
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            List<Column> c = new List<Column>();
            Column column = new Column();
            column.id = id;
            column.name = fieldName;
            column.dataType = dataType;
            column.required = required;
            column.minLength = minLength;
            column.maxLength = maxLength;
   
            var t= tabless.Where(e => e.id == tId).FirstOrDefault();
            var col = t.column.Where(e => e.id == id).FirstOrDefault();
             int index=t.column.IndexOf(col);
            t.column.Remove(col);
            c = t.column;
            c.Insert(index, column);
            t.column = c.ToList();
            var table= tabless.Where(e => e.id == tId).FirstOrDefault();
            return PartialView("_PartialColumns", table.column.ToList());
        }
        public int checkEditColumnExit(int id,string columnName) {
            if (!checkUser())
            {
                 RedirectToAction("login", "user");
            }
            var t = tabless.Where(e => e.id == tId).FirstOrDefault();
            foreach (var item in t.column)
            {
                if (item.id != id)
                {
                    if (item.name == columnName)
                        return 0;
                }
            }
            return 1;
        }
        public ActionResult relation()
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            tableRelation tableRelation = new tableRelation();
            tableRelation.tables = tabless;
            tableRelation.relation = relations;
            return View(tableRelation);
        }
        public ActionResult MakeRelation(int pKey, int fKey,string relation)
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            int flag = 0;
            tableRelation tableRelation = new tableRelation();
            var table = tabless.Where(e => e.id == pKey).FirstOrDefault();
            var t = tabless.Where(e => e.id == fKey).FirstOrDefault();
            table.foreignKey = fKey;
            foreach (var item in relations)
            {
                if (item.pTableName == table.tableName && item.fTableName == t.tableName)
                {
                    ViewBag.errorMessage = "Relation Already Exit";
                    flag = 1;
                    break;
                }
            }
            if (flag == 0)
            {
                ViewBag.errorMessage = "";
                relations.Add(new Relation { pkey = pKey, pTableName = table.tableName, fKey = fKey, fTableName = t.tableName, relationType = relation });

            }
            tableRelation.tables = tabless;
            tableRelation.relation = relations;
            return PartialView("_partialRelation", tableRelation);
        }
        public ActionResult getForeignKeyList(int pkey)
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            List<Tables> t = new List<Tables>();
            foreach (var item in tabless)
            {
                if (item.id != pkey) {
                    t.Add(item);
                }
            }
            return PartialView("_PartialForeignKey", t.ToList());
        }
        public ActionResult databaseCreated()
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "user");
            }
            databases.tables = tabless;
            databases.relations = relations;
            
            userInterfaceController u = new userInterfaceController();
            u.recieveDatabase(databases,uApplication);
            ManageDatabase m = new ManageDatabase();
            m.createDatabase(databases.name);
            string connectionString = m.getConnectionString(databases.name);
            m.makeDatabaseConnection(connectionString);
            m.openConnection();
            foreach (var item in databases.tables)
            {
                m.createTable(item.tableName,item.column);
            }
            m.createFrom();
            m.createRelationTable();
            m.makeRelation(databases.relations);
            m.closeConnection();
            saveDatabaseToProject(databases);
            
          
            return PartialView("_partialSuccessDialog");
        }
        public void saveDatabaseToProject(Databases d)
        {
            _applicationDbContext.userApplications.Add(uApplication);
            _applicationDbContext.SaveChanges();
            var uApp = _applicationDbContext.userApplications.Where(e => e.appName == uApplication.appName).FirstOrDefault();
            appDatabase app = new appDatabase();
            app.dbName = d.name;
            app.appId = uApp.appId;
            _applicationDbContext.appDatabases.Add(app);
            _applicationDbContext.SaveChanges();
            var database = _applicationDbContext.appDatabases.Where(e => e.dbName == d.name).FirstOrDefault();
            foreach (var item in d.tables)
            {
                dbTables tables = new dbTables();
                tables.tablename = item.tableName;
                tables.dbId = database.dbId;
                _applicationDbContext.dbTables.Add(tables);
                _applicationDbContext.SaveChanges();
                var t = _applicationDbContext.dbTables.Where(e => e.tablename == item.tableName).FirstOrDefault();

            }
        }
        public void setAppName(string appName)
        {
            this.appName = appName;
        }
       
    }
}
