using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.CommentAgg;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments.GetByFilter;

internal class GetCommentByFilterQueryHandler : IQueryHandler<GetCommentByFilterQuery, CommentFilterResult>
{
    private readonly ShopContext _context;

    public GetCommentByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<CommentFilterResult> Handle(GetCommentByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var result = from comment in _context.Comments
                     join product in _context.Products on comment.ProductId equals product.Id
                     join user in _context.Users on comment.UserId equals user.Id
					 orderby comment.CreationDate descending
                     select new { comment, product, user };

        if (@params.ProductId != null)
            result = result.Where(r => r.comment.ProductId == @params.ProductId);

        if (@params.CommentStatus != null)
            result = result.Where(r => r.comment.Status == @params.CommentStatus);

        if (@params.UserId != null)
            result = result.Where(r => r.comment.UserId == @params.UserId);

        if (@params.StartDate != null)
            result = result.Where(r => r.comment.CreationDate.Date >= @params.StartDate.Value.Date);

        if (@params.EndDate != null)
            result = result.Where(r => r.comment.CreationDate.Date <= @params.EndDate.Value.Date);

        var skip = (@params.PageId - 1) * @params.Take;

        var model = new CommentFilterResult
        {
            Data = await result.Skip(skip).Take(@params.Take)
                .Select(r => new CommentDto
                {
                    Id = r.comment.Id,
                    CreationDate = r.comment.CreationDate,
                    ProductId = r.comment.ProductId,
                    UserId = r.comment.UserId,
                    Text = r.comment.Text,
                    Status = r.comment.Status,
                    ProductTitle = r.product.Title,
                    ImageName = r.product.ImageName,
                    UserFullName = r.user.Name + " " + r.user.Family
                })
                .ToListAsync(cancellationToken),

            FilterParams = @params
        };

        // باید از همان result اولیه برای شمارش استفاده کنیم
        model.GeneratePaging(result.Select(r => r.comment), @params.Take, @params.PageId);
        return model;
    }
}