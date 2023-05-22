using Library_Management_System;
using System.Data.SqlClient;
using Spectre.Console;
using System.Diagnostics;

namespace Library_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Write(new FigletText("Library Management System").Centered().Color(Color.Red));

            Login login = new Login();
            Students students = new Students();
            Books books = new Books();
            bool Is_Logged_In = login.LoginUser();
            while (!Is_Logged_In)
            {
                Is_Logged_In =login.LoginUser();
            }
            //Console.WriteLine(Is_Logge_In);
            Console.WriteLine();
            var rule = new Rule("[green]WELCOME TO LIBRARY MANAGMENT SYSTEM[/]");
            rule.Style = Style.Parse("red dim");
            AnsiConsole.Write(rule);
            while (Is_Logged_In)
            {
                Console.WriteLine();
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[green]Select your choice :[/]")
                    .AddChoices(new[] {
                        "Add Student",
                        "Update Student By ID",
                        "Delete Student By ID",
                        "Search Student By ID",
                        "Students_Having_Books",
                        "View All Students",
                        "Add Book",
                        "Update Book By ID",
                        "Delete Book By ID",
                        "View All Books",
                        "View Books By Author",
                        "Issue Book",
                        "Return Book"
                   }));

                switch (choice)
                {
                    case "Add Student":
                        {
                            students.Add_Student();
                            break;
                        }
                    case "Update Student By ID":
                        {
                            students.update_Student_By_ID();
                            break;
                        }
                    case "Delete Student By ID":
                        {
                            students.Delete_Student_By_ID();
                            break;
                        }
                    case "View All Students":
                        {
                            students.View_All_Students(); 
                            break;
                        }
                    case "Search Student By ID":
                        {
                            students.Search_Student_Based_On_Student_ID();
                            break;
                        }
                    case "Students_Having_Books":
                        {
                            students.Students_Havinng_Books();
                            break;
                        }
                    case "Add Book":
                        {
                            books.Add_Book();
                            break;
                        }
                    case "Update Book By ID":
                        {
                            books.update_Book_By_ID();
                            break;
                        }
                    case "Delete Book By ID":
                        {
                            books.Delete_Book_By_ID();
                            break;
                        }
                    case "View All Books":
                        {
                            books.View_All_Boooks(); 
                            break;
                        }
                    case "View Books By Author":
                        {
                            books.View_Books_Based_On_Author();
                            break;
                        }
                    case "Issue Book":
                        {
                            books.Issue_Book();
                            break;
                        }
                    case "Return Book":
                        {
                            books.Return_Book();
                            break;
                        }
                }
            }
        }
    }
}

