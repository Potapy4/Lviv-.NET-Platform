using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using LvivDotNet.Application.Events.Models;
using LvivDotNet.Application.Interfaces;
using LvivDotNet.Common;
using Microsoft.AspNetCore.Http;

namespace LvivDotNet.Application.Events.Queries.GetEvents
{
    /// <summary>
    /// Get events query handler.
    /// </summary>
    public class GetEventsQueryHandler : BaseHandler<GetEventsQuery, Page<EventShortModel>>
    {
        /// <summary>
        /// Get events sql query.
        /// </summary>
        private const string GetEventsSqlQuery =
                "select * from public.event " +
                @"order by ""Id""" +
                "offset @Skip limit @Take";

        /// <summary>
        /// Initializes a new instance of the <see cref="GetEventsQueryHandler"/> class.
        /// </summary>
        /// <param name="dbConnectionFactory"> Database connection factory. </param>
        /// <param name="httpContextAccessor"> See <see cref="IHttpContextAccessor"/>. </param>
        public GetEventsQueryHandler(IDbConnectionFactory dbConnectionFactory, IHttpContextAccessor httpContextAccessor)
            : base(dbConnectionFactory, httpContextAccessor)
        {
        }

        /// <inheritdoc/>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "We already have a not-null check for request in MediatR")]
        protected override async Task<Page<EventShortModel>> Handle(GetEventsQuery request, IDbConnection connection, IDbTransaction transaction, CancellationToken cancellationToken)
        {
            var events = await connection.QueryAsync<EventShortModel>(GetEventsSqlQuery, request, transaction)
                .ConfigureAwait(false);

            return new Page<EventShortModel>
            {
                Items = events,
                Total = events.Count(),
            };
        }
    }
}