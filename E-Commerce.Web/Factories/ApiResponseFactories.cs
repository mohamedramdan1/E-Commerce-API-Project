using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactories
    {
        public static IActionResult GenerateApiValoidationErrorResponse(ActionContext Context)
        {
            var Error = Context.ModelState.Where(M => M.Value.Errors.Any())
                    .Select(M => new ValidationError()
                    {
                        Field = M.Key,
                        Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                    });

            var Response = new ValidationErrorToReturn()
            {
                ValidationError = Error
            };

            return new BadRequestObjectResult(Response);
        }
    }
}
