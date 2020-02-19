using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HumanitarianAssistance.Application.HR.Commands.Create {

    public class AddNewEmployeeDetailsCommandHandler : IRequestHandler<AddNewEmployeeDetailsCommand, ApiResponse> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHRService _hrService;

        public AddNewEmployeeDetailsCommandHandler (HumanitarianAssistanceDbContext dbContext, UserManager<AppUser> userManager, IHRService hrService) {
            _dbContext = dbContext;
            _userManager = userManager;
            _hrService = hrService;
        }

        public async Task<ApiResponse> Handle (AddNewEmployeeDetailsCommand request, CancellationToken cancellationToken) {

            AddNewEmployeeCommand data = new AddNewEmployeeCommand () {
                //  EmployeeID
                //  EmployeeCode
                EmployeeTypeId = request.EmployeeProfessionalDetails.EmployeeType,
                // EmployeeTypeName = request.EmployeeTypeName,
                EmployeeName = request.EmployeeBasicDetail.FullName,
                // IDCard = request.IDCard,
                FatherName = request.EmployeeBasicDetail.FatherName,
                GradeId = request.EmployeeProfessionalDetails.JobGrade,
                PermanentAddress = request.EmployeeBasicDetail.PermanentAddress,
                CurrentAddress = request.EmployeeBasicDetail.CurrentAddress,
                City = request.EmployeeBasicDetail.District,
                District = request.EmployeeBasicDetail.District,
                ProvinceId = request.EmployeeBasicDetail.Province,
                //  ProvinceName = request.ProvinceName,
                CountryId = request.EmployeeBasicDetail.Country,
                //  CountryName = request.CountryName,
                Phone = request.EmployeeBasicDetail.PhoneNo,
                //  Fax = request.Fax,
                Email = request.EmployeeBasicDetail.Email,
                ReferBy = request.EmployeeBasicDetail.ReferBy,
                Passport = request.EmployeeBasicDetail.PassportNumber,
                //  NationalityId = request.NationalityId,
                //  NationalityName = request.NationalityName,
                //  Language = request.Language,
                SexId = request.EmployeeBasicDetail.Gender,
                //  SexName = request.SexName,
                DateOfBirth = request.EmployeeBasicDetail.DateOfBirth.ToString (),
                // Age = request.Age,
                PlaceOfBirth = request.EmployeeBasicDetail.BirthPlace,
                HigherQualificationId = request.EmployeeBasicDetail.Qualification,
                // HigherQualificationName = request.HigherQualificationName,
                ProfessionId = request.EmployeeBasicDetail.Profession,
                // ProfessionName = request.ProfessionName,
                PreviousWork = request.EmployeeBasicDetail.PreviousWork,
                // Remarks = request.Remarks,
                ExperienceYear = request.EmployeeBasicDetail.ExperienceYear,
                ExperienceMonth = request.EmployeeBasicDetail.ExperienceMonth,
                // Resume = request.Resume,
                // EmployeePhoto = request.EmployeePhoto,
                // DocumentGUID = request.DocumentGUID,
                OfficeId = request.EmployeeProfessionalDetails.Office,
                MaritalStatus = request.EmployeeBasicDetail.MaritalStatus,
                PassportNo = request.EmployeeBasicDetail.PassportNumber,
                University = request.EmployeeBasicDetail.University,
                BirthPlace = request.EmployeeBasicDetail.BirthPlace.ToString(),
                IssuePlace = request.EmployeeBasicDetail.IssuePlace,
                MaritalStatusId = request.EmployeeBasicDetail.MaritalStatus,
                TinNumber = request.EmployeeBasicDetail.TinNumber,
                // OpeningPension = request.OpeningPension,
                EmployeeContractTypeId = request.EmployeeProfessionalDetails.EmployeeCotractType,
                HiredOn = request.EmployeeProfessionalDetails.HiredOn,
                //  FiredOn = request.FiredOn,
                //  FiredReason = request.FiredReason,
                //  ResignationOn = request.ResignationOn,
                //  ResignationReason = request.ResignationReason,
                Password = request.EmployeeBasicDetail.Password,
                AttendanceGroupId = request.EmployeeProfessionalDetails.AttendanceGroup,
                DutyStation = request.EmployeeProfessionalDetails.DutyStation,
                CreatedById = request.CreatedById,
                CreatedDate = request.CreatedDate,
                TrainingAndBenefits = request.EmployeeProfessionalDetails.TrainingAndBenefits,
                JobDescription = request.EmployeeProfessionalDetails.JobDescription,
                Department = request.EmployeeProfessionalDetails.Department,
                Designation = request.EmployeeProfessionalDetails.Designation
            };
            data.PensionDetailModel.PensionDate = request.EmployeePensionDetail.PensionDate;
            data.PensionDetailModel.PensionDetail = request.EmployeePensionDetail.PensionList.Select (x => new PensionDetail { CurrencyId = x.Currency, Amount = x.Amount }).ToList ();
            return await _hrService.AddNewEmployee (data);           
        }
    }
}