using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Management_System;
using Moq;
using NUnit.Framework;
using Xunit;
using FluentAssertions;

namespace Libray_Managment.Tests
{
    public  class BooksTests
    {
        [Fact]
        public void AddBook_WhenCalled_ReturnSuccessMessage()
        {
            var _booksmock = new Mock<IBooks>();
            _booksmock.Setup(b => b.Add_Book()).Callback(() => Console.WriteLine("Book Added Sucessfully"));

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            _booksmock.Object.Add_Book();

            var result = consoleOutput.ToString().Trim();

            result.Should().Contain("Book Added Sucessfully");

        }

        [Fact]
        public void DeleteBookByID_WhenCalled_ReturnSuccessMessage()
        {
            var _deletebookmock = new Mock<IBooks>();
            _deletebookmock.Setup(b => b.Delete_Book_By_ID()).Callback(() => Console.WriteLine("Book Deleted Sucessfully"));
            
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            _deletebookmock.Object.Delete_Book_By_ID();

            var result = consoleOutput.ToString().Trim();

            result.Should().Contain("Book Deleted Sucessfully");

        }

        [Fact]
        public void DeleteBookByID_WhenCalled_ReturnErrorMessage()
        {
            var _deletebookmock = new Mock<IBooks>();
            _deletebookmock.Setup(b => b.Delete_Book_By_ID()).Callback(() => Console.WriteLine("Record not found with the provided ID."));

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            _deletebookmock.Object.Delete_Book_By_ID();

            var result = consoleOutput.ToString().Trim();

            result.Should().Contain("Record not found with the provided ID.");

        }

        [Fact]
        public void UpdateBookByID_WhenCalled_ReturnSuccessMessage()
        {
            var _updatebookmock = new Mock<IBooks>();
            _updatebookmock.Setup(b => b.update_Book_By_ID()).Callback(() => Console.WriteLine("Book Updated Sucessfully"));

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            _updatebookmock.Object.update_Book_By_ID();

            var result = consoleOutput.ToString().Trim();

            result.Should().Contain("Book Updated Sucessfully");

        }

        [Fact]
        public void UpdateBookByID_WhenCalled_ReturnErrorMessage()
        {
            var _updatebookmock = new Mock<IBooks>();
            _updatebookmock.Setup(b => b.update_Book_By_ID()).Callback(() => Console.WriteLine("Record not found with the provided ID."));

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            _updatebookmock.Object.update_Book_By_ID();

            var result = consoleOutput.ToString().Trim();

            result.Should().Contain("Record not found with the provided ID.");

        }
    }
}
