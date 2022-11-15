using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace WFApp.Module
{
    internal class DataBaseManager
    {
        
    }
    public class SqliteDB
    {
        //=========================================
        //CONST
        const string SQLITE_DB_PATH = @"C:\SideProject\mydb.db";

        //=========================================
        //PRIVATE
       
        private SQLiteConnection conn = null;
        private SQLiteConnectionStringBuilder connSB = null;
        private SQLiteTransaction Transaction = null;
        private SQLiteCommand command = null;
        //=========================================

        public SqliteDB()
        {
            connSB = new SQLiteConnectionStringBuilder();
            connSB.DataSource = SQLITE_DB_PATH;

            
        }
         ~SqliteDB()
        {
            conn.Close();
        }
        public bool Connect()
        {
            bool result = false;
            conn = new SQLiteConnection(connSB.ConnectionString);
            try
            {
                conn.Open();
                result = true;
            }
            catch (Exception ex)
            {
                result = false; 
            }
            return result;
        }
        public bool BeginTrans()
        {
            Transaction = conn.BeginTransaction();
            return true;
        }
        public bool SimpleQuery(string sql)
        {
            command = conn.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
            return true;
        }
        public DataTable SelectDT(string sql)
        {
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, conn);
            adapter.Fill(dt);
            return dt;
        }
        public bool CommitTrans()
        {
            Transaction.Commit();
            return true;
        }
        public bool RollBack()
        {
            Transaction.Rollback();
            return true;
        }
    }
}
