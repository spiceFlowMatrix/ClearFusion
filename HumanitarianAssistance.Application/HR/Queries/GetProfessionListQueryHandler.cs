using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetProfessionListQueryHandler: IRequestHandler<GetProfessionListQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetProfessionListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(GetProfessionListQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> result= new Dictionary<string, object>();

            var query = _dbContext.ProfessionDetails.OrderByDescending(x=> x.ProfessionId).Where(x => x.IsDeleted == false).Select(x => new
            {
                ProfessionId = x.ProfessionId,
                ProfessionName = x.ProfessionName,
            }).AsQueryable();

            long count = await query.CountAsync();
            var queryResult= await query.Skip(request.PageSize * request.PageIndex).Take(request.PageSize).ToListAsync();
            result.Add("RecordCount", count);
            result.Add("Result", queryResult);
            return result;
        }
    }
}