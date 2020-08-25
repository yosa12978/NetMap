using Moq;
using NetMap.Data.Models;
using NetMap.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NetMap.Test.Tests.Repositories
{
    public class TokenRepositoryShould
    {
        [Fact]
        public void isTokenExist()
        {
            Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
            //_userRepository.Setup(m => m.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            //_userRepository.Object.CreateUser("yosa", "yosa@gmail.com", "123456789");
            _userRepository.Setup(m => m.GetUser("yosa")).Returns(new User());
            User user = _userRepository.Object.GetUser("yosa");

            Mock<ITokenRepository> _tokenRepository = new Mock<ITokenRepository>();
            _tokenRepository.Setup(m => m.isTokenExist(user.token)).Returns(true);
            Assert.True(_tokenRepository.Object.isTokenExist(user.token));
        }
    }
}
