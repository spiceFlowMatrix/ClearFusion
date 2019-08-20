using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using HumanitarianAssistance.Application.Project.Models;

namespace HumanitarianAssistance.Application.Project.Queries
{
        public class GetAllProjectIndicatorsQueryHandler : IRequestHandler<GetAllProjectIndicatorsQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetAllProjectIndicatorsQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<ApiResponse> Handle(GetAllProjectIndicatorsQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            ProjectIndicatorModel projectIndicators = new ProjectIndicatorModel();

            try
            {
                var indicators = _dbContext.ProjectIndicators
                                               .OrderByDescending(x => x.CreatedDate)
                                               .Where(x => x.IsDeleted == false)
                                               .Select(x => new ProjectIndicatorViewModel
                                               {
                                                   IndicatorCode = x.IndicatorCode,
                                                   IndicatorName = x.IndicatorName,
                                                   ProjectIndicatorId = x.ProjectIndicatorId
                                               }).AsQueryable();



                if (request.PageIndex != 0 || request.PageSize != 0)
                {
                    indicators = indicators.Skip((request.PageIndex * request.PageSize)).Take(request.PageSize);
                }

                long recordCount = await _dbContext.ProjectIndicators
                                              .Where(x => x.IsDeleted == false).CountAsync();

                var indicatorList = await indicators.ToListAsync();

                if (indicatorList.Any())
                {
                    projectIndicators.ProjectIndicators.AddRange(indicatorList);
                    response.data.ProjectIndicatorList = new ProjectIndicatorModel();
                    response.data.ProjectIndicatorList.ProjectIndicators = projectIndicators.ProjectIndicators;
                    response.data.ProjectIndicatorList.IndicatorRecordCount = recordCount;
                }

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex;
            }

            return response;
        }
    }
}
