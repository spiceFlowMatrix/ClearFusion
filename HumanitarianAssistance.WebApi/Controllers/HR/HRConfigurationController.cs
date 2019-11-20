using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Commands.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.HR
{
    [Produces("application/json")]
    [Route("api/EmployeePayroll/[Action]")]
    [Authorize]
    public class HRConfigurationController: BaseController
    {


        [HttpPost]
        public async Task<IActionResult> AddDesignationDetail([FromBody]AddDesignationDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return Ok(await _mediator.Send(command));
        }
    }
}