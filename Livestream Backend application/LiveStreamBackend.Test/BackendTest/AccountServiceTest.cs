/*using FluentAssertions;
using Livestream_Backend_application.DataTransfer;
using Livestream_Backend_application.Models;
using Livestream_Backend_application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;











namespace LiveStreamBackend.Test
{
    public class AccountServiceTest
    {

        //private readonly SignInManager<AppUser> _signInManager;
        / private  TokenService _tokenService;
        private readonly IDbContextFactory<LivestreamDBContext> testContext;
        //  private readonly InjectFixture _injectFixture;
        private AccountService SUT;
        public AccountServiceTest()
        {
           //   _injectFixture = new InjectFixture();
            //mock tokenService
           _tokenService = new FakeTokenService();

            var testContext = new TestContext();
           // SUT = new AccountService(_injectFixture.UserManager, _injectFixture.SignInManager, _tokenService);
        }

   // [Fact]
*//*    public async Task CreateCallsStore()
    {
        // Setup
        var store = new Mock<IUserStore<AppUser>>();
        var user = new AppUser { UserName = "Foo" };
        store.Setup(s => s.CreateAsync(user, CancellationToken.None)).ReturnsAsync(IdentityResult.Success).Verifiable();
        store.Setup(s => s.GetUserNameAsync(user, CancellationToken.None)).Returns(Task.FromResult(user.UserName)).Verifiable();
        store.Setup(s => s.SetNormalizedUserNameAsync(user, user.UserName.ToUpperInvariant(), CancellationToken.None)).Returns(Task.FromResult(0)).Verifiable();
        var userManager = MockHelpers.TestUserManager<AppUser>(store.Object);

        // Act
        var result = await userManager.CreateAsync(user);

        // Assert
        Assert.True(result.Succeeded);
        store.VerifyAll();
    }*//*


        [Fact]
        public async Task RegisterAUserTest()
        {

            var RegisterUser = new RegisterDto()
            {
                FirstName = "Eziek",
                LastName = "Dragon",
                Email = "Ez@gmail.com",
                Password = "OpenSayzMe3",
                StreamKey = "EzGamesSupersecretKey",
                UserName = "EzGames"
            };

            var user = new AppUser()
            {
                FirstName = RegisterUser.FirstName,
                UserName = RegisterUser.UserName,
                Email = RegisterUser.Email,
                StreamKey = RegisterUser.StreamKey,
                Lastname = RegisterUser.LastName

            };

            Assert.NotNull(user);


            //Act

            //Assert
        }
   
    }
}


*/