using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Wallet.Api.Middlewares.ExceptionsMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext context) => this.InvokeAsync(context);
        private async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorDetails = ConfigurateExceptionTypes(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorDetails.StatusCode;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDetails));
        }

        private ErrorModel ConfigurateExceptionTypes(Exception exception)
        {
            ErrorModel errorModel = new ErrorModel();
            // Exception type To Http Status configuration 
            switch (exception)
            {
                case var _ when exception is ArgumentException:
                case var _ when exception is ArgumentNullException:
                    {
                        errorModel.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorModel.Message = exception.Message;
                        break;
                    }
                case var _ when exception is InvalidOperationException:
                    {
                        errorModel.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorModel.Message = exception.Message;
                        break;
                    }
                default:
                    {
                        errorModel.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorModel.Message = $"Internal Server Error. {exception.Message}";
                        _logger.LogError($"Exception occured:{exception.GetType()} {exception.Message} {exception.StackTrace}");
                        break;
                    }
            }

            return errorModel;
        }
    }
}
