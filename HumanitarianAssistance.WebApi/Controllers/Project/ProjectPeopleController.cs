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
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Controllers.Project
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/ProjectPeople/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Project))]
    public class ProjectPeopleController : BaseController
    {
        [HttpPost]
        public async Task<ApiResponse> GetOpportunityControlList([FromBody] long projectId)
        {
            return await _mediator.Send(new GetOpportunityControlListQuery { projectId = projectId });
        }
        [HttpPost]
        public async Task<ApiResponse> GetLogisticsControlList([FromBody] long projectId)
        {
            return await _mediator.Send(new GetLogisticsControlListQuery { projectId = projectId });
        }
        [HttpPost]
        public async Task<ApiResponse> GetActivitiesControl([FromBody] long projectId)
        {
            return await _mediator.Send(new GetActivitiesControlListQuery { projectId = projectId });
        }
        [HttpPost]
        public async Task<ApiResponse> GetActivitiesControlPermission([FromBody] long projectId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new GetActivitiesControlPermissionQuery
            {
                projectId = projectId,
                userId = userId
            });
        }
        [HttpPost]
        public async Task<ApiResponse> GetHiringControl([FromBody] long projectId)
        {
            return await _mediator.Send(new GetHiringControlListQuery { projectId = projectId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddOpportunityControl([FromBody]AddOpportunityControlCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command); 
        }
        [HttpPost]
        public async Task<ApiResponse> EditOpportunityControl([FromBody]EditOpportunityControlCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow; 
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteOpportunityControl([FromBody] long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteOpportunityControlCommand
            {
                id = id,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            }); 
        }
        [HttpPost]
        public async Task<ApiResponse> AddLogisticsControl([FromBody]AddLogisticsControlCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; 
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command); 
        }
        [HttpPost]
        public async Task<ApiResponse> EditLogisticsControl([FromBody]EditLogisticsControlCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId; 
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command); 
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteLogisticsControl([FromBody] long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteLogisticsControlCommand 
            {
                id = id,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            }); 
        }
        [HttpPost]
        public async Task<ApiResponse> AddActivitiesControl([FromBody]AddActivitiesControlCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow; 
            return await _mediator.Send(command); 
        } 
        [HttpPost]
        public async Task<ApiResponse> EditActivitiesControl([FromBody]EditActivitiesControlCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command); 
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteActivitiesControl([FromBody] long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteActivitiesControlCommand
            {
                id = id,
                ModifiedById = userId, 
                ModifiedDate = DateTime.UtcNow
            }); 
        }
        [HttpPost]
        public async Task<ApiResponse> AddHiringControl([FromBody]AddHiringControlCommand command)
        { 
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command); 
        }
        [HttpPost]
        public async Task<ApiResponse> EditHiringControl([FromBody]EditHiringControlCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command); 
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteHiringControl([FromBody] long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteHiringControlCommand
            {
                id = id,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow 
            });
        }
    }
}


