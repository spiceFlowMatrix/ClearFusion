using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectCodeByProjectIdQueryHandler : IRequestHandler<GetProjectCodeByProjectIdQuery, object>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public GetProjectCodeByProjectIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<object> Handle(GetProjectCodeByProjectIdQuery request, CancellationToken cancellationToken)
        {
            string projectCode= string.Empty;

            try
            {
                ProjectDetail projectDetail = await _dbContext.ProjectDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId && x.IsDeleted == false);
                
                if (projectDetail == null)
                {
                    throw new Exception("Project Id not found");
                }

                projectCode = projectDetail.ProjectCode;
            }
            catch(Exception ex) 
            {
                throw ex;
            }

            return projectCode;
        }
    }
}