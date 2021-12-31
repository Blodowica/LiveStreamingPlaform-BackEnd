using Livestream_Backend_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.Services.Interfaces
{
   public interface IUserStreamService
    {
        public Task<GetUserStreamResponse> getUserStreamResponse(string userId);
    }
}
