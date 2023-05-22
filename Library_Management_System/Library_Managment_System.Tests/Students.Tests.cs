using Library_Management_System;
using System.Data.SqlClient;
using Moq;
using System.Xml.Linq;

namespace Library_Managment_System.Tests
{
    public class Tests
    {
        //private Mock<SqlConnection> mockConnection;
        private Mock<SqlCommand> mockCommand;
        private Students students;

        [SetUp] 
        public void SetUp() 
        {
            //mockConnection = new Mock<SqlConnection>();
            mockCommand = new Mock<SqlCommand>();

            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(() => 1);

            //mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);

            students = new Students();
            //students = new Students(mockConnection.Object);
        }

        [Test]
        public void Test1()
        {
            students.Add_Student();

            mockCommand.Verify(c =>c.ExecuteNonQuery(),Times.Once);

            //mockCommand.Verify(c =>c.Parameters.AddWithValue("@Name", name),Times.Once);
            
            //mockCommand.Verify(c =>c.Parameters.AddWithValue("@RollNo", rollNo),Times.Once);
        }
    }
}