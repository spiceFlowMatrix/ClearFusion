using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Configuration.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Commands.Create;
using HumanitarianAssistance.Application.Store.Commands.Update;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Application.Store.Queries;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.Store
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/VehicleTracker/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.VehicleTracker))]
    public class VehicleTrackerController: BaseController
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetVehicleList(GetVehicleListQuery request)
        {
            var result = await Task.FromResult(_mediator.Send(request));

            if (result.Exception == null)
            {
                return Ok(await result);
            }
            else
            {
                return BadRequest(result.Exception.InnerException.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddVehicleMileage([FromBody] AddVehicleMileageCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            var result = await Task.FromResult(_mediator.Send(command));

            if (result.Exception == null)
            {
                return Ok(await result);
            }
            else
            {
                return BadRequest(result.Exception.InnerException.Message);
            }
        }
        
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetVehicleById(long id)
        {
            var result = await Task.FromResult(_mediator.Send(new GetVehicleByIdQuery {VehicleId = id}));

            if (result.Exception == null)
            {
                return Ok(await result);
            }
            else
            {
                return BadRequest(result.Exception.InnerException.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> EditVehicleDetail(EditVehicleDetailCommand command)
        {
            var result = await Task.FromResult(_mediator.Send(command));

            if (result.Exception == null)
            {
                return Ok(await result);
            }
            else
            {
                return BadRequest(result.Exception.InnerException.Message);
            }
        }
    }
}