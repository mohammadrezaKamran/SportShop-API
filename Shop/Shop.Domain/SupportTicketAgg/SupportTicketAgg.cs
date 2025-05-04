using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.SupportTicketAgg
{
    public class SupportTicketAgg : AggregateRoot
    { 
        public long UserId { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }
        public List<TicketReply> Replies { get; private set; } = new();
        public TicketStatus Status { get; private set; }

        private SupportTicketAgg() { }
        public SupportTicketAgg(long userId, string title, string message)
        {
            UserId = userId;
            Title = title;
            Message = message;
            Status = TicketStatus.Open;
        }

        public void AddReply(long adminId, string message)
        {
            Replies.Add(new TicketReply(adminId, message));
            Status = TicketStatus.Answered;
        }
    }

    public enum TicketStatus
    {
        Open,
        Answered,
        Closed
    }
}


