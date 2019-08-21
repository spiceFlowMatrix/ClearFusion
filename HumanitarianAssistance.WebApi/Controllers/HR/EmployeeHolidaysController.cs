using HumanitarianAssistance.Application.HR.Commands.Create;
using HumanitarianAssistance.Application.HR.Commands.Delete;
using HumanitarianAssistance.Application.HR.Commands.Update;
using HumanitarianAssistance.Application.HR.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;


namespace HumanitarianAssistance.WebApi.Controllers.HR
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/EmployeeHolidays/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Hr))]
    [Authorize]
    public class EmployeeHolidaysController : BaseController
    {
        [HttpGet]
        public async Task<ApiResponse> GetAllHolidayDetails(int OfficeId)
        {
            return await _mediator.Send(new GetAllHolidayDetailsQuery { OfficeId = OfficeId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddHolidayDetails([FromBody] AddHolidayDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.CreatedById = userId;
            model.CreatedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditHolidayDetails([FromBody] EditHolidayDetailCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpDelete]
        public async Task<ApiResponse> DeleteHolidayDetails(long holidayId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _mediator.Send(new DeleteHolidayDetailCommand
            {
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow,
                HolidayId = holidayId
            });
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllDateforDisableCalenderDate(int officeid)
        {
            return await _mediator.Send(new GetDisabledCalenderDatesQuery { OfficeId = officeid });
        }

        [HttpGet]
        public async Task<object> GetAllDisableCalanderDate(int employeeid, int officeid)
        {
            return await _mediator.Send(new GetAllDisableCalenderDateQuery
            {
                OfficeId = officeid,
                EmployeeId = employeeid
            });
        }


        [HttpGet]
        public async Task<ApiResponse> GetAllHolidayWeeklyDetails(int officeid)
        {
            return await _mediator.Send(new GetAllHolidayWeeklyDetailsQuery { OfficeId = officeid });
        }
    }
}
