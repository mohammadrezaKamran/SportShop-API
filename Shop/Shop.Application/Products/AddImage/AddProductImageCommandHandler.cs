using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;

namespace Shop.Application.Products.AddImage
{
    internal class AddProductImageCommandHandler : IBaseCommandHandler<AddProductImageCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IFileService _fileService;

        public AddProductImageCommandHandler(IProductRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
        {
			var isSequenceDuplicate = await _repository
	                .IsSequenceDuplicateAsync(request.ProductId, request.Sequence);

			if (isSequenceDuplicate)
				return OperationResult.Error("اولویت نمایش تصویر تکراری است.");

			var product = await _repository.GetTracking(request.ProductId);
            if (product == null)
                return OperationResult.NotFound();

            var imageName = await _fileService
                .SaveFileAndGenerateName(request.ImageFile, Directories.ProductGalleryImage);

            var productImage = new ProductImage(imageName, request.Sequence,request.AltText);
            product.AddImage(productImage);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}