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
using System.Collections.Generic;

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
                                               .Where(x => x.IsDeleted == false && x.ProjectId == request.ProjectId)
                                               .Select(x => new ProjectIndicatorViewModel
                                               {
                                                   IndicatorCode = x.IndicatorCode,
                                                   IndicatorName = x.IndicatorName,
                                                   ProjectIndicatorId = x.ProjectIndicatorId,
                                                   Description = x.Description,
                                                   Questions = x.ProjectIndicatorQuestions.Where(y => y.IsDeleted == false).Count()
                                               }).AsQueryable();
                // Note: check for indicator is seletced in project activity 
                List<long> commonInicatorIdList = indicators.Select(x => x.ProjectIndicatorId).ToList();
                List<long> selectedIndicatorId = _dbContext.ProjectMonitoringReviewDetail
                                                           .Include(x => x.ProjectMonitoringIndicatorDetail)
                                                           .Where(x => x.ProjectId == request.ProjectId && x.IsDeleted == false)
                                                           .Select(x => x.ProjectMonitoringIndicatorDetail.Select(y => y.ProjectIndicatorId).ToList())
                                                 .FirstOrDefault();

                var indicotorList = await indicators.ToListAsync();

                if (selectedIndicatorId != null )
                {
                    commonInicatorIdList = commonInicatorIdList.Intersect(selectedIndicatorId).ToList();

                    foreach (var item in commonInicatorIdList)
                    {
                        var index = indicotorList.FindIndex(x => x.ProjectIndicatorId == item);
                        indicotorList[index].CanDelete = true;
                    }
                }

                long recordCount = await indicators.CountAsync();

                if (request.PageIndex != 0 || request.PageSize != 0)
                {
                    indicotorList = indicotorList.Skip((request.PageIndex * request.PageSize)).Take(request.PageSize).ToList();
                }



                //var indicatorList = await indicators.ToListAsync();


                if (indicotorList.Any())
                {
                    projectIndicators.ProjectIndicators.AddRange(indicotorList);
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
