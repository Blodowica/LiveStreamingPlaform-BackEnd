using Livestream_Backend_application.DataTransfer;
using Livestream_Backend_application.Models;
using Livestream_Backend_application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.Services
{
    public class UserStreamService : IUserStreamService
    {
        private readonly IDbContextFactory<LivestreamDBContext> _livestreamDBContext;
      

        public UserStreamService(IDbContextFactory<LivestreamDBContext> livestreamDBContext)
        {
            _livestreamDBContext = livestreamDBContext;
        }


        public async Task<GetUserStreamResponse> getUserStreamResponse(string userId)
        {
            using var db = _livestreamDBContext.CreateDbContext();

            var s = await db.Streams
                 .Where(x => x.UserId == userId)
                 .Select(x => new
                 {
                     x.StreamId,
                     x.Title,
                     x.Description,
                     x.UserId,

                 }).FirstOrDefaultAsync();
            if (s == null) return null;
          

            return new GetUserStreamResponse()
            {
                StreamId = s.StreamId,
                Title = s.Title,
                Description = s.Description,
                UserId = s.UserId,
                
                

            };
                
        }
    }
}
