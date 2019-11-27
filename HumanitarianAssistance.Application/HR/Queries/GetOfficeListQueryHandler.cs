using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetOfficeListQueryHandler : IRequestHandler<GetOfficeListQuery, object>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetOfficeListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetOfficeListQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> result= new Dictionary<string, object>();

            var query = _dbContext.OfficeDetail.OrderByDescending(x=> x.OfficeId).Where(x => x.IsDeleted == false).Select(x => new
            {
                OfficeId = x.OfficeId,
                OfficeCode = x.OfficeCode,
                SupervisorName = x.SupervisorName,
                OfficeName= x.OfficeName,
                PhoneNo= x.PhoneNo,
                FaxNo= x.FaxNo
            }).AsQueryable();

            long count = await query.CountAsync();
            var queryResult= await query.Skip(request.PageSize * request.PageIndex).Take(request.PageSize).ToListAsync();
            result.Add("RecordCount", count);
            result.Add("Result", queryResult);
            
            return result;
        }
    }
}