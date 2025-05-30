using AngleSharp.Dom;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.CommentAgg;
using Shop.Infrastructure.Persistent.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Report.UserReport.GetLatestComment
{
    internal class GetLatestCommentQueryHandler : IQueryHandler<GetLatestCommentQuery, List<LatestCommentDto>>
    {
        private readonly ShopContext _context;

        public GetLatestCommentQueryHandler(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<LatestCommentDto>> Handle(GetLatestCommentQuery request, CancellationToken cancellationToken)
        {
            return await _context.Comments
            .OrderByDescending(c => c.CreationDate)
            .Take(10)
            .Select(c => new LatestCommentDto
            {
                Id = c.Id,
                ProductId = c.ProductId,
                Comment = c.Text,
                CreationDate = c.CreationDate
            }).ToListAsync(cancellationToken);
        }
    }
}
