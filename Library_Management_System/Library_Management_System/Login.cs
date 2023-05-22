using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Spectre.Console;

namespace Library_Management_System
{
    public  class Login : ILogin
    {
        public bool LoginUser() 
        {
            AnsiConsole.MarkupLine("[green]Please Login to continue:[/]");
            Console.WriteLine();
            string username = AnsiConsole.Ask<string>("[yellow]Enter User Name:[/]");
            string password = AnsiConsole.Ask<string>("[yellow]Enter Password:[/]");

            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=Library_Management_System;Integrated Security = true");
            con.Open();
            string query = "select count(*) from Login_User where Username = @username and Password = @password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            int count = (int)cmd.ExecuteScalar();

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
            
            con.Close();
        }
    }
}
