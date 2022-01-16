using AutoMapper;
using Livestream_Backend_application.DataTransfer;
using Livestream_Backend_application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.SignalR.Comments
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Comment, commentDto>()
              .ForMember(d => d.DisplayName, o => o.MapFrom(S => S.Author.FirstName))
              .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName));

        }
    }
}
