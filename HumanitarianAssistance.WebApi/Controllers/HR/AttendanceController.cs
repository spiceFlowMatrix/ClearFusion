using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Commands.Create;
using HumanitarianAssistance.Application.HR.Commands.Common;
using HumanitarianAssistance.Application.HR.Commands.Delete;
using HumanitarianAssistance.Application.HR.Commands.Update;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.HR.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.HR
{
    [Produces("application/json")]
    [Route("api/Attendance/[Action]")]
    [Authorize]
    public class AttendanceController : BaseController
    {

        #region "Monthly Payroll Hours"

        [HttpPost]
        public async Task<ApiResponse> GetAllPayrollMonthlyHourDetail([FromBody] GetPayrollMonthlyHourQuery model)
        {
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> AddPayrollMonthlyHourDetail([FromBody] AddPayrollMonthlyHourCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditPayrollMonthlyHourDetail([FromBody] EditPayrollMonthlyHourCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        #endregion

        [HttpPost]
        public async Task<ApiResponse> AddEmployeeAttendanceDetails([FromBody] List<EmployeeAttendanceModel> model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            AddAttendanceDetailCommand command = new AddAttendanceDetailCommand();

            if (model.Any())
            {
                command.EmployeeAttendance = model;

                command.EmployeeAttendance.ForEach(x =>
                {
                    x.CreatedById = userId;
                    x.CreatedDate = DateTime.UtcNow;
                });
            }

            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<object> AddEmployeeLeaveDetails([FromBody] List<AssignLeaveToEmployeeModel> model)
        {
            AddEmployeeLeaveCommand command = new AddEmployeeLeaveCommand();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            command.AssignEmployeeLeaveList = model;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> GetEmployeeAttendanceDetails([FromBody]GetEmployeeAttendanceQuery model)
        {
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredAttendanceDetails([FromBody] GetFilteredAttendanceQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }
        [HttpPost]
        public async Task<object> GetAllEmployeesAttendanceByDate([FromBody] GetEmployeesAttendanceByDateQuery model)
        {
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<object> EditEmployeeAttendanceByDate([FromBody] EditEmployeeAttendanceByDateCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<object> MonthlyEmployeeAttendanceReport([FromQuery] MonthlyEmployeeAttendanceReportQuery model)
        {
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeAssignLeave(int EmployeeId)
        {
            return await _mediator.Send(new GetEmployeeAssignLeaveQuery { EmployeeId = EmployeeId });
        }

        [HttpPost]
        public async Task<ApiResponse> AssignLeaveToEmployeeDetail([FromBody] AddLeaveToEmployeeCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> AddEmployeeApplyLeaveDetail([FromBody] List<EmployeeApplyLeaveModel> model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AddEmployeeApplyLeaveCommand command = new AddEmployeeApplyLeaveCommand();

            command.EmployeeApplyLeave = model;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> GetEmployeeApplyLeaveDetailById(int employeeid)
        {
            return await _mediator.Send(new GetEmployeeAppliedLeaveByIdQuery { EmployeeId = employeeid });
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllEmployeeApplyLeaveList(int OfficeId)
        {
            return await _mediator.Send(new GetEmployeeApplyLeaveListQuery { OfficeId = OfficeId });
        }

        [HttpPost]
        public async Task<ApiResponse> ApproveEmployeeLeave([FromBody]List<ApproveLeaveModel> model)
        {
            ApproveEmployeeLeaveCommand command = new ApproveEmployeeLeaveCommand();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.AppliedLeave = model;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<object> RejectEmployeeLeave([FromBody]List<ApproveLeaveModel> model)
        {
            RejectEmployeeLeaveCommand command = new RejectEmployeeLeaveCommand();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.AppliedLeave = model;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> DeleteApplyEmployeeLeave(int applyleaveid)
        {
            DeleteEmployeeAppliedLeaveCommand command = new DeleteEmployeeAppliedLeaveCommand();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.AppliedLeaveId = applyleaveid;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> EditEmployeeAssignLeave([FromBody] EditEmployeeAssignLeaveCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeLeave([FromBody] ApplyEmployeeLeaveCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            var item = await _mediator.Send(model);
            return Ok(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeAppliedLeaves(int id)
        {
            GetEmployeeAppliedLeavesQuery query = new GetEmployeeAppliedLeavesQuery();
            query.EmployeeId = id;
            var item = await _mediator.Send(query);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveRejectLeave([FromBody] ApproveRejectLeaveCommand model)
        {
            model.ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var item = await _mediator.Send(model);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> GetPayrollDailyHourByEmployeeIds([FromBody]GetPayrollDailyHourByEmployeeIdsQuery model)
        {
            var item = await _mediator.Send(model);
            return Ok(item);
        }

        [HttpPost]
        public async Task<object> AddEditEmployeeAttendanceDetails([FromBody] AddEditAttendanceDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            AddEditAttendanceDetailCommand command = new AddEditAttendanceDetailCommand();

            if (model.EmployeeAttendance.Any())
            {
                model.EmployeeAttendance.ForEach(x =>
                {
                    x.CreatedById = userId;
                    x.CreatedDate = DateTime.UtcNow;
                });
            }

            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeeAppliedLeaveHours([FromBody] GetEmployeeAppliedLeaveHoursQuery model)
        {
            var item = await _mediator.Send(model);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> MarkWholeMonthAttendanceByEmployeeId([FromBody] MarkWholeMonthAttendanceByEmployeeIdCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            var item = await _mediator.Send(model);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> GetAppliedLeaveDates([FromBody]GetAppliedLeaveDatesQuery model)
        {
            var item = await _mediator.Send(model);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> GetAttendanceGroupDetailById([FromBody]long AttendanceGroupId)
        {
            GetAttendanceGroupDetailByIdQuery model = new GetAttendanceGroupDetailByIdQuery{
                AttendanceGroupId = AttendanceGroupId
            };
            var item = await _mediator.Send(model);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayrollDailyHoursToAttendanceGroups([FromBody]AddPayrollDailyHoursToAttendanceGroupsCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;
            var item = await _mediator.Send(model);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> GetPayrollMonthlyHourByAttendanceGroups([FromBody]long AttendanceGroupId)
        {
            GetPayrollMonthlyHourByAttendanceGroupsQuery model = new GetPayrollMonthlyHourByAttendanceGroupsQuery{
                AttendanceGroupId = AttendanceGroupId
            };
            var item = await _mediator.Send(model);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> EditPayrollMonthlyHourById([FromBody]EditPayrollMonthlyHourByIdCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;
            var item = await _mediator.Send(model);
            return Ok(item);
        }
    }
}