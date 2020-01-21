using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Commands.Common;
using HumanitarianAssistance.Application.Project.Commands.Create;
using HumanitarianAssistance.Application.Project.Commands.Delete;
using HumanitarianAssistance.Application.Project.Commands.Update;
using HumanitarianAssistance.Application.Project.Queries;
using HumanitarianAssistance.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Controllers.Project
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/HiringRequest/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Project))]
    public class HiringRequestController : BaseController
    {

        [HttpPost]
        public async Task<ApiResponse> GetProjectHiringRequestDetail([FromBody]GetallHiringRequestDetailQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost]
        public async Task<ApiResponse> GetProjectHiringRequestDetailByHiringRequestId([FromBody]long HiringRequestId)
        {
            return await _mediator.Send(new GetProjectHiringRequestDetailByHiringRequestIdQuery { HiringRequestId = HiringRequestId });
        }

        [HttpPost]
        public async Task<ApiResponse> GetAllProjectHiringRequestDetailByHiringRequestId([FromBody]long HiringRequestId)
        {
            return await _mediator.Send(new GetAllProjectHiringRequestDetailByHiringRequestIdQuery { HiringRequestId = HiringRequestId });
        }
        [HttpPost]
        public async Task<ApiResponse> GetAllEmployeeList([FromBody]GetAllEmployeeListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost]
        public async Task<ApiResponse> GetHiringCandidatesListById([FromBody]GetAllCandidateListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost]
        public async Task<ApiResponse> AddHiringRequestDetail([FromBody]AddProjectHiringRequestCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> EditHiringRequestDetail([FromBody]EditHiringRequestDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> AddHiringRequestCandidate([FromBody]AddHiringRequestCandidateCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> EditHiringRequestCandidate([FromBody]EditHiringRequestCandidateCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> AddCandidateInterviewDetail([FromBody]AddCandidateInterviewDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ApiResponse> HiringRequestSelectCandidate([FromBody]HiringRequestSelectCandidateCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> CompleteHiringRequest([FromBody]CompleteHiringRequestCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
            
        }
         [HttpPost]
        public async Task<ApiResponse> ClosedHiringRequest([FromBody]ClosedHiringRequestCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
            
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteCandidatDetail([FromBody]DeleteCandidateDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<ApiResponse> GetHiringCandidatesByOfficeId([FromQuery] int OfficeId)
        {
            return await _mediator.Send(new GetHiringCandidatesByOfficeIdQuery { OfficeId = OfficeId });
        }
        //new Api's
         
       [HttpPost]
        public async Task<ApiResponse> AddNewCandidateDetail([FromBody]AddNewCandidateDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        } 
       [HttpPost]
        public async Task<ApiResponse> GetAllCandidateList([FromBody]GetAllCandidateListQuery query)
        {
            return await _mediator.Send(query);
        }  
        
       [HttpPost]
         public async Task<ApiResponse> UpdateCandidateStatusByStatusId([FromBody]UpdateCandidateStatusByStatusIdCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        } 
       
        [HttpPost]
        public async Task<ApiResponse> AddExistingCandidateDetail([FromBody]AddExistingCandidateDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        } 

        [HttpPost]
        public async Task<ApiResponse> GetAllExistingCandidateList([FromBody]GetAllExistingCandidateListQuery query)
        {
            return await _mediator.Send(query);
        } 

        [HttpPost]
         public async Task<ApiResponse> GetCandidateDetailsByCandidateId([FromBody]long CandidateId)
        {
            return await _mediator.Send(new GetCandidateDetailsByCandidateIdQuery{ CandidateId=CandidateId});
        }  

        [HttpPost]
        public async Task<ApiResponse> GetAllHiringRequestDetailForInterviewByHiringRequestId([FromBody] GetAllHiringRequestDetailForInterviewByHiringRequestIdQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        public async Task<ApiResponse> GetTechnicalQuestionsByDesignationId([FromBody]int DesignationId)
        {
            return await _mediator.Send(new GetTechnicalQuestionsByDesignationIdQuery{ DesignationId=DesignationId});
        }   

        [HttpPost]
    public async Task<ApiResponse> AddInterviewDetails([FromBody]AddInterviewDetailsCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }


        [HttpPost]
        public async Task<ApiResponse> GetInterviewDetailsByInterviewId([FromBody]int InterviewId)
        {
            return await _mediator.Send(new GetInterviewDetailsByInterviewIdQuery{ InterviewId=InterviewId});
        }     
           
        [HttpPost]
        public async Task<ApiResponse> GetHiringRequestCode([FromBody]long ProjectId)
        {
            return await _mediator.Send(new GetHiringRequestCodeQuery{
                ProjectId = ProjectId
           });
        }  

        [HttpPost]
        public async Task<ApiResponse> DownloadCandidateCvByRequestId([FromBody]long requestId)
        {   
           DownloadCandidateCvByRequestIdQuery model = new DownloadCandidateCvByRequestIdQuery();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.requestId = requestId;
            return await _mediator.Send(model);
        }  
        [HttpPost]
        public async Task<IActionResult> GetCandidateAllDetailByCandidateId([FromBody]int CandidateId)
        {
            var result = await Task.FromResult(_mediator.Send(new GetCandidateAllDetailByCandidateIdQuery{ CandidateId=CandidateId}));

            if (result.Exception == null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Exception.InnerException.Message);
            }           
        }   
             
    }  
}
