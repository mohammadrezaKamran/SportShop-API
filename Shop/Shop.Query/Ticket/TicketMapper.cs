using Shop.Domain.CommentAgg;
using Shop.Domain.SupportTicketAgg;
using Shop.Query.Comments.DTOs;
using Shop.Query.Ticket.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Ticket
{
    public static class TicketMapper
    {
        public static TicketDto MapFilterTicket(this SupportTicketAgg ticketAgg)
        {
            if (ticketAgg == null)
                return null;

            return new TicketDto()
            {
                Id = ticketAgg.Id,
                CreationDate = ticketAgg.CreationDate,
                Status = ticketAgg.Status,
                UserId = ticketAgg.UserId,
                Message = ticketAgg.Message,
                Title = ticketAgg.Title,
                Replies = ticketAgg.Replies.Select(r => new TicketReplyDto
                {
                    AdminId = r.AdminId,
                    Message = r.Message,

                }).ToList()
            };
        }
    }
}
