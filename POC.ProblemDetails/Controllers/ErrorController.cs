using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;

namespace POC.ProblemDetails.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Automatic mapping to problem details using <see cref="ProblemDetailsOptions.MapToStatusCode{TException}"/>
        /// </summary>
        [HttpGet("exception")]
        public Task Exception()
        {
            throw new InvalidOperationException("Error Ipsum");
        }

        /// <summary>
        /// Map the custom property to details
        /// </summary>
        [HttpGet("custom-exception")]
        public Task CustomException()
        {
            throw new CustomDomainException
            {
                CustomProperty = "Custom error"
            };
        }

        /// <summary>
        /// Automatic mapping to problem details when ApiController attribute is used
        /// </summary>
        [HttpGet("default")]
        public ActionResult DefaultError()
        {
            return Conflict();
        }

        /// <summary>
        /// Automatic mapping to problem details using <see cref="MvcBuilderExtensions.AddProblemDetailsConventions"/>
        /// </summary>
        [HttpGet("convention")]
        public ActionResult Convention()
        {
            return Conflict("Problem details convention");
        }

        /// <summary>
        /// Automatic mapping to problem details using <see cref="MvcBuilderExtensions.AddProblemDetailsConventions"/>
        /// </summary>
        [HttpGet("validation-model")]
        public ActionResult ValidationModel()
        {
            ModelState.AddModelError("validation", "validation error message");
            return Conflict(ModelState);
        }
    }
}
