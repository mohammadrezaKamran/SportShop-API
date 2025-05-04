using Common.Application;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Ticket.AddReply;
using Shop.Application.Ticket.CreateTicket;
using Shop.Query.Orders.DTOs;
using Shop.Query.Ticket.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.SupportTicket
{
    public interface ISupportTicketFacade
    {
        Task<OperationResult> CreateTicket(CreateTicketCommand command);
        Task<OperationResult> AddReply(AddReplyCommand command);

        Task<TicketFilterResult> GetByFilter(TicketFilterParams ticketParams);
    }
}
