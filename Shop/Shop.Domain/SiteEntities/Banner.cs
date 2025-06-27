using Common.Domain;
using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.SiteEntities
{
	public class Banner : BaseEntity
	{
		public string Link { get; private set; }
		public string ImageName { get; private set; }
		public BannerPosition Position { get; private set; }

		public string? Title { get; private set; }
		public string? Description { get; private set; }
		public bool IsActive { get; private set; } = false;
		public int Order { get; private set; }
		public string AltText { get; private set; }

		public Banner(string link, string imageName, BannerPosition position, string? title, string? description, bool isActive, int order, string altText)
		{
			Guard(link, imageName, altText, order);

			Link = link;
			ImageName = imageName;
			Position = position;
			Title = title;
			Description = description;
			IsActive = isActive;
			Order = order;
			AltText = altText;
		}

		public void Edit(string link, string imageName, BannerPosition position, string? title, string? description, bool isActive, int order, string altText)
		{
			Guard(link, imageName, altText, order);

			Link = link;
			ImageName = imageName;
			Position = position;
			Title = title;
			Description = description;
			IsActive = isActive;
			Order = order;
			AltText = altText;
		}

		public void Guard(string link, string imageName, string altText, int order)
		{
			NullOrEmptyDomainDataException.CheckString(link, nameof(link));
			NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
			NullOrEmptyDomainDataException.CheckString(altText, nameof(altText));
			if (order <= 0 || order > 30)
			{
				throw new InvalidDomainDataException("اولویت تایین نشده است");
			}
		}
	}

	public enum BannerPosition
	{
		بالا_سمت_راست,
		بالا_سمت_چپ,
		پایین_سمت_راست,
		پایین_وسط,
		پایین_سمت_چپ
	}
}