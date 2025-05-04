using Common.Application;
using MediatR;
using Shop.Application.Ticket.AddReply;
using Shop.Application.Ticket.CreateTicket;
using Shop.Query.Ticket.GetBtFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.SupportTicket
{
    internal class SupportTicketFacade : ISupportTicketFacade
    {
        private readonly IMediator _mediator;

        public SupportTicketFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> AddReply(AddReplyCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> CreateTicket(CreateTicketCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<TicketFilterResult> GetByFilter(TicketFilterParams ticketParams)
        {
            return await _mediator.Send(new GetTicketByFilterQuery(ticketParams));
        }
    }
}
