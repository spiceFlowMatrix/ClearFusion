using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Store.Commands.Create;
using HumanitarianAssistance.Application.Store.Commands.Delete;
using HumanitarianAssistance.Application.Store.Commands.Update;
using HumanitarianAssistance.Application.Store.Queries;
using HumanitarianAssistance.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.Store
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/VehicleTracker/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.VehicleTracker))]
    [Authorize]
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
            var result = _mediator.Send(command);

            // if (result.Exception == null)
            // {
                 return Ok(await result);
            // }
            // else
            // {
            //     return BadRequest(result.Exception.InnerException.Message);
            // }
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
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

         [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeletePurchasedVehicle(long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
           
            var result = await Task.FromResult(_mediator.Send(new DeletePurchasedVehicleCommand { PurchasedVehicleId = id, ModifiedById= userId, ModifiedDate= DateTime.UtcNow }));

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
        public async Task<IActionResult> GetVehicleMonthlyBreakdownDataById(GetVehicleMonthlyBreakdownDataByIdQuery request)
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

    }
}