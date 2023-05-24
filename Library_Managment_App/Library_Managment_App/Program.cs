using Spectre.Console;

namespace Library_Managment_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Login login = new Login();
            Students students = new Students();
            Books books = new Books();
            AnsiConsole.Write(new FigletText("Library Management System").Centered().Color(Color.DeepSkyBlue2));

            AnsiConsole.MarkupLine("[CornflowerBlue]Please Login to continue:[/]");
            Console.WriteLine();

            bool Is_Logged_In = false;
            while (!Is_Logged_In)
            {
                string username = AnsiConsole.Ask<string>("[yellow]Enter User Name:[/]");
                string password = AnsiConsole.Ask<string>("[yellow]Enter Password:[/]");
                Is_Logged_In = login.LoginUser(username, password);
            }

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
                        "Update Student",
                        "Delete Student",
                        "View Student",
                        "View All Students",
                        "Students Having Books",
                        "Add Book",
                        "Update Book",
                        "Delete Book",
                        "Issue Book",
                        "Return Book",
                        "View All Books",
                        "View Books By Author"
                   }));

                switch (choice)
                {
                    case "Add Student":
                        {
                            students.Add_Student();
                            break;
                        }
                    case "Update Student":
                        {
                            students.update_Student();
                            break;
                        }
                    case "Delete Student":
                        {
                            students.Delete_Student();
                            break;
                        }
                    case "View All Students":
                        {
                            students.View_Students();
                            break;
                        }
                    case "View Student":
                        {
                            students.View_Student();
                            break;
                        }
                    case "Students Having Books":
                        {
                            students.Students_Havinng_Books();
                            break;
                        }
                    case "Add Book":
                        {
                            books.Add_Book();
                            break;
                        }
                    case "Update Book":
                        {
                            books.Update_Book();
                            break;
                        }
                    case "Delete Book":
                        {
                            books.Delete_Book();
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