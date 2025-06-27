using Common.Query;
using Shop.Query.SiteEntities.SiteSettings.Dtos;
using Shop.Query.SiteEntities.SiteSettings.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.SiteEntities.SiteSettings.GetByGroup
{
	public record GetSiteSettingByQuery(SiteSettingGroup Group):IQuery<List<SiteSettingDto>>;		
}
