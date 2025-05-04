using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Ticket.CreateTicket
{
    public record CreateTicketCommand(long userId, string title, string message) :IBaseCommand;

}
