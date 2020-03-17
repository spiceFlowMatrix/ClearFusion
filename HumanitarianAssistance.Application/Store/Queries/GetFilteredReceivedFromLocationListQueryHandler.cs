using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetFilteredReceivedFromLocationListQueryHandler : IRequestHandler<GetFilteredReceivedFromLocationListQuery, object>
    {
         private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetFilteredReceivedFromLocationListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(GetFilteredReceivedFromLocationListQuery request, CancellationToken cancellationToken)
        {
            
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {

                var list = await _dbContext.StoreSourceCodeDetail.Where(x => x.IsDeleted == false &&
                                     (x.Code.ToLower().Contains(request.FilterValue.ToLower()) ||
                                     x.Description.ToLower().Contains(request.FilterValue.ToLower()))).ToListAsync();

                response.Add("SourceCodeList", list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
            
            
           
        }
    }
}