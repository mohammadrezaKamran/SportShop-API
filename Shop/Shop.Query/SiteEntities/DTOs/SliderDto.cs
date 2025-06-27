using Common.Query;

namespace Shop.Query.SiteEntities.DTOs;

public class SliderDto : BaseDto
{
    public string Title { get; set; }
    public string Link { get; set; }
    public string ImageName { get; set; }

	public string? Description { get; set; }
	public bool IsActive { get; set; }
	public int Order { get; set; }
	public string AltText { get; set; }
}