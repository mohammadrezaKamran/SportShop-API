using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.GetFilterDetail;
using static GetProductByIdQueryHandler;

public class GetProductByIdQueryHandler : IQueryHandler<GetFilterDetailQuery, FilterDetailsDto?>
{
	private readonly ShopContext _context;

	public GetProductByIdQueryHandler(ShopContext context)
	{
		_context = context;
	}

	public async Task<FilterDetailsDto?> Handle(GetFilterDetailQuery request, CancellationToken cancellationToken)
	{
		var allSizes = _context.Products
		.SelectMany(p => p.ProductVariants)
		.Where(v => !string.IsNullOrEmpty(v.Size))
		.Select(v => v.Size)
		.Distinct()
		.ToList();

		var allColors = _context.Products
			.SelectMany(p => p.ProductVariants)
			.Where(v => !string.IsNullOrEmpty(v.Color))
			.Select(v => v.Color.Trim())
			.Distinct()
			.ToList();

		var allBrands = _context.Products
			.Where(p => !string.IsNullOrEmpty(p.BrandName))
			.Select(p => p.BrandName.Trim())
			.Distinct()
			.ToList();

		return new FilterDetailsDto()
		{
			AllSizes = allSizes,
			AllBrands = allBrands,
			AllColors = allColors,
		};
	}
}