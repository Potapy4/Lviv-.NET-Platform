﻿using System.Collections.Generic;
using LvivDotNet.Application.Events.Commands.AddEvent;
using LvivDotNet.Application.Events.Commands.UpdateEvent;
using LvivDotNet.Application.Events.Models;
using LvivDotNet.Application.Events.Queries.GetEvent;
using LvivDotNet.Application.Events.Queries.GetEvents;
using LvivDotNet.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LvivDotNet.Application.TicketTemplates.Models;
using LvivDotNet.Application.TicketTemplates.Queries.GetTicketTemplates;

namespace LvivDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly IMediator mediator;

        public EventsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public Task<int> AddEvent([FromBody] AddEventCommand command)
            => this.mediator.Send(command);

        [HttpGet("{id:int}")]
        public Task<EventModel> GetEvent(int id)
            => this.mediator.Send(new GetEventQuery { EventId = id });

        [HttpGet("ticket/templates/{eventId:int}")]
        public Task<IEnumerable<TicketTemplateModel>> GetTicketTemplates(int eventId)
            => this.mediator.Send(new GetTicketTemplatesQuery { EventId = eventId });

        [HttpGet]
        public Task<Page<EventShortModel>> GetEvents([FromQuery] int take, [FromQuery] int skip)
            => this.mediator.Send(new GetEventsQuery { Take = take, Skip = skip });

        [HttpPut]
        public Task UpdateEvent([FromBody] UpdateEventCommand command)
            => this.mediator.Send(command);
    }
}
