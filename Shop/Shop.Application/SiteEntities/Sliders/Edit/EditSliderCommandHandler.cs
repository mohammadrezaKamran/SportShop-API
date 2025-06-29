﻿using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Sliders.Edit;

internal class EditSliderCommandHandler : IBaseCommandHandler<EditSliderCommand>
{
    private readonly IFileService _fileService;
    private readonly ISliderRepository _repository;

    public EditSliderCommandHandler(IFileService fileService, ISliderRepository repository)
    {
        _fileService = fileService;
        _repository = repository;
    }
    public async Task<OperationResult> Handle(EditSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _repository.GetTracking(request.Id);
        if (slider == null)
            return OperationResult.NotFound();

		bool isDuplicate = await _repository.IsOrderDuplicateAsync(

							order: request.Order,
							excludeId: request.Id
						);

		if (isDuplicate)
			return OperationResult.Error("اولویت وارد شده تکراری است");


		var imageName = slider.ImageName;
        var oldImage = slider.ImageName;
        if (request.ImageFile != null)
            imageName = await _fileService
                .SaveFileAndGenerateName(request.ImageFile, Directories.SliderImages);

        slider.Edit(request.Title, request.Link, imageName, request.Description, request.IsActive, request.Order, request.AltText);

        await _repository.Save();
        DeleteOldImage(request.ImageFile, oldImage);
        return OperationResult.Success();
    }

    private void DeleteOldImage(IFormFile? imageFile, string oldImage)
    {
        if (imageFile != null)
            _fileService.DeleteFile(Directories.SliderImages, oldImage);
    }
}