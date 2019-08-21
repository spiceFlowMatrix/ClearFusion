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
using HumanitarianAssistance.Application.Project.Models;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllProjectListQueryHandler : IRequestHandler<GetAllProjectListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public GetAllProjectListQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(GetAllProjectListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var ProjectList = await _dbContext.ProjectDetail
                                          .Where(x => !x.IsDeleted)
                                          .OrderByDescending(x => x.ProjectId).Select(x => new ProjectDetailModel
                                          {
                                              ProjectId = x.ProjectId,
                                              ProjectCode = x.ProjectCode,
                                              ProjectName = x.ProjectName,
                                              ProjectDescription = x.ProjectDescription,
                                              IsWin = _dbContext.WinProjectDetails.Where(y => y.ProjectId == x.ProjectId).Select(y => y.IsWin).FirstOrDefault(),
                                              IsCriteriaEvaluationSubmit = x.IsCriteriaEvaluationSubmit,
                                              ProjectPhase = x.ProjectPhaseDetailsId == x.ProjectPhaseDetails.ProjectPhaseDetailsId ? x.ProjectPhaseDetails.ProjectPhase.ToString() : "",
                                              TotalDaysinHours = x.EndDate == null ? (Convert.ToString(Math.Round(DateTime.UtcNow.Subtract(x.StartDate.Value).TotalHours, 0) + ":" + DateTime.UtcNow.Subtract(x.StartDate.Value).Minutes)) : (Convert.ToString(Math.Round(x.EndDate.Value.Subtract(x.StartDate.Value).TotalHours, 0) + ":" + x.EndDate.Value.Subtract(x.StartDate.Value).Minutes))
                                          }).ToListAsync();
                response.data.ProjectDetailModel = ProjectList;
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
