using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.SiteEntities.SiteSettings.Dtos;
using Shop.Query.SiteEntities.SiteSettings.GetByGroup;

public class GetSiteSettingByQueryHandler : IQueryHandler<GetSiteSettingByQuery, List<SiteSettingDto>>
{
	private ShopContext _context;

	public GetSiteSettingByQueryHandler(ShopContext context)
	{
		_context = context;
	}

	public async Task<List<SiteSettingDto>> Handle(GetSiteSettingByQuery request, CancellationToken cancellationToken)
	{
		var settings = await _context.SiteSettings.Where(c=>c.Group==request.Group).Select(s => new SiteSettingDto
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
