using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Shop.Query.Ticket.GetByFilter
{
    internal class GetTicketByFilterQueryHandler : IQueryHandler<GetTicketByFilterQuery, TicketFilterResult>
    {
        private readonly ShopContext _context;

        public GetTicketByFilterQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<TicketFilterResult> Handle(GetTicketByFilterQuery request, CancellationToken cancellationToken)
        {
            var @params = request.FilterParams;

            var result = _context.SupportTickets.AsNoTracking().OrderByDescending(d => d.CreationDate).Include(d => d.Replies).AsQueryable();

            if (@params.UserId.HasValue)
                result = result.Where(r => r.UserId == @params.UserId.Value);

            if (@params.AdminId.HasValue)
                result = result.Where(t => t.Replies.Any(r => r.AdminId == @params.AdminId.Value));

            if (@params.TicketStatus.HasValue)
                result = result.Where(r => r.Status == @params.TicketStatus.Value);

            if (@params.StartDate.HasValue)
                result = result.Where(r => r.CreationDate.Date >= @params.StartDate.Value.Date);

            if (@params.EndDate.HasValue)
                result = result.Where(r => r.CreationDate.Date <= @params.EndDate.Value.Date);

            if (!string.IsNullOrWhiteSpace(@params.Title))
                result = result.Where(r => r.Title.Contains(@params.Title));

            var skip = (@params.PageId - 1) * @params.Take;
            var model = new TicketFilterResult()
            {
                Data = await result.Skip(skip).Take(@params.Take)
                    .Select(t => t.MapFilterTicket())
                    .ToListAsync(cancellationToken),
                FilterParams = @params
            };
            model.GeneratePaging(result, @params.Take, @params.PageId);
            return model;
        }
    }
}
