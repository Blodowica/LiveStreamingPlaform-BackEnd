using FluentAssertions;
using Livestream_Backend_application.Controller;
using Livestream_Backend_application.DataTransfer;
using Livestream_Backend_application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;












namespace LiveStreamBackend.Test
{
    public class AccountServiceTest
    {

        private Mock<UserManager<AppUser>> _mockUserManager;
        private Mock<SignInManager<AppUser>> _mockSignInManager;
        private Mock<TokenService> _tokenService;
        private AccountController _controller;
    

        public AccountServiceTest()
        {
            /*   var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
               var httpContext = new DefaultHttpContext();

               var claims = new List<Claim>()
               {
                   new Claim(ClaimTypes.NameIdentifier, "1"),
               };

               mockHttpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);*/

            var userStoreMock = new Mock<IUserStore<AppUser>>();

            _mockUserManager = new Mock<UserManager<AppUser>>(userStoreMock.Object,
                null, null, null, null, null, null, null, null);

            var contextAccessor = new Mock<IHttpContextAccessor>();
            var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<AppUser>>();

            _mockSignInManager = new Mock<SignInManager<AppUser>>(_mockUserManager.Object,
                contextAccessor.Object, userPrincipalFactory.Object, null, null, null);

            _tokenService = new Mock<TokenService>();

         

        }

        private static Mock<UserManager<AppUser>> SetupUserManager(AppUser user)
        {
            var manager = MockHelpers.MockUserManager<AppUser>();
            manager.Setup(m => m.FindByNameAsync(user.UserName)).ReturnsAsync(user);
            manager.Setup(m => m.FindByIdAsync(user.Id)).ReturnsAsync(user);
            manager.Setup(m => m.GetUserIdAsync(user)).ReturnsAsync(user.Id.ToString());
            manager.Setup(m => m.GetUserNameAsync(user)).ReturnsAsync(user.UserName);
            return manager;
        }


