
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FYP1.ViewModel;
using System.Threading.Tasks;
using FYP1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace FYP1.Controllers
{
    public class EditDatabaseController : Controller
    {
        static List<int> formIds = new List<int>();
        static int tableid;
        static int cFlag = 0;
        static string databaseName;
        static string tName;
        static string tableNAME;
        static List<Column> columns = new List<Column>();
        private static int columnId = 0;


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
            if (checkUser())
            {
                try
                {
                    databaseName = dbName;
                    ViewBag.DatbaseName = databaseName;
                    return View(getAllTables(dbName));
                }
                catch (Exception)
                {
                    ViewBag.edbErrorMsg = "No databases created by you";
                }

            }
            return RedirectToAction("login", "User");

        }
        private List<Tables> getAllTables(string dbName)
        {
            if (checkUser())
            {
                List<Tables> tables1 = new List<Tables>();
                ManageDatabase m = new ManageDatabase();
                SqlConnection conn = new SqlConnection(m.getConnectionString(dbName));
                conn.Open();
                try
                {
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
                        
                        tables1.Add(new Tables { id = i, tableName = tables[i], column = columns });
                    }
                    conn.Close();
                    return tables1;
                }
                catch (Exception)
                {
                    ViewBag.TaleErrorMsg = "No table exists";
                    return null;
                }
            }
            RedirectToAction("login", "user");
            return null;
            
           
           
        }
        public ActionResult partialTable()
        {
            if (checkUser())
            {
                return PartialView("_PartialTable");

            }
            return RedirectToAction("Login", "user");
        }
        public int checkTableExist(string tableName)
        {
            if (checkUser())
            {
                ManageDatabase m = new ManageDatabase();
                SqlConnection conn = new SqlConnection(m.getConnectionString(databaseName));
                conn.Open();
                try
                {
                    List<string> tables = new List<string>();
                    DataTable dt = conn.GetSchema("Tables");

                    foreach (DataRow item in dt.Rows)
                    {
                        string tablename = (string)item[2];
                        if (tablename != "form")
                        {
                            tables.Add(tablename);
                        }
                    }
                    foreach (var item in tables)
                    {
                        if (item == tableName)
                        {
                            conn.Close();
                            return 0;
                        }
                    }
                    conn.Close();
                    return 1;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            RedirectToAction("login", "user");
            return 0;



        }
        public ActionResult addTableName(string tableName)
        {
            if (checkUser())
            {
                try
                {
                    cFlag = 0;
                    tName = tableName;
                    tableNAME = tableName;
                    return PartialView("_PartialColumns", columns);
                }
                catch (Exception)
                {
                    return RedirectToAction("index", "EditDatabase");
                }

            }
            return RedirectToAction("Login", "user");

        }
        public int checkColumnExit(string columnName)
        {
            if(checkUser() && columnName != null)
            {
                foreach (var item in columns)
                {
                    if (item.name == columnName)
                        return 0;
                }
            }

            return 1;
        }
       
        private List<Column> getColumns()
        {
            if (checkUser())
            {
                try
                {
                    ManageDatabase m = new ManageDatabase();
                    SqlConnection conn = new SqlConnection(m.getConnectionString(databaseName));
                    conn.Open();
                    List<Column> columns = new List<Column>();
                    SqlCommand sqlcom = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName", conn);
                    sqlcom.Parameters.AddWithValue("@tableName", tableNAME);
                    sqlcom.ExecuteNonQuery();
                    SqlDataReader drr1;
                    int i = 0;
                    drr1 = sqlcom.ExecuteReader();
                    while (drr1.Read())
                    {
                        if (drr1["COLUMN_NAME"].ToString() != tableNAME + "id")
                            columns.Add(new Column { id = ++i, dataType = drr1["DATA_TYPE"].ToString(), name = drr1["COLUMN_NAME"].ToString(), maxLength = Convert.ToInt32(Convert.IsDBNull(drr1["CHARACTER_MAXIMUM_LENGTH"]) ? null : (int?)drr1["CHARACTER_MAXIMUM_LENGTH"]) });
                    }
                    drr1.Close();
                    
                    string sqlquery1 = "select * from dataType" + tableNAME;
                    SqlCommand sqlcom1 = new SqlCommand(sqlquery1, conn);
                    sqlcom1.ExecuteNonQuery();
                    SqlDataReader drr2;
                    drr2= sqlcom1.ExecuteReader();
                    for (int j = 0; j < columns.Count; j++)
                    {
                        while (drr2.Read())
                        {
                            if (drr2["fieldName"].ToString() == columns[i].name)
                            {
                                columns[i].dataType = drr2["dataType"].ToString();
                                break;
                            }
                        }
                    }
                    drr2.Close();
                    conn.Close();
                    return columns;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            RedirectToAction("Login", "user");
            return null;

        }
        public ActionResult addColumns(string fieldName, string dataType, int required, int minLength, int maxLength, int fKey)
        {
            if (checkUser())
            {
                try
                {
                    if (cFlag == 0)
                    {
                        columns.Add(new Column { id = columnId++, name = fieldName, dataType = dataType, required = required, minLength = minLength, maxLength = maxLength });
                        return PartialView("_PartialColumns", columns);
                    }
                    else
                    {
                        Column column = new Column();
                        column.dataType = dataType;
                        column.id = 1;
                        column.name = fieldName;
                        column.minLength = minLength;
                        column.maxLength = maxLength;
                        column.required = column.required;

                        ManageDatabase m = new ManageDatabase();
                        m.makeDatabaseConnection(m.getConnectionString(databaseName));
                        m.openConnection();
                        m.addColumnToTable(tableNAME, column);
                        m.addValueIntoDataTypeTable("dataType"+tableNAME, column);
                        m.closeConnection();
                        return PartialView("_PartialColumns", getColumns());
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("index", "EditDatabase");
                }
            }
            return RedirectToAction("Login", "User");

        }

        public ActionResult saveTable()
        {
            if (checkUser())
            {
                try
                {
                    ManageDatabase m = new ManageDatabase();
                    m.makeDatabaseConnection(m.getConnectionString(databaseName));
                    m.openConnection();
                    m.createTable(tName, columns);
                    columns.Clear();
                    m.closeConnection();
                    return PartialView("_partialSlideBar", getAllTables(databaseName));
                }
                catch (Exception)
                {
                    return RedirectToAction("index", "EditDatabase");
                }

            }
            return RedirectToAction("login", "user");

        }
        public ActionResult viewColumnPartial(int tableId, string tableName)
        {
            if (checkUser())
            {
                try
                {
                    ManageDatabase m = new ManageDatabase();
                    cFlag = 1;
                    tableid = tableId;
                    tableNAME = tableName;
                    List<Tables> t = getAllTables(databaseName);
                    var table = t.Where(e => e.id == tableId).FirstOrDefault();
                    SqlConnection conn = new SqlConnection(m.getConnectionString(databaseName));
                    conn.Open();
                    string sqlquery1 = "select * from dataType" + tableName;
                    SqlCommand sqlcom1 = new SqlCommand(sqlquery1, conn);
                    sqlcom1.ExecuteNonQuery();
                    SqlDataReader drr2;
                    drr2 = sqlcom1.ExecuteReader();
                    for (int j = 0; j < table.column.Count; j++)
                    {
                        while (drr2.Read())
                        {
                            if (drr2["fieldName"].ToString() == table.column[j].name)
                            {
                                table.column[j].dataType = drr2["dataType"].ToString();
                                break;
                            }
                        }
                    }
                    drr2.Close();
                    conn.Close();
                    return PartialView("_PartialColumns", table.column);
                }
                catch (Exception)
                {
                    return RedirectToAction("index", "EditDatabase");
                }
            }
            return RedirectToAction("login", "user");
        }
        public List<string> deleteColumn(string columnName)
        {
            if (checkUser())
            {
                try
                {
                    formIds.Clear();
                    List<string> f = new List<string>();
                    ManageDatabase m = new ManageDatabase();
                    m.makeDatabaseConnection(m.getConnectionString(databaseName));
                    m.openConnection();
                    m.deleteColumn(columnName, tableNAME);
                    //m.deleteEditDataTypeColumn(columnName, "dataType" + tableNAME);
                    List<Form> forms = m.getForm(tableNAME);
                    m.closeConnection();
                    foreach (var item in forms)
                    {
                        f.Add(item.formString);
                        formIds.Add(item.formId);
                    }

                    return f;
                }
                catch (Exception)
                {
                    RedirectToAction("index", "EditDatabase");
                    return null;
                }
            }
            RedirectToAction("login", "user");
            return null;


        }
        public ActionResult saveUpdateForm(string formString)
        {
            if (checkUser())
            {
                try
                {
                    ManageDatabase m = new ManageDatabase();
                    m.makeDatabaseConnection(m.getConnectionString(databaseName));
                    m.openConnection();
                    m.saveUpdateForm(formString, tableNAME, formIds[0]);
                    formIds.Remove(formIds[0]);

                    m.closeConnection();
                    return PartialView("_PartialColumns", getColumns());
                }
                catch (Exception)
                {
                    return RedirectToAction("index", "EditDatabase");
                }
            }
            return RedirectToAction("login", "user");

        }
        public ActionResult getAllColumnsofTables()
        {
            return PartialView("_PartialColumns", getColumns());
        }
        public ActionResult updateColumn(string columnName)
        {
            if (checkUser())
            {
                try
                {
                    ManageDatabase m = new ManageDatabase();
                    m.makeDatabaseConnection(m.getConnectionString(databaseName));
                    m.openConnection();
                    Column c = m.getColumn(columnName, tableNAME);
                    m.closeConnection();
                    return PartialView("_PartialEditColumn", c);
                }
                catch (Exception)
                {
                    return RedirectToAction("index", "EditDatabase");
                }

            }
            return RedirectToAction("login", "user");

        }
        public int checkEditColumnExist(string columnName)
        {
            if (checkUser())
            {
                try
                {
                    int flag;
                    ManageDatabase m = new ManageDatabase();
                    m.makeDatabaseConnection(m.getConnectionString(databaseName));
                    m.openConnection();
                    flag = m.checkColumnExist(columnName, tableNAME);
                    m.closeConnection();
                    return flag;
                }
                catch (Exception)
                {

                }
            }
             RedirectToAction("login", "user");
            return 0;

        }

        public List<string> editColumn(string columnName, string fieldName, string dataType, int required, int minLength, int maxLength, int fKey)
        {
            if (checkUser())
            {
                try
                {
                    formIds.Clear();
                    ManageDatabase m = new ManageDatabase();
                    m.makeDatabaseConnection(m.getConnectionString(databaseName));
                    m.openConnection();
                    m.editDatabasesColumn(columnName, fieldName, dataType, required, maxLength, tableNAME);
                    m.editDataTypeColumn(columnName, fieldName,dataType, tableNAME);
                    List<Form> forms = m.getForm(tableNAME);
                    List<string> f = new List<string>();
                    m.closeConnection();
                    foreach (var item in forms)
                    {
                        f.Add(item.formString);
                        formIds.Add(item.formId);
                    }
                    return f;
                }
                catch (Exception)
                {
                    RedirectToAction("index", "EditDatabase");
                }
            }
            RedirectToAction("login", "user");
            return null;

        }
        public ActionResult deleteTableFromDB(string tableName)
        {
            if (checkUser())
            {
                try
                {
                    ManageDatabase m = new ManageDatabase();
                    m.makeDatabaseConnection(m.getConnectionString(databaseName));
                    m.openConnection();
                    m.deleteTable(tableName);
                    m.deleteDataTypeTable("dataType" + tableName);
                    m.closeConnection();
                    return PartialView("_partialSlideBar", getAllTables(databaseName));
                }
                catch (Exception)
                {

                }
            }
            return RedirectToAction("login", "user");

        }
        public ActionResult relation()
        {
            if (checkUser())
            {
                try
                {
                    tableRelation tableRelation = new tableRelation();
                    List<Tables> tables = getAllTables(databaseName);
                    ViewBag.DatbaseName = databaseName;
                    List<Tables> t = new List<Tables>();
                    foreach (var item in tables)
                    {
                        if (item.tableName != "relation")
                        {
                            t.Add(item);
                        }
                    }
                    tableRelation.tables = t;
                    ManageDatabase m = new ManageDatabase();
                    m.makeDatabaseConnection(m.getConnectionString(databaseName));
                    m.openConnection();
                    tableRelation.relation = m.getAllRelation();
                    m.closeConnection();
                    return View(tableRelation);
                }
                catch (Exception)
                {
                    RedirectToAction("index", "EditDatabase");
                }
            }
            return RedirectToAction("login", "user");

        }
        public ActionResult MakeRelation(string pKey, string fKey, string relation)
        {
            if (checkUser())
            {
                try
                {
                    int flag = 0;
                    tableRelation tableRelation = new tableRelation();
                    List<Relation> relations = new List<Relation>();
                    ManageDatabase m = new ManageDatabase();
                    m.makeDatabaseConnection(m.getConnectionString(databaseName));
                    m.openConnection();

                    relations = m.getAllRelation();
                    foreach (var item in relations)
                    {
                        if (item.pTableName == pKey && item.fTableName == fKey)
                        {
                            flag = 1;
                            ViewBag.errorMessage = "Relation Already Exits";
                        }
                    }
                    if (flag == 0)
                    {
                        ViewBag.errorMessage = "";
                        m.makeSingleRelation(pKey, fKey, relation);
                    }
                    relations = m.getAllRelation();
                    tableRelation.relation = relations;
                    tableRelation.tables = getAllTables(databaseName);
                    m.closeConnection();
                    return PartialView("_partialRelation", tableRelation);
                }
                catch (Exception)
                {
                    ViewBag.errorMessage = "Relation Already Exits";
                    RedirectToAction("index", "EditDatabase");
                }
            }
            return RedirectToAction("login", "user");

        }
        public ActionResult getForeignKeyList(string pkey)
        {
            if (checkUser())
            {
                try
                {
                    List<Tables> t = getAllTables(databaseName);
                    List<Tables> tables = new List<Tables>();
                    foreach (var item in t)
                    {
                        if (item.tableName != pkey)
                        {
                            tables.Add(item);
                        }
                    }
                    return PartialView("_PartialForeignKey", tables.ToList());
                }
                catch (Exception)
                {
                    RedirectToAction("relation", "user");
                }

            }
            return RedirectToAction("login", "user");

        }

    }
}