using Library_Managment_App;
using Moq;
using FluentAssertions;

namespace Library_Managment_App.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void LoginUser_WhenCalled_ReturnsTrue()
        {
            var _loginmock = new Mock<ILogin>();
            _loginmock.Setup(l => l.LoginUser("harish","harish@18")).Returns(true);
            var res = _loginmock.Object.LoginUser("harish", "harish@18");
            res.Should().Be(true);
        }

        [Fact]
        public void LoginUser_WhenCalled_ReturnsFalse()
        {
            var _loginmock = new Mock<ILogin>();
            _loginmock.Setup(l => l.LoginUser("harish", "harish18")).Returns(false);
            var res = _loginmock.Object.LoginUser("harish", "harish18");
            res.Should().Be(false);
        }
        [Fact]
        public void AddBook_WhenCalled_ReturnsValue()
        {
            var _booksmock = new Mock<IBooks>();
            _booksmock.Setup(b => b.Add_Book()).Returns(1);
            var result = _booksmock.Object.Add_Book();
            result.Should().Be(1);
        }


        [Fact]
        public void DeleteBookByID_WhenCalled_ReturnsValue()
        {
            var _deletebookmock = new Mock<IBooks>();
            _deletebookmock.Setup(b => b.Delete_Book()).Returns(1);
            var result = _deletebookmock.Object.Delete_Book();
            result.Should().Be(1);
        }


        [Fact]
        public void UpdateBookByID_WhenCalled_ReturnsValue()
        {
            var _updatebookmock = new Mock<IBooks>();
            _updatebookmock.Setup(b => b.Update_Book()).Returns(1);
            var result = _updatebookmock.Object.Update_Book();
            result.Should().Be(1);
        }
        [Fact]
        public void AddStudent_WhenCalled_ReturnsValue()
        {
            var _studentmock = new Mock<IStudents>();
            _studentmock.Setup(s => s.Add_Student()).Returns(1);
            var res = _studentmock.Object.Add_Student();
            res.Should().Be(1);
        }

        [Fact]
        public void AddStudent_WhenCalled_Failed()
        {
            var _booksmock = new Mock<IBooks>();
            _booksmock.Setup(b => b.Add_Book()).Returns(0);
            var result = _booksmock.Object.Add_Book();
            result.Should().Be(0);
        }

        [Fact]
        public void DeleteStudentByID_WhenCalled_ReturnsValue()
        {
            var _deletestudentmock = new Mock<IStudents>();
            _deletestudentmock.Setup(s => s.Delete_Student()).Returns(1);
            var res = _deletestudentmock.Object.Delete_Student();
            res.Should().Be(1);
        }

        [Fact]
        public void UpdateStudentByID_WhenCalled_RetursValue()
        {
            var _updatestudentmock = new Mock<IStudents>();
            _updatestudentmock.Setup(s => s.update_Student()).Returns(1);
            var res = _updatestudentmock.Object.update_Student();
            res.Should().Be(1);
        }

        [Fact]
        public void ViewStudent_WhenCalled_RetursValue()
        {
            var _viewstudentmock = new Mock<IStudents>();
            _viewstudentmock.Setup(s => s.View_Student()).Returns(1);
            var res = _viewstudentmock.Object.View_Student();
            res.Should().Be(1);
        }

        [Fact]
        public void ViewStudents_WhenCalled_RetursValue()
        {
            var _viewstudentsmock = new Mock<IStudents>();
            _viewstudentsmock.Setup(s => s.View_Students()).Returns(1);
            var res = _viewstudentsmock.Object.View_Students();
            res.Should().Be(1);
        }

        [Fact]
        public void ViewStudentsHavingBooks_WhenCalled_RetursValue()
        {
            var _viewstudentsmock = new Mock<IStudents>();
            _viewstudentsmock.Setup(s => s.Students_Havinng_Books()).Returns(1);
            var res = _viewstudentsmock.Object.Students_Havinng_Books();
            res.Should().Be(1);
        }
    }
}