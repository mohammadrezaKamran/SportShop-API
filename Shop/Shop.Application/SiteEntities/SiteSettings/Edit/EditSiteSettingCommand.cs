using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.SiteEntities.SiteSettings.Edit
{
    public record EditSiteSettingCommand(long Id ,string Key, string Value, SiteSettingGroup? Group, string? Description) : IBaseCommand;
}
