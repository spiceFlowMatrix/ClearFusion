using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Store.Commands.Create;
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
    public class GeneratorTrackerController: BaseController
    {
        

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGeneratorList(GetVehicleListQuery request)
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