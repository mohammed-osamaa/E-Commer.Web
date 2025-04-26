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
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Something went Wrong.");
                // Set Status Code
                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };
                // Set Content Type
                context.Response.ContentType = "application/json";
                // Set Response Body
                var response = new ResponseToReturn
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                // Serialize the response to JSON
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
