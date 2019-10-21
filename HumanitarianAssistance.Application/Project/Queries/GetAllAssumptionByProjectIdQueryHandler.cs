using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Helpers;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllAssumptionByProjectIdQueryHandler : IRequestHandler<GetAllAssumptionByProjectIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllAssumptionByProjectIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetAllAssumptionByProjectIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var list = await _dbContext.CEAssumptionDetail.Where(x => x.IsDeleted == false &&
                                                                                   x.ProjectId == request.ProjectId)
                                                                                .OrderByDescending(x => x.AssumptionDetailId)
                                                                                .ToListAsync();

                response.data.CEAssumptionDetail = list;
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
