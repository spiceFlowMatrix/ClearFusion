using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddSalaryHeadCommandHandler : IRequestHandler<AddSalaryHeadCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddSalaryHeadCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddSalaryHeadCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                SalaryHeadDetails obj = _mapper.Map<SalaryHeadDetails>(request);
                obj.IsDeleted = false;
                await _dbContext.SalaryHeadDetails.AddAsync(obj);
                await _dbContext.SaveChangesAsync();

                List<int> employeeIds = await _dbContext.EmployeeDetail.Where(x => x.IsDeleted == false &&
                                                                                   x.EmployeeTypeId == (int)EmployeeTypeStatus.Active)
                                                                       .Select(x => x.EmployeeID)
                                                                       .ToListAsync();

                if (employeeIds.Any())
                {
                    List<EmployeePayroll> employeePayrollList = new List<EmployeePayroll>();

                    foreach (int employeeid in employeeIds)
                    {
                        EmployeePayroll employeePayroll = new EmployeePayroll();
                        employeePayroll.IsDeleted = false;
                        employeePayroll.AccountNo = request.AccountNo;
                        employeePayroll.SalaryHeadId = obj.SalaryHeadId;
                        employeePayroll.HeadTypeId = request.HeadTypeId;
                        employeePayroll.AccountNo = request.AccountNo;
                        employeePayroll.TransactionTypeId = request.TransactionTypeId;
                        employeePayroll.MonthlyAmount = (double)request.MonthlyAmount;
                        employeePayrollList.Add(employeePayroll);
                    }

                    await _dbContext.EmployeePayroll.AddRangeAsync(employeePayrollList);
                    await _dbContext.SaveChangesAsync();
                }

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}