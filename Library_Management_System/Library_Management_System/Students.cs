using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Spectre.Console;

namespace Library_Management_System
{
    public class Students : IStudents
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=Library_Management_System;Integrated Security = true");
            con.Open();
            return con;
        }
        public void Add_Student()
        {
            SqlConnection con = GetConnection();
            string query = $"insert into Students values(@Student_ID,@Student_Name,@Book_Issued)";
            SqlCommand cmd = new SqlCommand(query, con);

            int student_id = AnsiConsole.Ask<int>("[yellow]Enter Student ID:[/]");
            string student_name = AnsiConsole.Ask<string>("[yellow]Enter Student Name:[/]");
            string book_issued = AnsiConsole.Ask<string>("[yellow]Enter Status of Book Issued(yes or no):[/]");

            cmd.Parameters.AddWithValue("@Student_ID", student_id);
            cmd.Parameters.AddWithValue("@Student_Name", student_name);
            cmd.Parameters.AddWithValue("@Book_Issued", book_issued);
            cmd.ExecuteNonQuery();

            AnsiConsole.MarkupLine("[rgb(124,211,76)]Student Added Successfully!![/]");

            con.Close();
        }

        public void update_Student_By_ID()
        {
            SqlConnection con = GetConnection();
            int id = AnsiConsole.Ask<int>("[yellow]Enter the Student ID you want to update:[/]");
            string query = $"SELECT COUNT(*) FROM Students where Student_ID = {id}";
            SqlCommand count = new SqlCommand(query, con);
            int recordCount = (int)count.ExecuteScalar();

            if (recordCount > 0)
            {
                string updatequery = $"update Students set Student_Name=@Student_Name,Book_Issued=@Book_Issued where Student_ID = {id}";
                SqlCommand cmd = new SqlCommand(updatequery, con);
                string Student_Name = AnsiConsole.Ask<string>("[yellow]Enter Updated Student Name:[/]");
                string Book_Issued = AnsiConsole.Ask<string>("[yellow]Enter Updated Book_Issued_Status:[/]");
                cmd.Parameters.AddWithValue("@Student_Name", Student_Name);
                cmd.Parameters.AddWithValue("@Book_Issued", Book_Issued);
                cmd.ExecuteNonQuery();
                AnsiConsole.MarkupLine("[rgb(124,211,76)]Student Updated Sucessfully [/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Record not found with the provided ID.[/]");
            }
            con.Close();
        }

        public void Delete_Student_By_ID()
        {
            SqlConnection con = GetConnection();
            int id = AnsiConsole.Ask<int>("[yellow]Enter the Student id you want to Delete:[/]");
            string query = $"SELECT COUNT(*) FROM Students where Student_ID = {id}";
            SqlCommand count = new SqlCommand(query, con);
            int recordCount = (int)count.ExecuteScalar();

            if (recordCount > 0)
            {
                string deletequery = $"delete from Students where Student_ID = {id}";
                SqlCommand cmd = new SqlCommand(deletequery, con);
                cmd.ExecuteNonQuery();
                AnsiConsole.MarkupLine("[rgb(124,211,76)]Student Deleted Sucessfully [/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Record not found with the provided ID.[/]");
            }

            con.Close();
        }

        public void Search_Student_Based_On_Student_ID()
        {
            string id = AnsiConsole.Ask<string>("[yellow]Enter Student ID you Want to get: [/]");
            string query = $"select * from Students where Student_ID = {id}";
            SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            var table = new Table();
            table.AddColumn("Student ID");
            table.AddColumn("Student Name");
            table.AddColumn("Book Issued");
            table.Title("[underline rgb(131,111,255)]STUDENT DETAILS[/]");
            table.BorderColor(Color.LightSlateGrey);
            foreach (var column in table.Columns)
            {
                column.Centered();
            }

            while (reader.Read())
            {
                table.AddRow(reader["Student_ID"].ToString(), reader["Student_Name"].ToString(), reader["Book_Issued"].ToString());
            }

            AnsiConsole.Write(table);
            con.Close();
        }

        public void View_All_Students()
        {
            SqlConnection con = GetConnection();
            string query = "select * from Students";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            var table = new Table();
            table.AddColumn("Student ID");
            table.AddColumn("Student Name");
            table.AddColumn("Book Taken(YES/NO)");
            table.Title("[underline rgb(131,111,255)]STUDENTS DETAILS[/]");
            table.BorderColor(Color.LightSlateGrey);
            foreach (var column in table.Columns)
            {
                column.Centered();
            }

            while (reader.Read())
            {
                table.AddRow(reader["Student_ID"].ToString(), reader["Student_Name"].ToString(), reader["Book_Issued"].ToString());
            }

            AnsiConsole.Write(table);
            con.Close();
        }

        public void Students_Havinng_Books()
        {
            SqlConnection con = GetConnection();
            string query = "select Students.Student_ID,Students.Student_Name,Books.Title,Books.Author,Books.Publication from Books join Students on Books.Student_ID = Students.Student_ID";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            var table = new Table();
            table.AddColumn("Student ID");
            table.AddColumn("Student Name");
            table.AddColumn("Title");
            table.AddColumn("Author");
            table.AddColumn("Publication");
            table.Title("[underline rgb(131,111,255)]STUDENTS DETAILS TAKEN BOOKS[/]");
            table.BorderColor(Color.LightSlateGrey);
            foreach (var column in table.Columns)
            {
                column.Centered();
            }

            while (reader.Read())
            {
                table.AddRow(reader["Student_ID"].ToString(), reader["Student_Name"].ToString(), reader["Title"].ToString(), reader["Author"].ToString(), reader["Publication"].ToString());
            }

            AnsiConsole.Write(table);
            con.Close();
        }
    }
}
