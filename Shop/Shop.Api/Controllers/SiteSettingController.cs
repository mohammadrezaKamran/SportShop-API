﻿using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Comments.Create;
using Shop.Application.SiteEntities.SiteSettings.Create;
using Shop.Application.SiteEntities.SiteSettings.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Comments;
using Shop.Presentation.Facade.Roles;
using Shop.Presentation.Facade.Siteentities.SiteSettings;
using Shop.Query.Comments.DTOs;
using Shop.Query.Roles.DTOs;
using Shop.Query.SiteEntities.SiteSettings.Dtos;

namespace Shop.Api.Controllers
{

    public class SiteSettingController : ApiController
    {
        private readonly ISiteSettingFacade _siteSettingFacade;

        public SiteSettingController(ISiteSettingFacade siteSettingFacade)
        {
            _siteSettingFacade = siteSettingFacade;
        }


        [HttpGet("SeoData")]
        public async Task<ApiResult<SeoDataDto>> GetSeoData([FromQuery] string pageKey)
        {
            var result = await _siteSettingFacade.GetSeoDataForPage(pageKey);
            return QueryResult(result);

        }
		[PermissionChecker(Permission.PanelAdmin)]
		[HttpPost]
        public async Task<ApiResult> CreateSiteSetting(CreateSiteSettingCommand command)
        {
            var result = await _siteSettingFacade.CreateSiteSetting(command);
            return CommandResult(result);
        }
		[PermissionChecker(Permission.PanelAdmin)]
		[HttpPut]
        public async Task<ApiResult> EditSiteSetting(EditSiteSettingCommand command)
        {
            var result = await _siteSettingFacade.EditSiteSetting(command);
            return CommandResult(result);
        }

        [HttpGet]
        public async Task<ApiResult<List<SiteSettingDto>>> GetSiteSetting()
        {
            var result = await _siteSettingFacade.GetSiteSettings();
            return QueryResult(result);
        }

		[HttpGet("ByGroup")]
		public async Task<ApiResult<List<SiteSettingDto>>> GetSiteSettingsByGroup([FromQuery]SiteSettingGroup group)
		{
			var result = await _siteSettingFacade.GetSiteSettingsByGroup(group);
			return QueryResult(result);
		}


		[HttpGet("ById{id}")]
        public async Task<ApiResult<SiteSettingDto>> GetSiteSettingById(long id)
        {
            var result = await _siteSettingFacade.GetSiteSettingById(id);
            return QueryResult(result);
        }

        [HttpGet("ByKey{key}")]
        public async Task<ApiResult<SiteSettingDto>> GetSiteSettingByKey(string key)
        {
            var result = await _siteSettingFacade.GetSiteSettingByKey(key);
            return QueryResult(result);
        }
    }
}