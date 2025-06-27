using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.Exceptions;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Banners.Create;

public class CreateBannerCommandHandler : IBaseCommandHandler<CreateBannerCommand>
{
    private readonly IBannerRepository _repository;
    private readonly IFileService _fileService;
    public CreateBannerCommandHandler(IBannerRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
		if (await _repository.IsOrderDuplicateAsync(request.Position, request.Order))
			throw new InvalidDomainDataException("اولویت انتخاب‌شده قبلاً استفاده شده است.");

		var imageName = await _fileService
            .SaveFileAndGenerateName(request.ImageFile, Directories.BannerImages);
        var banner = new Banner(request.Link, imageName, request.Position, request.Title , request.Description ,request.IsActive ,request.Order , request.AltText);

        _repository.Add(banner);
        await _repository.Save();
        return OperationResult.Success();
    }
}