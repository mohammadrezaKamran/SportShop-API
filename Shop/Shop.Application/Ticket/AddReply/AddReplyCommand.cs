using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Ticket.AddReply
{
    public record AddReplyCommand(long adminId,long ticketId,string message):IBaseCommand;

}
