using Common.Application;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.SiteEntities.SiteSettings.Dtos;
using Shop.Query.SiteEntities.SiteSettings.GetAll;

public class GetSiteSettingListQueryHandler : IQueryHandler<GetSiteSettingListQuery, List<SiteSettingDto>>
{
    private readonly ShopContext _context;

    public GetSiteSettingListQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<SiteSettingDto>> Handle(GetSiteSettingListQuery request, CancellationToken cancellationToken)
    {
        var settings= await _context.SiteSettings.OrderByDescending(d=>d.CreationDate).Select(s => new SiteSettingDto
        {
            Id = s.Id,
            Key = s.Key,
            Value = s.Value,
            Description = s.Description,
            Group = s.Group,
            CreationDate = s.CreationDate,
        }).ToListAsync(cancellationToken);

        return settings;
    }
}
