using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Commands.Create;
using HumanitarianAssistance.Application.Accounting.Commands.Delete;
using HumanitarianAssistance.Application.Accounting.Commands.Update;
using HumanitarianAssistance.Application.Accounting.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.WebApi.Controllers.Accounting
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/ChartOfAccount/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Accounting))]
    [Authorize]
    public class ChartOfAccountController : BaseController
    {

        [HttpPost]
        public async Task<ApiResponse> GetMainLevelAccount([FromBody]long id)
        {
            return await _mediator.Send(new GetMainLevelAccountQuery { Id = id });
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllAccountsByParentId([FromBody]int id)
        {
            return await _mediator.Send(new GetAllAccountsByParentIdQuery { ParentId = id });
        }

        [HttpPost]
        public async Task<ApiResponse> AddChartOfAccount([FromBody]AddChartOfAccountCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditChartOfAccount([FromBody]EditChartOfAccountCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DeleteChartOfAccount([FromBody]long accountId)
        {
            // var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _mediator.Send(new DeleteChartOfAccountCommand
            {
                AccountId = accountId,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }

    }
}