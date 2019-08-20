using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEmployeeAccountSalaryCommandHandler: IRequestHandler<EditEmployeeAccountSalaryCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EditEmployeeAccountSalaryCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<ApiResponse> Handle(EditEmployeeAccountSalaryCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var existrecord = await _dbContext.EmployeePayrollAccountHead.Where(x => x.EmployeeId == request.EmployeePayrollAccount.FirstOrDefault().EmployeeId).ToListAsync();

                if (existrecord.Any())
                {
                    _dbContext.EmployeePayrollAccountHead.RemoveRange(existrecord);
                    await _dbContext.SaveChangesAsync();
                }

                List<EmployeePayrollAccountHead> employeepayrollAccountlist = new List<EmployeePayrollAccountHead>();

                foreach (var list in request.EmployeePayrollAccount)
                {
                    EmployeePayrollAccountHead obj = new EmployeePayrollAccountHead();
                    obj.EmployeeId = list.EmployeeId;
                    obj.AccountNo = list.AccountNo;
                    obj.Description = list.Description;
                    obj.PayrollHeadName = list.PayrollHeadName;
                    obj.PayrollHeadTypeId = list.PayrollHeadTypeId;
                    obj.TransactionTypeId = list.TransactionTypeId;
                    obj.PayrollHeadId = list.PayrollHeadId;
                    obj.IsDeleted = false;

                    employeepayrollAccountlist.Add(obj);
                }

                await _dbContext.EmployeePayrollAccountHead.AddRangeAsync(employeepayrollAccountlist);
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