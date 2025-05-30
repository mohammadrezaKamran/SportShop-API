using Common.Domain.Repository;
using Shop.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.SiteEntities.Repositories
{
    public interface ISiteSettingRepository:IBaseRepository<SiteSetting>
    {
        Task<bool> ExistSiteSetting(string key);
    }
}
