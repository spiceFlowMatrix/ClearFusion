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
using System.Collections.Generic;

namespace HumanitarianAssistance.WebApi.Controllers.Accounting
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/VoucherTransaction/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Accounting))]
    [Authorize ("view:vouchers")]
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
        public async Task<ApiResponse> GetAllTransactionsByVoucherId([FromBody] GetAllTransactionsByVoucherIdQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        public async Task<ApiResponse> AddEditTransactionList([FromBody] AddEditTransactionListCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<IActionResult> VerifySelectedVouchers([FromBody] List<long> VoucherNos)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            VerifySelectedVouchersCommand command = new VerifySelectedVouchersCommand
            {
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow,
                VoucherNos = VoucherNos
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<object> DeleteSelectedVouchers([FromBody] List<long> voucherNos)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            DeleteSelectedVouchersCommand command = new DeleteSelectedVouchersCommand
            {
                ModifiedDate = DateTime.UtcNow,
                ModifiedById = userId,
                VoucherNoList = voucherNos
            };

            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<object> GetFilteredInputLevelAccountList([FromQuery] string data)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            GetFilteredInputLevelAccountListQuery command = new GetFilteredInputLevelAccountListQuery
            {
                FilterValue = data
            };

            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<object> GetFilteredProjectList([FromQuery] string data)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            GetFilteredProjectListQuery command = new GetFilteredProjectListQuery
            {
                FilterValue = data
            };
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<object> SaveTransactionList([FromBody] SaveTransactionListCommand transactions)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            transactions.CreatedById = userId;
            return await _mediator.Send(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredBudgetLineList([FromBody] GetFilteredBudgetLineListQuery request)
        {
            var result =  await _mediator.Send(request);
            return Ok(result);
        }
    }
}