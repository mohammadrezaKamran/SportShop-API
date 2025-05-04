using Common.Domain;

namespace Shop.Domain.SupportTicketAgg
{
    public class TicketReply:BaseEntity
    {
        public long AdminId { get; private set; }
        public string Message { get; private set; }

        public TicketReply(long adminId, string message)
        {
            AdminId = adminId;
            Message = message;
        }
    }
}


