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
    public class GetAllVouchersWithoutFilterQueryHandler : IRequestHandler<GetAllVouchersWithoutFilterQuery, object>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllVouchersWithoutFilterQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(GetAllVouchersWithoutFilterQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> reponse = new Dictionary<string, object>();

            try
            {
               var result = await _dbContext.VoucherDetail
                                            .Where(x=> x.IsDeleted == false)
                                            .Select(x=> new 
                                            {
                                                x.VoucherNo,
                                                x.ReferenceNo,
                                            })
                                            .ToListAsync();
                
                reponse.Add("Vouchers", result);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return reponse;
        }
    }
}