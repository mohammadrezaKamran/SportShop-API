﻿using Common.Application.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Auth.OTP;
using Shop.Domain.CategoryAgg;
using Shop.Domain.CommentAgg;
using Shop.Domain.OrderAgg.Repository;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.RoleAgg.Repository;
using Shop.Domain.SiteEntities;
using Shop.Domain.SiteEntities.Repositories;
using Shop.Domain.UserAgg.Repository;
using Shop.Infrastructure._Utilities.MediatR;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Infrastructure.Persistent.Ef.CategoryAgg;
using Shop.Infrastructure.Persistent.Ef.CommentAgg;
using Shop.Infrastructure.Persistent.Ef.OrderAgg;
using Shop.Infrastructure.Persistent.Ef.OTP;
using Shop.Infrastructure.Persistent.Ef.ProductAgg;
using Shop.Infrastructure.Persistent.Ef.RoleAgg;
using Shop.Infrastructure.Persistent.Ef.SiteEntities.Repositories;
using Shop.Infrastructure.Persistent.Ef.UserAgg;
using Shop.Infrastructure.Services.Sms;

namespace Shop.Infrastructure
{
    public class InfrastructureBootstrapper
    {
        public static void Init(IServiceCollection services,string connectionString)
        {
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IBannerRepository, BannerRepository>();
            services.AddTransient<ISliderRepository, SliderRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IShippingMethodRepository, ShippingMethodRepository>();
            services.AddTransient<ISiteSettingRepository, SiteSettingRepository>();

            services.AddSingleton<ICustomPublisher, CustomPublisher>();
            services.AddScoped<ISmsSender,KaveNegarSmsSender>();
            services.AddScoped<IOtpRepository, OtpRepository>();

            services.AddTransient(_ => new DapperContext(connectionString));
            services.AddDbContext<ShopContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
        }
    }
}