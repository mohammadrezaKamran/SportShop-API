using Common.Query;
using Common.Query.Filter;
using Shop.Domain.CommentAgg;
using Shop.Domain.SupportTicketAgg;
using Shop.Query.Comments.DTOs;
using Shop.Query.Ticket.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Ticket.DTOs
{
    public class TicketDto:BaseDto
    {
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public TicketStatus Status { get; set; }
        public List<TicketReplyDto> Replies { get; set; }
    }

    public class TicketReplyDto:BaseDto
    {
        public long AdminId { get; set; }
        public string Message { get; set; }
    }
}
public class TicketFilterParams : BaseFilterParam
{
    public long? UserId { get; set; }
    public long? AdminId { get; set; }
    public string? Title { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public TicketStatus? TicketStatus { get; set; }

}
public class TicketFilterResult : BaseFilter<TicketDto, TicketFilterParams>
{

}
