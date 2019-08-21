using System.Linq;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using HumanitarianAssistance.Domain.Entities.Project;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Helpers;
using System;
using System.Threading;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllStrengthConsiderationDetailsQueryHandler: IRequestHandler<GetAllStrengthConsiderationDetailsQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllStrengthConsiderationDetailsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
             _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllStrengthConsiderationDetailsQuery request, CancellationToken token)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var StrengthConsiderationDetail = await _dbContext.StrengthConsiderationDetail
                                                                  .Where(x => x.IsDeleted == false)
                                                                  .Select(x => new StrengthConsiderationDetail
                                                                    {
                                                                        StrengthConsiderationId = x.StrengthConsiderationId,
                                                                        StrengthConsiderationName = x.StrengthConsiderationName
                                                                    })
                                                                    .OrderBy(x => x.StrengthConsiderationName).ToListAsync();
               
                response.data.StrengthConsiderationDetail = StrengthConsiderationDetail;
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