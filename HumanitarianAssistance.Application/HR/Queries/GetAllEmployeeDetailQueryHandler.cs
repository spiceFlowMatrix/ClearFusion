using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
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
    public class GetAllEmployeeDetailQueryHandler : IRequestHandler<GetAllEmployeeDetailQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmployeeDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllEmployeeDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                List<EmployeeDetailsAllModel> employeedetaillist = await _dbContext.EmployeeDetail
                    .Include(e => e.EmployeeProfessionalDetail)
                    .ThenInclude(p => p.professionDetails)
                    .Where(x => x.EmployeeProfessionalDetail.OfficeId == request.OfficeId && x.EmployeeProfessionalDetail.EmployeeTypeId == request.EmployeeType)
                    .Where(x => x.IsDeleted == false)
                    .Select(x => new EmployeeDetailsAllModel
                     {
                         EmployeeTypeId = x.EmployeeTypeId,
                         EmployeeID = x.EmployeeID,
                         EmployeeCode = x.EmployeeCode,
                         EmployeeName = x.EmployeeName,
                         EmployeePhoto = x.EmployeePhoto,
                         DocumentGUID = x.DocumentGUID + x.Extension,
                         EmployeeDOB = x.DateOfBirth.ToString(),
                         HiredOn = x.EmployeeProfessionalDetail.HiredOn ?? null,
                         SexName = x.SexId == (int)Gender.MALE ? "Male" : x.SexId == (int)Gender.FEMALE ? "Female" : x.SexId == (int)Gender.OTHER ? "Other" : null,
                         Age = x.Age,
                         Email = x.Email,
                         Profession = x.EmployeeProfessionalDetail.professionDetails.ProfessionName,
                         ProfessionId = x.EmployeeProfessionalDetail.ProfessionId,
                         DesignationId = x.EmployeeProfessionalDetail.DesignationId,
                         ExperienceYear = x.ExperienceYear,
                         ExperienceMonth = x.ExperienceMonth,
                         MaritalStatus = x.MaritalStatus,
                         PassportNo = x.PassportNo,
                         University = x.University,
                         BirthPlace = x.BirthPlace,
                         IssuePlace = x.IssuePlace,
                         City = x.City,
                         CurrentAddress = x.CurrentAddress,
                         PermanentAddress = x.PermanentAddress,
                         PreviousWork = x.PreviousWork,
                         Qualificationid = x.HigherQualificationId
                     }).ToListAsync();

                response.data.EmployeeDetailsList = employeedetaillist;
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