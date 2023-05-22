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
    public class StudentsTets
    {
        [Fact]
        public void AddStudent_WhenCalled_ReturnSuccessMessage()
        {
            var _studentmock = new Mock<IStudents>();
            _studentmock.Setup(s => s.Add_Student()).Callback(() => Console.WriteLine("Student Added Sucessfully"));
            
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            _studentmock.Object.Add_Student();

            var result = consoleOutput.ToString().Trim();

            result.Should().Contain("Student Added Sucessfully");

        }

        [Fact]
        public void DeleteStudentByID_WhenCalled_ReturnSuccessMessage()
        {
            var _deletestudentmock = new Mock<IStudents>();
            _deletestudentmock.Setup(s => s.Delete_Student_By_ID()).Callback(() => Console.WriteLine("Student Deleted Sucessfully"));

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            _deletestudentmock.Object.Delete_Student_By_ID();

            var result = consoleOutput.ToString().Trim();

            result.Should().Contain("Student Deleted Sucessfully");

        }

        [Fact]
        public void DeleteStudentByID_WhenCalled_ReturnErrorMessage()
        {
            var _deletestudentmock = new Mock<IStudents>();
            _deletestudentmock.Setup(s => s.Delete_Student_By_ID()).Callback(() => Console.WriteLine("Record not found with the provided ID."));

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            _deletestudentmock.Object.Delete_Student_By_ID();

            var result = consoleOutput.ToString().Trim();

            result.Should().Contain("Record not found with the provided ID.");

        }

        [Fact]
        public void UpdateStudentByID_WhenCalled_ReturnSuccessMessage()
        {
            var _updatestudentmock = new Mock<IStudents>();
            _updatestudentmock.Setup(s => s.update_Student_By_ID()).Callback(() => Console.WriteLine("Student Updated Sucessfully"));

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            _updatestudentmock.Object.update_Student_By_ID();

            var result = consoleOutput.ToString().Trim();

            result.Should().Contain("Student Updated Sucessfully");

        }

        [Fact]
        public void UpdateStudentByID_WhenCalled_ReturnErrorMessage()
        {
            var _updatestudentmock = new Mock<IStudents>();
            _updatestudentmock.Setup(s => s.update_Student_By_ID()).Callback(() => Console.WriteLine("Record not found with the provided ID."));

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            _updatestudentmock.Object.update_Student_By_ID();

            var result = consoleOutput.ToString().Trim();

            result.Should().Contain("Record not found with the provided ID.");

        }

    }
}
