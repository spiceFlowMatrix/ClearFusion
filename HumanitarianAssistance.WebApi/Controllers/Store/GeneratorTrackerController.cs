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
    [Route("api/GeneratorTracker/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.GeneratorTracker))]
    [Authorize]
    public class GeneratorTrackerController : BaseController
    {


        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGeneratorList(GetGeneratorListQuery request)
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

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGeneratorById(long id)
        {
            var result = await Task.FromResult(_mediator.Send(new GetGeneratorByIdQuery { GeneratorId = id }));

            return Ok(await result);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddGeneratorUsageHours(AddGeneratorUsageHoursCommand request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.CreatedById = userId;
            request.CreatedDate = DateTime.UtcNow;

            var result = await Task.FromResult(_mediator.Send(request));

            return Ok(await result);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> EditGeneratorDetail(EditGeneratorDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            var result = await Task.FromResult(_mediator.Send(command));

            return Ok(await result);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeletePurchasedGenerator(long id)
        {
            var result = await Task.FromResult(_mediator.Send(new DeletePurchasedGeneratorCommand { PurchasedGeneratorId = id }));

            return Ok(await result);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGeneratorMonthlyBreakdownDataById(GetGeneratorMonthlyBreakdownDataByIdQuery request)
        {
            var result = await Task.FromResult(_mediator.Send(request));

            return Ok(await result);
        }
    }
}