using AutoMapper;
using AutoMapper.QueryableExtensions;
using Livestream_Backend_application.DataTransfer;
using Livestream_Backend_application.Models;
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
    public class List
    {


        public class Query : IRequest<Result<List<commentDto>>>
        {
            public int StreamId  { get; set; }
        }


        public class Handler : IRequestHandler<Query, Result<List<commentDto>>>
        {
            private readonly LivestreamDBContext _context;
            private readonly IMapper _mapper;

            public Handler(LivestreamDBContext context, IMapper mapper)
            {
              _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<commentDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var comments = await _context.Comments
                     .Where(x => x.Stream.StreamId == request.StreamId)
                     .OrderByDescending(x => x.CreatedAt)
                     .ProjectTo<commentDto>(_mapper.ConfigurationProvider)
                     .ToListAsync();

                return Result<List<commentDto>>.Success(comments);
            }
        }

    }
}
