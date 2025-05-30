using Common.Application;
using MediatR;
using Shop.Application.SiteEntities.SiteSettings.Create;
using Shop.Application.SiteEntities.SiteSettings.Edit;
using Shop.Presentation.Facade.Siteentities.SiteSettings;
using Shop.Query.SiteEntities.Banners.GetById;
using Shop.Query.SiteEntities.SiteSettings.Dtos;
using Shop.Query.SiteEntities.SiteSettings.GetAll;
using Shop.Query.SiteEntities.SiteSettings.GetById;
using Shop.Query.SiteEntities.SiteSettings.GetByKey;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class SiteSettingFacade : ISiteSettingFacade
{
    private readonly IMediator _mediator;

    public SiteSettingFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> CreateSiteSetting(CreateSiteSettingCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditSiteSetting(EditSiteSettingCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<SiteSettingDto> GetSiteSettingById(long id)
    {
        return await _mediator.Send(new GetSiteSettingByIdQuery(id));
    }

    public async Task<SiteSettingDto> GetSiteSettingByKey(string key)
    {
        return await _mediator.Send(new GetSiteSettingByKeyQuery(key));
    }

    public async Task<List<SiteSettingDto>> GetSiteSettings()
    {
        return await _mediator.Send(new GetSiteSettingListQuery());
    }
}
