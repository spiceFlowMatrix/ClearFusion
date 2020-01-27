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
    public class GetAdvanceDetailByIdQueryHandler: IRequestHandler<GetAdvanceDetailByIdQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAdvanceDetailByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetAdvanceDetailByIdQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response  = new Dictionary<string, object>(); 

            try
            {

                var result = await _dbContext.Advances.Where(x=> x.IsDeleted == false && x.AdvancesId == request.Id)
                                                        .Select(x=> new  
                                                        {
                                                            AdvanceId = x.AdvancesId,
                                                            AdvanceDate = x.AdvanceDate,
                                                            ApprovedBy = x.ApprovedBy,
                                                            NumberOfInstallments= x.NumberOfInstallments,
                                                            ModeOfReturn= x.ModeOfReturn,
                                                            RequestAmount= x.RequestAmount,
                                                            Description= x.Description
                                                        }).FirstOrDefaultAsync();

                response.Add("AdvanceDetail", result);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}