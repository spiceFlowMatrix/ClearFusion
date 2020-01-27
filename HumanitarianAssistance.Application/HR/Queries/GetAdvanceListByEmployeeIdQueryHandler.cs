using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
                var result = await _dbContext.Advances
                                             .Include(x=> x.CurrencyDetails)
                                             .Include(x=> x.ApprovedByEmployee)
                                             .Where(x=> x.IsDeleted == false && x.EmployeeId == request.EmployeeId)
                                             .Select(x=> new  {
                                                 CurrencyId = x.CurrencyId,
                                                 CurrencyName = x.CurrencyDetails.CurrencyName,
                                                 ApprovedByEmployeeId = x.ApprovedBy,
                                                 ApprovedByEmployeeName = x.ApprovedByEmployee.EmployeeName,
                                                 ModeOfReturn = x.ModeOfReturn,
                                                 RequestAmount = x.RequestAmount,
                                                 AdvanceAmount = x.AdvanceAmount,
                                                 Status = x.ApprovedBy

                                             })
                                             .ToListAsync();

            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return response;
        }
    }
}