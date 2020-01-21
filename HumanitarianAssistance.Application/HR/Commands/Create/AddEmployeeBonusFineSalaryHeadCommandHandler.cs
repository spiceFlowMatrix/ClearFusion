using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddEmployeeBonusFineSalaryHeadCommandHandler: IRequestHandler<AddEmployeeBonusFineSalaryHeadCommand, object>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;

        public AddEmployeeBonusFineSalaryHeadCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(AddEmployeeBonusFineSalaryHeadCommand request, CancellationToken cancellationToken)
        {
            bool success= false;

            try
            {
                if(request == null)
                {
                    throw new Exception(StaticResource.RequestValuesInAppropriate);
                }
                
                EmployeeBonusFineSalaryHead salaryHead = new EmployeeBonusFineSalaryHead 
                {
                    EmployeeId = request.EmployeeId,
                    Amount= request.Amount,
                    SalaryHeadName= request.SalaryHead,
                    CreatedById= request.CreatedById,
                    CreatedDate = request.CreatedDate,
                    Description = request.Description,
                    TransactionTypeId = request.IsBonus ? (int)TransactionType.Debit : (int)TransactionType.Credit
                };

                await _dbContext.EmployeeBonusFineSalaryHead.AddAsync(salaryHead);
                await _dbContext.SaveChangesAsync();
                success = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}