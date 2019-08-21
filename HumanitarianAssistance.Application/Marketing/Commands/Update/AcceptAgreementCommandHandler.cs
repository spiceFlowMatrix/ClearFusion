using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Update
{
    public class AcceptAgreementCommandHandler : IRequestHandler<AcceptAgreementCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AcceptAgreementCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AcceptAgreementCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var jobInfo = await _dbContext.JobDetails.FirstOrDefaultAsync(c => c.JobId == request.JobId && c.IsDeleted == false);
                if (jobInfo.IsApproved == true)
                {
                    jobInfo.IsAgreementApproved = true;
                    jobInfo.ModifiedById = request.ModifiedById;
                    jobInfo.ModifiedDate = request.ModifiedDate;
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Agreement Accepted";
                    response.data.JobDetailModel = jobInfo;
                    response.data.jobListTotalCount = await _dbContext.JobDetails.CountAsync(x => x.IsDeleted == false);
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Job is not approved.";
                    response.data.JobDetailModel = jobInfo;
                    response.data.jobListTotalCount = await _dbContext.JobDetails.CountAsync(x => x.IsDeleted == false);

                }

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}
