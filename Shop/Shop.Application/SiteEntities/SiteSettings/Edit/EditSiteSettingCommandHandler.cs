using Common.Application;
using Shop.Application.SiteEntities.SiteSettings.Edit;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;

public class EditSiteSettingCommandHandler : IBaseCommandHandler<EditSiteSettingCommand>
{
    private readonly ISiteSettingRepository _siteSettingRepository;

    public EditSiteSettingCommandHandler(ISiteSettingRepository siteSettingRepository)
    {
        _siteSettingRepository = siteSettingRepository;
    }

    public async Task<OperationResult> Handle(EditSiteSettingCommand request, CancellationToken cancellationToken)
    {
        var setting = await _siteSettingRepository.GetTracking(request.Id);
               if (setting == null)
            return OperationResult.NotFound();

        setting.UpdateValue(request.Value);
        setting.UpdateGroup(request.Group);
        setting.UpdateDescription(request.Description);

        await _siteSettingRepository.Save();
        return OperationResult.Success();
    }
}
