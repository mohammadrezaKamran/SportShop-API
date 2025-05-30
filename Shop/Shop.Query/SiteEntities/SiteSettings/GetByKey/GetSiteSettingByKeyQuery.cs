using Common.Query;
using Shop.Query.SiteEntities.Banners.GetById;
using Shop.Query.SiteEntities.DTOs;
using Shop.Query.SiteEntities.SiteSettings.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.SiteEntities.SiteSettings.GetByKey
{
    public record GetSiteSettingByKeyQuery(string key) : IQuery<SiteSettingDto>;

}
