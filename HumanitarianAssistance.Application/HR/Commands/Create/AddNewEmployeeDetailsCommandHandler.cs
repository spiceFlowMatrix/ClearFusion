using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create {

    public class AddNewEmployeeDetailsCommandHandler : IRequestHandler<AddNewEmployeeDetailsCommand, ApiResponse> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public AddNewEmployeeDetailsCommandHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle (AddNewEmployeeDetailsCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                EmployeeDetail emp = new EmployeeDetail () {
                    EmployeeName = request.FirstName + ' ' + request.LastName,
                    Email = request.Email,
                    Phone = request.PhoneNo.ToString(),
                   // Password = request.Password,
                    Sex = ((Gender)request.Gender).ToString(),
                    SexId = request.Gender,
                    DateOfBirth = request.DateOfBirth,
                    MaritalStatus =((MaritalStatus)request.MaritalStatus).ToString(),
                    MaritalStatusId =request.MaritalStatus,
                    CountryId = request.Country,
                    ProvinceId = request.Province,
                    //DistrictId = request.District,
                    BirthPlace = request.BirthPlace,
                    //TinNumber = request.TinNumber,
                    PassportNo = request.PassportNumber,
                    University = request.University,
                   // ProfessionId = request.Profession,
                    HigherQualificationId = request.Qualification,
                    ExperienceYear = request.ExperienceYear,
                    ExperienceMonth = request.ExperienceMonth,
                    IssuePlace = request.IssuePlace,
                    ReferBy = request.ReferBy,
                    PreviousWork = request.PreviousWork,
                    CurrentAddress = request.CurrentAddress,
                    PermanentAddress = request.PermanentAddress
                    // EmployeeType
                    // JobGrade
                    // Office
                    // Department
                    // Designation
                    // EmployeeCotractType
                    // HiredOn
                    // AttendanceGroup
                    // DutyStation
                    // TrainingAndBenefits
                    // JobDescription
                    // Currency
                    // Amount
                };
                await _dbContext.EmployeeDetail.AddAsync (emp);
                await _dbContext.SaveChangesAsync ();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";

            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}