using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddCandidateInterviewDetailCommandHandler : IRequestHandler<AddCandidateInterviewDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddCandidateInterviewDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddCandidateInterviewDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (!string.IsNullOrEmpty(request.JobDescription))
                {
                    string descrtiopn = string.Empty;
                    descrtiopn = request.JobDescription.ToLower().Trim();

                    // note cjeck the jobname and update in interview table
                    JobHiringDetails Jobdetail = await _dbContext.JobHiringDetails.FirstOrDefaultAsync(x => x.JobDescription.ToLower().Trim() == descrtiopn && x.IsDeleted == false);
                    InterviewDetails obj = new InterviewDetails();
                    if (Jobdetail != null)
                    {
                        obj.InterviewStatus = null; //Approve - Reject Flag
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = request.CreatedDate;
                        obj.IsDeleted = false;
                        obj.JobId = Jobdetail.JobId;
                        obj.EmployeeID = request.EmployeeID;
                        await _dbContext.InterviewDetails.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Job does not exists");
                    }
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
