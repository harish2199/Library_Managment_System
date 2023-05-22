using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Spectre.Console;
using System.Collections;

namespace Library_Management_System
{
    public class Books
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=Library_Management_System;Integrated Security = true");
            con.Open();
            return con;
        }

        public void Add_Book()
        {
            SqlConnection con = GetConnection();
            string query = $"insert into Books(Title,Author,Publication,Available) values(@Title,@Author,@Publication,@Available)";
            SqlCommand cmd = new SqlCommand(query, con);

            string title = AnsiConsole.Ask<string>("[yellow]Enter Book Title:[/]");
            string author = AnsiConsole.Ask<string>("[yellow]Enter Author Name:[/]");
            string publication = AnsiConsole.Ask<string>("[yellow]Enter Publication:[/]");
            string available = AnsiConsole.Ask<string>("[yellow]Enter Avalability of Book (yes or no):[/]");


            cmd.Parameters.AddWithValue("@Title", title);
            cmd.Parameters.AddWithValue("@Author", author);
            cmd.Parameters.AddWithValue("@Publication", publication);
            cmd.Parameters.AddWithValue("@Available", available);

            cmd.ExecuteNonQuery();

            AnsiConsole.MarkupLine("[green]Book Added Sucessfully [/]");

            con.Close();
        }

        public void update_Book_By_ID()
        {
            SqlConnection con = GetConnection();
            int id = AnsiConsole.Ask<int>("[yellow]Enter the Book ID you want to update:[/]");
            string query = $"SELECT COUNT(*) FROM Books where ID = {id}";
            SqlCommand count = new SqlCommand(query, con);

            int recordCount = (int)count.ExecuteScalar();

            if (recordCount > 0)
            {
                string updatequery = $"update Books set Title=@Title,Author=@Author,Publication=@Publication,Available=@Available where ID = {id}";
                SqlCommand cmd = new SqlCommand(updatequery, con);

                string Title = AnsiConsole.Ask<string>("[yellow]Enter Updated Book Title:[/]");
                string Author = AnsiConsole.Ask<string>("[yellow]Enter Updated Author Name:[/]");
                string Publication = AnsiConsole.Ask<string>("[yellow]Enter Updated Publication:[/]");
                string Available = AnsiConsole.Ask<string>("[yellow]Enter Updated Availability status:[/]");

                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.Parameters.AddWithValue("@Author", Author);
                cmd.Parameters.AddWithValue("@Publication", Publication);
                cmd.Parameters.AddWithValue("@Available", Available);

                cmd.ExecuteNonQuery();

                AnsiConsole.MarkupLine("[rgb(124,211,76)]Book Updated Sucessfully [/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Record not found with the provided ID.[/]");
            }
            con.Close();
        }

        public void Delete_Book_By_ID()
        {
            SqlConnection con = GetConnection();
            int id = AnsiConsole.Ask<int>("[yellow]Enter the Book id you want to Delete:[/]");
            string query = $"SELECT COUNT(*) FROM Books where ID = {id}";
            SqlCommand count = new SqlCommand(query, con);
            int recordCount = (int)count.ExecuteScalar();

            if (recordCount > 0)
            {
                string deletequery = $"delete from Books where ID = {id}";
                SqlCommand cmd = new SqlCommand(deletequery, con);
                cmd.ExecuteNonQuery();
                AnsiConsole.MarkupLine("[green]Book Deleted Sucessfully [/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Record not found with the provided ID.[/]");
            }

            con.Close();
        }

        public void View_All_Boooks()
        {
            SqlConnection con = GetConnection();
            string query = "select * from Books";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Title");
            table.AddColumn("Author");
            table.AddColumn("Publication");
            table.AddColumn("Availability");
            table.Title("[underline rgb(131,111,255)]BOOKS DETAILS[/]");
            table.BorderColor(Color.LightSlateGrey);
            foreach (var column in table.Columns)
            {
                column.Centered();
            }

            while (reader.Read())
            {
                table.AddRow(reader["ID"].ToString(), reader["Title"].ToString(), reader["Author"].ToString(), reader["Publication"].ToString(), reader["Available"].ToString());
            }

            AnsiConsole.Write(table);
            con.Close();
        }

        public void View_Books_Based_On_Author()
        {
            SqlConnection con = GetConnection();
            string author = AnsiConsole.Ask<string>("[yellow]Enter Author Name you Want to get: [/]");
            string query = $"select * from Books where Author = '{author}'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Title");
            table.AddColumn("Author");
            table.AddColumn("Publication");
            table.AddColumn("Available");
            table.Title("[underline rgb(131,111,255)]STUDENT DETAILS[/]");
            table.BorderColor(Color.LightSlateGrey);
            foreach (var column in table.Columns)
            {
                column.Centered();
            }

            while (reader.Read())
            {
                table.AddRow(reader["ID"].ToString(), reader["Title"].ToString(), reader["Author"].ToString(), reader["Publication"].ToString(), reader["Available"].ToString());
            }

            AnsiConsole.Write(table);
            con.Close();

        }

        public void Issue_Book()
        {
            string connectionstring = "server=IN-333K9S3;database=Library_Management_System;Integrated Security = true";
            //Checking if student already taken book or not
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();

                int id = AnsiConsole.Ask<int>("[yellow]Enter the Student ID:[/]");
                string query = $"select * from Students where Student_ID = {id}";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string bookIssued = reader["Book_Issued"].ToString();
                    if (bookIssued.ToLower() == "yes")
                    {
                        AnsiConsole.MarkupLine("[red]Book is already issued!![/]");
                        return;
                    }
                }

                reader.Close();

                //Checking book is available or not
                using (SqlConnection Con = new SqlConnection(connectionstring))
                {
                    Con.Open();
                    int book_id = AnsiConsole.Ask<int>("[yellow]Enter the Book ID you want:[/]");
                    string query1 = $"select * from Books where ID = '{book_id}'";
                    SqlCommand Cmd = new SqlCommand(query1, Con);
                    SqlDataReader reader1 = Cmd.ExecuteReader();
                    while (reader1.Read())
                    {
                        string available = reader1["Available"].ToString();
                        if (available.ToLower() == "no")
                        {
                            AnsiConsole.MarkupLine("[red]Book is already issued to other student!![/]");
                            return;
                        }
                    }

                    reader1.Close();

                
                //Chianging the avalibility status of the book
                using (SqlConnection Con1 = new SqlConnection(connectionstring))
                {
                    Con1.Open();
                    //string title = AnsiConsole.Ask<string>("[yellow]Enter the Book title you want:[/]");
                    string query2 = $"update Books set Available=@Available,Student_ID=@Student_ID where ID = '{book_id}'";
                    SqlCommand cmd1 = new SqlCommand(query2, Con1);
                    cmd1.Parameters.AddWithValue("@Available", "NO");
                    cmd1.Parameters.AddWithValue("@Student_ID", id);
                    cmd1.ExecuteNonQuery();
                }
                //Changing status of the student
                using (SqlConnection Con2 = new SqlConnection(connectionstring))
                {
                    Con2.Open();
                    string query2 = $"update Students set Book_Issued=@Book_Issued where Student_ID = {id}";
                    SqlCommand cmd2 = new SqlCommand(query2, Con2);
                    cmd2.Parameters.AddWithValue("@Book_Issued", "YES");
                    cmd2.ExecuteNonQuery();
                }
                }
                AnsiConsole.MarkupLine("[green]Book is issued to student!![/]");
            }
        }

        public void Return_Book()
        {
            string connectionstring = "server=IN-333K9S3;database=Library_Management_System;Integrated Security = true";

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();

                int id = AnsiConsole.Ask<int>("[yellow]Enter the Student ID:[/]");
                string query = $"select * from Students where Student_ID = {id}";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string bookIssued = reader["Book_Issued"].ToString();
                    if (bookIssued.ToLower() == "no")
                    {
                        AnsiConsole.MarkupLine("[red]Book is not Issued to this student!![/]");
                        return;
                    }
                }

                reader.Close();

                using (SqlConnection Con1 = new SqlConnection(connectionstring))
                {
                    Con1.Open();
                    string query1 = $"update Books set Available=@Available,Student_ID=@Student_ID where Student_ID = {id}";
                    SqlCommand cmd1 = new SqlCommand(query1, Con1);
                    cmd1.Parameters.AddWithValue("@Available", "YES");
                    cmd1.Parameters.AddWithValue("@Student_ID", DBNull.Value);

                    cmd1.ExecuteNonQuery();
                }

                using (SqlConnection Con2 = new SqlConnection(connectionstring))
                {
                    Con2.Open();
                    string query2 = $"update Students set Book_Issued=@Book_Issued where Student_ID = {id}";
                    SqlCommand cmd2 = new SqlCommand(query2, Con2);
                    cmd2.Parameters.AddWithValue("@Book_Issued", "NO");
                    cmd2.ExecuteNonQuery();
                }

                AnsiConsole.MarkupLine("[green]Book is Returned!![/]");

            }
        }
    }

}

