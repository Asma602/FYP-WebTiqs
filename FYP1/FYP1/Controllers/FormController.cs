using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FYP1.Models;
using FYP1.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace FYP.Controllers
{
    public class FormController : Controller
    {

        ManageDatabase mdb = new ManageDatabase();
        static List<Form> formList = new List<Form>();
        public int fid = 0;
        static string EDITTABLEDATA;
        static int EDITFORMDATA = 0;
        static string tName;
        static string databaseName;
        static string[] column;
        static int recordId;
        static string editRecordTable;
        private readonly ApplicationDbContext _applicationDbContext;
        private IHostingEnvironment _env;
        private UserManager<IdentityUser> _userManager;

        public FormController(ApplicationDbContext applicationDbContext, IHostingEnvironment hostingEnvironment, UserManager<IdentityUser> userManager)
        {
            _env = hostingEnvironment;
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }

        public bool checkUser()
        {
            if (_userManager.GetUserId(HttpContext.User) == null)
            {
                return false;
            }
            return true;
        }
        [HttpGet]
        public IActionResult previewSystem()
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "User");
            }
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
                return RedirectToAction("index", "form");
            }

        }
        [HttpPost]
        public IActionResult Index(string dbName, string password)
        {
            

            if (!checkUser())
            {
                return RedirectToAction("login", "User");
            }
            try
            {
                databaseName = dbName;
                appDatabase appDb = _applicationDbContext.appDatabases.Where(e => e.dbName.Equals(dbName)).FirstOrDefault();
                userApplication userApplication = _applicationDbContext.userApplications.Where(e => e.appId == appDb.appId).FirstOrDefault();
                if (userApplication.appPassword == password)
                {
                    applicationDatabase appDatabase = new applicationDatabase();
                    appDatabase.appDatabases = appDb;
                    appDatabase.userApplications = userApplication;
                    formsApplicationDatabase formsApplicationDatabase = new formsApplicationDatabase();
                    formsApplicationDatabase.applicationDatabase = appDatabase;
                    formsApplicationDatabase.forms = GetForms(appDb.dbName);
                    return View(formsApplicationDatabase);
                }
                else
                {
                    ViewBag.errorMsg = "Incorrect Password!";
                }
            }
            catch (Exception)
            {
                ViewBag.errorMsg = "Invalid Password!";
                return RedirectToAction("index", "user");
            }

            ViewBag.errorMsg = "Invalid Password!";
            return RedirectToAction("index", "user");

        }
        public List<Form> GetForms(string dbName)
        {
            if (!checkUser())
            {
                RedirectToAction("login", "User");
            }

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
            conn.Close();
            return (checkList);


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
        public IActionResult addForm(int idOfTable, string nameOfForm, string formString, string tableName, string nameOfDb)
        {
            if (!checkUser())
            {
                RedirectToAction("login", "User");
            }
            try
            {
                Form f = new Form();
                f.tableid = idOfTable;
                f.formString = formString;
                f.formTitle = nameOfForm;
                f.tableName = tableName;
                databaseName = nameOfDb;
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
              
                return RedirectToAction("index", "userInterface");
            }
            catch (Exception)
            {
                return RedirectToAction("index", "userInterface");
            }
        }
        public string[] getView(int getFormId, string tableName)
        {
            EDITFORMDATA = getFormId;
            EDITTABLEDATA = tableName;

            if (!checkUser())
            {
                RedirectToAction("login", "User");
            }
            string[] error = new string[1];
            error[0] = "Error!";
            try
            {
                formList = GetForms(databaseName);
                var form = formList.Where(f => f.formId == getFormId).FirstOrDefault();
                string[] arr = new string[3];
                arr[0] = form.formString;
                arr[1] = form.formTitle;
                tName = tableName;
                return arr;
            }
            catch (Exception)
            {
                return error;
            }
        }
        
        public List<Form> getForms()
        {
            if (!checkUser())
            {
                 RedirectToAction("login", "User");
            }
            return formList;
        }

        public ActionResult styleForm()
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "User");
            }
            return PartialView("~/Views/userInterface/_styleSideBar.cshtml");
        }
        public int saveFormVals(string fname, string[] cols, string[] attrVals)
        {
            if (!checkUser())
            {
                 RedirectToAction("login", "User");
            }

            try
            {
                ManageDatabase m = new ManageDatabase();
                string connectionString = m.getConnectionString(databaseName);
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                int count = 0;
         
                SqlCommand sql = new SqlCommand("insert into " + tName + "(" + cols[0] + ")values(@name)", con);
                sql.Parameters.AddWithValue("@name", attrVals[0].ToString());
                sql.ExecuteNonQuery();
                con.Close();
                connectionString = m.getConnectionString(databaseName);
                 con = new SqlConnection(connectionString);
                con.Open();
                string sqlQuery = "select * from " + tName;
                SqlCommand sqlcom = new SqlCommand(sqlQuery, con);
                sqlcom.ExecuteNonQuery();
                SqlDataReader drr;
                drr = sqlcom.ExecuteReader();
                while (drr.Read())
                {
                    count = Int16.Parse(drr[tName + "id"].ToString());
                }

                drr.Close();
                
                for (int i = 1; i < cols.Length; i++)
                { 
                  
                        SqlCommand sql1 = new SqlCommand("UPDATE " + tName + " SET " + cols[i] + "=@name where " + tName + "id=" + count, con);
                        sql1.Parameters.AddWithValue("@name", attrVals[i]);
                        sql1.ExecuteNonQuery();
                }
                con.Close();

                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public ActionResult getRecord(string[] cols, string tableName, string dbName)
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "User");
            }
            try
            {
                column = cols;
                List<string> COLUMN = cols.ToList();
                COLUMN.Insert(0,tableName+"id");
                List<appData> appDatas = new List<appData>();
                ManageDatabase m = new ManageDatabase();
                string connectionstring = m.getConnectionString(dbName);
                SqlConnection con = new SqlConnection(connectionstring);
                con.Open();
                string sqlquery = "select * from " + tableName;
                SqlCommand sqlcom = new SqlCommand(sqlquery, con);
                sqlcom.ExecuteNonQuery();
                SqlDataReader drr;
                drr = sqlcom.ExecuteReader();
                while (drr.Read())
                {
                    List<string> data = new List<string>();
                    data.Add(drr[tableName + "id"].ToString());
                    for (int i = 0; i < cols.Length; i++)
                    {
                        data.Add(drr[cols[i]].ToString());
                    }
                    appDatas.Add(new appData { cols = data, tableName = tableName, dbName = dbName, attribute = COLUMN});
                }

                drr.Close();
                con.Close();
                return PartialView("_PartialApplicationData", appDatas);
            }
            catch (Exception)
            {
                return RedirectToAction("index","form");
            }

        }
        public ActionResult deleteAppDataRecord(int id,string tableName,string dbName)
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "User");
            }
            try
            {
                ManageDatabase m = new ManageDatabase();
                string connectionstring = m.getConnectionString(dbName);
                SqlConnection con = new SqlConnection(connectionstring);
                con.Open();
                SqlCommand command = new SqlCommand("DELETE FROM " + tableName + " WHERE " + tableName + "id= " + id, con);
                command.ExecuteNonQuery();
                con.Close();
                return getRecord(column, tableName, dbName);
            }
            catch (Exception)
            {
                return RedirectToAction("index","form");
            }

        }
        public string[] editAppDataRecord(int id)
        {
            recordId = id;
            if (!checkUser())
            {
                RedirectToAction("login", "User");
            }
            string[] error = new string[1];
            error[0] = "Error!";
            try
            {
                formList = GetForms(databaseName);
                var form = formList.Where(f => f.formId == EDITFORMDATA).FirstOrDefault();
                string[] arr = new string[3];
                arr[0] = form.formString;
                arr[1] = form.formTitle;
               
                tName = EDITTABLEDATA;
                return arr;
            }
            catch (Exception)
            {
                return error;
            }
        }
        public ActionResult updateAppRecord(string [] cols,string[] attrVals)
        {
            if (!checkUser())
            {
                return RedirectToAction("login", "User");
            }
            try
            {
                ManageDatabase m = new ManageDatabase();
                string connectionstring = m.getConnectionString(databaseName);
                SqlConnection con = new SqlConnection(connectionstring);
                con.Open();
                for (int i = 0; i < attrVals.Length; i++)
                {
                    SqlCommand sql = new SqlCommand("UPDATE " + EDITTABLEDATA + " SET " + cols[i] + "=@name where " + EDITTABLEDATA + "id=" + recordId, con);
                    sql.Parameters.AddWithValue("@name", attrVals[i]);
                    sql.ExecuteNonQuery();

                }
                con.Close();

                return getRecord(cols, EDITTABLEDATA, databaseName);
            }
            catch (Exception)
            {
                return RedirectToAction("index", "form");
            }

        }
        [HttpGet]
        public async Task<IActionResult> ExportXml(string table)
        {
            
            if (!checkUser())
            {
                 RedirectToAction("login", "User");
            }
            string webRootPath = _env.WebRootPath;
            string fileName = table + "file.xlsx";
            ManageDatabase m = new ManageDatabase();
            string connectionString = m.getConnectionString(databaseName);
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;
            DataTable dt = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter("select * from "+table,con);
            try
            {

                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    var filePath = Path.Combine(@"C:\Exported Data\", fileName);
                    FileInfo fileInfo = new FileInfo(fileName);
                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                        fileInfo = new FileInfo(fileName);
                    }
                    ExcelPackage excel = new ExcelPackage(fileInfo);

                    var worksheet = excel.Workbook.Worksheets.Add(table + "data");

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = dt.Columns[i].ColumnName.ToString();
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 2, j + 1].Value = dt.Rows[i][j].ToString();
                        }
                    }
                    excel.Save();
                    byte[] result = excel.GetAsByteArray();
                    adp.Dispose();


                    con.Close();
                    return File(result, "application/vnd.ms-excel", table+".xlsx");
                }
            }
            catch (Exception)
            {
                adp.Dispose();

            }
            con.Close();
            return RedirectToAction("index", "form");

        }
        [HttpGet]
        public async Task<IActionResult> ExportCsv(string table)
        {
            if (!checkUser())
            {
                 RedirectToAction("login", "User");
            }
            List<string> columns = new List<string>();
            ManageDatabase m = new ManageDatabase();
            string connectionString = m.getConnectionString(databaseName);
            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionString;

            con.Open();
            try
            {
                string sqlQuery = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName";
                SqlCommand sqlcom = new SqlCommand(sqlQuery, con);
                sqlcom.Parameters.AddWithValue("@tableName", table);
                sqlcom.ExecuteNonQuery();

                SqlDataReader drr;
                drr = sqlcom.ExecuteReader();
                drr.Read();
                while (drr.Read())
                {
                    columns.Add(drr["COLUMN_NAME"].ToString());

                }
                drr.Close();

                string str = ",";
                StringBuilder sb = new StringBuilder();
                SqlDataAdapter sda = new SqlDataAdapter("Select * from " + table, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                ds.Tables[0].TableName = table;
                foreach (DataRow dataRow in ds.Tables[table].Rows)
                {
                    for (int i = 0; i < columns.Count(); i++)
                    {
                        sb.Append(dataRow[columns[i]].ToString() + str);
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("\r\n");
                }
                con.Close();

                return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", table + "data.txt");
            }
            catch (Exception)
            {
                con.Close();
            }

            con.Close();
            return RedirectToAction("index", "form");
        }
        public ActionResult getForeignKey(string parentTable)
        {

            List<int> fid = new List<int>();
            ForeignKeyData foreignKeyData = new ForeignKeyData();
            ManageDatabase m = new ManageDatabase();
            SqlConnection conn = new SqlConnection(m.getConnectionString(databaseName));
            conn.Open();
            string sqlQuery = "select * from " + parentTable;
            SqlCommand sqlcom = new SqlCommand(sqlQuery, conn);
            sqlcom.ExecuteNonQuery();
            SqlDataReader drr;
            drr = sqlcom.ExecuteReader();
            while (drr.Read())
            {
                fid.Add(Int16.Parse(drr[parentTable + "id"].ToString()));
            }
            drr.Close();
            foreignKeyData.parentDataId = fid;
            foreignKeyData.tableName = parentTable + "id";

            conn.Close();
            return PartialView("_ForeignKeyData", foreignKeyData);
        }
    }
}
