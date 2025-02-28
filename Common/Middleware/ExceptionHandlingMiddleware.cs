using Newtonsoft.Json;
using System.Net;
using API.Common.CustomException;
using API.Models.Common;

namespace API.Common.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (context.Response.HasStarted)
            {
                return Task.CompletedTask;
            }

            string? result = null;
            result = JsonConvert.SerializeObject(ApiResponseModel<string>.GetErrorResponse(exception.Message));
            var code = HttpStatusCode.InternalServerError;

            if (exception is BadHttpRequestException or ApplicationException)
            {
                code = HttpStatusCode.BadRequest;
                if (exception.Message.Contains("Invalid Token"))
                {
                    code = HttpStatusCode.Forbidden;
                }
            }
            else if (exception is UnauthorizedAccessException)
            {
                code = HttpStatusCode.Unauthorized;
            }
            else if (exception is CustomValidationException)
            {
                code = HttpStatusCode.UnprocessableEntity;
                result = JsonConvert.SerializeObject(ApiResponseModel<string>.GetErrorResponse(((CustomValidationException)exception).Errors!));
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
