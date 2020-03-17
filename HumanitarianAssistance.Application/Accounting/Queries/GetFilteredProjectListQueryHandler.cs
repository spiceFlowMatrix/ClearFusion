using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetFilteredProjectListQueryHandler: IRequestHandler<GetFilteredProjectListQuery, object>
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
                if(request.FilterValue == null || request.FilterValue == "null")
                {
                    var projectList = await _dbContext.ProjectDetail.Where(x=> x.IsDeleted == false)
                                    .OrderByDescending(x=>x.CreatedDate)
                                    .Select(x=> new {
                                        ProjectId= x.ProjectId,
                                        ProjectName=  x.ProjectName,
                                        ProjectCode= x.ProjectCode
                                    }).Take(15)
                                    .ToListAsync();

                    response.Add("projectList", projectList);
                } 
                else 
                {
                    var projectList = await _dbContext.ProjectDetail.Where(x=> x.IsDeleted == false &&
                                  x.ProjectName.ToLower().Contains(request.FilterValue.ToLower()) ||
                                  x.ProjectCode.ToLower().Contains(request.FilterValue.ToLower()))
                                  .Select(x=> new {
                                      ProjectId= x.ProjectId,
                                      ProjectName=  x.ProjectName,
                                      ProjectCode= x.ProjectCode
                                  })
                                  .ToListAsync();

                    response.Add("projectList", projectList);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return response;   
        }
    }
}