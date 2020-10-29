using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FYP1.Models;
using FYP1.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FYP1.Controllers
{
    public class EditUserApplicationController : Controller
    {
        static string databaseName;
        private ApplicationDbContext _applicationDbContext;

        public EditUserApplicationController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public bool checkUser()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return false;
            }
            return true;
        }
       
        [HttpPost]
        public IActionResult Index(string dbName)
        {
            if(checkUser() && dbName != null)
            {
                try
                {
                    appDatabase appDb = _applicationDbContext.appDatabases.Where(e => e.dbName.Equals(dbName)).FirstOrDefault();
                    userApplication userApplication = _applicationDbContext.userApplications.Where(e => e.appId == appDb.appId).FirstOrDefault();
                    ViewBag.dbName = dbName;
                    databaseName = dbName;
                    ViewBag.appName = userApplication.appName;
                    ViewBag.appLogo = userApplication.appLogo;
                    FormsTables ft = new FormsTables();
                   
                    
                    ManageDatabase mdb = new ManageDatabase();
                    SqlConnection conn = new SqlConnection(mdb.getConnectionString(dbName));
                    conn.Open();

                    string sqlQuery = "select * from form";
                    SqlCommand sqlcom = new SqlCommand(sqlQuery, conn);
                    sqlcom.ExecuteNonQuery();
                    List<Form> cForms = new List<Form>();
                    SqlDataReader drr;
                    drr = sqlcom.ExecuteReader();
                    while (drr.Read())
                    {
                        Form f = new Form();
                        f.formId = Int16.Parse(drr["formId"].ToString());
                        f.formString = drr["formString"].ToString();
                        f.formTitle = drr["formTitle"].ToString();
                        f.tableName = drr["tableName"].ToString();
                        cForms.Add(f);
                    }
                    drr.Close();
                    conn.Close();
                    ft.tables = getAllTables(dbName);
                    ft.forms = cForms;
                    return View(ft);
                }
                catch (Exception)
                {
                    ViewBag.ErrorMsg = "No applications";
                }

            }
            return RedirectToAction("login", "user");

        }
        public Form getApplicationFormView(int formId) {
            ManageDatabase mdb = new ManageDatabase();
            SqlConnection conn = new SqlConnection(mdb.getConnectionString(databaseName));
            conn.Open();

            string sqlQuery = "select * from form";
            SqlCommand sqlcom = new SqlCommand(sqlQuery, conn);
            sqlcom.ExecuteNonQuery();
            List<Form> cForms = new List<Form>();
            SqlDataReader drr;
            drr = sqlcom.ExecuteReader();
            while (drr.Read())
            {
                Form f = new Form();
                f.formId = Int16.Parse(drr["formId"].ToString());
                f.formString = drr["formString"].ToString();
                f.formTitle = drr["formTitle"].ToString();
                f.tableName = drr["tableName"].ToString();
                cForms.Add(f);
            }
            drr.Close();
            conn.Close();
            Form t = cForms.Where(e => e.formId == formId).FirstOrDefault();
            return t;
        }
        public ActionResult deleteApplicationForm(int formId) {
            ManageDatabase mdb = new ManageDatabase();
            SqlConnection conn = new SqlConnection(mdb.getConnectionString(databaseName));
            conn.Open();

            string sqlQuery ="delete from form where formId = " + formId;
            SqlCommand sqlcom = new SqlCommand(sqlQuery, conn);
            sqlcom.ExecuteNonQuery();
            conn.Close();
            conn = new SqlConnection(mdb.getConnectionString(databaseName));
            conn.Open();
            string sqlQ = "select * from form";
            SqlCommand sqlc= new SqlCommand(sqlQ, conn);
            sqlc.ExecuteNonQuery();
         
         
            List<Form> cForms = new List<Form>();
            SqlDataReader drr;
            drr = sqlc.ExecuteReader();
            while (drr.Read())
            {
                Form f = new Form();
                f.formId = Int16.Parse(drr["formId"].ToString());
                f.formString = drr["formString"].ToString();
                f.formTitle = drr["formTitle"].ToString();
                f.tableName = drr["tableName"].ToString();
                cForms.Add(f);
            }
            drr.Close();
            conn.Close();


            return PartialView("_form",cForms);
        }

        private List<Tables> getAllTables(string dbName)
        {
            if (checkUser())
            {
                try
                {
                    List<Tables> tables1 = new List<Tables>();
                    ManageDatabase m = new ManageDatabase();
                    SqlConnection conn = new SqlConnection(m.getConnectionString(dbName));
                    conn.Open();
                    List<string> tables = new List<string>();
                    DataTable dt = conn.GetSchema("Tables");

                    foreach (DataRow item in dt.Rows)
                    {
                        string tablename = (string)item[2];
                        if (tablename != "form" && tablename != "relation" && !tablename.Contains("dataType"))
                        {
                            tables.Add(tablename);
                        }
                    }

                    for (int i = 0; i < tables.Count(); i++)
                    {
                        int k = 0;
                        List<Column> columns = new List<Column>();

                        SqlCommand sqlcom = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName", conn);
                        sqlcom.Parameters.AddWithValue("@tableName", tables[i]);
                        sqlcom.ExecuteNonQuery();
                        SqlDataReader drr1;
                        drr1 = sqlcom.ExecuteReader();
                        while (drr1.Read())
                        {
                            if (drr1["COLUMN_NAME"].ToString() != tables[i] + "id")
                                columns.Add(new Column { id = ++k, dataType = drr1["DATA_TYPE"].ToString(), name = drr1["COLUMN_NAME"].ToString(), maxLength = Convert.ToInt32(Convert.IsDBNull(drr1["CHARACTER_MAXIMUM_LENGTH"]) ? null : (int?)drr1["CHARACTER_MAXIMUM_LENGTH"]) });
                        }
                        drr1.Close();
                        SqlCommand sqlcom1 = new SqlCommand("SELECT * FROM dataType" + tables[i], conn);
                        sqlcom1.ExecuteNonQuery();
                        SqlDataReader drr2;
                        drr2 = sqlcom1.ExecuteReader();
                        while (drr2.Read())
                        {
                            string fieldName = drr2["fieldName"].ToString();
                            foreach (var item in columns)
                            {
                                if (fieldName == item.name)
                                {
                                    item.dataType = drr2["dataType"].ToString();
                                }
                            }
                        }
                        tables1.Add(new Tables { id = i, tableName = tables[i], column = columns });
                        drr2.Close();

                    }

                    conn.Close();
                    return tables1;
                }
                catch (Exception)
                {

                }
            }
            RedirectToAction("login", "user");
            return null;

        }

        public IActionResult getTableAttributes(string nameTable)
        {
            if (checkUser())
            {
                try
                {
                    List<Tables> tablesList = getAllTables(databaseName);
                    var t = tablesList.Where(x => x.tableName == nameTable).FirstOrDefault();
                    List<Column> tableAttrs = new List<Column>();
                    tableAttrs = t.column;
                    ManageDatabase m = new ManageDatabase();
                    m.makeDatabaseConnection(m.getConnectionString(databaseName));
                    m.openConnection();
                    List<Relation> relations = m.getAllRelation();
                    foreach (var item in relations)
                    {
                        foreach (var i in tableAttrs)
                        {
                            if (i.name == item.pTableName + "id")
                            {
                                i.dataType = "select";
                            }
                        }
                    }
                    return PartialView("_createForm", tableAttrs);
                }
                catch (Exception)
                {
                    RedirectToAction("index", "EditUserApplication");
                }

            }
            return RedirectToAction("login", "user");

        }
        public IActionResult addForm(int idOfTable, string nameOfForm, string formString, string tableName, string nameOfDb)
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "User");
            }
            try
            {
                Form f = new Form();
                f.tableid = idOfTable;
                f.formString = formString;
                f.formTitle = nameOfForm;
                f.tableName = tableName;
                databaseName = nameOfDb;
                ManageDatabase mdb = new ManageDatabase();
                SqlConnection conn = new SqlConnection(mdb.getConnectionString(nameOfDb));
                conn.Open();

                string sqlQuery = "insert into form(tableid,formString,formTitle,tableName)Values( @tableid,@formString,@formTitle,@tableName)";
                SqlCommand sqlcom = new SqlCommand(sqlQuery, conn);
                sqlcom.Parameters.AddWithValue("@tableid", f.tableid);
                sqlcom.Parameters.AddWithValue("@formString", f.formString);
                sqlcom.Parameters.AddWithValue("@formTitle", f.formTitle);
                sqlcom.Parameters.AddWithValue("@tableName", f.tableName);
                sqlcom.ExecuteNonQuery();
                conn.Close();

                return RedirectToAction("index","EditUserApplication");
            }
            catch (Exception)
            {
                return RedirectToAction("index", "EditUserApplication");
            }
        }
        public ActionResult previewSystem()
        {
            if (checkUser())
            {
                try
                {
                    appDatabase appDb = _applicationDbContext.appDatabases.Where(e => e.dbName.Equals(databaseName)).FirstOrDefault();
                    userApplication userApplication = _applicationDbContext.userApplications.Where(e => e.appId == appDb.appId).FirstOrDefault();
                    applicationDatabase appDatabase = new applicationDatabase();
                    appDatabase.appDatabases = appDb;
                    databaseName = appDb.dbName;
                    appDatabase.userApplications = userApplication;
                    formsApplicationDatabase formsApplicationDatabase = new formsApplicationDatabase();
                    formsApplicationDatabase.applicationDatabase = appDatabase;
                    formsApplicationDatabase.forms = GetForms(appDb.dbName);
                    return View(formsApplicationDatabase);
                }
                catch (Exception)
                {
                    return RedirectToAction("index", "EditUserApplication");
                }
            }
            return RedirectToAction("login", "user");

        }
        public List<Form> GetForms(string dbName)
        {
            if (checkUser())
            {
                try
                {
                    ManageDatabase mdb = new ManageDatabase();
                    List<Form> checkList = new List<Form>();
                    SqlConnection conn = new SqlConnection(mdb.getConnectionString(dbName));
                    conn.Open();

                    string sqlQuery = "select * from form";
                    SqlCommand sqlcom = new SqlCommand(sqlQuery, conn);
                    sqlcom.ExecuteNonQuery();

                    SqlDataReader drr;
                    drr = sqlcom.ExecuteReader();
                    while (drr.Read())
                    {
                        Form f = new Form();
                        f.formId = Int16.Parse(drr["formId"].ToString());
                        f.formString = drr["formString"].ToString();
                        f.formTitle = drr["formTitle"].ToString();
                        f.tableName = drr["tableName"].ToString();
                        checkList.Add(f);
                    }
                    drr.Close();
                    return (checkList);
                }
                catch (Exception)
                {

                }
            }
            RedirectToAction("login", "user");
            return null;


        }
        public string[] getView(int getFormId, string tableName)
        {
            if (checkUser())
            {
                try
                {
                    List<Form> formList = new List<Form>();
                    formList = GetForms(databaseName);
                    var form = formList.Where(f => f.formId == getFormId).FirstOrDefault();
                    string[] arr = new string[3];
                    arr[0] = form.formString;
                    arr[1] = form.formTitle;
                    return arr;
                }
                catch (Exception){
                    ViewBag.errorMsg = "Form doesn't exist";
                }
            }
            RedirectToAction("login", "user");
            return null;

        }
        public ActionResult deleteForm(int FormId)
        {

            if (checkUser())
            {
                try
                {
                    ManageDatabase m = new ManageDatabase();
                    m.makeDatabaseConnection(m.getConnectionString(databaseName));
                    m.openConnection();
                    m.deleteForm(FormId);
                    m.closeConnection();
                    return PartialView("_FormList", GetForms(databaseName));
                }
                catch (Exception)
                {

                }
            }
            return RedirectToAction("login", "user"); ;
            
        }
        public ActionResult styleForm()
        {
            return PartialView("~/Views/userInterface/_styleSideBar.cshtml");
        }
        public int checkDuplicateName(string fname, string database)
        {
            if (!checkUser())
            {
                RedirectToAction("login", "User");
            }
            try
            { 
                List<Form> formsList = new List<Form>();
                formsList = GetForms(database);
                foreach (var item in formsList)
                {
                    if (fname.Equals(item.formTitle))
                    {
                        return 1;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }

            return 0;

        }
    }
}