using Common.Domain.ValueObjects;
using Common.Query;
using Shop.Query.SiteEntities.SiteSettings.Dtos;
using Shop.Query.SiteEntities.SiteSettings.GetByKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.SiteEntities.SiteSettings.GetSeoDataForPages
{
    public record GetSeoDataForPagesQuery(string PageKey) : IQuery<SeoDataDto>;

}
