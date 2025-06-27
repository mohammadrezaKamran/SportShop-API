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
            var query = await (
                 from comment in _context.Comments
                 join product in _context.Products on comment.ProductId equals product.Id
                 join user in _context.Users on comment.UserId equals user.Id
                 orderby comment.CreationDate descending
                 select new LatestCommentDto
                 {
                     Id = comment.Id,
                     Comment = comment.Text,
                     CreationDate = comment.CreationDate,
                     ProductTitle = product.Title,
                     ImageName = product.ImageName,
                     UserName = user.Name
                 }).Take(10).ToListAsync(cancellationToken);

            return query;
        }
    }
}
