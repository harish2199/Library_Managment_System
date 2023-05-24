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
    public class Books : IBooks
    {
        SqlConnection con = new SqlConnection("server=IN-333K9S3;database=Library_Managment_App;Integrated Security = true");
        public int Add_Book()
        {
            string query = $"select * from books";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            var row = ds.Tables[0].NewRow();

            string title = AnsiConsole.Ask<string>("[yellow]Enter Book Title:[/]");
            string author = AnsiConsole.Ask<string>("[yellow]Enter Book Author:[/]");
            string publication = AnsiConsole.Ask<string>("[yellow]Enter Publication:[/]");
            int quantity = AnsiConsole.Ask<int>("[yellow]Enter Quantity of Books Available:[/]");

            row["Title"] = title;
            row["Author"] = author;
            row["Publication"] = publication;
            row["Quantity"] = quantity;
            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            int res = adapter.Update(ds);

            AnsiConsole.MarkupLine($"[green]Book Added Successfully[/]");
            return res;
        }

        public int Update_Book()
        {
            int id = AnsiConsole.Ask<int>("[yellow]Enter the Book ID you want to Delete:[/]");
            string query = $"select * from books where Book_ID = {id}";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            int recordCount = ds.Tables[0].Rows.Count;
            if (recordCount > 0)
            {
                string title = AnsiConsole.Ask<string>("[yellow]Enter Updated Book Title:[/]");
                string author = AnsiConsole.Ask<string>("[yellow]Enter Updated Book Author:[/]");
                string publication = AnsiConsole.Ask<string>("[yellow]Enter Updated Publication:[/]");
                int quantity = AnsiConsole.Ask<int>("[yellow]Enter Updated Quantity of Books Available:[/]");

                ds.Tables[0].Rows[0]["Title"] = title;
                ds.Tables[0].Rows[0]["Author"] = author;
                ds.Tables[0].Rows[0]["Publication"] = publication;
                ds.Tables[0].Rows[0]["Quantity"] = quantity;
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ds);
                AnsiConsole.MarkupLine($"[green]Book Updated Successfully!![/]");

                return recordCount;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Book Not Found With the given ID[/]");
                return recordCount;
            }
        }

        public int Delete_Book()
        {
            int id = AnsiConsole.Ask<int>("[yellow]Enter the Book ID you want to Delete:[/]");
            string query = $"select * from books where Book_ID = {id}";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            int recordCount = ds.Tables[0].Rows.Count;
            if (recordCount > 0)
            {
                ds.Tables[0].Rows[0].Delete();
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ds);
                AnsiConsole.MarkupLine($"[green]Book Deleted Successfully!![/]");
                return recordCount;
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Book Not Found With the given ID[/]");
                return recordCount;
            }
        }

        public void View_All_Boooks()
        {
            string query = "select * from books";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            var table = new Table();
            table.AddColumn("Book ID");
            table.AddColumn("Title");
            table.AddColumn("Author");
            table.AddColumn("Publication");
            table.AddColumn("Quantity");
            table.Title("[underline rgb(131,111,255)]BOOKS DETAILS[/]");
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
        }

        public void View_Books_Based_On_Author()
        {
            string author = AnsiConsole.Ask<string>("[yellow]Enter Author Name you Want to get: [/]");
            string query = $"select * from books where Author = '{author}'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Title");
            table.AddColumn("Author");
            table.AddColumn("Publication");
            table.AddColumn("Quantity");
            table.Title("[underline rgb(131,111,255)]BOOK DETAILS[/]");
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
        }

        public void Issue_Book()
        {
            int roll = AnsiConsole.Ask<int>("[yellow]Enter Student Roll Number: [/]");

            string studentQuery = $"select * from students where Roll_Number = {roll}";
            SqlDataAdapter studentAdp = new SqlDataAdapter(studentQuery, con);
            DataSet studentds = new DataSet();
            studentAdp.Fill(studentds);

            int studentCount = studentds.Tables[0].Rows.Count;
            if (studentCount == 0)
            {
                AnsiConsole.MarkupLine("[red]Student with the given ID does not exist![/]");
                return;
            }

            string issueQuery = $"select * from issuebook where Roll_Number = {roll}";
            SqlDataAdapter issueAdp = new SqlDataAdapter(issueQuery, con);
            DataSet issueds = new DataSet();
            issueAdp.Fill(issueds);

            int issueRecordCount = issueds.Tables[0].Rows.Count;
            if (issueRecordCount > 0)
            {
                AnsiConsole.MarkupLine("[red]Student has already taken a book! plz return the old one.[/]");
                return;
            }

            int bookId = AnsiConsole.Ask<int>("[yellow]Enter Book ID: [/]");

            string bookQuery = $"select * from books where Book_ID = {bookId}";
            SqlDataAdapter bookAdp = new SqlDataAdapter(bookQuery, con);
            DataSet bookds = new DataSet();
            bookAdp.Fill(bookds);

            int bookRecordCount = bookds.Tables[0].Rows.Count;
            if (bookRecordCount == 0)
            {
                AnsiConsole.MarkupLine("[red]Book with the given ID does not exist![/]");
                return;
            }

            int quantity = (int)bookds.Tables[0].Rows[0]["Quantity"];
            if (quantity > 0)
            {
                bookds.Tables[0].Rows[0]["Quantity"] = quantity - 1;
                SqlCommandBuilder builder = new SqlCommandBuilder(bookAdp);
                bookAdp.Update(bookds);

                DateTime dateTime = DateTime.Now;
                var row = issueds.Tables[0].NewRow();
                row["Roll_Number"] = roll;
                row["Book_ID"] = bookId;
                row["Issue_Date"] = dateTime.Date;
                issueds.Tables[0].Rows.Add(row);

                SqlCommandBuilder builder1 = new SqlCommandBuilder(issueAdp);
                issueAdp.Update(issueds);

                AnsiConsole.MarkupLine("[green]Book issued to the student![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Book with the given ID is not available![/]");
                return;
            }
        }

        public void Return_Book()
        {
            int roll = AnsiConsole.Ask<int>("[yellow]Enter Student Roll Number: [/]");
            string query = $"select * from issuebook where Roll_Number = {roll}";
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            int recordCount = ds.Tables[0].Rows.Count;
            if (recordCount == 0)
            {
                AnsiConsole.MarkupLine("[red]Student has not taken any book![/]");
                return;
            }

            int bookId = (int)ds.Tables[0].Rows[0]["Book_ID"];
            ds.Tables[0].Rows[0].Delete();

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Update(ds);

            AnsiConsole.MarkupLine("[green]Book returned from student[/]");

            string bookQuery = $"select * from books where Book_ID = {bookId}";
            SqlDataAdapter bookAdapter = new SqlDataAdapter(bookQuery, con);
            DataSet bookDataSet = new DataSet();
            bookAdapter.Fill(bookDataSet);

            int quantity = (int)bookDataSet.Tables[0].Rows[0]["Quantity"];
            bookDataSet.Tables[0].Rows[0]["Quantity"] = quantity + 1;

            SqlCommandBuilder builder1 = new SqlCommandBuilder(bookAdapter);
            bookAdapter.Update(bookDataSet);
        }
    }
}

