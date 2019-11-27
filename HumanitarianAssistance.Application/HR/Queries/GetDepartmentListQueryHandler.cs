using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetDepartmentListQueryHandler : IRequestHandler<GetDepartmentListQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetDepartmentListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> result= new Dictionary<string, object>();

            var query = _dbContext.Department.OrderByDescending(x=> x.DepartmentId).Include(x=>x.OfficeDetails).Where(x => x.IsDeleted == false).Select(x => new
            {
                DepartmentId = x.DepartmentId,
                DepartmentName = x.DepartmentName,
                OfficeName = x.OfficeDetails.OfficeName,
                OfficeId = x.OfficeDetails.OfficeId
            }).AsQueryable();

            long count = await query.CountAsync();
            var queryResult= await query.Skip(request.PageSize * request.PageIndex).Take(request.PageSize).ToListAsync();
            result.Add("RecordCount", count);
            result.Add("Result", queryResult);
            
            return result;
        }
    }
}