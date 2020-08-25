using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using NetMap.Data.Repositories.Interfaces;

namespace NetMap.Test.Tests.Repositories
{
    public class UserRepositoryShould
    {
        [Fact]
        public void isUserExist()
        {
            Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
            //_userRepository.Setup(m => m.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            //_userRepository.Object.CreateUser("yosa", "yosa@gmail.com", "123456789");

            _userRepository.Setup(m => m.isUsernameExist("yosa")).Returns(true);

            Assert.True(_userRepository.Object.isUsernameExist("yosa"));
        }
    }
}
