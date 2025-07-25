using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Commands.Common;
using HumanitarianAssistance.Application.HR.Commands.Create;
using HumanitarianAssistance.Application.HR.Commands.Delete;
using HumanitarianAssistance.Application.HR.Commands.Update;
using HumanitarianAssistance.Application.HR.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.HR
{
    [Produces("application/json")]
    [Route("api/HRConfiguration/[Action]")]
    [Authorize]
    public class HRConfigurationController : BaseController
    {


        [HttpPost]
        public async Task<IActionResult> AddDesignationDetail([FromBody]AddDesignationDetailCommand command)
        {
            command.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedDate = DateTime.UtcNow;
            return Ok(await _mediator.Send(command));
        }

        [HttpPost]
        public async Task<IActionResult> GetAllDesignationDetail([FromBody] GetAllDesignationDetailQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> EditDesignationDetail([FromBody]EditDesignationDetailCommand command)
        {
            command.ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedDate = DateTime.UtcNow;
            return Ok(await _mediator.Send(command));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteDesignationDetail([FromBody] int Id)
        {
            return Ok(await _mediator.Send(new DeleteDesignationDetailCommand
            {
                Id = Id,
                ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                ModifiedDate = DateTime.UtcNow
            }));

        }
        [HttpPost]
        public async Task<IActionResult> GetEducationDegreeList([FromBody] GetAllEducationDegreeQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> AddEducationDegree([FromBody] AddEducationDegreeCommand request)
        {
            request.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.CreatedDate = DateTime.UtcNow;
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> EditEducationDegree([FromBody] EditEducationDegreeCommand request)
        {
            request.ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            request.ModifiedDate = DateTime.UtcNow;
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEducationDegree([FromBody] int Id)
        {
            return Ok(await _mediator.Send(new DeleteEducationDegreeCommand
            {
                Id = Id,
                ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                ModifiedDate = DateTime.UtcNow
            }));

        }

        [HttpPost]
        public async Task<IActionResult> GetOfficeList([FromBody] GetOfficeListQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> GetDepartmentList([FromBody] GetDepartmentListQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> GetJobGradeList([FromBody] GetJobGradeListQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> GetAttendanceGroupList([FromBody] GetAttendanceGroupListQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> GetProfessionList([FromBody] GetProfessionListQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> GetQualificationList([FromBody] GetQualificationListQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> GetAllExitInterviewQuestions([FromBody] GetAllExitInterviewQuestionsQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> UpsertExitInterviewQuestion([FromBody] UpsertExitInterviewQuestionCommand request)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            request.CreatedById = userId;
            request.CreatedDate = DateTime.UtcNow;
            request.ModifiedById = userId;
            request.ModifiedDate = DateTime.UtcNow;
            return Ok(await _mediator.Send(request));
        }


        [HttpPost]
        public async Task<IActionResult> GetSequenceNumber([FromBody] GetSequenceNumberQuery model)
        {
            return Ok(await _mediator.Send(model));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExitInterviewQuestion([FromBody] int Id)
        {
            return Ok(await _mediator.Send(new DeleteExitInterviewQuestionCommand { Id = Id }));
        }

        [HttpPost]
        public async Task<IActionResult> GetAllLeaveReasonType([FromBody] GetAllLeaveReasonTypeQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLeaveType([FromBody] int Id)
        {
            return Ok(await _mediator.Send(new DeleteLeaveTypeCommand { Id = Id }));
        }
    }
}