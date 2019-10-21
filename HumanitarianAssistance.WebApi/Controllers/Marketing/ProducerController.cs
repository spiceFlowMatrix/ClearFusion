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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Controllers.Marketing
{
    [Produces("application/json")]
    [Route("api/Producer/[Action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProducerController:BaseController
    {
        private readonly JsonSerializerSettings _serializerSettings;
        public ProducerController()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllProducerList()
        {
            return await _mediator.Send(new GetAllProducerListQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> GetProducerById([FromBody]int model)
        {
            return await _mediator.Send(new GetProducerByIdQuery
            {
                Id = model
            });
        }

        [HttpPost]
        public async Task<ApiResponse> AddProducer([FromBody]AddEditProducerCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteProducer([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteProducerCommand
            {
                Id = model,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }
    }
}
