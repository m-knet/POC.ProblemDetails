using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;

namespace POC.ProblemDetails.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {

        [HttpGet("exception")]
        public Task Exception()
        {
            // Automatic mapping to problem details using MapToStatusCode<InvalidOperationException>
            throw new InvalidOperationException("Error Ipsum");
        }

        [HttpGet("custom-exception")]
        public Task CustomException()
        {
            // Map the custom property to details
            throw new CustomDomainException {CustomProperty = "Custom error"};
        }

        [HttpGet("default")]
        public ActionResult DefaultError()
        {
            // Automatic mapping to problem details when ApiController attribute is used
            return Conflict();
        }

        [HttpGet("custom")]
        public ActionResult CustomError()
        {
            // Custom error
            return Conflict(new StatusCodeProblemDetails(StatusCodes.Status409Conflict)
            {
                Detail = "Custom error"
            });
        }

        [HttpGet("no-problem-details")]
        public ActionResult Avoid()
        {
            // No problem details, to avoid
            return Conflict("This will not return problem details");
        }
    }
}
