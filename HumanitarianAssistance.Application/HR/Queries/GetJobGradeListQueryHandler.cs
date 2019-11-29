using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetJobGradeListQueryHandler: IRequestHandler<GetJobGradeListQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetJobGradeListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetJobGradeListQuery request, CancellationToken cancellationToken)
        {
             Dictionary<string, object> result= new Dictionary<string, object>();

            var query = _dbContext.JobGrade.OrderByDescending(x=> x.GradeId).Where(x => x.IsDeleted == false).Select(x => new
            {
                GradeId = x.GradeId,
                GradeName = x.GradeName,
            }).AsQueryable();

            long count = await query.CountAsync();
            var queryResult= await query.Skip(request.PageSize * request.PageIndex).Take(request.PageSize).ToListAsync();
            result.Add("RecordCount", count);
            result.Add("Result", queryResult);
            
            return result;
        }
    }
}