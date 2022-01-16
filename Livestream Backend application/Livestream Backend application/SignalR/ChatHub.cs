using Livestream_Backend_application.Models;
using Livestream_Backend_application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Reactivities.Application.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livestream_Backend_application.SignalR
{
    public class ChatHub : Hub
    {

        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendComment(Create.Command command)
        {
            var comment = await _mediator.Send(command);


            await Clients.Group(command.StreamId.ToString())
                .SendAsync("ReceiveComment", comment.Value);

        }
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var streamId = httpContext.Request.Query["streamId"];
            await Groups.AddToGroupAsync(Context.ConnectionId, streamId);
            var result = await _mediator.Send(new List.Query { StreamId = Convert.ToInt32(streamId) });
            await Clients.Caller.SendAsync("LoadingComments", result.Value);


        }
    }
}
