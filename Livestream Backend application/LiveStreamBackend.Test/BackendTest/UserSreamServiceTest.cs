using Livestream_Backend_application.DataTransfer;
using Livestream_Backend_application.Models;
using Livestream_Backend_application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LiveStreamBackend.Test
{
    public class UserSreamServiceTest
    {
        private readonly TestContext testContextFactory;
        private UserStreamService UST;

        public UserSreamServiceTest()
        {
            testContextFactory = new TestContext();
            UST = new UserStreamService(testContextFactory);
        }


        [Fact]
        public async Task GetUserSTreamTest()
        {
            using var db = testContextFactory.CreateDbContext();
            var user = new AppUser()
            {
                FirstName = "John",
                Lastname = "Dragon",
                Email = "Ez@gmail.com",
                StreamKey = "EzGamesSupersecretKey",
                UserName = "EzGames"
            };

            await db.AddAsync(user);
            await db.SaveChangesAsync();

            var stream = new AppStreams()
            {
                Title = "Title",
                Description = "Description",
                UserId = user.Id,

            };
            await db.AddAsync(stream);
            await db.SaveChangesAsync();

            var response = new GetUserStreamResponse
            {
                Title = "Title",
                Description = "Description",
                StreamId = stream.StreamId,
                UserId = stream.UserId,
            };

            var getStreamResponse = await UST.getUserStreamResponse(user.Id);

            Assert.Equal(getStreamResponse.Title, stream.Title);
            Assert.Equal(getStreamResponse.Description, stream.Description);
            Assert.Equal(getStreamResponse.UserId, stream.UserId);

        }
        [Fact]
        public async Task GetUserSTreamWithNonExistinguserIdTest()
        {
            using var db = testContextFactory.CreateDbContext();
            var user = new AppUser()
            {
                FirstName = "John",
                Lastname = "Dragon",
                Email = "Ez@gmail.com",
                StreamKey = "EzGamesSupersecretKey",
                UserName = "EzGames"
            };

            await db.AddAsync(user);
            await db.SaveChangesAsync();

            var stream = new AppStreams()
            {
                Title = "Title",
                Description = "Description",
                UserId = user.Id,

            };
            await db.AddAsync(stream);
            await db.SaveChangesAsync();

            var response = new GetUserStreamResponse
            {
                Title = "Title",
                Description = "Description",
                StreamId = stream.StreamId,
                UserId = stream.UserId,
            };

            var getStreamResponse = await UST.getUserStreamResponse("WORNID-43820");

            Assert.Null(getStreamResponse);

        }
    }
}
