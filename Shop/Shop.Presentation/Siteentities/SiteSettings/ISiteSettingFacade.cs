using Common.Application;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Application.SiteEntities.SiteSettings.Create;
using Shop.Application.SiteEntities.SiteSettings.Edit;
using Shop.Query.SiteEntities.DTOs;
using Shop.Query.SiteEntities.SiteSettings.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Presentation.Facade.Siteentities.SiteSettings
{
    public interface ISiteSettingFacade
    {

        Task<OperationResult> CreateSiteSetting(CreateSiteSettingCommand command);
        Task<OperationResult> EditSiteSetting(EditSiteSettingCommand command);

        Task<SiteSettingDto> GetSiteSettingById(long id);
        Task<SiteSettingDto> GetSiteSettingByKey(string key);
        Task<List<SiteSettingDto>> GetSiteSettings();
		Task<List<SiteSettingDto>> GetSiteSettingsByGroup(SiteSettingGroup Group);
		Task<SeoDataDto> GetSeoDataForPage(string pageKey);
    }
}
