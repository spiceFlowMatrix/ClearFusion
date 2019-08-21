using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProjectMonitoringReviewCommandHandler : IRequestHandler<AddProjectMonitoringReviewCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddProjectMonitoringReviewCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddProjectMonitoringReviewCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectMonitoringReviewDetail projectMonitoringReviewDetail = new ProjectMonitoringReviewDetail();
                projectMonitoringReviewDetail.ActivityId = request.ActivityId;
                projectMonitoringReviewDetail.CreatedById = request.CreatedById;
                projectMonitoringReviewDetail.ProjectId = request.ProjectId;
                projectMonitoringReviewDetail.MonitoringDate = request.MonitoringDate;
                projectMonitoringReviewDetail.CreatedDate = request.CreatedDate;
                projectMonitoringReviewDetail.IsDeleted = false;
                projectMonitoringReviewDetail.NegativePoints = request.NegativePoints;
                projectMonitoringReviewDetail.PostivePoints = request.PositivePoints;
                projectMonitoringReviewDetail.Recommendations = request.Recommendations;
                projectMonitoringReviewDetail.Remarks = request.Remarks;

                await _dbContext.ProjectMonitoringReviewDetail.AddAsync(projectMonitoringReviewDetail);
                await _dbContext.SaveChangesAsync();

                foreach (var item in request.MonitoringReviewModel)
                {
                    ProjectMonitoringIndicatorDetail monitoringIndicatorDetail = new ProjectMonitoringIndicatorDetail();
                    monitoringIndicatorDetail.CreatedById = request.CreatedById;
                    monitoringIndicatorDetail.CreatedDate = request.CreatedDate;
                    monitoringIndicatorDetail.IsDeleted = false;
                    monitoringIndicatorDetail.ProjectMonitoringReviewId = projectMonitoringReviewDetail.ProjectMonitoringReviewId;
                    monitoringIndicatorDetail.ProjectIndicatorId = item.ProjectIndicatorId;

                    await _dbContext.ProjectMonitoringIndicatorDetail.AddAsync(monitoringIndicatorDetail);
                    await _dbContext.SaveChangesAsync();

                    foreach (var obj in item.IndicatorQuestions)
                    {
                        ProjectMonitoringIndicatorQuestions monitoringQuestions = new ProjectMonitoringIndicatorQuestions();
                        monitoringQuestions.IsDeleted = false;
                        monitoringQuestions.CreatedDate = request.CreatedDate;
                        monitoringQuestions.CreatedById = request.CreatedById;
                        monitoringQuestions.QuestionId = obj.QuestionId;
                        monitoringQuestions.Verification = obj.Verification;
                        monitoringQuestions.VerificationId = obj.VerificationId;
                        monitoringQuestions.MonitoringIndicatorId = monitoringIndicatorDetail.MonitoringIndicatorId;
                        monitoringQuestions.Score = obj.Score;

                        await _dbContext.ProjectMonitoringIndicatorQuestions.AddAsync(monitoringQuestions);
                        await _dbContext.SaveChangesAsync();
                    }
                }
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
