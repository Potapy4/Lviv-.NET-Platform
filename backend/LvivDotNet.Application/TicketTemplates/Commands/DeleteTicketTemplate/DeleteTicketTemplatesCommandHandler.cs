﻿using Dapper;
using LvivDotNet.Application.Exceptions;
using LvivDotNet.Application.Interfaces;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace LvivDotNet.Application.TicketTemplates.Commands.DeleteTicketTemplate
{
    public class DeleteTicketTemplatesCommandHandler : BaseHandler<DeleteTicketTemplateCommand>
    {
        public DeleteTicketTemplatesCommandHandler(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }

        protected override async Task<Unit> Handle(DeleteTicketTemplateCommand request, CancellationToken cancellationToken, IDbConnection connection, IDbTransaction transaction)
        {
            var deletedCount = await connection.ExecuteAsync(
                    "delete from dbo.[ticket_template] " +
                    "where Id = @Id",
                    request,
                    transaction
                );

            if (deletedCount == 0)
            {
                throw new NotFoundException("Ticket Template", request.Id);
            }

            return Unit.Value;
        }
    }
}
