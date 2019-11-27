using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Commands.Create;
using HumanitarianAssistance.Application.HR.Commands.Update;
using HumanitarianAssistance.Application.HR.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.HR
{
    [Produces("application/json")]
    [Route("api/HRConfiguration/[Action]")]
    [Authorize]
    public class HRConfigurationController: BaseController
    {


        [HttpPost]
        public async Task<IActionResult> AddDesignationDetail([FromBody]AddDesignationDetailCommand command)
        {
            command.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedDate = DateTime.UtcNow;
            return Ok(await _mediator.Send(command));
        }

        [HttpPost]
        public async Task<IActionResult> GetAllDesignationDetail([FromBody] GetAllDesignationDetailQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> EditDesignationDetail([FromBody]EditDesignationDetailCommand command)
        {
            command.ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedDate = DateTime.UtcNow;
            return Ok(await _mediator.Send(command));
        }

        [HttpPost]
        public async Task<IActionResult> GetEducationDegreeList([FromBody] GetAllEducationDegreeQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> AddEducationDegree([FromBody] AddEducationDegreeCommand request)
        {
            request.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.CreatedDate = DateTime.UtcNow;
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> EditEducationDegree([FromBody] EditEducationDegreeCommand request)
        {
            request.ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.ModifiedDate = DateTime.UtcNow;
            return Ok(await _mediator.Send(request));
        }
    }
}