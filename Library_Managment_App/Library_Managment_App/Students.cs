using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library_Managment_App
{
    public class Students : IStudents
    {
        SqlConnection con = new SqlConnection("server=IN-333K9S3;database=Library_Managment_App;Integrated Security = true");
        public int Add_Student()
        {
            string query = $"select * from students";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            var row = ds.Tables[0].NewRow();

            string name = AnsiConsole.Ask<string>("[yellow]Enter Student Name:[/]");
            string department = AnsiConsole.Ask<string>("[yellow]Enter Student Department:[/]");
            row["Name"] = name;
            row["Department"] = department;
            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            int res = adapter.Update(ds);

            AnsiConsole.MarkupLine($"[green]Student Added Successfully[/]");
            return res;
        }

        public int update_Student()
        {
            int roll = AnsiConsole.Ask<int>("[yellow]Enter the Student Roll Number you want to update:[/]");
            string query = $"select * from students where Roll_Number = {roll}";
            SqlDataAdapter adapter = new SqlDataAdapter( query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            int recordCount = ds.Tables[0].Rows.Count;
            if (recordCount > 0)
            {
                string name = AnsiConsole.Ask<string>("[yellow]Enter Student Name:[/]");
                string department = AnsiConsole.Ask<string>("[yellow]Enter Student Department:[/]");

                ds.Tables[0].Rows[0]["Name"] = name;
                ds.Tables[0].Rows[0]["Department"] = department;
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ds);
                AnsiConsole.MarkupLine($"[green]Student Updated Successfully!![/]");

                return recordCount;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Student Not Found With the given Roll Number[/]");
                return recordCount;
            }
        }

        public int Delete_Student()
        {
            int roll = AnsiConsole.Ask<int>("[yellow]Enter the Student Roll Number you want to Delete:[/]");
            string query = $"select * from students where Roll_Number = {roll}";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            int recordCount = ds.Tables[0].Rows.Count;
            if (recordCount > 0)
            {
                ds.Tables[0].Rows[0].Delete();
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ds);
                AnsiConsole.MarkupLine($"[green]Student Deleted Successfully!![/]");
                return recordCount;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Student Not Found With the given Roll Number[/]");
                return recordCount;
            }
        }

        public int View_Students()
        {
            string query = $"select * from students";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            int res = adapter.Fill(ds);

            var table = new Table();
            table.AddColumn("Roll Number");
            table.AddColumn("Name");
            table.AddColumn("Department");
            table.Title("[underline rgb(131,111,255)]STUDENTS DETAILS[/]");
            table.BorderColor(Color.LightSlateGrey);
            foreach (var column in table.Columns)
            {
                column.Centered();
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString());
            }
            AnsiConsole.Write(table);
            return res;
        }

        public int View_Student()
        {
            int roll = AnsiConsole.Ask<int>("[yellow]Enter the Student Roll Number you want to get:[/]");
            string query = $"select * from students where Roll_Number = {roll}";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            int res = adapter.Fill(ds);

            var table = new Table();
            table.AddColumn("Roll Number");
            table.AddColumn("Name");
            table.AddColumn("Department");
            table.Title("[underline rgb(131,111,255)]STUDENT DETAILS[/]");
            table.BorderColor(Color.LightSlateGrey);
            foreach (var column in table.Columns)
            {
                column.Centered();
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[i].Rows[0][2].ToString());
            }
            AnsiConsole.Write(table);
            return res;
        }

        public int Students_Havinng_Books()
        {
            string query = "select students.Roll_Number,students.Name,books.Title,books.Author,books.Publication from issuebook join students on issuebook.Roll_Number = students.Roll_Number join books on issuebook.Book_ID = books.Book_ID";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            int res = adapter.Fill(ds);
            var table = new Table();
            table.AddColumn("Roll Number");
            table.AddColumn("Student Name");
            table.AddColumn("Title");
            table.AddColumn("Author");
            table.AddColumn("Publication");
            table.Title("[underline rgb(131,111,255)]STUDENTS WHO TAKEN BOOKS[/]");
            table.BorderColor(Color.LightSlateGrey);
            foreach (var column in table.Columns)
            {
                column.Centered();
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][4].ToString());
            }

            AnsiConsole.Write(table);
            Console.WriteLine();
            AnsiConsole.MarkupLine($"[DeepSkyBlue2]Number of students having books =[/] [underline green]{ds.Tables[0].Rows.Count}[/]");
            Console.WriteLine();

            return res;
        }
    }
}
