using Common.Query;
using Shop.Domain.SiteEntities;

namespace Shop.Query.SiteEntities.DTOs;

public class BannerDto : BaseDto
{
    public string Link { get;  set; }
    public string ImageName { get;  set; }
    public BannerPosition Position { get;  set; }

	public string? Title { get; set; }
	public string? Description { get; set; }
	public bool IsActive { get; set; }
	public int Order { get; set; }
	public string AltText { get; set; }
}
 