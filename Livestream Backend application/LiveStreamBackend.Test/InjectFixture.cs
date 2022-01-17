using Castle.DynamicProxy.Generators;
using Livestream_Backend_application.DataTransfer;
using Livestream_Backend_application.Models;
using Livestream_Backend_application.Services;
using Livestream_Backend_application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveStreamBackend.Test
{
    public class InjectFixture : IDisposable
    {
        public readonly UserManager<AppUser> UserManager;
        public readonly SignInManager<AppUser> SignInManager;
       public readonly IAccountService AccountService;
        public readonly LivestreamDBContext DbContext;
      public readonly TokenService tokenService;
        //public readonly IGenerator Generator;

        public InjectFixture()
        {
            var options = new DbContextOptionsBuilder<LivestreamDBContext>()
                .UseInMemoryDatabase(databaseName: "FakeDatabase")
                .Options;

            DbContext = new LivestreamDBContext(options);

            var users = new List<AppUser>
            {
                new AppUser
                {
                    UserName = "Test",
                    Id = Guid.NewGuid().ToString(),
                    Email = "test@test.it"
                }
            }.AsQueryable();

            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(x => x.Users)
                .Returns(users);

            fakeUserManager.Setup(x => x.DeleteAsync(It.IsAny<AppUser>()))
                .ReturnsAsync(IdentityResult.Success);
            fakeUserManager.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            fakeUserManager.Setup(x => x.UpdateAsync(It.IsAny<AppUser>()))
                .ReturnsAsync(IdentityResult.Success);
            fakeUserManager.Setup(x =>
                    x.ChangeEmailAsync(It.IsAny<AppUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var signInManager = new Mock<FakeSignInManager>();
            signInManager.Setup(
                    x => x.PasswordSignInAsync(It.IsAny<AppUser>(), It.IsAny<string>(), It.IsAny<bool>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);


            var fakeTokenServicd = new Mock<FakeTokenService>();

            fakeTokenServicd.Setup(x => x.CreateToken(It.IsAny<AppUser>()));


            UserManager = fakeUserManager.Object;
            SignInManager = signInManager.Object;
         //   AccountService = new AccountService(UserManager, SignInManager, tokenService );
           // Generator = new Generator();
        }

        public void Dispose()
        {
            UserManager?.Dispose();
            DbContext?.Dispose();
        }
    }
}
