using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddNewEmployeeCommandHandler : IRequestHandler<AddNewEmployeeCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHRService _hrService;
        public AddNewEmployeeCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IHRService hrService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hrService = hrService;
        }

        public async Task<ApiResponse> Handle(AddNewEmployeeCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    EmployeeDetail obj = _mapper.Map<EmployeeDetail>(request);
                    obj.IsDeleted = false;

                    await _dbContext.EmployeeDetail.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();

                    OfficeDetail OfficeDetail = await _dbContext.OfficeDetail.FirstOrDefaultAsync(x => x.OfficeId == request.OfficeId && x.IsDeleted == false);
                    obj.EmployeeCode = "E" + obj.EmployeeID;

                    _dbContext.EmployeeDetail.Update(obj);
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
                        TinNumber = request.TinNumber,
                        HiredOn = request.HiredOn,
                        EmployeeContractTypeId = request.EmployeeContractTypeId,
                        FiredOn = request.FiredOn,
                        FiredReason = request.FiredReason,
                        ResignationOn = request.ResignationOn,
                        ResignationReason = request.ResignationReason
                    };

                    EmployeeProfessionalDetail obj1 = _mapper.Map<EmployeeProfessionalDetail>(empprofessional);
                    await _dbContext.EmployeeProfessionalDetail.AddAsync(obj1);
                    await _dbContext.SaveChangesAsync();

                    if (request.EmployeeTypeId != (int)EmployeeTypeStatus.Prospective)
                    {
                        bool isEmployeeSalaryHeadSaved = await _hrService.AddEmployeePayrollDetails(obj.EmployeeID);

                        if (!isEmployeeSalaryHeadSaved)
                        {
                            throw new Exception(StaticResource.SalaryHeadNotSaved);
                        }
                    }

                    List<int> office = new List<int>();
                    office.Add(request.OfficeId);

                    UserModel addUserCommand = new UserModel
                    {
                        Email= request.Email,
                        Phone= request.Phone,
                        FirstName= request.EmployeeName,
                        OfficeId= office,
                        EmployeeId = request.EmployeeID,
                        Status= (int)UserStatus.Active
                    };

                    response = await _hrService.AddUser(addUserCommand);

                    if(response.StatusCode != 200)
                    {
                        throw new Exception(response.Message);
                    }

                    tran.Commit();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = ex.Message;
                }
            }
            return response;
        }
    }
}
