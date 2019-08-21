using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeProfessionalDetailQueryHandler : IRequestHandler<GetEmployeeProfessionalDetailQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetEmployeeProfessionalDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeeProfessionalDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                List<EmployeeProfessionalDetailModel> employeeProfessionallist = await _dbContext.EmployeeProfessionalDetail
                                      .Include(z => z.AttendanceGroupMaster)
                                      .Include(e => e.EmployeeType)
                                      .Include(o => o.OfficeDetail)
                                      .Include(d => d.DesignationDetails)
                                      .Include(d => d.Department)
                                      .Where(x => x.EmployeeId == request.EmployeeId)
                                      .Select(x => new EmployeeProfessionalDetailModel
                                      {
                                          EmployeeProfessionalId = x.EmployeeProfessionalId,
                                          EmployeeTypeId = x.EmployeeTypeId,
                                          EmployeeTypeName = x.EmployeeType.EmployeeTypeName ?? null,
                                          Status = x.Status,
                                          OfficeId = x.OfficeId,
                                          OfficeName = x.OfficeDetail.OfficeName ?? null,
                                          DesignationId = x.DesignationId,
                                          DesignationName = x.DesignationDetails.Designation ?? null,
                                          DepartmentId = x.DepartmentId,
                                          DepartmentName = x.Department.DepartmentName ?? null,
                                          EmployeeContractTypeId = x.EmployeeContractTypeId,
                                          HiredOn = x.HiredOn != null ? Convert.ToDateTime(x.HiredOn).Date : x.HiredOn,
                                          FiredOn = x.FiredOn != null ? Convert.ToDateTime(x.FiredOn).Date : x.FiredOn,
                                          FiredReason = x.FiredReason,
                                          ResignationOn = x.ResignationOn != null ? Convert.ToDateTime(x.ResignationOn).Date : x.ResignationOn,
                                          ResignationReason = x.ResignationReason,
                                          JobDescription = x.JobDescription,
                                          TrainingBenefits = x.TrainingBenefits,
                                          AttendanceGroupId = x.AttendanceGroupId,
                                          AttendanceGroupName = x.AttendanceGroupMaster == null ? "" : x.AttendanceGroupMaster.Name
                                      }).ToListAsync();
                                       response.data.EmployeeProfessionalList = employeeProfessionallist;
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
