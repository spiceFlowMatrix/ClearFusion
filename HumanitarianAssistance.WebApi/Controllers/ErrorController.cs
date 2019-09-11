
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Logger.Commands.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers
{
    public class ErrorController: BaseController
    {
        [Route("Error")]
        [AllowAnonymous]
        public async Task<ApiResponse> Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            LogErrorCommand model= new LogErrorCommand
            {
                Path= exceptionDetails.Path,
                StackTrace= exceptionDetails.Error.StackTrace,
                InnerException= exceptionDetails.Error.InnerException !=null? exceptionDetails.Error.InnerException.StackTrace : null,
                Message= exceptionDetails.Error.InnerException !=null?  exceptionDetails.Error.InnerException.Message : exceptionDetails.Error.Message
            };

            return await _mediator.Send(model);
        }
    }
}