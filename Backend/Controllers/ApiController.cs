using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BackendRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {
            // send all validation errors so user can correct form
            if (errors.All(e => e.Type == ErrorType.Validation))
            {
                var modelStateDictionary = new ModelStateDictionary();

                foreach (var error in errors)
                {
                    modelStateDictionary.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem(modelStateDictionary);
            }

            // return 500 if something unexpected happen
            if (errors.Any(e => e.Type == ErrorType.Unexpected))
            {
                return Problem();
            }

            // choose appropriate code 
            Error firstError = errors[0];

            var statusCode = firstError.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: statusCode, title: firstError.Description);
        }
    }
}
