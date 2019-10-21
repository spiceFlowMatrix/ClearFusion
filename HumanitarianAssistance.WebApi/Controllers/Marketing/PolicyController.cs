using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Commands.Common;
using HumanitarianAssistance.Application.Marketing.Commands.Create;
using HumanitarianAssistance.Application.Marketing.Commands.Delete;
using HumanitarianAssistance.Application.Marketing.Queries;
using HumanitarianAssistance.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Controllers.Marketing
{
    [Produces("application/json")]
    [Route("api/Policy/[Action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PolicyController : BaseController
    {
        private readonly JsonSerializerSettings _serializerSettings;
        
        public PolicyController()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        [HttpGet]
        public async Task<ApiResponse> GetPolicyList()
        {
            return await _mediator.Send(new GetPolicyListQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllSchedule()
        {
            return await _mediator.Send(new GetAllScheduleQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> GetScheduleByDate([FromBody]string model)
        {
            return await _mediator.Send(new GetScheduleByDateQuery
            {
                date = model
            });
        }

        [HttpPost]
        public async Task<ApiResponse> GetFilteredPolicylist([FromBody]GetFilteredPolicyListQuery model)
        {
            return await _mediator.Send(model);
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
        public async Task<ApiResponse> GetPolicyPaginatedList([FromBody]GetPolicyPaginatedListQuery model)
        {
            return await _mediator.Send(model);
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
        public async Task<ApiResponse> GetPolicyById([FromBody]int model)
        {
            return await _mediator.Send(new GetPolicyByIdQuery
            {
                PolicyId = model
            });
        }

        [HttpPost]
        public async Task<ApiResponse> GetPolicyTimeScheduleList([FromBody] int Id)
        {
            return await _mediator.Send(new GetPolicyTimeScheduleListQuery
            {
                Id = Id
            });
        }

        [HttpPost]
        public async Task<ApiResponse> GetPolicyTimeScheduleById([FromBody] int Id)
        {
            return await _mediator.Send(new GetPolicyTimeScheduleByIdQuery
            {
                Id = Id
            });
        }

        [HttpPost]
        public async Task<ApiResponse> GetDayScheduleByPolicyId([FromBody] int Id)
        {
            return await _mediator.Send(new GetDayScheduleByPolicyIdQuery
            {
                Id = Id
            });
        }

        [HttpPost]
        public async Task<ApiResponse> AddPolicyRepeatDays([FromBody] AddEditPolicyRepeatDaysCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditPolicy([FromBody]AddEditPolicyCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditPolicyTimeSchedule([FromBody] AddEditPolicyTimeScheduleCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
        public async Task<ApiResponse> PolicySchedules([FromBody]AddEditPolicySchedulesCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> AddSchedule([FromBody]AddEditPolicySchedulesCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditPolicyOrderSchedule([FromBody] AddEditPolicyOrderScheduleCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> DeletePolicy([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeletePolicyCommand
            {
                PolicyId = model,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }

        [HttpPost]
        public async Task<ApiResponse> DeletePolicyTimeSchedule([FromBody] int Id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeletePolicyTimeScheduleCommand
            {
                Id = Id,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }
    }
}
