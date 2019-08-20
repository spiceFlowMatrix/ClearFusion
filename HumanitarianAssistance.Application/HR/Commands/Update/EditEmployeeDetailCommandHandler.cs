using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEmployeeDetailCommandHandler : IRequestHandler<EditEmployeeDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public EditEmployeeDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(EditEmployeeDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var employeeinfo = await _dbContext.EmployeeDetail.FirstOrDefaultAsync(x => x.EmployeeID == request.EmployeeID && x.IsDeleted==false);
                if (employeeinfo != null)
                {
                    var OfficeDetail = await _dbContext.OfficeDetail.FirstOrDefaultAsync(x => x.OfficeId == request.OfficeId && x.IsDeleted == false);
                    employeeinfo.EmployeeCode = OfficeDetail.OfficeCode + employeeinfo.EmployeeID;
                    employeeinfo.EmployeeTypeId = request.EmployeeTypeId;
                    employeeinfo.EmployeeName = request.EmployeeName;
                    employeeinfo.IDCard = request.IDCard;
                    employeeinfo.FatherName = request.FatherName;
                    employeeinfo.GradeId = request.GradeId;
                    employeeinfo.PermanentAddress = request.PermanentAddress;
                    employeeinfo.City = request.City;
                    employeeinfo.District = request.District;
                    employeeinfo.ProvinceId = request.ProvinceId;
                    employeeinfo.CountryId = request.CountryId;
                    employeeinfo.Phone = request.Phone;
                    employeeinfo.Fax = request.Fax;
                    employeeinfo.Email = request.Email;
                    employeeinfo.ReferBy = request.ReferBy;
                    employeeinfo.Passport = request.Passport;
                    employeeinfo.NationalityId = request.NationalityId;
                    employeeinfo.Language = request.Language;
                    employeeinfo.SexId = request.SexId;
                    employeeinfo.Age = request.Age;
                    employeeinfo.DateOfBirth = Convert.ToDateTime(request.DateOfBirth);
                    employeeinfo.HigherQualificationId = request.HigherQualificationId;
                    employeeinfo.CurrentAddress = request.CurrentAddress;
                    employeeinfo.PreviousWork = request.PreviousWork;
                    employeeinfo.Remarks = request.Remarks;
                    employeeinfo.ExperienceYear = request.ExperienceYear;
                    employeeinfo.ExperienceMonth = request.ExperienceMonth;
                    employeeinfo.EmployeePhoto = request.EmployeePhoto;
                    employeeinfo.MaritalStatusId = request.MaritalStatus;
                    employeeinfo.University = request.University;
                    employeeinfo.PassportNo = request.PassportNo;
                    employeeinfo.BirthPlace = request.BirthPlace;
                    employeeinfo.IssuePlace = request.IssuePlace;

                     _dbContext.EmployeeDetail.Update(employeeinfo);
                    await _dbContext.SaveChangesAsync();
                    var employeeprofessionalinfo = await _dbContext.EmployeeProfessionalDetail.FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeID && x.IsDeleted==false);
                    employeeprofessionalinfo.ProfessionId = request.ProfessionId;
                    employeeprofessionalinfo.TinNumber = request.TinNumber;
                    _dbContext.EmployeeProfessionalDetail.Update(employeeprofessionalinfo);
                    await _dbContext.SaveChangesAsync();

                    var user = await _dbContext.UserDetails.FirstOrDefaultAsync(x => x.AspNetUserId == request.ModifiedById);

                    LoggerDetailsModel loggerObj = new LoggerDetailsModel
                    {
                        NotificationId = (int)Common.Enums.LoggerEnum.EmployeeUpdate,
                        IsRead = false,
                        UserName = user.FirstName + " " + user.LastName,
                        UserId = request.ModifiedById,
                        LoggedDetail = "Employee " + employeeinfo.EmployeeName + " Updated",
                        CreatedDate = request.CreatedDate
                    };

                    response.LoggerDetailsModel = loggerObj;

                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
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
