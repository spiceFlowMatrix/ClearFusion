using AutoMapper;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class ApproveEmployeeInterviewRequestCommandHandler : IRequestHandler<ApproveEmployeeInterviewRequestCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public ApproveEmployeeInterviewRequestCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(ApproveEmployeeInterviewRequestCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<EmployeeAppraisalDetailsModel> lst = new List<EmployeeAppraisalDetailsModel>();
                var emplst = await _dbContext.InterviewDetails.FirstOrDefaultAsync(x => x.InterviewDetailsId == request.InterviewDetailsId);
                emplst.InterviewStatus = "approved";
                emplst.ModifiedById = request.ModifiedById;
                emplst.ModifiedDate = request.ModifiedDate;
                await _dbContext.SaveChangesAsync();
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
