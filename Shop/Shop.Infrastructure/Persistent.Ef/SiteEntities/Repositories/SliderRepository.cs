using Microsoft.EntityFrameworkCore;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.Ef.SiteEntities.Repositories;

internal class SliderRepository : BaseRepository<Slider>, ISliderRepository
{
    public SliderRepository(ShopContext context) : base(context)
    {
    }

    public void Delete(Slider slider)
    {
        Context.Sliders.Remove(slider);
    }

	public async Task<bool> IsOrderDuplicateAsync(int order, long? excludeId = null)
	{
		return await Context.Sliders
	   .Where(s => s.Order == order)
	   .Where(s => excludeId == null || s.Id != excludeId.Value)
	   .AnyAsync();
	}
}
