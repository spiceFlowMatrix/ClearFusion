using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HumanitarianAssistance.Application.Accounting.Commands.Common;
using HumanitarianAssistance.Application.Accounting.Commands.Create;
using HumanitarianAssistance.Application.Accounting.Commands.Update;
using HumanitarianAssistance.Application.Accounting.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.WebApi.Controllers.Accounting
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/VoucherTransaction/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Accounting))]
    [Authorize]
    public class VoucherTransactionController : BaseController
    {
        [HttpPost]
        public async Task<ApiResponse> GetAllVoucherList([FromBody]GetAllVoucherListQuery model)
        {
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> GetVoucherDetailByVoucherNo([FromBody] long id)
        {
            return await _mediator.Send(new GetVoucherDetailByVoucherNoQuery { VoucherId = id });
        }

        [HttpPost]
        public async Task<ApiResponse> AddVoucherDetail([FromBody] AddVoucherDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditVoucherDetail([FromBody] EditVoucherDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> VerifyVoucher([FromBody] long id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _mediator.Send(new VerifyVoucherCommand
            {
                VoucherId = id,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllTransactionsByVoucherId([FromBody] long id)
        {
            return await _mediator.Send(new GetAllTransactionsByVoucherIdQuery { VoucherId = id });
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditTransactionList([FromBody] AddEditTransactionListCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

    }
}