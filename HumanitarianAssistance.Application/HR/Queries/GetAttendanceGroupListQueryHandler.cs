using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAttendanceGroupListQueryHandler : IRequestHandler<GetAttendanceGroupListQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAttendanceGroupListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetAttendanceGroupListQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            var query = _dbContext.AttendanceGroupMaster.OrderByDescending(x => x.AttendanceGroupId).Where(x => x.IsDeleted == false).Select(x => new
            {
                AttendanceGroupId = x.AttendanceGroupId,
                Name = x.Name,
                Description = x.Description,
            }).AsQueryable();

            long count = await query.CountAsync();
            var queryResult = await query.Skip(request.PageSize * request.PageIndex).Take(request.PageSize).ToListAsync();
            result.Add("RecordCount", count);
            result.Add("Result", queryResult);
            return result;
        }
    }
}