        [Fact]
        public async Task CreateUserTest()
        {
            // Setup
            var store = new Mock<IUserStore<AppUser>>();
            var user = new AppUser {  UserName = "foo" };
            store.Setup(s => s.CreateAsync(user, CancellationToken.None)).ReturnsAsync(IdentityResult.Success).Verifiable();
            store.Setup(s => s.GetUserNameAsync(user, CancellationToken.None)).Returns(Task.FromResult(user.UserName)).Verifiable();
            var userManager = MockHelpers.TestUserManager<AppUser>(store.Object);

            // Act
            var result = await userManager.CreateAsync(user);

            // Assert
            Assert.True(result.Succeeded);
            store.VerifyAll();
        }
        [Fact]
        public async Task FindByWrongEmailTest()
        {
            // Setup
            var user = new AppUser()
            {
                FirstName = "Jonny",
                UserName = "Jonny1",
                Email = "Ez@gmail.com",
                StreamKey = "Jonnykey",
                Lastname = "Jonnyl",

            };
          
            var store = new Mock<IUserEmailStore<AppUser>>();
          
            var userManager = MockHelpers.TestUserManager(store.Object);

            // Act
          
            var result = await userManager.FindByEmailAsync(user.Email);

            // Assert
            Assert.Null(result);
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
                FirstName = "John",
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
        [Fact]
        public async Task RegisterUsernameThatAlreadyExist()
        {

            bool stillRegisterUser = false;

            var RegisterUser = new RegisterDto()
            {
                FirstName = "John",
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
            var newRegisterUser = new RegisterDto()
            {
                FirstName = "John",
                LastName = "Dragon",
                Email = "Ez@gmail.com",
                Password = "OpenSayzMe3",
                StreamKey = "EzGamesSupersecretKey",
                UserName = "EzGames"
            };

            var newuser = new AppUser()
            {
                FirstName = newRegisterUser.FirstName,
                UserName = newRegisterUser.UserName,
                Email = newRegisterUser.Email,
                StreamKey = newRegisterUser.StreamKey,
                Lastname = newRegisterUser.LastName

            };
            var store = new Mock<IUserStore<AppUser>>();
            store.Setup(s => s.CreateAsync(newuser, CancellationToken.None)).ReturnsAsync(IdentityResult.Success).Verifiable();
            store.Setup(s => s.GetUserNameAsync(newuser, CancellationToken.None)).Returns(Task.FromResult(newuser.UserName)).Verifiable();
            store.Setup(s => s.FindByIdAsync(newuser.Id, CancellationToken.None)).Returns(Task.FromResult(newuser)).Verifiable();

            var userManager = MockHelpers.TestUserManager<AppUser>(store.Object);
            await userManager.CreateAsync(newuser);
            
            var foundUser = await userManager.FindByIdAsync(newuser.Id);
            if (foundUser.UserName != user.UserName)
            {
                stillRegisterUser = true;
            }
            else
            {
                stillRegisterUser = false;
            }

            Assert.False(stillRegisterUser);

        }

        [Fact]
        public async Task LoginUserTest()
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

            var store = new Mock<IUserPasswordStore<AppUser>>();
            
            store.Setup(s => s.CreateAsync(newUser, CancellationToken.None)).ReturnsAsync(IdentityResult.Success).Verifiable();
            
  
            var userManager = MockHelpers.TestUserManager<AppUser>(store.Object);

            var result = await userManager.CreateAsync(newUser, RegisterUser.Password);


            var Emailstore = new Mock<IUserEmailStore<AppUser>>();
            var login = new LoginDto
            {
                Email = "Ez@gmail.com",
                Password = "OpenSayzMe3"
            };
            Emailstore.Setup(s => s.FindByEmailAsync(login.Email, CancellationToken.None));
            var userManager2 =  MockHelpers.TestUserManager<AppUser>(Emailstore.Object);

            var loginUser = await userManager2.FindByEmailAsync(login.Email);

       
            login.Email.Equals(RegisterUser.Email);
            login.Password.Equals(RegisterUser.Password);



        }


        [Fact]
        public async Task LoginUserWrongCredentialsTest()
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

            var store = new Mock<IUserPasswordStore<AppUser>>();

            store.Setup(s => s.CreateAsync(newUser, CancellationToken.None)).ReturnsAsync(IdentityResult.Success).Verifiable();


            var userManager = MockHelpers.TestUserManager<AppUser>(store.Object);

            var result = await userManager.CreateAsync(newUser, RegisterUser.Password);


            var Emailstore = new Mock<IUserEmailStore<AppUser>>();
            var login = new LoginDto
            {
                Email = "Ez@gmail.com",
                Password = "OpenSayzMe2"
            };
            Emailstore.Setup(s => s.FindByEmailAsync(login.Email, CancellationToken.None));
            var userManager2 = MockHelpers.TestUserManager<AppUser>(Emailstore.Object);

            var loginUser = await userManager2.FindByEmailAsync(login.Email);



            Assert.True(login.Email == RegisterUser.Email);
           Assert.False(login.Password == RegisterUser.Password);

        }
        [Fact]
        public async Task LoginUserWrongEmailTest()
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

            var store = new Mock<IUserPasswordStore<AppUser>>();

            store.Setup(s => s.CreateAsync(newUser, CancellationToken.None)).ReturnsAsync(IdentityResult.Success).Verifiable();


            var userManager = MockHelpers.TestUserManager<AppUser>(store.Object);

            var result = await userManager.CreateAsync(newUser, RegisterUser.Password);


            var Emailstore = new Mock<IUserEmailStore<AppUser>>();
            var login = new LoginDto
            {
                Email = "Wr0ng@gmail.com",
                Password = "OpenSayzMe2"
            };
            Emailstore.Setup(s => s.FindByEmailAsync(login.Email, CancellationToken.None));
            var userManager2 = MockHelpers.TestUserManager<AppUser>(Emailstore.Object);

            var loginUser = await userManager2.FindByEmailAsync(login.Email);

            Assert.Null(loginUser);

            

        }


    }
}


