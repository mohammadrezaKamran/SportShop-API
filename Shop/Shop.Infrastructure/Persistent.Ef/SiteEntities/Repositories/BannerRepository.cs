using Microsoft.EntityFrameworkCore;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities.Repositories
{
    internal class BannerRepository : BaseRepository<Banner>, IBannerRepository
    {
        public BannerRepository(ShopContext context) : base(context)
        {
        }

        public void Delete(Banner banner)
        {
            Context.Banners.Remove(banner);
        }

		public async Task<bool> IsOrderDuplicateAsync(BannerPosition position, int order, long? excludeId = null)
		{
			return await Context.Banners
		            .Where(b => b.Position == position && b.Order == order && b.IsActive)
		            .Where(b => !excludeId.HasValue || b.Id != excludeId.Value) // In Edit -> Except itself
		            .AnyAsync();
		}
	}
}