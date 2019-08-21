﻿using System.Threading.Tasks;
using LvivDotNet.Application.Events.Commands.UpdateEvent;
using LvivDotNet.Application.Tests.Common;
using LvivDotNet.Common;
using LvivDotNet.WebApi.Controllers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace LvivDotNet.Application.Tests.Events.Commands
{
    /// <summary>
    /// Update event test.
    /// </summary>
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class UpdateEventTest : BaseTest
    {
        private EventsController EventsController { get; set; }

        /// <summary>
        /// One-time test setup. Executed exactly once before all tests.
        /// Initialize Events controller.
        /// </summary>
        /// <returns> Task representing asynchronous operation. </returns>
        [OneTimeSetUp]
        public async Task SetUp()
        {
            this.EventsController = new EventsController(ServiceProvider.GetRequiredService<IMediator>());
            JwtToken = await ServiceProvider.GetAuthorizedJwtToken();
        }

        /// <summary>
        /// Test event update logic.
        /// <see cref="UpdateEventCommand"/>.
        /// </summary>
        /// <returns> Task representing asynchronous operation. </returns>
        [Test]
        [Repeat(500)]
        public async Task UpdateEvent()
        {
            var addEventCOmmand = Fakers.AddEventCommand.Generate();

            var eventId = await this.EventsController.AddEvent(addEventCOmmand);

            var command = Fakers.UpdateEventCommand.Generate();
            command.Id = eventId;

            await this.EventsController.UpdateEvent(command);

            var result = await this.EventsController.GetEvent(eventId);

            Assert.AreEqual(command.Address, result.Address);
            Assert.AreEqual(command.Description, result.Description);
            Assert.IsTrue(command.EndDate.IsEqual(result.EndDate));
            Assert.IsTrue(command.StartDate.IsEqual(result.StartDate));
            Assert.AreEqual(command.MaxAttendees, result.MaxAttendees);
            Assert.AreEqual(command.Name, result.Name);
            Assert.AreEqual(command.Title, result.Title);
        }
    }
}
