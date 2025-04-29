using Azure;
using DomainLayer.Exceptions;
using Shared.ErrorModels;

namespace E_Commer.Web.CustomExceptionHandlerMiddleware
{
    public class CustomExceptiomHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptiomHandlerMiddleware> _logger;

        public CustomExceptiomHandlerMiddleware(RequestDelegate Next , ILogger<CustomExceptiomHandlerMiddleware> logger)
        {
            _next = Next;
            this._logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                await ProcessNotFoundEndpoint(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went Wrong.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Set Response Body
            var response = new ResponseToReturn
            {
                ErrorMessage = ex.Message
            };
            // Set Status Code
            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnAuthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException => GetBadRequestErrors(badRequestException ,response),
                _ => StatusCodes.Status500InternalServerError
            };

            // Serialize the response to JSON And Handle the Content Type (Application/Json )
            await context.Response.WriteAsJsonAsync(response);
        }

        private static int GetBadRequestErrors(BadRequestException badRequestException , ResponseToReturn response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task ProcessNotFoundEndpoint(HttpContext context)
        {
            // Check if the response status code is 404
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ResponseToReturn
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = $"Endpoint {context.Request.Path} is not found."
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
