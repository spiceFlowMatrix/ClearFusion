using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static HumanitarianAssistance.Application.Configuration.Queries.GetEmployeeDetailByEmployeeIdQuery;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetEmployeeDetailByEmployeeIdQueryHandler : IRequestHandler<GetEmployeeDetailByEmployeeIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeeDetailByEmployeeIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeeDetailByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                response.data.EmployeeDetailListData = await _dbContext.EmployeeDetail.Include(x => x.EmployeeProfessionalDetail).Include(x => x.EmployeeProfessionalDetail.OfficeDetail).Include(x => x.EmployeeProfessionalDetail.Department).Include(x => x.QualificationDetails).Include(x => x.EmployeeProfessionalDetail.DesignationDetails).Where(x => x.EmployeeID == request.EmployeeId).Select(x => new EmployeeDetailListModel
                {
                    EmployeeId = x.EmployeeID,
                    EmployeeName = x.EmployeeName,
                    EmployeeCode = x.EmployeeCode,
                    FathersName = x.FatherName,
                    Position = x.EmployeeProfessionalDetail.DesignationDetails.Designation,
                    Department = x.EmployeeProfessionalDetail.Department.DepartmentName,
                    Qualification = x.QualificationDetails.QualificationName,
                    DutyStation = x.EmployeeProfessionalDetail.OfficeDetail.OfficeName,
                    RecruitmentDate = x.EmployeeProfessionalDetail.HiredOn,
                    TenureWithCHA = (DateTime.Now.Date - x.EmployeeProfessionalDetail.HiredOn.Value.Date).Days.ToString() + " Days",
                    Gender = x.SexId == 1 ? "Male" : x.SexId == 2 ? "Female" : "Other"
                }).ToListAsync();


                //response.data.EmployeeDetailListData = await _uow.GetDbContext().EmployeeContract.Where(x => x.EmployeeId == EmployeeId).Select(x => new EmployeeDetailList /*Include(x=> x.Employee).Include(x => x.Employee.EmployeeProfessionalDetail).Include(x => x.Employee.EmployeeProfessionalDetail.OfficeDetail).Include(x => x.Employee.EmployeeProfessionalDetail.Department).Include(x => x.Employee.QualificationDetails).Include(x => x.Employee.EmployeeProfessionalDetail.DesignationDetails)*/
                //{
                //    EmployeeId = x.Employee.EmployeeID,
                //    EmployeeName = x.Employee.EmployeeName,
                //    EmployeeCode = x.Employee.EmployeeCode,
                //    FathersName = x.Employee.FatherName,
                //    Position = x.Employee.EmployeeProfessionalDetail.DesignationDetails.Designation,
                //    Department = x.Employee.EmployeeProfessionalDetail.Department.DepartmentName,
                //    Qualification = x.Employee.QualificationDetails.QualificationName,
                //    DutyStation = x.Employee.EmployeeProfessionalDetail.OfficeDetail.OfficeName,
                //    OfficeId= x.Employee.EmployeeProfessionalDetail.OfficeDetail.OfficeId,
                //    RecruitmentDate = x.Employee.EmployeeProfessionalDetail.HiredOn,
                //    ContractStartDate= x.ContractStartDate,
                //    ContractEndDate= x.ContractEndDate,
                //    TenureWithCHA = (DateTime.Now.Date - x.Employee.EmployeeProfessionalDetail.HiredOn.Value.Date).Days.ToString() + " Days",
                //    Gender = x.Employee.SexId == 1 ? "Male" : x.Employee.SexId == 2 ? "Female" : "Other"
                //}).ToListAsync();
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
