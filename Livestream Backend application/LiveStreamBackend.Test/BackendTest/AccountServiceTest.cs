using FluentAssertions;
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
     

        public AccountServiceTest()
        {
           
                
        }

         [Fact]
        public async Task CreateUserTest()
        {
            // Setup
            var store = new Mock<IUserStore<AppUser>>();
            var user = new AppUser {  UserName = "foo" };
            store.Setup(s => s.CreateAsync(user, CancellationToken.None)).ReturnsAsync(IdentityResult.Success).Verifiable();
            store.Setup(s => s.GetUserNameAsync(user, CancellationToken.None)).Returns(Task.FromResult(user.UserName)).Verifiable();
           // store.Setup(s => s.SetNormalizedUserNameAsync(user, user.UserName.ToUpperInvariant(), CancellationToken.None)).Returns(Task.FromResult(0)).Verifiable();
            var userManager = MockHelpers.TestUserManager<AppUser>(store.Object);

            // Act
            var result = await userManager.CreateAsync(user);

            // Assert
            Assert.True(result.Succeeded);
            store.VerifyAll();
        }

        [Fact]
        public async Task RegisterUserTest()
        {
          

            var RegisterUser = new RegisterDto()
            {
                FirstName = "Jonny",
                LastName = "Dragon",
                Email = "Ez@gmail.com",
                Password = "OpenSayzMe3",
                StreamKey = "EzGamesSupersecretKey",
                UserName = "EzGames"
            };
            var newUser = new AppUser()
            {
                FirstName = RegisterUser.FirstName,
                UserName = RegisterUser.UserName,
                Email = RegisterUser.Email,
                StreamKey = RegisterUser.StreamKey,
                Lastname = RegisterUser.LastName

            };

            var store = new Mock<IUserStore<AppUser>>();
            store.Setup(s => s.CreateAsync(newUser, CancellationToken.None)).ReturnsAsync(IdentityResult.Success).Verifiable();
            store.Setup(s => s.GetUserNameAsync(newUser, CancellationToken.None)).Returns(Task.FromResult(newUser.UserName)).Verifiable();
            var userManager = MockHelpers.TestUserManager<AppUser>(store.Object);

            var result = await userManager.CreateAsync(newUser);

            Assert.True(result.Succeeded);


        }
       
        [Fact]
        public async Task RegisterUserThatAlreadyExist()
        {

           bool doubleUser = false;

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
            var store = new Mock<IUserStore<AppUser>>();
            store.Setup(s => s.CreateAsync(user, CancellationToken.None)).ReturnsAsync(IdentityResult.Success).Verifiable();
            store.Setup(s => s.GetUserNameAsync(user, CancellationToken.None)).Returns(Task.FromResult(user.UserName)).Verifiable();
            store.Setup(s => s.FindByIdAsync(user.Id, CancellationToken.None)).Returns(Task.FromResult(user)).Verifiable();

            var userManager = MockHelpers.TestUserManager<AppUser>(store.Object);
             await userManager.CreateAsync(user);
            var foundUser = await userManager.FindByIdAsync(user.Id);
            if (foundUser == null)
            {
                 doubleUser = true;
            }
            else
            {
                doubleUser = false;
            }

            Assert.False(doubleUser);

        }

    }
}


