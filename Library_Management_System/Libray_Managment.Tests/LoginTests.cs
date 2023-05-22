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
    public class LoginTests
    {
        [Fact]
        public void LoginUser_WhenCalled_ReturnsTrue()
        {
            var _loginmock = new Mock<ILogin>();
            _loginmock.Setup(l => l.LoginUser()).Returns(true);
            var res = _loginmock.Object.LoginUser();
            res.Should().Be(true);   
        }

        [Fact]

        public void LoginUser_WhenCalled_ReturnsFalse()
        {
            var _loginmock = new Mock<ILogin>();
            _loginmock.Setup(l => l.LoginUser()).Returns(false);
            var res = _loginmock.Object.LoginUser();
            res.Should().Be(false);
        }
    }
}
