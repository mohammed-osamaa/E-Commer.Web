using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commer.Web.Fatories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateValidationErrorsResponse(ActionContext context)
        {
            var errors = context.ModelState.Where(M => M.Value!.Errors.Any())
                                                  .Select(error => new ValidationError()
                                                  {
                                                      Field = error.Key,
                                                      Errors = error.Value!.Errors.Select(e => e.ErrorMessage)
                                                  });
            var response = new ValidationErrorToReturn()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "Validation Errors",
                ValidationErrors = errors
            };

            return new BadRequestObjectResult(response);
        }
    }
}
