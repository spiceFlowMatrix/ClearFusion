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
using HumanitarianAssistance.Domain.Entities.HR;

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
                var resignDetail = await _dbContext.EmployeeResignationDetail.FirstOrDefaultAsync(x=>x.IsDeleted == false && x.EmployeeID == request.EmployeeId);
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
                                            Name = x.EmployeeName,
                                            FatherName= x.FatherName,
                                            EmployeeCode = x.EmployeeCode,
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
                                            EmploymentStatusId = x.EmployeeProfessionalDetail.EmployeeTypeId,
                                            DutyStation= x.EmployeeProfessionalDetail.DutyStation != null ? (_dbContext.OfficeDetail.FirstOrDefault(z=> z.OfficeId == x.EmployeeProfessionalDetail.DutyStation.Value).OfficeName) : "",
                                            HiredOn = x.EmployeeProfessionalDetail.HiredOn != null ? x.EmployeeProfessionalDetail.HiredOn.Value.ToShortDateString() : "",
                                            AttendanceGroup = x.EmployeeProfessionalDetail.AttendanceGroupId != null ? x.EmployeeProfessionalDetail.AttendanceGroupMaster.Name : "",
                                            JobDescription = x.EmployeeProfessionalDetail.JobDescription,
                                            Resigned= x.EmployeeProfessionalDetail.ResignationOn == null ? "No" : "Yes",
                                            ResignedOn = x.EmployeeProfessionalDetail.ResignationOn != null ? x.EmployeeProfessionalDetail.ResignationOn.Value.ToShortDateString() : "",
                                            ResignedReason = x.EmployeeProfessionalDetail.ResignationReason,
                                            Terminated = x.EmployeeProfessionalDetail.FiredOn == null ? "No": "Yes",
                                            TerminatedOn = x.EmployeeProfessionalDetail.FiredOn != null ? x.EmployeeProfessionalDetail.FiredOn.Value.ToShortDateString() : "",
                                            TerminationReason = x.EmployeeProfessionalDetail.FiredReason,
                                            OfficeId =  x.EmployeeProfessionalDetail.OfficeId,
                                            IsResigned = x.IsResigned,
                                            ResignationStatus = x.ResignationStatus,
                                            Tenure = calculateTenure(x.EmployeeProfessionalDetail.HiredOn, x.EmployeeProfessionalDetail.FiredOn, x.IsResigned, x.EmployeeID, resignDetail)
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

        private string calculateTenure(DateTime? d1, DateTime? d2, bool IsResigned, int EmployeeId, EmployeeResignationDetail resignDetail) {
            if (d2 == null && !IsResigned) {
                d2 = DateTime.Now;
            } 
            else if(d2 == null && IsResigned) 
            {
                if(resignDetail != null) {
                    d2 = resignDetail.ResignDate;
                } else {
                    d2 = DateTime.Now;
                }
            }
            int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            DateTime fromDate;
            DateTime toDate;
            int year = 0;
            int month = 0;
            int day = 0;
            
            if (d1 > d2)
            {
                fromDate = d2.Value;
                toDate = d1.Value;
            }
            else
            {
                fromDate = d1.Value;
                toDate = d2.Value;
            }
            var increment = 0; 
            if (fromDate.Day > toDate.Day)
            { 
                increment = monthDay[fromDate.Month - 1]; 
            }
            if (increment== -1)
            {
                if (DateTime.IsLeapYear(fromDate.Year))
                {
                    increment = 29;
                } 
                else
                {
                    increment = 28;
                }
            }
            if (increment != 0)
            {    
                day = (toDate.Day+ increment) - fromDate.Day;
                increment = 1; 
            }
            else
            {       
                day = toDate.Day - fromDate.Day;
            }

            if ((fromDate.Month + increment) > toDate.Month)
            {   
                month = (toDate.Month+ 12) - (fromDate.Month + increment);
                increment = 1;
            }
            else
            {    
                month = (toDate.Month) - (fromDate.Month + increment);
                increment = 0;
            }
            year = toDate.Year - (fromDate.Year + increment);
            return $"{year} Years {month} Months {day} Days";
        } 
    }
}
