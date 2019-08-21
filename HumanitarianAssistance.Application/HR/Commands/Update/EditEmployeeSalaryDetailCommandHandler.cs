using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEmployeeSalaryDetailCommandHandler : IRequestHandler<EditEmployeeSalaryDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;

        public EditEmployeeSalaryDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(EditEmployeeSalaryDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var existrecord = await _dbContext.EmployeePayroll.Where(x => x.EmployeeID == request.EmployeePayroll[0].EmployeeId).ToListAsync();
                _dbContext.EmployeePayroll.RemoveRange(existrecord);
                await _dbContext.SaveChangesAsync();

                List<EmployeePayroll> employeepayrolllist = new List<EmployeePayroll>();

                foreach (var list in request.EmployeePayroll)
                {
                    EmployeePayroll obj = new EmployeePayroll();

                    if (list.HeadTypeId == (int)SalaryHeadType.GENERAL && list.MonthlyAmount == 0)
                    {
                        throw new Exception("Basic Pay cannot be Zero");
                    }

                    obj.EmployeeID = list.EmployeeId;
                    obj.CurrencyId = list.CurrencyId;
                    obj.HeadTypeId = list.HeadTypeId;
                    obj.SalaryHeadId = list.SalaryHeadId;
                    obj.MonthlyAmount = list.MonthlyAmount;
                    obj.PaymentType = list.PaymentType;
                    obj.PensionRate = list.PensionRate;
                    obj.AccountNo = list.AccountNo;
                    obj.TransactionTypeId = list.TransactionTypeId;
                    obj.IsDeleted = false;

                    employeepayrolllist.Add(obj);
                }
                await _dbContext.EmployeePayroll.AddRangeAsync(employeepayrolllist);
                await _dbContext.SaveChangesAsync();
                

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