using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Application.HR.Models;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllApprovedEmployeeQueryHandler : IRequestHandler<GetAllApprovedEmployeeQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllApprovedEmployeeQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetAllApprovedEmployeeQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                var approvedEmployeeList = await _dbContext.InterviewScheduleDetails
                    .Include(i => i.JobGrade)
                    .Include(i => i.EmployeeDetails)
                    .Include(i => i.JobHiringDetails).Where(x => (x.Approval3 == true && x.Approval4 == true))
                    .Select(x => new InterviewScheduleModel
                    {
                        EmployeeId = x.EmployeeId,
                        EmployeeName = x.EmployeeDetails.EmployeeName,
                        PhoneNo = x.EmployeeDetails.Phone,
                        JobId = x.JobHiringDetails.JobId,
                        JobCode = x.JobHiringDetails.JobCode,
                        GradeId = x.GradeId,
                        GradeName = x.JobGrade.GradeName,
                        Approval1 = x.Approval1,    //For General Assembly (Grade == 1)
                        Approval2 = x.Approval2,    //For Director (Grade > 1)
                        Approval3 = x.Approval3,    //For General Admin (Approval1 == true || Approval2 == true)
                        Approval4 = x.Approval4     //For Field Office (Approval1 == true || Approval2 == true)

                    }).ToListAsync();
                response.data.InterviewApprovedEmployeeList = approvedEmployeeList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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