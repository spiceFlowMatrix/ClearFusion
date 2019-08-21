using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Commands.Common;
using HumanitarianAssistance.Application.Marketing.Commands.Delete;
using HumanitarianAssistance.Application.Marketing.Queries;
using HumanitarianAssistance.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HumanitarianAssistance.WebApi.Controllers.Marketing
{
    [Produces("application/json")]
    [Route("api/Scheduler/[Action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SchedulerController : Controller
    {
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly UserManager<AppUser> _userManager;
        private IMediator _mediator;
        public SchedulerController(UserManager<AppUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllPolicyScheduleList(string text)
        {
            return await _mediator.Send(new GetAllPolicyScheduleListQuery
            {
                text = text
            });
        }

        [HttpPost]
        public async Task<ApiResponse> GetScheduleDetailsById([FromBody] int model)
        {
            return await _mediator.Send(new GetScheduleDetailsByIdQuery
            {
                model = model
            });
        }

        [HttpPost]
        public async Task<ApiResponse> GetChannelById([FromBody]int model)
        {
            return await _mediator.Send(new GetChannelByIdQuery
            {
                model = model
            });
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllChannelList()
        {
            return await _mediator.Send(new GetAllChannelListQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> AddChannel([FromBody]AddEditChannelCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteChannel([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteChannelCommand
            {
                model=model,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllChannelListByMedium([FromBody]int model)
        {
            return await _mediator.Send(new GetAllChannelListByMediumQuery
            {
                model = model
            });
        }

        [HttpPost]
        public async Task<ApiResponse> AddSchedule([FromBody]AddEditScheduleCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteSchedule([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteScheduleCommand
            {
                model = model,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllScheduleList(string text)
        {
            return await _mediator.Send(new GetAllScheduleListQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> FilterScheduleList([FromBody]FilterScheduleListQuery model)
        {
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> AddPlayoutMinute([FromBody]AddEditPlayoutMinutesCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }
    }
}