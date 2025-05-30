using Microsoft.EntityFrameworkCore;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities.Repositories;

internal class SiteSettingRepository : BaseRepository<SiteSetting>, ISiteSettingRepository
{
    public SiteSettingRepository(ShopContext context) : base(context)
    {
        
    }

    public async Task<bool> ExistSiteSetting(string key)
    {
        return await Context.SiteSettings.AnyAsync(s => s.Key == key);
    }
}