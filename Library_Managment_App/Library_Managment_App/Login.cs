using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Managment_App
{
    public class Login : ILogin
    {
        SqlConnection con = new SqlConnection("server=IN-333K9S3;database=Library_Managment_App;Integrated Security = true");
        public bool LoginUser(string username, string password)
        {
            string query = $"select * from login where username = '{username}' and password = '{password}'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            int count = ds.Tables[0].Rows.Count;

            if (count > 0)
            {
                AnsiConsole.MarkupLine("[green]Login successfully!![/]");
                return true;
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid username or password. Please try again.[/]");
                return false;
            }

        }
    }
}
