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
    public class GetFilteredProjectListQueryHandler : IRequestHandler<GetFilteredProjectListQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetFilteredProjectListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(GetFilteredProjectListQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {

                var list = await _dbContext.ProjectDetail.Where(x => x.IsDeleted == false &&
                                     (x.ProjectCode.ToLower().Contains(request.FilterValue.ToLower()) ||
                                     x.ProjectName.ToLower().Contains(request.FilterValue.ToLower()))).ToListAsync();

                response.Add("ProjectList", list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}