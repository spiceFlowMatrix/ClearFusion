using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeBonusFineSalaryHeadQueryHandler : IRequestHandler<GetEmployeeBonusFineSalaryHeadQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetEmployeeBonusFineSalaryHeadQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetEmployeeBonusFineSalaryHeadQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                var result = await _dbContext.EmployeeBonusFineSalaryHead
                                       .Where(x => x.IsDeleted == false &&
                                       x.EmployeeId == request.EmployeeId && x.Month == request.Month && x.Year == DateTime.UtcNow.Year)
                                       .Select(x => new
                                       {
                                           Id = x.Id,
                                           SalaryHeadName = x.SalaryHeadName,
                                           Description = x.Description,
                                           Amount = x.Amount,
                                           TransactionTypeId = x.TransactionTypeId
                                       }).ToListAsync();

                response.Add("BonusFineSalaryHead", result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}