using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetDefaultAccountingPeriodQueryHandler : IRequestHandler<GetDefaultAccountingPeriodQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetDefaultAccountingPeriodQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetDefaultAccountingPeriodQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> reponse = new Dictionary<string, object>();

            try
            {
                var result = await _dbContext.FinancialYearDetail.Where(x => x.IsDeleted == false && x.IsDefault == true)
                                        .Select(x => new 
                                        { 
                                            x.StartDate,
                                            x.EndDate
                                        }).FirstOrDefaultAsync();

                reponse.Add("AccountingPeriod", result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return reponse;
        }
    }
}