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
    public class GetAdvanceHistoryByIdQueryHandler: IRequestHandler<GetAdvanceHistoryByIdQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAdvanceHistoryByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetAdvanceHistoryByIdQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {

                var result = await _dbContext.AdvanceHistoryDetail
                                       .Where(x=> x.IsDeleted == false && x.AdvanceId == request.AdvanceId)
                                       .Select(x=> new 
                                       {
                                          PaymentDate = x.PaymentDate.ToShortDateString(),
                                          InstallmentPaid= x.InstallmentPaid,
                                          InstallmentBalance= x.InstallmentBalance 
                                       }).ToListAsync();

                response.Add("AdvanceHistory", result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}