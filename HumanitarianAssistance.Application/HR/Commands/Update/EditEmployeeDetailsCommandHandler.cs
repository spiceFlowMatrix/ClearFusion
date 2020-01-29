using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Domain.Entities;

namespace HumanitarianAssistance.Application.HR.Commands.Update {
    public class EditEmployeeDetailsCommandHandler : IRequestHandler<EditEmployeeDetailsCommand, ApiResponse> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public EditEmployeeDetailsCommandHandler (HumanitarianAssistanceDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle (EditEmployeeDetailsCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                var employeeinfo = await _dbContext.EmployeeDetail.FirstOrDefaultAsync (x => x.EmployeeID == request.EmployeeBasicDetail.EmployeeId && x.IsDeleted == false);
                if (employeeinfo != null) {
                    employeeinfo.EmployeeTypeId = request.EmployeeProfessionalDetails.EmployeeType;
                    employeeinfo.EmployeeName = request.EmployeeBasicDetail.FullName;
                    //   employeeinfo.IDCard = request.IDCard;
                    employeeinfo.FatherName = request.EmployeeBasicDetail.FatherName;
                    employeeinfo.GradeId = request.EmployeeProfessionalDetails.JobGrade;
                    employeeinfo.PermanentAddress = request.EmployeeBasicDetail.PermanentAddress;
                    //   employeeinfo.City = request.City;
                    //  employeeinfo.District = request.EmployeeBasicDetail.District;
                    employeeinfo.ProvinceId = request.EmployeeBasicDetail.Province;
                    employeeinfo.CountryId = request.EmployeeBasicDetail.Country;
                    employeeinfo.Phone = request.EmployeeBasicDetail.PhoneNo;
                    // employeeinfo.Fax = request.Fax;
                    employeeinfo.Email = request.EmployeeBasicDetail.Email;
                    employeeinfo.ReferBy = request.EmployeeBasicDetail.ReferBy;
                    employeeinfo.Passport = request.EmployeeBasicDetail.PassportNumber;
                    // employeeinfo.NationalityId = request.NationalityId;
                    // employeeinfo.Language = request.Language;
                    employeeinfo.SexId = request.EmployeeBasicDetail.Gender;
                    // employeeinfo.Age = request.Age;
                    employeeinfo.DateOfBirth = Convert.ToDateTime (request.EmployeeBasicDetail.DateOfBirth);
                    employeeinfo.HigherQualificationId = request.EmployeeBasicDetail.Qualification;
                    employeeinfo.CurrentAddress = request.EmployeeBasicDetail.CurrentAddress;
                    employeeinfo.PreviousWork = request.EmployeeBasicDetail.PreviousWork;
                    // employeeinfo.Remarks = request.Remarks;
                    employeeinfo.ExperienceYear = request.EmployeeBasicDetail.ExperienceYear;
                    employeeinfo.ExperienceMonth = request.EmployeeBasicDetail.ExperienceMonth;
                    // employeeinfo.EmployeePhoto = request.EmployeePhoto;
                    employeeinfo.MaritalStatusId = request.EmployeeBasicDetail.MaritalStatus;
                    employeeinfo.University = request.EmployeeBasicDetail.University;
                    employeeinfo.PassportNo = request.EmployeeBasicDetail.PassportNumber;
                    employeeinfo.BirthPlace = request.EmployeeBasicDetail.BirthPlace;
                    employeeinfo.IssuePlace = request.EmployeeBasicDetail.IssuePlace;
                    employeeinfo.GradeId = request.EmployeeProfessionalDetails.JobGrade;
                    await _dbContext.SaveChangesAsync ();

                    var employeeprofessionalinfo = await _dbContext.EmployeeProfessionalDetail.FirstOrDefaultAsync (x => x.EmployeeId == request.EmployeeBasicDetail.EmployeeId && x.IsDeleted == false);
                    employeeprofessionalinfo.ProfessionId = request.EmployeeBasicDetail.Profession;
                    employeeprofessionalinfo.TinNumber = request.EmployeeBasicDetail.TinNumber;
                    employeeprofessionalinfo.EmployeeTypeId = request.EmployeeProfessionalDetails.EmployeeType;
                    employeeprofessionalinfo.OfficeId = request.EmployeeProfessionalDetails.Office;
                    employeeprofessionalinfo.DepartmentId = request.EmployeeProfessionalDetails.Department;
                    employeeprofessionalinfo.DesignationId = request.EmployeeProfessionalDetails.Designation;
                    employeeprofessionalinfo.EmployeeContractTypeId = request.EmployeeProfessionalDetails.EmployeeCotractType;
                    employeeprofessionalinfo.HiredOn = request.EmployeeProfessionalDetails.HiredOn;
                    employeeprofessionalinfo.AttendanceGroupId = request.EmployeeProfessionalDetails.AttendanceGroup;
                    employeeprofessionalinfo.DutyStation = request.EmployeeProfessionalDetails.DutyStation;
                    employeeprofessionalinfo.TrainingBenefits = request.EmployeeProfessionalDetails.TrainingAndBenefits;
                    employeeprofessionalinfo.JobDescription = request.EmployeeProfessionalDetails.JobDescription;

                    await _dbContext.SaveChangesAsync ();

                    UserDetails user = new UserDetails();
                    user.Password = request.EmployeeBasicDetail.Password;
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}