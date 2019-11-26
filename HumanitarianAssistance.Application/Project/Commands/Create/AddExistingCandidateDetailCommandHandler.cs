using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
        public class AddExistingCandidateDetailCommandHandler : IRequestHandler<AddExistingCandidateDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddExistingCandidateDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(AddExistingCandidateDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse ();
            try {
                var employeeDetail = await _dbContext.EmployeeDetail.Where(x => x.EmployeeID==request.EmployeeId &&
                                                                  x.EmployeeTypeId == (int)EmployeeTypeStatus.Active &&
                                                                  x.IsDeleted == false &&
                                                                  x.EmployeeProfessionalDetail.OfficeId != null).FirstOrDefaultAsync();
 
                if (employeeDetail != null) {
                    HiringRequestCandidateStatus obj = new HiringRequestCandidateStatus () {
                    ProjectId = request.ProjectId,
                    HiringRequestId = request.HiringRequestId,
                    EmployeeID = employeeDetail.EmployeeID,
                    CandidateStatus = 2,
                    // InterviewId = 1,
                    CreatedById = request.CreatedById,
                    CreatedDate = request.CreatedDate
                    };
                    await _dbContext.HiringRequestCandidateStatus.AddAsync (obj);
                    await _dbContext.SaveChangesAsync ();
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}