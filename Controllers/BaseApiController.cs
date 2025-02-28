using API.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    //[Authorize]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult InfoMessage(string message) => Ok(GetInfoObject(message));
        protected IActionResult SuccessMessage(string message) => Ok(GetSuccessObject(message));
        protected IActionResult WarningMessage(string message) => Ok(GetInfoObject(message));
        protected IActionResult DuplicateMessage(string message) => StatusCode(StatusCodes.Status200OK, GetFailedObject(message));
        protected IActionResult NotFoundMessage(string message) => StatusCode(StatusCodes.Status404NotFound, GetNotFoundObject(message));
        protected IActionResult ErrorMessage(string message) => StatusCode(StatusCodes.Status400BadRequest, GetFailedObject(message));
        protected IActionResult ErrorMessage(Exception ex) => StatusCode(StatusCodes.Status500InternalServerError, GetFailedObject(ex));

        private static ApiResponseModel<string> GetSuccessObject(string message)
        {
            return ApiResponseModel<string>.GetSuccessResponse(message);
        }
        private static ApiResponseModel<string> GetInfoObject(string message)
        {
            return ApiResponseModel<string>.GetInfoResponse(message);
        }
        private static ApiResponseModel<string> GetNotFoundObject(string message)
        {
            return ApiResponseModel<string>.GetInfoResponse(message);
        }
        private static ApiResponseModel<string> GetFailedObject(string message)
        {
            return ApiResponseModel<string>.GetErrorResponse(message);
        }
        private static ApiResponseModel<Exception> GetFailedObject(Exception ex)
        {
            return ApiResponseModel<Exception>.GetErrorResponse(ex.Message);
        }
    }
}
