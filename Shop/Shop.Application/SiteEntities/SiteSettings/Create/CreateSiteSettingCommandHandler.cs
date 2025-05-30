using Common.Application;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.SiteSettings.Create;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;

public class CreateSiteSettingCommandHandler : IBaseCommandHandler<CreateSiteSettingCommand>
{
    private readonly ISiteSettingRepository _siteSettingRepository;

    public CreateSiteSettingCommandHandler(ISiteSettingRepository siteSettingRepository)
    {
        _siteSettingRepository = siteSettingRepository;
    }

    public async Task<OperationResult> Handle(CreateSiteSettingCommand request, CancellationToken cancellationToken)
    {
        var exists = await _siteSettingRepository.ExistSiteSetting(request.Key);
        if (exists)
            return OperationResult.Error("کلیدی با این مقدار قبلاً ثبت شده است.");

        var setting = new SiteSetting(request.Key, request.Value, request.Group, request.Description);

        _siteSettingRepository.Add(setting);
        await _siteSettingRepository.Save();

        return OperationResult.Success();
    }
}
