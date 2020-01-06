using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllLeaveReasonTypeQueryHandler: IRequestHandler<GetAllLeaveReasonTypeQuery, object>
    {       
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllLeaveReasonTypeQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetAllLeaveReasonTypeQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> result= new Dictionary<string, object>();

            var query = _dbContext.LeaveReasonDetail.OrderByDescending(x=> x.LeaveReasonId).Where(x => x.IsDeleted == false).Select(x => new
            {
                LeaveReasonId = x.LeaveReasonId,
                ReasonName = x.ReasonName,
                Description= x.Description,
                Unit= x.Unit
            }).AsQueryable();

            long count = await query.CountAsync();
            var queryResult= await query.Skip(request.PageSize * request.PageIndex).Take(request.PageSize).ToListAsync();
            result.Add("RecordCount", count);
            result.Add("Result", queryResult);
            
            return result;
        }
    }
}