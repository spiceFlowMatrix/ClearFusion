using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddNewEmployeeCommandHandler : IRequestHandler<AddNewEmployeeCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddNewEmployeeCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddNewEmployeeCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                EmployeeDetail obj = _mapper.Map<EmployeeDetail>(request);
                obj.IsDeleted = false;
                await _dbContext.EmployeeDetail.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
                OfficeDetail OfficeDetail = await _dbContext.OfficeDetail.FirstOrDefaultAsync(x => x.OfficeId == request.OfficeId && x.IsDeleted == false);
                EmployeeDetail emp = await _dbContext.EmployeeDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.EmployeeID == obj.EmployeeID);
                emp.EmployeeCode = "E" + obj.EmployeeID;
                 _dbContext.EmployeeDetail.Update(emp);
                await _dbContext.SaveChangesAsync();
                EmployeeProfessionalDetailModel empprofessional = new EmployeeProfessionalDetailModel
                {
                    EmployeeId = obj.EmployeeID,
                    EmployeeTypeId = request.EmployeeTypeId,
                    OfficeId = request.OfficeId,
                    CreatedById = request.CreatedById,
                    CreatedDate = request.CreatedDate,
                    IsDeleted = request.IsDeleted,
                    ProfessionId = request.ProfessionId,
                    TinNumber = request.TinNumber
                };
                EmployeeProfessionalDetail obj1 = _mapper.Map<EmployeeProfessionalDetail>(empprofessional);
                await _dbContext.EmployeeProfessionalDetail.AddAsync(obj1);
                await _dbContext.SaveChangesAsync();

                UserDetails user = await _dbContext.UserDetails.FirstOrDefaultAsync(x => x.AspNetUserId == request.CreatedById && x.IsDeleted == false);

                LoggerDetailsModel loggerObj = new LoggerDetailsModel();
                loggerObj.NotificationId = (int)Common.Enums.LoggerEnum.EmployeeCreated;
                loggerObj.IsRead = false;
                loggerObj.UserName = user.FirstName + " " + user.LastName;
                loggerObj.UserId = request.CreatedById;
                loggerObj.LoggedDetail = "Employee " + obj.EmployeeName + " Created";
                loggerObj.CreatedDate = request.CreatedDate;

                response.LoggerDetailsModel = loggerObj;

                await _dbContext.SaveChangesAsync();
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
