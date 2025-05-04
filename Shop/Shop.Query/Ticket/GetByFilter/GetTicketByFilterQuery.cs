using Common.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Ticket.GetByFilter
{
    public class GetTicketByFilterQuery : QueryFilter<TicketFilterResult, TicketFilterParams>
    {
        public GetTicketByFilterQuery(TicketFilterParams filterParams) : base(filterParams) { }
    }
}
