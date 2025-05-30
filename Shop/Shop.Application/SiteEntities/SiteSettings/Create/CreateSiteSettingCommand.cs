using Common.Application;
using Shop.Application.SiteEntities.Banners.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.SiteEntities.SiteSettings.Create
{
    public record CreateSiteSettingCommand(string Key,string Value,SiteSettingGroup? Group,string? Description) : IBaseCommand;
}
