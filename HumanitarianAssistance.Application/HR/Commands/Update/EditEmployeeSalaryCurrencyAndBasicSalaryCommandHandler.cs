using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEmployeeSalaryCurrencyAndBasicSalaryCommandHandler : IRequestHandler<EditEmployeeSalaryCurrencyAndBasicSalaryCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public EditEmployeeSalaryCurrencyAndBasicSalaryCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(EditEmployeeSalaryCurrencyAndBasicSalaryCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                EmployeeBasicSalaryDetail payroll = await _dbContext.EmployeeBasicSalaryDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.PayrollId);

                if(payroll == null)
                {
                    throw new Exception(StaticResource.RecordNotFound);
                }

                payroll.CurrencyId = request.CurrencyId;
                payroll.BasicSalary = request.ActiveSalary;
                payroll.ModifiedById = request.ModifiedById;
                payroll.ModifiedDate = DateTime.UtcNow;

                _dbContext.EmployeeBasicSalaryDetail.Update(payroll);
                await _dbContext.SaveChangesAsync();
                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}