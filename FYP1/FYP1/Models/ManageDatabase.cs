using FYP1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace FYP1.Models
{
    public class ManageDatabase
    {
        static private SqlConnection connectionString;
        public void createDatabase(string dbName)
        {
            SqlConnection conn = new SqlConnection("Server=Asma-Liaquat\\ASMA;uid = sa; pwd = asma; database = master");
            string sqlQuery = "create database " + dbName;
            conn.Open();
            SqlCommand sqlcom = new SqlCommand(sqlQuery, conn);
            sqlcom.ExecuteNonQuery();
            conn.Close();
        }
        public string getConnectionString(string databaseName)
        {
            return "Server=Asma-Liaquat\\ASMA;uid = sa; pwd = asma; database = " + databaseName;
        }
        public void makeDatabaseConnection(string connString)
        {
            connectionString = new SqlConnection(connString);
        }
        public SqlConnection myConnString() { return connectionString; }
        public void openConnection()
        {
            connectionString.Open();
        }


        public void createTable(string tableName, List<Column> columns)
        {
            string sqlQuery = "create table " + tableName + " (" + tableName + "id int PRIMARY KEY not null identity(1,1)) ";
            SqlCommand sqlcom = new SqlCommand(sqlQuery, connectionString);
            sqlcom.ExecuteNonQuery();
            foreach (var item in columns)
            {
                string required = "null";

                string sql = "alter table " + tableName + " add " + item.name + " varchar(" + item.maxLength + ") " + required;
                SqlCommand sqlcomm = new SqlCommand(sql, connectionString);
                sqlcomm.ExecuteNonQuery();
            }
            createTableDataType(tableName, columns);
        }
        public void createTableDataType(string tableName, List<Column> columns)
        {
            string sqlQuery = "create table dataType" + tableName + "(dataType" + tableName+"id int PRIMARY KEY not null identity(1,1), fieldName varchar(100) , dataType varchar(100))";
            SqlCommand sqlcom = new SqlCommand(sqlQuery, connectionString);
            sqlcom.ExecuteNonQuery();

            insertValueInTable("dataType" + tableName, columns);

        }
        public void insertValueInTable(string tableName, List<Column> columns)
        {
            foreach (var item in columns)
            {
                SqlCommand sql2 = new SqlCommand("insert into " + tableName + "(fieldName,dataType)Values(@fieldName,@dataType)", connectionString);
                sql2.Parameters.AddWithValue("@fieldName", item.name);
                sql2.Parameters.AddWithValue("@dataType", item.dataType);
                sql2.ExecuteNonQuery();
            }
        }
        public void addValueIntoDataTypeTable(string tableName, Column column)
        {
            SqlCommand sql2 = new SqlCommand("insert into " + tableName + "(fieldName,dataType)Values(@fieldName,@dataType)", connectionString);
            sql2.Parameters.AddWithValue("@fieldName", column.name);
            sql2.Parameters.AddWithValue("@dataType", column.dataType);
            sql2.ExecuteNonQuery();
        }
        public void makeRelation(List<Relation> relations)
        {
            foreach (var item in relations)
            {

                if (item.relationType == "one-to-many")
                {

                    string sqlquery = "alter table " + item.fTableName + " add " + item.pTableName + "id int";
                    SqlCommand sqlcom = new SqlCommand(sqlquery, connectionString);
                    sqlcom.ExecuteNonQuery();
                    string sql = "alter table " + item.fTableName + " add FOREIGN KEY(" + item.pTableName + "id) REFERENCES " + item.pTableName + "(" + item.pTableName + "id) ON DELETE CASCADE";
                    SqlCommand sqlcomm = new SqlCommand(sql, connectionString);
                    sqlcomm.ExecuteNonQuery();
                    SqlCommand sql2 = new SqlCommand("insert into dataType" + item.fTableName+ "(fieldName,dataType)Values(@fieldName,@dataType)", connectionString);
                    sql2.Parameters.AddWithValue("@fieldName", item.pTableName+"id");
                    sql2.Parameters.AddWithValue("@dataType", "int");
                    sql2.ExecuteNonQuery();
                }
                else
                {
                    string sqlquery = "alter table " + item.fTableName + " add " + item.pTableName + "id int";
                    SqlCommand sqlcom = new SqlCommand(sqlquery, connectionString);
                    sqlcom.ExecuteNonQuery();
                    string sql = "alter table " + item.fTableName + " add FOREIGN KEY(" + item.pTableName + "id) REFERENCES " + item.pTableName + "(" + item.pTableName + "id) ON DELETE CASCADE";
                    SqlCommand sqlcomm = new SqlCommand(sql, connectionString);
                    sqlcomm.ExecuteNonQuery();
                    string sql1 = "alter table " + item.fTableName + " add  UNIQUE(" + item.pTableName + "id)";
                    SqlCommand sqlcomm1 = new SqlCommand(sql1, connectionString);
                    sqlcomm1.ExecuteNonQuery();
                    SqlCommand sql2 = new SqlCommand("insert into dataType" + item.fTableName + "(fieldName,dataType)Values(@fieldName,@dataType)", connectionString);
                    sql2.Parameters.AddWithValue("@fieldName", item.fTableName + "id");
                    sql2.Parameters.AddWithValue("@dataType", "int");
                    sql2.ExecuteNonQuery();
                }
            }
            
            foreach (var item in relations)
            {
                SqlCommand sql2 = new SqlCommand("insert into relation(primaryTable,foreignTable,relationType)Values(@primaryTable,@foriegnTable,@relationType)", connectionString);          
                sql2.Parameters.AddWithValue("@primaryTable", item.pTableName);
                sql2.Parameters.AddWithValue("@foriegnTable", item.fTableName);
                sql2.Parameters.AddWithValue("@relationType", item.relationType);
                sql2.ExecuteNonQuery();
            }
        }
        public void createFrom()
        {
            string sqlQuery = "create table form(formId int primary key not null identity(1,1) ,tableid int , formString varchar(MAX) not null,formTitle varchar(100) not null,tableName varchar(100) not null)";
            SqlCommand sqlcom = new SqlCommand(sqlQuery, connectionString);
            sqlcom.ExecuteNonQuery();
            string q = "SET IDENTITY_INSERT form ON";
            SqlCommand sqm = new SqlCommand(q, connectionString);
            sqm.ExecuteNonQuery();

        }
        public void createRelationTable()
        {
            string sqlQuery = "create table relation(relationId int primary key not null identity(1,1) ,primaryTable varChar(200) , foreignTable varchar(200),relationType varchar(200))";
            SqlCommand sqlcom1 = new SqlCommand(sqlQuery, connectionString);
            sqlcom1.ExecuteNonQuery();  
        }
        //public void saveRelations(List<Form> fList)
        //{

        //    foreach (var item in fList)
        //    {
        //        SqlCommand sql = new SqlCommand("insert into form(tableid,formString,formTitle,tableName)Values(@tableid,@formString,@formTitle,@tableName)", connectionString);
        //        //  sql.Parameters.AddWithValue("@formId", item.formId);
        //        sql.Parameters.AddWithValue("@tableid", item.tableid);
        //        sql.Parameters.AddWithValue("@formString", item.formString);
        //        sql.Parameters.AddWithValue("@formTitle", item.formTitle);
        //        sql.Parameters.AddWithValue("@tableName", item.tableName);
        //        sql.ExecuteNonQuery();
        //    }

        //}

        public void saveForm(List<Form> fList)
        {

            foreach (var item in fList)
            {
                SqlCommand sql = new SqlCommand("insert into form(tableid,formString,formTitle,tableName)Values(@tableid,@formString,@formTitle,@tableName)", connectionString);
                //  sql.Parameters.AddWithValue("@formId", item.formId);
                sql.Parameters.AddWithValue("@tableid", item.tableid);
                sql.Parameters.AddWithValue("@formString", item.formString);
                sql.Parameters.AddWithValue("@formTitle", item.formTitle);
                sql.Parameters.AddWithValue("@tableName", item.tableName);
                sql.ExecuteNonQuery();
            }

        }
        /*  public void createUserApplication(userApplication uApplication)
        {
            string sqlQuery = "create table userApplication(id int primary key ,applicationName varchar(100) not null,password varchar(100) not null,logo varchar(MAX) not null)";
            SqlCommand sqlcom = new SqlCommand(sqlQuery, connectionString);
            sqlcom.ExecuteNonQuery();
            SqlCommand sql = new SqlCommand("insert into userApplication(id,applicationName,password,logo)Values(@id,@name,@password,@logo)", connectionString);
            sql.Parameters.AddWithValue("@id", uApplication.id);
            sql.Parameters.AddWithValue("@name", uApplication.applicationName);
            sql.Parameters.AddWithValue("@password", uApplication.password);
            sql.Parameters.AddWithValue("@logo", uApplication.logo);
            sql.ExecuteNonQuery();
        }*/
        public void addColumnToTable(string tableName, Column column)
        {

            
            string sql = "alter table " + tableName + " add " + column.name + " varchar(" + column.maxLength + ") null" ;
            SqlCommand sqlcomm = new SqlCommand(sql, connectionString);
            sqlcomm.ExecuteNonQuery();
        }
        public void deleteColumn(string columnName, string tableName)
        {
            string datTable = "dataType" + tableName;

            string sql = "alter table " + tableName + " DROP COLUMN " + columnName ;
            string sql1 = "delete from "+datTable+" where fieldName = '"+columnName+"'";
            SqlCommand sqlcomm = new SqlCommand(sql, connectionString);
            SqlCommand sql1comm = new SqlCommand(sql1, connectionString);
            sqlcomm.ExecuteNonQuery();
            sql1comm.ExecuteNonQuery();
            //deleteEditDataTypeColumn(columnName, "dataType" + tableName);

        }
        public void deleteEditDataTypeColumn(string columnName, string tableName) 
        {
            string sql1 = "delete from Exam where fieldName = date";
            //string sql1 = "delete from "+tableName+" where fieldName = "+columnName;
            SqlCommand sqlcomm1 = new SqlCommand(sql1, connectionString);
            sqlcomm1.ExecuteNonQuery();
        }
        public List<Form> getForm(string tableName)
        {
            List<Form> forms = new List<Form>();
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM form WHERE tableName = @tableName", connectionString);
            sqlcom.Parameters.AddWithValue("@tableName", tableName);
            sqlcom.ExecuteNonQuery();
            SqlDataReader drr1;
            drr1 = sqlcom.ExecuteReader();
            while (drr1.Read())
            {
                forms.Add(new Form { formId = Int16.Parse(drr1["formId"].ToString()), formString = drr1["formString"].ToString(), formTitle = drr1["formTitle"].ToString(), tableName = drr1["tableName"].ToString() });
            }
            drr1.Close();
            return forms;
        }
        public void saveUpdateForm(string formString, string tableName, int formId)
        {
            SqlCommand sqlcom = new SqlCommand("Update form  set formString=@formString where tableName=@tableName and formId=@formId", connectionString);
            sqlcom.Parameters.AddWithValue("@formString", formString);
            sqlcom.Parameters.AddWithValue("@tableName", tableName);
            sqlcom.Parameters.AddWithValue("@formId", formId);
            sqlcom.ExecuteNonQuery();
        }
        public Column getColumn(string columnName, string tableName)
        {
            Column column = new Column();
            SqlCommand sqlcom = new SqlCommand("select * from INFORMATION_SCHEMA.COLUMNS  where TABLE_NAME= @tableName", connectionString);
            sqlcom.Parameters.AddWithValue("@tableName", tableName);
            sqlcom.ExecuteNonQuery();
            SqlDataReader drr1;
            drr1 = sqlcom.ExecuteReader();
            while (drr1.Read())
            {
                if (drr1["COLUMN_NAME"].ToString() == columnName)
                {
                    column.id = Convert.ToInt32(drr1["ORDINAL_POSITION"].ToString());
                    column.name = drr1["COLUMN_NAME"].ToString();
                    column.dataType = drr1["DATA_TYPE"].ToString();
                    column.maxLength = Convert.ToInt32(drr1["CHARACTER_MAXIMUM_LENGTH"].ToString());
                    break;
                }
            }
            drr1.Close();
            return column;

        }
        public int checkColumnExist(string columnName, string tableName)
        {
            Column column = new Column();
            SqlCommand sqlcom = new SqlCommand("select * from INFORMATION_SCHEMA.COLUMNS  where TABLE_NAME= @tableName", connectionString);
            sqlcom.Parameters.AddWithValue("@tableName", tableName);
            sqlcom.ExecuteNonQuery();
            SqlDataReader drr1;
            drr1 = sqlcom.ExecuteReader();
            while (drr1.Read())
            {
                if (drr1["COLUMN_NAME"].ToString() == columnName)
                {
                    return 0;
                }
            }
            return 1;
        }
        public void editDatabasesColumn(string columnName, string fieldName, string dataType, int required, int maxLength, string tableName)
        {
            string require = "null";
            if (required == 1)
            {
                require = "not null";
            }
            else if (required == 0)
            {
                require = "null";
            }
            SqlCommand sql = new SqlCommand("SP_RENAME '" + tableName + "." + columnName + "', @fieldName", connectionString);
            sql.Parameters.AddWithValue("@fieldName", fieldName);
            sql.ExecuteNonQuery();
            SqlCommand sql1 = new SqlCommand("alter Table " + tableName + " alter COLUMN " + fieldName + " VARCHAR(" + maxLength + ")", connectionString);
            sql1.Parameters.AddWithValue("@fieldName", fieldName);
            sql1.ExecuteNonQuery();
           

        }
        public void editDataTypeColumn(string columnName, string fieldName, string dataType, string tableName)
        {
            SqlCommand sqlcom = new SqlCommand("Update dataType" + tableName + " set fieldName=@fieldName, dataType=@dataType where fieldName=@fName", connectionString);
            sqlcom.Parameters.AddWithValue("@fieldName", fieldName);
            sqlcom.Parameters.AddWithValue("@fName", columnName);
            sqlcom.Parameters.AddWithValue("@dataType", dataType);
            sqlcom.ExecuteNonQuery();
        }
        public void deleteForm(int formId)
        {
            string sql = "delete from form where formId=" + formId;
            SqlCommand sqlcomm = new SqlCommand(sql, connectionString);
            sqlcomm.ExecuteNonQuery();
        }
        public void deleteTable(string tableName)
        {
            string sql = "DROP TABLE " + tableName;
            SqlCommand sqlcomm = new SqlCommand(sql, connectionString);
            sqlcomm.ExecuteNonQuery();
            List<Form> forms = getForm(tableName);
            foreach (var item in forms)
            {
                string sql1 = "delete from form where formId=" + item.formId;
                SqlCommand sqlcom = new SqlCommand(sql1, connectionString);
                sqlcom.ExecuteNonQuery();
            }
        }
        public void deleteDataTypeTable(string tableName)
        {
            string sql = "DROP TABLE " + tableName;
            SqlCommand sqlcomm = new SqlCommand(sql, connectionString);
            sqlcomm.ExecuteNonQuery();
        }
        public List<Relation> getAllRelation()
        {
            List<Relation> relations = new List<Relation>();
            string query = "select * from relation";
            SqlCommand sqlcom = new SqlCommand(query, connectionString);
            sqlcom.ExecuteNonQuery();
            SqlDataReader drr;
            drr = sqlcom.ExecuteReader();

            while (drr.Read())
            {
                Relation r = new Relation();
                r.pTableName = drr["primaryTable"].ToString();
                r.fTableName = drr["foreignTable"].ToString();
                r.relationType = drr["relationType"].ToString();
                relations.Add(r);
            }
            drr.Close();
            return relations;
        }
        public void makeSingleRelation(string pKey, string fKey, string relation)
        {
            SqlCommand sql2 = new SqlCommand("insert into relation(primaryTable,foreignTable,relationType)Values(@primaryTable,@foriegnTable,@relationType)", connectionString);
            sql2.Parameters.AddWithValue("@primaryTable", pKey);
            sql2.Parameters.AddWithValue("@foriegnTable", fKey);
            sql2.Parameters.AddWithValue("@relationType", relation);
            sql2.ExecuteNonQuery();
            if (relation == "one-to-many")
            {

                string sqlquery = "alter table " + fKey + " add " + pKey + "id int";
                SqlCommand sqlcom = new SqlCommand(sqlquery, connectionString);
                sqlcom.ExecuteNonQuery();
                string sql = "alter table " + fKey + " add FOREIGN KEY(" + pKey + "id) REFERENCES " + pKey + "(" + pKey + "id)";
                SqlCommand sqlcomm = new SqlCommand(sql, connectionString);
                sqlcomm.ExecuteNonQuery();
            }
            else
            {
                string sqlquery = "alter table " + fKey + " add " + pKey + "id int";
                SqlCommand sqlcom = new SqlCommand(sqlquery, connectionString);
                sqlcom.ExecuteNonQuery();
                string sql = "alter table " + fKey + " add  FOREIGN KEY(" + pKey + "id) REFERENCES " + pKey + "(" + pKey + "id)";
                SqlCommand sqlcomm = new SqlCommand(sql, connectionString);
                sqlcomm.ExecuteNonQuery();
                string sql1 = "alter table " + fKey + " add  UNIQUE(" + pKey + "id)";
                SqlCommand sqlcomm1 = new SqlCommand(sql1, connectionString);
                sqlcomm1.ExecuteNonQuery();
            }

        }
        public void deleteApplication(string dbName)
        {
           
            SqlConnection conn = new SqlConnection("Server=Asma-Liaquat\\ASMA;uid = sa; pwd = asma; database = master");
            conn.Open();
            string sqlQuery = "drop database " + dbName;   
            SqlCommand sqlcom = new SqlCommand(sqlQuery, conn);
            sqlcom.ExecuteNonQuery();
            conn.Close();
            
        }
        public void closeConnection()
        {
            connectionString.Close();
        }
    }
}