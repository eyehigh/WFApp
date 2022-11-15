using System.Data;
using WFApp.Module;

namespace WFApp
{
    public partial class Form1 : Form
    {
        SqliteDB sqliteDB;
        public Form1()
        {
            InitializeComponent();
            sqliteDB = new SqliteDB();
            sqliteDB.Connect();
            FUNC1();
            string select = @"SELECT * FROM employee;";
            DataTable dt = sqliteDB.SelectDT(select);
            MessageBox.Show(dt.Rows[0]["name"].ToString());
        }

        public void FUNC1()
        {
            string create_test =
                @"
                      CREATE TABLE employee 
                    (
                        id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        name VARCHAR(255), 
                        department VARCHAR(255), 
                        position VARCHAR(255),
                        join_date DATE
                    );
                ";
            //sqliteDB.SimpleQuery(create_test); // 한번만 실행하고 주석처리할것
            string insert_test =
               @"
                    INSERT INTO  employee 
                     (name, department, position, join_date) 
                    VALUES 
	                ('tony park', 'Data Engineering', 'manager', '2015-02-19');
                ";
            //sqliteDB.SimpleQuery(insert_test); // 한번만 실행하고 주석처리할것
        }
    }
}