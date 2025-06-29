﻿using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.Exceptions;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Sliders.Create;

internal class CreateSliderCommandHandler : IBaseCommandHandler<CreateSliderCommand>
{
    private readonly IFileService _fileService;
    private readonly ISliderRepository _repository;

    public CreateSliderCommandHandler(IFileService fileService, ISliderRepository repository)
    {
        _fileService = fileService;
        _repository = repository;
    }

    public async Task<OperationResult> Handle(CreateSliderCommand request, CancellationToken cancellationToken)
    {
		if (await _repository.IsOrderDuplicateAsync(request.Order))
			throw new InvalidDomainDataException("اولویت انتخاب‌شده قبلاً استفاده شده است.");

		var imageName = await _fileService
            .SaveFileAndGenerateName(request.ImageFile, Directories.SliderImages);

        var slider = new Slider(request.Title,request.Link,imageName , request.Description , request.IsActive ,request.Order , request.AltText);

        _repository.Add(slider);
        await _repository.Save();
        return OperationResult.Success();
    }
}