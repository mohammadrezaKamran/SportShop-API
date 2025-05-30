using System.Net;
using Common.Application.Validation;
using Common.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.AspNetCore.Middlewares;

public static class ApiCustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseApiCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiCustomExceptionHandlerMiddleware>();
    }
}

public class ApiCustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostingEnvironment _env;
    private readonly ILogger<ApiCustomExceptionHandlerMiddleware> _logger;

    public ApiCustomExceptionHandlerMiddleware(RequestDelegate next,
        IHostingEnvironment env,
        ILogger<ApiCustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _env = env;
        _logger = logger;
    }

	public async Task Invoke(HttpContext context)
	{
		string message = null;
		HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
		AppStatusCode apiStatusCode = AppStatusCode.ServerError;

		try
		{
			await _next(context);
		}
		catch (InvalidDomainDataException ex)
		{
			_logger.LogError(ex, ex.Message);
			httpStatusCode = HttpStatusCode.BadRequest;
			apiStatusCode = AppStatusCode.LogicError;
			SetErrorMessage(ex);
			await WriteToResponseAsync();
		}
		catch (InvalidCommandException ex)
		{
			_logger.LogError(ex, ex.Message);
			httpStatusCode = HttpStatusCode.BadRequest;
			apiStatusCode = AppStatusCode.LogicError;
			SetErrorMessage(ex);
			await WriteToResponseAsync();
		}
		catch (OperationCanceledException ex)
		{
			_logger.LogWarning(ex, "Request was cancelled.");
			httpStatusCode = HttpStatusCode.BadRequest;
			apiStatusCode = AppStatusCode.BadRequest;
			message = "Request was cancelled.";
			await WriteToResponseAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, ex.Message);
			httpStatusCode = HttpStatusCode.InternalServerError;
			apiStatusCode = AppStatusCode.ServerError;
			SetErrorMessage(ex);
			await WriteToResponseAsync();
		}

		void SetErrorMessage(Exception exception)
		{
			if (_env.IsDevelopment())
			{
				var dic = new Dictionary<string, string?>
				{
					["Exception"] = exception.Message,
					["StackTrace"] = exception.StackTrace
				};

				if (exception.InnerException != null)
				{
					dic.Add("InnerException.Exception", exception.InnerException.Message);
					dic.Add("InnerException.StackTrace", exception.InnerException.StackTrace);
				}

				message = JsonConvert.SerializeObject(dic, Formatting.Indented);
			}
			else
			{
				message = "An unexpected error occurred.";
			}
		}

		async Task WriteToResponseAsync()
		{
			if (context.Response.HasStarted)
			{
				_logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
				return;
			}

			var result = new ApiResult
			{
				IsSuccess = false,
				MetaData = new()
				{
					AppStatusCode = apiStatusCode,
					Message = message
				}
			};

			var json = JsonConvert.SerializeObject(result, Formatting.Indented);

			context.Response.StatusCode = (int)httpStatusCode;
			context.Response.ContentType = "application/json";
			await context.Response.WriteAsync(json);
		}
	}

}