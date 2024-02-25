using AuthenticationWebApi.Controllers;
using JWTAuth;
using JwtAuthHandler.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Microsoft.DependencyInjection.Attributes;

namespace JwtToken.tests
{
    public class JwtTokenTests
    {

        [Fact, TestOrder(1)]
        public void GenerateJwtToken_ReturnsToken()
        {
            // Arrange
            var authRequest = new AuthenticateRequest()
            {
                UserName = "user",
                Password = "user123"
            };

            var configure = new Mock<IConfiguration>();
            var handler = new Mock<JwtTokenHandler>(configure.Object);
            var accountController = new AccountController(handler.Object);
            // Act
            AuthenticateResponse tokenObj = accountController.Authenticate(authRequest).Value;

            // Assert
            Assert.NotNull(tokenObj.Token);
            Assert.Equal("user",tokenObj.UserName);
        }
    }
}