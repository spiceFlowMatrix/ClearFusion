using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Commands.Create;
using HumanitarianAssistance.Application.HR.Commands.Delete;
using HumanitarianAssistance.Application.HR.Commands.Update;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.HR.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Commands.Create;
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
            return await _mediator.Send(new GetEmployeesMonthlyPayrollQuery
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

            EmployeeApprovePayrollCommand command = new EmployeeApprovePayrollCommand();
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
            return await _mediator.Send(new GetSalaryByEmployeeIdQuery { EmployeeId = employeeid });
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

            return await _mediator.Send(new GetAdvancesByOfficeIdQuery
            {
                Month = month,
                Year = year,
                OfficeId = OfficeId
            });
        }

        [HttpPost]
        public async Task<ApiResponse> EditAdvances([FromBody] EditAdvanceCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> AddAdvances([FromBody]AddAdvanceCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> EmployeesSalarySummary([FromQuery] GetEmployeesSalarySummaryQuery model)
        {
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> DisapproveEmployeeApprovedSalary([FromBody]DisapproveEmployeeApprovedSalaryCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> GetEmployeeOpeningPensionDetail(int employeeId)
        {
            return await _mediator.Send(new GetEmployeePensionDetailQuery { EmployeeId = employeeId });
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeSalaryCurrencyAndBasicSalary([FromBody]AddEmployeeSalaryCurrencyAndBasicSalaryCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployeeSalaryCurrencyAndBasicSalary([FromBody]EditEmployeeSalaryCurrencyAndBasicSalaryCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeBasicPayAndCurrency([FromQuery] int id)
        {
            var result = await _mediator.Send(new GetEmployeeBasicPayAndCurrencyQuery() { EmployeeId = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeBonusFineSalaryHead([FromBody]AddEmployeeBonusFineSalaryHeadCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeeBonusFineSalaryHead([FromBody] GetEmployeeBonusFineSalaryHeadQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployeeBonusFineSalaryHead([FromBody] int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            DeleteEmployeeBonusFineSalaryHeadCommand command = new DeleteEmployeeBonusFineSalaryHeadCommand
            {
                Id = id,
                ModifiedById = userId
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeeAccumulatedSalaryHead([FromBody] GetEmployeeAccumulatedSalaryHeadQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeeMonthlyPayroll([FromBody] GetEmployeeMonthlyPayrollQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveEmployeeMonthlyPayroll([FromBody] ApproveEmployeeMonthlyPayrollCommand model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeEmployeeMonthlyPayroll([FromBody] RevokeEmployeePayrollCommand model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAdvanceListByEmployeeId([FromQuery] int id)
        {
            var result = await _mediator.Send(new GetAdvanceListByEmployeeIdQuery() { EmployeeId = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewAdvanceRequest([FromBody] AddNewAdvanceRequestCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAdvanceDetailById([FromQuery] int id)
        {
            var result = await _mediator.Send(new GetAdvanceDetailByIdQuery() { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveAdvanceRequest([FromBody] ApproveAdvanceRequestCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> RejectAdvanceRequest([FromQuery] long id)
        {
            RejectAdvanceRequestCommand command = new RejectAdvanceRequestCommand();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            command.AdvanceId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditAdvanceRequest([FromBody] EditAdvanceRequestCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            var result = await _mediator.Send(model);
            return Ok(result);
        }
    }
}