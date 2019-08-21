using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Domain.Entities.Project;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Helpers;
using System;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllGenderConsiderationQueryHandler: IRequestHandler<GetAllGenderConsiderationQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllGenderConsiderationQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllGenderConsiderationQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var GenderConsiderationDetail = await _dbContext.GenderConsiderationDetail
                                                          .Where(x => x.IsDeleted == false)
                                                          .Select(x => new GenderConsiderationDetail
                                                        {
                                                            GenderConsiderationId = x.GenderConsiderationId,
                                                            GenderConsiderationName = x.GenderConsiderationName
                                                        }).OrderBy(x => x.GenderConsiderationName).ToListAsync();
                
                response.data.GenderConsiderationDetail = GenderConsiderationDetail;
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