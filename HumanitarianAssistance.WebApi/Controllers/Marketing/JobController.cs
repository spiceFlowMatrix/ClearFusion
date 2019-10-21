using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Commands.Common;
using HumanitarianAssistance.Application.Marketing.Commands.Create;
using HumanitarianAssistance.Application.Marketing.Commands.Delete;
using HumanitarianAssistance.Application.Marketing.Commands.Update;
using HumanitarianAssistance.Application.Marketing.Queries;
using HumanitarianAssistance.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Controllers.Marketing
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Job/[Action]")]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Marketing))]
    public class JobController : BaseController
    {
        [HttpGet]
        public async Task<ApiResponse> GetJobsList()
        {
            return await _mediator.Send(new GetAllJobDetailsQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> GetJobsPaginatedList([FromBody]GetJobsPaginatedListQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpPost]

        public async Task<ApiResponse> FetchInvoice([FromBody]int jobId)
        {
            return await _mediator.Send(new FetchInvoiceQuery { jobId = jobId });
        }
        [HttpPost]
        public async Task<ApiResponse> GetJobDetailsById([FromBody]int model)
        {
            return await _mediator.Send(new GetJobDetailsByIdQuery { jobId = model }); 
        }
        [HttpPost]
        public async Task<ApiResponse> GetFilteredJoblist([FromBody]FilterJobListQuery query)
        {
            return await _mediator.Send(query); 
        }
        [HttpPost]
        public async Task<ApiResponse> GetFilteredJobslist([FromBody]FilterJobsListQuery query)
        {
            return await _mediator.Send(query); 
        }
        [HttpGet]
        public async Task<ApiResponse> GetAllPhaseList()
        {
            return await _mediator.Send(new GetAllPhaseQuery());
        }
        [HttpPost]
        public async Task<ApiResponse> GetPhaseById([FromBody]int model)
        {
            return await _mediator.Send(new GetPhaseByIdQuery { JobPhaseId = model });
        }
        [HttpPost]
        public async Task<ApiResponse> ApproveJob([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new ApproveJobCommand()
            {
               ModifiedById = userId,
               ModifiedDate = DateTime.UtcNow,
               JobId=model
            });
        }
        [HttpPost]
        public async Task<ApiResponse> GenerateInvoice([FromBody]int jobId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new GenerateInvoiceCommand()
            {
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow,
                jobId=jobId
        });
        }
        [HttpPost]
        public async Task<ApiResponse> ApproveInvoice([FromBody]int jobId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new ApproveInvoiceCommand
            {
                jobId = jobId,
                CreatedById=userId,
                CreatedDate=DateTime.UtcNow,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }
        [HttpPost]
        public async Task<ApiResponse> AddEditJobDetail([FromBody]AddEditJobDetailCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; 
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeleteJobDetail([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeleteJobDetailCommand
            {
                JobId = model,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }

        [HttpPost]
        public async Task<ApiResponse> AcceptAgreement([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new AcceptAgreementCommand
            {
                JobId = model,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }
        [HttpPost]
        public async Task<ApiResponse> AddPhase([FromBody]AddEditPhaseCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            command.CreatedById = userId;
            command.CreatedDate = DateTime.UtcNow;
            command.ModifiedById = userId;
            command.ModifiedDate = DateTime.UtcNow;
            return await _mediator.Send(command);
        }
        [HttpPost]
        public async Task<ApiResponse> DeletePhase([FromBody]int model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new DeletePhaseCommand
            {
                JobPhaseId = model,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }
        [HttpPost]
        public async Task<ApiResponse> RemoveInvoice([FromBody]int jobId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new RemoveInvoiceCommand 
            {
                jobId = jobId,
                ModifiedById = userId,
                ModifiedDate = DateTime.UtcNow
            });
        }
    }
    

}





