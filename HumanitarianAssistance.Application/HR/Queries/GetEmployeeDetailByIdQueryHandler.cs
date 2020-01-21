using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeDetailByIdQueryHandler: IRequestHandler<GetEmployeeDetailByIdQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetEmployeeDetailByIdQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetEmployeeDetailByIdQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                var result = await _dbContext.EmployeeDetail
                                        .Include(x=> x.EmployeeProfessionalDetail)
                                        .ThenInclude(x=> x.EmployeeType)
                                        .Include(x=> x.EmployeeProfessionalDetail)
                                        .ThenInclude(x=> x.AttendanceGroupMaster)
                                        .Include(x=> x.CountryDetails)
                                        .Include(x=> x.ProvinceDetails)
                                        .Include(x=> x.QualificationDetails)
                                        .Where(x=> x.IsDeleted == false &&
                                                x.EmployeeID == request.EmployeeId)
                                        .Select(x=> new 
                                        {
                                            FirstName = x.EmployeeName.Trim().Substring(0, ((x.EmployeeName.IndexOf(' ') != -1) ? x.EmployeeName.IndexOf(' ') :  x.EmployeeName.Length-1)),
                                            LastName= (x.EmployeeName.IndexOf(' ') != -1) ? (x.EmployeeName.Trim().Substring(x.EmployeeName.IndexOf(' '), x.EmployeeName.Length-1)) : "",
                                            Email= x.Email,
                                            Phone = x.Phone,
                                            Sex= (x.SexId == (int)Gender.MALE) ? "Male" : (x.SexId == (int)Gender.FEMALE) ? "Female" : "Other",
                                            DateOfBirth = x.DateOfBirth != null ? x.DateOfBirth.Value.ToShortDateString() : "",
                                            Country = x.CountryDetails != null ? x.CountryDetails.CountryName : "",
                                            State = x.ProvinceDetails != null ? x.ProvinceDetails.ProvinceName : "",
                                            Profession = x.Profession,
                                            Qualification = x.QualificationDetails != null ? x.QualificationDetails.QualificationName : "",
                                            ExperienceYear = x.ExperienceYear,
                                            ExperienceMonth = x.ExperienceMonth,
                                            PreviousWork = x.PreviousWork,
                                            CurrentAddress = x.CurrentAddress,
                                            PermanentAddress = x.PermanentAddress,
                                            EmployementStatus = x.EmployeeType != null ? x.EmployeeType.EmployeeTypeName : "",
                                            DutyStation= x.EmployeeProfessionalDetail.DutyStation != null ? (_dbContext.OfficeDetail.FirstOrDefault(z=> z.OfficeId == x.EmployeeProfessionalDetail.DutyStation.Value).OfficeName) : "",
                                            HiredOn = x.EmployeeProfessionalDetail.HiredOn != null ? x.EmployeeProfessionalDetail.HiredOn.Value.ToShortDateString() : "",
                                            AttendanceGroup = x.EmployeeProfessionalDetail.AttendanceGroupId != null ? x.EmployeeProfessionalDetail.AttendanceGroupMaster.Name : "",
                                            JobDescription = x.EmployeeProfessionalDetail.JobDescription,
                                            Resigned= x.EmployeeProfessionalDetail.ResignationOn == null ? "No" : "Yes",
                                            ResignedOn = x.EmployeeProfessionalDetail.ResignationOn != null ? x.EmployeeProfessionalDetail.ResignationOn.Value.ToShortDateString() : "",
                                            ResignedReason = x.EmployeeProfessionalDetail.ResignationReason,
                                            Terminated = x.EmployeeProfessionalDetail.FiredOn == null ? "No": "Yes",
                                            TerminatedOn = x.EmployeeProfessionalDetail.FiredOn != null ? x.EmployeeProfessionalDetail.FiredOn.Value.ToShortDateString() : "",
                                            TerminationReason = x.EmployeeProfessionalDetail.ResignationReason
                                        }).FirstOrDefaultAsync();

                if(result == null)
                {
                    throw new Exception(StaticResource.RecordNotFound);
                }

                response.Add("EmployeeDetail", result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}