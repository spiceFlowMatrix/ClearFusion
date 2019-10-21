using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectAreaByIdQueryHandler: IRequestHandler<GetProjectAreaByIdQuery, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public GetProjectAreaByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {

            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(GetProjectAreaByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var Projectarea = await _dbContext.ProjectArea
                       .FirstOrDefaultAsync(x => !x.IsDeleted && x.ProjectId == request.ProjectId);
                       
                response.data.projectArea = Projectarea;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
        
    }
}