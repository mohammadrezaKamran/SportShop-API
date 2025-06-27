using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;
using Common.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Banners.Edit;

public class EditBannerCommandHandler : IBaseCommandHandler<EditBannerCommand>
{
	private readonly IBannerRepository _repository;
	private readonly IFileService _fileService;
	public EditBannerCommandHandler(IBannerRepository repository, IFileService fileService)
	{
		_repository = repository;
		_fileService = fileService;
	}

	public async Task<OperationResult> Handle(EditBannerCommand request, CancellationToken cancellationToken)
	{
		var banner = await _repository.GetTracking(request.Id);
		if (banner == null)
			return OperationResult.NotFound();

		bool isDuplicate = await _repository.IsOrderDuplicateAsync(
								position: request.Position,
								order: request.Order,
								excludeId: request.Id
							);

		if (isDuplicate)
			return OperationResult.Error("اولویت وارد شده تکراری است");


		var imageName = banner.ImageName;
		var oldImage = banner.ImageName;

		if (request.ImageFile.IsImage())
			imageName = await _fileService
				.SaveFileAndGenerateName(request.ImageFile, Directories.BannerImages);

		banner.Edit(request.Link, imageName, request.Position, request.Title, request.Description, request.IsActive, request.Order, request.AltText);

		DeleteOldImage(request.ImageFile, oldImage);
		await _repository.Save();
		return OperationResult.Success();
	}

	private void DeleteOldImage(IFormFile? imageFile, string oldImage)
	{
		if (imageFile.IsImage())
			_fileService.DeleteFile(Directories.BannerImages, oldImage);
	}
}