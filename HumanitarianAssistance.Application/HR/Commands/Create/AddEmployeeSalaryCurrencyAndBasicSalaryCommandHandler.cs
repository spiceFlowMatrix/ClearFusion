using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddEmployeeSalaryCurrencyAndBasicSalaryCommandHandler : IRequestHandler<AddEmployeeSalaryCurrencyAndBasicSalaryCommand, object>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        public AddEmployeeSalaryCurrencyAndBasicSalaryCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(AddEmployeeSalaryCurrencyAndBasicSalaryCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                EmployeeBasicSalaryDetail payroll = new EmployeeBasicSalaryDetail 
                {
                    CreatedDate= DateTime.UtcNow,
                    CreatedById = request.CreatedById,
                    BasicSalary = request.ActiveSalary,
                    EmployeeId = request.EmployeeId,
                    CurrencyId = request.CurrencyId,
                    CapacityBuildingAmount = request.CapacityBuilding,
                    SecurityAmount = request.Security
                };

                await _dbContext.EmployeeBasicSalaryDetail.AddAsync(payroll);
                await _dbContext.SaveChangesAsync();
                success= true;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}