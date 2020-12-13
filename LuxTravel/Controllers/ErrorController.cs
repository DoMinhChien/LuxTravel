using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LuxTravel.Constants;
namespace LuxTravel.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;
            var businessException = exception as BusinessException;

            if (businessException != null)
            {
                var errorResult = new ErrorResult()
                {
                    StatusCode = GetStatusCode(businessException.Code),
                    ErrorResponse = new ErrorResponse()
                    {
                        Code = businessException.Code
                    }
                };

                Response.StatusCode = (int)errorResult.StatusCode;
                return errorResult;
            }

            return new ErrorResult(); // Your error model
        }

        private HttpStatusCode GetStatusCode(string code)
        {
            if (code == Constants.Constants.NOT_FOUND_CODE)
            {
                return HttpStatusCode.NotFound;
            }

            return HttpStatusCode.BadRequest;
        }
    }
}
