﻿using System.Collections.Generic;
using System.Threading.Tasks;
using LvivDotNet.Application.Events.Commands.AddEvent;
using LvivDotNet.Application.Events.Commands.UpdateEvent;
using LvivDotNet.Application.Events.Models;
using LvivDotNet.Application.Events.Queries.GetEvent;
using LvivDotNet.Application.Events.Queries.GetEvents;
using LvivDotNet.Application.TicketTemplates.Models;
using LvivDotNet.Application.TicketTemplates.Queries.GetTicketTemplates;
using LvivDotNet.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LvivDotNet.WebApi.Controllers
{
    /// <summary>
    /// Events controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventsController"/> class.
        /// </summary>
        /// <param name="mediator"> Mediator service. </param>
        public EventsController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        /// <summary>
        /// Gets mediator service.
        /// </summary>
        public IMediator Mediator { get; }

        /// <summary>
        /// Add`s event.
        /// </summary>
        /// <param name="command"> Add event command. </param>
        /// <returns> New event id.  </returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public Task<int> AddEvent([FromBody] AddEventCommand command)
            => this.Mediator.Send(command);

        /// <summary>
        /// Get`s even.
        /// </summary>
        /// <param name="id"> Event id. </param>
        /// <returns> Event model. </returns>
        [HttpGet("{id:int}")]
        public Task<EventModel> GetEvent(int id)
            => this.Mediator.Send(new GetEventQuery { EventId = id });

        /// <summary>
        /// Gets`a event ticket templates.
        /// </summary>
        /// <param name="eventId"> Event id. </param>
        /// <returns> Ticket templates models collection. </returns>
        [HttpGet("ticket/templates/{eventId:int}")]
        public Task<IEnumerable<TicketTemplateModel>> GetTicketTemplates(int eventId)
            => this.Mediator.Send(new GetTicketTemplatesQuery { EventId = eventId });

        /// <summary>
        /// Get`s events.
        /// </summary>
        /// <param name="take"> Count of events to take. </param>
        /// <param name="skip"> Count of events to skip. </param>
        /// <returns> Page of short event models. </returns>
        [HttpGet]
        public Task<Page<EventShortModel>> GetEvents([FromQuery] int take = 10, [FromQuery] int skip = 0)
            => this.Mediator.Send(new GetEventsQuery { Take = take, Skip = skip });

        /// <summary>
        /// Update`s event.
        /// </summary>
        /// <param name="command"> Update event command. </param>
        /// <returns> Empty task. </returns>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public Task UpdateEvent([FromBody] UpdateEventCommand command)
            => this.Mediator.Send(command);
    }
}
