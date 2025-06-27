using Common.Domain.ValueObjects;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.SiteEntities.SiteSettings.Dtos;
using Shop.Query.SiteEntities.SiteSettings.GetSeoDataForPages;
using System.Text.Json;

public class GetSeoDataForPagesQueryHandler : IQueryHandler<GetSeoDataForPagesQuery, SeoDataDto>
{
    private readonly ShopContext _context;

    public GetSeoDataForPagesQueryHandler(ShopContext shopContext)
    {
        _context = shopContext;
    }

    public async Task<SeoDataDto> Handle(GetSeoDataForPagesQuery request, CancellationToken cancellationToken)
    {
        var settings = await _context.SiteSettings
         .Where(s => s.Key.StartsWith(request.PageKey + "."))
         .ToListAsync();

        if (!settings.Any()) return null;

        var seoData = new SeoDataDto
        {
            MetaTitle = settings.FirstOrDefault(s => s.Key == $"{request.PageKey}.MetaTitle")?.Value,
            MetaDescription = settings.FirstOrDefault(s => s.Key == $"{request.PageKey}.MetaDescription")?.Value,
            MetaKeyWords = settings.FirstOrDefault(s => s.Key == $"{request.PageKey}.MetaKeyWords")?.Value,
            Canonical = settings.FirstOrDefault(s => s.Key == $"{request.PageKey}.Canonical")?.Value,
            IndexPage = bool.TryParse(
                        settings.FirstOrDefault(s => s.Key == $"{request.PageKey}.IndexPage")?.Value,
                        out var result) && result,
            Schema = settings.FirstOrDefault(s => s.Key == $"{request.PageKey}.Schema")?.Value,
        };

        return seoData;

    }
}
