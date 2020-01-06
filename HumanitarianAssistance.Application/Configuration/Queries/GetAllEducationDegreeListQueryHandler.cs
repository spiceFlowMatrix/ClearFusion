using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllEducationDegreeListQueryHandler : IRequestHandler<GetAllEducationDegreeListQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllEducationDegreeListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetAllEducationDegreeListQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.EducationDegreeMaster
                                         .Where(x=> x.IsDeleted == false)
                                         .Select(x=> new {
                                            x.Id,
                                            x.Name
                                         })
                                         .ToListAsync();
        }
    }
}