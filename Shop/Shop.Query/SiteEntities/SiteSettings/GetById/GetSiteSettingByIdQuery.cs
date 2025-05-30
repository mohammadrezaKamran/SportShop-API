using Common.Query;
using Shop.Query.SiteEntities.SiteSettings.Dtos;
using Shop.Query.SiteEntities.SiteSettings.GetByKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.SiteEntities.SiteSettings.GetById
{
    public record GetSiteSettingByIdQuery(long Id):IQuery<SiteSettingDto>;
}
