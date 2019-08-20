using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Commands.Create;
using HumanitarianAssistance.Application.HR.Commands.Update;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.HR.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.HR
{

    [Produces("application/json")]
    [Route("api/EmployeePayroll/[Action]")]
    [Authorize]
    public class EmployeePayrollController : BaseController
    {

        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeMonthlyPayrollListApproved(int officeid, int month, int year, int paymentType)
        {
            return await _mediator.Send(new GetApprovedPayrollByMonthQuery
            {
                Month = month,
                OfficeId = officeid,
                Year = year,
                PaymentType = paymentType
            });
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeMonthlyPayrollList(int officeid, int currencyid, int month, int year, int paymentType)
        {
            return await _mediator.Send(new GetApprovedPayrollByMonthQuery
            {
                Month = month,
                OfficeId = officeid,
                Year = year,
                PaymentType = paymentType
            });
        }

        [HttpPost]
        public async Task<ApiResponse> EmployeePaymentTypeReportForSaveOnly([FromBody]List<EmployeeMonthlyPayrollModel> model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            SaveEmployeePayrollPaymentCommand command = new SaveEmployeePayrollPaymentCommand();
            command.EmployeeMonthlyPayroll = model;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> EmployeePaymentTypeReport([FromBody]List<EmployeeMonthlyPayrollModel> model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            SaveEmployeePayrollPaymentCommand command = new SaveEmployeePayrollPaymentCommand();
            command.EmployeeMonthlyPayroll = model;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> EmployeePensionReport([FromBody]GetEmployeePensionReportQuery model)
        {
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<object> EmployeeSalaryTaxDetails([FromBody]GetEmployeeSalaryTaxDetailQuery model)
        {
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<object> GetAllEmployeePension(int OfficeId)
        {
            return await _mediator.Send(new GetAllEmployeePensionQuery
            {
                OfficeId = OfficeId
            });
        }

        [HttpGet]
        public async Task<ApiResponse> GetEmployeePensionHistoryDetail(int EmployeeId, int OfficeId)
        {
            return await _mediator.Send(new GetEmployeePensionHistoryQuery
            {
                OfficeId = OfficeId,
                EmployeeId = EmployeeId
            });
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeAccountSalaryDetail([FromBody] List<EmployeePayrollAccountModel> model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            EditEmployeeAccountSalaryCommand command = new EditEmployeeAccountSalaryCommand();
            command.EmployeePayrollAccount = model;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> GetEmployeeAdvanceHistoryDetail(long AdvanceID)
        {
            return await _mediator.Send(new GetEmployeeAdvanceHistoryDetailQuery { AdvanceID = AdvanceID });
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeSalaryDetail([FromBody] List<EmployeePayrollModel> model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            EditEmployeeSalaryDetailCommand command = new EditEmployeeSalaryDetailCommand();
            command.EmployeePayroll = model;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> GetEmployeeSalaryDetailsByEmployeeId(int employeeid)
        {
           return await _mediator.Send(new GetSalaryByEmployeeIdQuery { EmployeeId= employeeid});
        }

        [HttpPost]
        public async Task<ApiResponse> RejectAdvances([FromBody]RejectAdvanceCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> ApproveAdvances([FromBody]ApproveAdvanceCommand model)
        {
           var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllAdvancesByOfficeId([FromQuery] int OfficeId, int month, int year)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _mediator.Send(new GetAdvancesByOfficeIdQuery {
                Month= month,
                Year= year,
                OfficeId= OfficeId
            });
        }

        [HttpPost]
        public async Task<ApiResponse> EditAdvances([FromBody] EditAdvanceCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById= userId;
            model.ModifiedDate= DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> AddAdvances([FromBody]AddAdvanceCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById= userId;
            model.CreatedDate= DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> EmployeesSalarySummary([FromQuery] GetEmployeesSalarySummaryQuery model)
        {
            return await _mediator.Send(model);
        }
    }
}