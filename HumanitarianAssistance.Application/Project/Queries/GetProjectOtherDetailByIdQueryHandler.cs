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
    public class GetProjectOtherDetailByIdQueryHandler: IRequestHandler<GetProjectOtherDetailByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetProjectOtherDetailByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }


        public async Task<ApiResponse> Handle(GetProjectOtherDetailByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var OtherProjectDetail = await _dbContext.ProjectOtherDetail
                                         .FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectId == request.Id);

                response.data.OtherProjectDetailById = OtherProjectDetail;
                response.StatusCode = 200;
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