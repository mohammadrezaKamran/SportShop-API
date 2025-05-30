using Common.Application;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.SiteEntities.SiteSettings.Dtos;
using Shop.Query.SiteEntities.SiteSettings.GetByKey;

public class GetSiteSettingByKeyQueryHandler : IQueryHandler<GetSiteSettingByKeyQuery, SiteSettingDto>
{
    private readonly ShopContext _context;

    public GetSiteSettingByKeyQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<SiteSettingDto> Handle(GetSiteSettingByKeyQuery request, CancellationToken cancellationToken)
    {
        var setting = await _context.SiteSettings.FirstOrDefaultAsync(c => c.Key == request.key , cancellationToken);

        if (setting == null)
            return new SiteSettingDto();

        return new SiteSettingDto
        {
            Id = setting.Id,
            Key = setting.Key,
            Value = setting.Value,
            Description = setting.Description,
            Group = setting.Group,
            CreationDate = setting.CreationDate,
        };
    }
}
