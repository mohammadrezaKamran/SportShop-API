using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using Common.AspNetCore;
using Common.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;
using Shop.Api.Infrastructure;
using Shop.Api.Infrastructure.JwtUtil;
using Shop.Config;

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
	.Enrich.FromLogContext()
	.MinimumLevel.Debug()
	.CreateLogger();

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(option =>
    {
        option.InvalidModelStateResponseFactory = (context =>
        {
            var result = new ApiResult()
            {
                IsSuccess = false,
                MetaData = new()
                {
                    AppStatusCode = AppStatusCode.BadRequest,
                    Message = ModelStateUtil.GetModelStateErrors(context.ModelState)
                }
            };
            return new BadRequestObjectResult(result);
        });
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Enter Token",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    option.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.RegisterShopDependency(connectionString);
builder.Services.RegisterApiDependency(builder.Configuration);

CommonBootstrapper.Init(builder.Services);
builder.Services.AddTransient<IFileService, FileService>();

builder.Services.AddJwtAuthentication(builder.Configuration);

// SERILOG Instead of ILogger
builder.Host.UseSerilog();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders=ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("ShopApi");
app.UseAuthentication();
app.UseAuthorization();

app.UseApiCustomExceptionHandler();
app.MapControllers();

app.Run();
