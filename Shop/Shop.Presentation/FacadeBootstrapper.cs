using Microsoft.Extensions.DependencyInjection;
using Shop.Presentation.Facade.Categories;
using Shop.Presentation.Facade.Comments;
using Shop.Presentation.Facade.Orders;
using Shop.Presentation.Facade.Products;
using Shop.Presentation.Facade.Roles;
using Shop.Presentation.Facade.SiteEntities.Banner;
using Shop.Presentation.Facade.Siteentities.ShippingMethods;
using Shop.Presentation.Facade.SiteEntities.Slider;
using Shop.Presentation.Facade.Users;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.Domain.Auth.OTP;
using Common.Application.Validation;
using Shop.Presentation.Facade.Report;
using Shop.Presentation.Facade.Siteentities.SiteSettings;

namespace Shop.Presentation.Facade;

public static class FacadeBootstrapper
{
    public static void InitFacadeDependency(this IServiceCollection services)
    {
        services.AddScoped<ICategoryFacade, CategoryFacade>();
        services.AddScoped<ICommentFacade, CommentFacade>();
        services.AddScoped<IOrderFacade, OrderFacade>();
        services.AddScoped<IProductFacade, ProductFacade>();
        services.AddScoped<IRoleFacade, RoleFacade>();
        services.AddScoped<IBannerFacade, BannerFacade>();
        services.AddScoped<ISliderFacade, SliderFacade>();
        services.AddScoped<IShippingMethodFacade, ShippingMethodFacade>();
        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<IUserAddressFacade, UserAddressFacade>();
        services.AddScoped<IReportFacade, ReportFacade>();
        services.AddScoped<ISiteSettingFacade, SiteSettingFacade>();
    }
}