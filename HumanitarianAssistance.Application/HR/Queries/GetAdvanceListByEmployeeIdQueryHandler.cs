using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAdvanceListByEmployeeIdQueryHandler: IRequestHandler<GetAdvanceListByEmployeeIdQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAdvanceListByEmployeeIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetAdvanceListByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response  = new Dictionary<string, object>();

            try
            {
                EmployeeBasicSalaryDetail model = await _dbContext.EmployeeBasicSalaryDetail
                                                                  .Include(x=> x.CurrencyDetails)
                                                                  .FirstOrDefaultAsync(x=> x.IsDeleted == false && x.EmployeeId == request.EmployeeId);

                if(model == null) 
                {
                    throw new Exception("Employee payroll currency not set");
                }
                else if(model.CurrencyId == null)
                {
                    throw new Exception("Employee payroll currency not set");
                }

                var result = await _dbContext.Advances
                                             .Include(x=> x.CurrencyDetails)
                                             .Include(x=> x.ApprovedByEmployee)
                                             .Where(x=> x.IsDeleted == false && x.EmployeeId == request.EmployeeId)
                                             .Select(x=> new  {
                                                 AdvanceId = x.AdvancesId,
                                                 CurrencyId = model.CurrencyId,
                                                 CurrencyName = model.CurrencyDetails.CurrencyName,
                                                 ApprovedByEmployeeId = x.ApprovedBy,
                                                 ApprovedByEmployeeName = x.ApprovedByEmployee.EmployeeName,
                                                 ModeOfReturn = x.ModeOfReturn,
                                                 RequestAmount = x.RequestAmount,
                                                 AdvanceAmount = x.AdvanceAmount,
                                                 Status = x.IsApproved == null ? "UnApproved" : x.IsApproved.Value ? "Approved" : "Rejected",
                                             })
                                             .ToListAsync();
                
                response.Add("Advances", result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return response;
        }
    }
}