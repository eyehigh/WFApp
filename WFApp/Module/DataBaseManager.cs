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
        /// <summary>
        /// 데이터베이스 연결
        /// </summary>
        /// <returns></returns>
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
                LogManager.ShowMessage(ex.Message);
                result = false; 
            }
            return result;
        }
        /// <summary>
        /// 쿼리를 수행할 커맨드 생성
        /// </summary>
        /// <returns></returns>
        public bool NewCommand()
        {
            bool result = false;
            try
            {
                command = conn.CreateCommand();
                result= true;
            }
            catch (Exception ex)
            {
                LogManager.ShowMessage(ex.Message +"\nNewCommand Exception");
                result= false;
            }
            return result;
        }



        public bool BeginTrans()
        {
            try
            {
                Transaction = conn.BeginTransaction();
            }
            catch(Exception ex)
            {
                LogManager.ShowMessage(ex.Message + "\nBeginTrans Exception");
                return false;
            }
            
            return true;
        }
        public bool Query(string sql)
        {
            
            command.CommandText = sql;
            command.ExecuteNonQuery();
            return true;
        }
        /// <summary>
        /// 트랙잭션을 사용하여 1회성 쿼리
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool Query_TR(string sql)
        {

            NewCommand();
            try
            {
                BeginTrans();             //트랜잭션 시작 
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                LogManager.ShowMessage(ex.Message + "\nQuery_TR : "+sql);
                RollBack();
                return false;
            }
            CommitTrans(); //커밋
            return true;
        }
        public bool TableExistCheck(string Table_Name)
        {
            command.CommandText = "SELECT COUNT(*) FROM "+Table_Name;
            try
            {
                command.ExecuteScalar();
            }
            catch (Exception ex) 
            {
                return false;
            }
            return true;
        }
        public DataTable SelectDT(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, conn);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                LogManager.ShowMessage(ex.Message + "\nSelectDT : " + sql);
                dt.Dispose();
                return dt;
            }
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
