using SQLitePCL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFApp.Data;
using WFApp.Module;

namespace WFApp
{
    public partial class MainForm : Form
    {
        SqliteDB sqliteDB;
        public MainForm()
        {
            InitializeComponent();

            sqliteDB = new SqliteDB();
            sqliteDB.Connect();
            FUNC2();
            DataTable dt = sqliteDB.SelectDT(Employee.SQL_SELECT_ALL);
            //MessageBox.Show(dt.Rows[0]["name"].ToString());
            Employee.DtToLst(dt);
        }

        public void FUNC1()
        {
            
        }
        public void FUNC2()
        {
            try
            {
                sqliteDB.BeginTrans();
                sqliteDB.NewCommand();

               if ( sqliteDB.TableExistCheck("Employee") == false )
               {
                    sqliteDB.Query(Employee.SQL_CREATE_EMP_TABLE);
               }
               else
                {
                    return;
                }
                

                Employee emp = new Employee();
                emp.SET(0, "Rum", "Command", "", "2018-10-19");
                sqliteDB.Query(emp.GET_INSERT_SQL());

                emp.SET(0, "Jin", "Command", "", "2015-05-02");
                sqliteDB.Query(emp.GET_INSERT_SQL());

                emp.SET(0, "Walker", "Command", "", "2015-05-02");
                sqliteDB.Query(emp.GET_INSERT_SQL());

                emp.SET(0, "Vermouth", "Command", "", "2016-12-25");
                sqliteDB.Query(emp.GET_INSERT_SQL());

                emp.SET(0, "Kir", "Intelligence", "CIA", "2020-08-20");
                sqliteDB.Query(emp.GET_INSERT_SQL());

                emp.SET(0, "Bourbon", "Intelligence", "Police", "2018-09-08");
                sqliteDB.Query(emp.GET_INSERT_SQL());

                emp.SET(0, "Chianti", "Assassination", "Sniper", "2017-04-29");
                sqliteDB.Query(emp.GET_INSERT_SQL());

                emp.SET(0, "Khorne", "Assassination", "Sniper", "2017-05-07");
                sqliteDB.Query(emp.GET_INSERT_SQL());

                sqliteDB.CommitTrans();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                sqliteDB.RollBack();
            }
        }
    }
}
