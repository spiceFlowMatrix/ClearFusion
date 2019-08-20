using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class InterviewApprovalsCommandHandler : IRequestHandler<InterviewApprovalsCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public InterviewApprovalsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(InterviewApprovalsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                foreach (var list in request.InterViewSchedule)
                {
                    var approvalRecord = await _dbContext.InterviewScheduleDetails.FirstOrDefaultAsync(x => x.IsDeleted == false && x.EmployeeId == list.EmployeeId);
                    if (approvalRecord != null)
                    {
                        if (request.approvalId == 1)
                            approvalRecord.Approval1 = list.Approval1;
                        else if (request.approvalId == 2)
                            approvalRecord.Approval2 = list.Approval2;
                        else if (request.approvalId == 3)
                            approvalRecord.Approval3 = list.Approval3;
                        else if (request.approvalId == 4)
                            approvalRecord.Approval4 = list.Approval4;

                        approvalRecord.ModifiedById = request.ModifiedById;
                        approvalRecord.ModifiedDate = request.ModifiedDate;
                        approvalRecord.IsDeleted = false;
                        await _dbContext.SaveChangesAsync();
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                }
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
