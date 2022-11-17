using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace WFApp.Data
{
    internal static class Model
    {
        public static List<Employee> lst_emp = new List<Employee>();
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Join_date { get; set; }
        public Employee()
        {
            this.Name = null;
            this.Department = null;
            this.Position = null;
            this.Join_date = null;
        }
        public Employee(string name, string department, string position, string join_data) 
        { 
            this.Name = name;
            this.Department = department;
            this.Position = position;
            this.Join_date = join_data;
        }
        public void SET(int id, string name, string department, string position, string join_data)
        {
            this.Id = id;
            this.Name = name;
            this.Department = department;
            this.Position = position;
            this.Join_date = join_data;
        }
        
        public static void DtToLst(DataTable dt)
        {
            if(dt!=null && dt.Rows.Count>0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    Employee emp = new Employee();
                    emp.SET(
                        Int32.Parse( dr["ID"].ToString() ),
                        dr["Name"].ToString(),
                        dr["Department"].ToString(),
                        dr["Position"].ToString(),
                        dr["Join_Date"].ToString()
                        );
                        Model.lst_emp.Add( emp );
                }
            }
        }
        public string GET_INSERT_SQL()
        {
            StringBuilder sb= new StringBuilder();
            sb.Append("INSERT INTO  employee (name, department, position, join_date)");
            sb.Append("VALUES(");
            sb.Append("'" + Name + "',");
            sb.Append("'" + Department + "',");
            sb.Append("'" + Position + "',");
            sb.Append("'" + Join_date + "'");
            sb.Append(");");


            return sb.ToString();
        }
        //===========================
        public const string SQL_IF_EXIST_EMP_TABLE = @"SELECT COUNT(*) FROM employee;";

        public const string SQL_CREATE_EMP_TABLE = @"
                      CREATE TABLE employee 
                    (
                        id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        name VARCHAR(255), 
                        department VARCHAR(255), 
                        position VARCHAR(255),
                        join_date DATE
                    );
                ";
        public const string SQL_SELECT_ALL = @"SELECT * FROM employee;";
    }
}
