using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetGainLossCaculatorConfigurationQueryHandler : IRequestHandler<GetGainLossCaculatorConfigurationQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetGainLossCaculatorConfigurationQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetGainLossCaculatorConfigurationQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                var data = await _dbContext.GainLossCalculatorConfiguration
                                                                        .Where(x => x.IsDeleted == false && x.UserId == request.UserId)
                                                                        .Select(x => new
                                                                        {
                                                                            CurrencyId = x.CurrencyId,
                                                                            ComparisionDate= x.ComparisionDate,
                                                                            StartDate= x.StartDate,
                                                                            EndDate= x.EndDate,
                                                                            DebitAccount= x.DebitAccountId,
                                                                            CreditAccount= x.CreditAccountId
                                                                        })
                                                                        .FirstOrDefaultAsync();
                response.Add("CalculatorConfiguration", data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}