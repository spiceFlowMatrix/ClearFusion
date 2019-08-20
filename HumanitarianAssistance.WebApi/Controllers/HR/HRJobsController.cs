using HumanitarianAssistance.Application.HR.Commands.Create;
using HumanitarianAssistance.Application.HR.Commands.Update;
using HumanitarianAssistance.Application.HR.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Controllers.HR
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/HRJobs/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Hr))]
    [Authorize]
    public class HRJobsController : BaseController
    {
        [HttpPost]
        public async Task<ApiResponse> AddJobHiringDetail([FromBody]AddJobHiringDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> EditJobHiringDetail([FromBody]EditJobHiringDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllJobHiringDetails(int OfficeId)
        {
            return await _mediator.Send(new GetAllJobHiringDetailsQuery()
            {
                OfficeId = OfficeId
            });
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllJobGrade()
        {
            return await _mediator.Send(new GetAllJobGradeQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetJobCode(int officeId)
        {
            return await _mediator.Send(new GetJobCodeQuery { OfficeId = officeId});
        }
    }
}

