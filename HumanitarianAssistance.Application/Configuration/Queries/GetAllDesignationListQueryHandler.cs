using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllDesignationListQueryHandler: IRequestHandler<GetAllDesignationListQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllDesignationListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetAllDesignationListQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.DesignationDetail
                                         .Where(x=> x.IsDeleted == false)
                                         .Select(x=> new {
                                            x.DesignationId,
                                            x.Designation
                                         })
                                         .ToListAsync();
            return result;
        }
    }
}