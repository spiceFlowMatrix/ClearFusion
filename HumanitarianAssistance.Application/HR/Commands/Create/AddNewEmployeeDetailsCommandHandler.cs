using System;
using System.Collections.Generic;
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
                //  City = request.City,
                //  District = request.District,
                ProvinceId = request.EmployeeBasicDetail.Province,
                //  ProvinceName = request.ProvinceName,
                CountryId = request.EmployeeBasicDetail.Country,
                //  CountryName = request.CountryName,
                Phone = request.EmployeeBasicDetail.PhoneNo.ToString (),
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
                BirthPlace = request.EmployeeBasicDetail.BirthPlace,
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
            };
           // data.PensionDetailModel

            // public PensionDetailModel PensionDetailModel

            return await _hrService.AddNewEmployee (data);
            // ApiResponse response = new ApiResponse ();
            // try {
            // EmployeeDetail emp = new EmployeeDetail () {
            //     EmployeeName = request.EmployeeBasicDetail.FullName,
            //     FatherName = request.EmployeeBasicDetail.FatherName,
            //     Email = request.EmployeeBasicDetail.Email,
            //     Phone = request.EmployeeBasicDetail.PhoneNo.ToString (),
            //     // Password = request.Password,
            //     SexId = request.EmployeeBasicDetail.Gender,
            //     DateOfBirth = request.EmployeeBasicDetail.DateOfBirth,
            //     MaritalStatusId = request.EmployeeBasicDetail.MaritalStatus,
            //     CountryId = request.EmployeeBasicDetail.Country,
            //     ProvinceId = request.EmployeeBasicDetail.Province,
            //     //DistrictId = request.District,
            //     BirthPlace = request.EmployeeBasicDetail.BirthPlace,
            //     PassportNo = request.EmployeeBasicDetail.PassportNumber,
            //     University = request.EmployeeBasicDetail.University,
            //     HigherQualificationId = request.EmployeeBasicDetail.Qualification,
            //     ExperienceYear = request.EmployeeBasicDetail.ExperienceYear,
            //     ExperienceMonth = request.EmployeeBasicDetail.ExperienceMonth,
            //     IssuePlace = request.EmployeeBasicDetail.IssuePlace,
            //     ReferBy = request.EmployeeBasicDetail.ReferBy,
            //     PreviousWork = request.EmployeeBasicDetail.PreviousWork,
            //     CurrentAddress = request.EmployeeBasicDetail.CurrentAddress,
            //     PermanentAddress = request.EmployeeBasicDetail.PermanentAddress,
            //     GradeId = request.EmployeeProfessionalDetails.JobGrade,
            //     CreatedById = request.CreatedById,
            //     CreatedDate = request.CreatedDate,
            //     IsDeleted = false
            //     // Currency
            //     // Amount
            // };
            // await _dbContext.EmployeeDetail.AddAsync (emp);
            // await _dbContext.SaveChangesAsync ();

            // EmployeeProfessionalDetail empprofessional = new EmployeeProfessionalDetail {
            //     EmployeeId = emp.EmployeeID,
            //     EmployeeTypeId = request.EmployeeProfessionalDetails.EmployeeType,
            //     OfficeId = request.EmployeeProfessionalDetails.Office,
            //     ProfessionId = request.EmployeeBasicDetail.Profession,
            //     TinNumber = request.EmployeeBasicDetail.TinNumber,
            //     HiredOn = request.EmployeeProfessionalDetails.HiredOn,
            //     EmployeeContractTypeId = request.EmployeeProfessionalDetails.EmployeeCotractType,
            //     // FiredOn = request.FiredOn,
            //     // FiredReason = request.FiredReason,
            //     // ResignationOn = request.ResignationOn,
            //     // ResignationReason = request.ResignationReason,
            //     AttendanceGroupId = request.EmployeeProfessionalDetails.AttendanceGroup,
            //     DutyStation = request.EmployeeProfessionalDetails.DutyStation,
            //     JobDescription = request.EmployeeProfessionalDetails.JobDescription,
            //     DepartmentId = request.EmployeeProfessionalDetails.Department,
            //     DesignationId = request.EmployeeProfessionalDetails.Designation,
            //     TrainingBenefits = request.EmployeeProfessionalDetails.TrainingAndBenefits,
            //     CreatedById = request.CreatedById,
            //     CreatedDate = request.CreatedDate,
            //     IsDeleted = request.IsDeleted
            // };

            // await _dbContext.EmployeeProfessionalDetail.AddAsync (empprofessional);
            // await _dbContext.SaveChangesAsync ();

            // List<MultiCurrencyOpeningPension> pensionDetail = new List<MultiCurrencyOpeningPension> ();

            // if (request.EmployeePensionDetail != null) {

            //     foreach (var item in request.EmployeePensionDetail) {
            //     MultiCurrencyOpeningPension detail = new MultiCurrencyOpeningPension () {
            //     EmployeeID = emp.EmployeeID,
            //     // PensionStartDate = request.PensionDetailModel.PensionDate,
            //     Amount = item.Amount,
            //     CreatedById = request.CreatedById,
            //     CreatedDate = request.CreatedDate,
            //     IsDeleted = false,
            //     CurrencyId = item.Currency,
            //         };

            //         pensionDetail.Add (detail);
            //     }
            //     await _dbContext.MultiCurrencyOpeningPension.AddRangeAsync (pensionDetail);
            //     await _dbContext.SaveChangesAsync ();

            // }

            // AppUser newUser = new AppUser {
            //     UserName = request.EmployeeBasicDetail.Email,
            //     FirstName = request.EmployeeBasicDetail.FullName,
            //     LastName = request.EmployeeBasicDetail.FullName,
            //     Email = request.EmployeeBasicDetail.Email,
            //     PhoneNumber = request.EmployeeBasicDetail.PhoneNo.ToString ()
            // };

            // AppUser existUser = await _userManager.FindByNameAsync (request.EmployeeBasicDetail.Email);

            // if (existUser == null) {
            //     IdentityResult objNew = await _userManager.CreateAsync (newUser, request.EmployeeBasicDetail.Password);

            //     if (!objNew.Succeeded) {
            //         throw new Exception ("Could Not Create App User");
            //     }

            //     UserDetails user = new UserDetails ();

            //     user.FirstName = request.EmployeeBasicDetail.FullName;
            //     user.LastName = request.EmployeeBasicDetail.FullName;
            //     user.Password = request.EmployeeBasicDetail.Password;
            //     user.Status = request.Status;
            //     user.Username = request.EmployeeBasicDetail.Email;
            //     user.CreatedById = request.CreatedById;
            //     user.CreatedDate = request.CreatedDate;
            //     user.UserType = request.UserType;
            //     user.AspNetUserId = newUser.Id;
            //     user.EmployeeId = emp.EmployeeID;

            //     await _dbContext.UserDetails.AddAsync (user);
            //     await _dbContext.SaveChangesAsync ();

            //     List<UserDetailOffices> lst = new List<UserDetailOffices> ();

            //     await _dbContext.UserDetailOffices.AddRangeAsync (lst);
            //     await _dbContext.SaveChangesAsync ();

            //     response.StatusCode = StaticResource.successStatusCode;
            //     response.Message = StaticResource.SuccessText;
            // } else {
            //     response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
            //     response.Message = StaticResource.EmailAlreadyExist;
            // }

            // } catch (Exception ex) {
            //     response.StatusCode = StaticResource.failStatusCode;
            //     response.Message = StaticResource.SomethingWrong + ex.Message;
            // }
            // return response;
        }
    }
}