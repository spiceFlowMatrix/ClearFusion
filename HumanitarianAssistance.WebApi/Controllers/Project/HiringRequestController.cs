using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Commands.Common;
using HumanitarianAssistance.Application.Project.Commands.Create;
using HumanitarianAssistance.Application.Project.Commands.Delete;
using HumanitarianAssistance.Application.Project.Commands.Update;
using HumanitarianAssistance.Application.Project.Queries;
using HumanitarianAssistance.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Controllers.Project
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/HiringRequest/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Project))]
    public class HiringRequestController : BaseController
    {

        [HttpPost]
        public async Task<ApiResponse> GetProjectHiringRequestDetail([FromBody]GetallHiringRequestDetailQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeList()
        {
            return await _mediator.Send(new GetAllEmployeeListQuery());
        }
        [HttpPost]
        public async Task<ApiResponse> GetHiringCandidatesListById([FromBody]GetAllCandidateListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost]
        public async Task<ApiResponse> AddHiringRequestDetail([FromBody]AddProjectHiringRequestCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> EditHiringRequestDetail([FromBody]EditHiringRequestDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> AddHiringRequestCandidate([FromBody]AddHiringRequestCandidateCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> EditHiringRequestCandidate([FromBody]EditHiringRequestCandidateCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> AddCandidateInterviewDetail([FromBody]AddCandidateInterviewDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> HiringRequestSelectCandidate([FromBody]HiringRequestSelectCandidateCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command); 
        }
        [HttpPost]
        public async Task<ApiResponse> CompleteHiringRequest([FromBody]long hiringRequestId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; 
            return await _mediator.Send(new CompleteHiringRequestCommand
            {
                hiringRequestId = hiringRequestId,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteCandidatDetail([FromBody]DeleteCandidateDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        } 
    }
}
