using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.SiteEntities
{
    public class Slider : BaseEntity
    {
        public string Title { get; private set; }
        public string Link { get; private set; }
        public string ImageName { get; private set; }

		public string? Description { get; private set; }
		public bool IsActive { get; private set; }=false;
		public int Order { get; private set; }
		public string AltText { get; private set; }


		public Slider(string title, string link, string imageName, string? description, bool isActive, int order, string altText)
		{
			Guard(title, link, imageName , altText , order);

			Title = title;
			Link = link;
			ImageName = imageName;
			Description = description;
			IsActive = isActive;
			Order = order;
			AltText = altText;
		}

		public void Edit(string title, string link, string imageName, string? description, bool isActive, int order, string altText)
        {
			Guard(title, link, imageName, altText, order);
			Title = title;
			Link = link;
			ImageName = imageName;
			Description = description;
			IsActive = isActive;
			Order = order;
			AltText = altText;
		}

        public void Guard(string title, string link, string imageName ,string altText ,int order)
        {
            NullOrEmptyDomainDataException.CheckString(link, nameof(link));
            NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));
			NullOrEmptyDomainDataException.CheckString(altText, nameof(altText));
			if (order <= 0 || order > 30)
			{
				throw new InvalidDomainDataException("اولویت تایین نشده است");
			}
		}
    }
}