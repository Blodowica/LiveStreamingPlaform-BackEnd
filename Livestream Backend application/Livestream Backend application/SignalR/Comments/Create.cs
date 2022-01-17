using AutoMapper;
using FluentValidation;
using Livestream_Backend_application.DataTransfer;
using Livestream_Backend_application.Models;
using Livestream_Backend_application.Services.Interfaces;
using Livestream_Backend_application.SignalR.Comments;
using MediatR;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Livestream_Backend_application.SignalR.Comments
{
    public class Create
    {
        public class Command : IRequest<Result<commentDto>>
        {

            public string Body { get; set; }
            public int   StreamId { get; set; }

            public AppUser Author { get; set; }

        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Body).NotEmpty();
            }
        }


        public class Handler : IRequestHandler<Command, Result<commentDto>>
        {
            private readonly LivestreamDBContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(LivestreamDBContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _context = context;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }

            public async Task<Result<commentDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var stream = await _context.Streams.FindAsync(request.StreamId);
                if (stream == null) return null;

                var user = await _context.appUsers
                    .SingleOrDefaultAsync(x => x.UserName == request.Author.UserName );

                var comment = new Comment
                {
                    Author = user,
                    Stream = stream,
                    Body = request.Body,

                };

                stream.Comments.Add(comment);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Result<commentDto>.Success(_mapper.Map<commentDto>(comment));

                return Result<commentDto>.Failure("Failed to add the comment");
               
            }
        }
    }
}
