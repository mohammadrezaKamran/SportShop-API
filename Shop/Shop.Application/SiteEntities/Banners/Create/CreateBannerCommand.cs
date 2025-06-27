using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities;

namespace Shop.Application.SiteEntities.Banners.Create;

public class CreateBannerCommand : IBaseCommand
{
  
    public string Link { get;  set; }
    public IFormFile ImageFile { get;  set; }
    public BannerPosition Position { get;  set; }

	public string? Title { get; set; }
	public string? Description { get; set; }
	public bool IsActive { get; set; }
	public int Order { get; set; }
	public string AltText { get; set; }
}