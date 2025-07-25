﻿using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditProjectMonitoringByMonitoringIdCommandHandler : IRequestHandler<EditProjectMonitoringByMonitoringIdCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditProjectMonitoringByMonitoringIdCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditProjectMonitoringByMonitoringIdCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                var monitoring = await _dbContext.ProjectMonitoringReviewDetail
                                                .FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectMonitoringReviewId == request.ProjectMonitoringReviewId);

                // monitoring.ActivityId = request.ActivityId;
                monitoring.ModifiedById = request.ModifiedById;
                // monitoring.ProjectId = request.ProjectId;
                monitoring.MonitoringDate = request.MonitoringDate;
                monitoring.ModifiedDate = request.ModifiedDate;
                monitoring.IsDeleted = false;
                monitoring.NegativePoints = request.NegativePoints;
                monitoring.PostivePoints = request.PositivePoints;
                monitoring.Recommendations = request.Recommendations;
                monitoring.Remarks = request.Remarks;

                _dbContext.ProjectMonitoringReviewDetail.Update(monitoring);
                await _dbContext.SaveChangesAsync();

                List<ProjectMonitoringIndicatorDetail> indicators = await _dbContext.ProjectMonitoringIndicatorDetail.Where(x => x.IsDeleted == false && x.ProjectMonitoringReviewId == monitoring.ProjectMonitoringReviewId).ToListAsync();

                indicators.ForEach(x => x.IsDeleted = true);

                _dbContext.ProjectMonitoringIndicatorDetail.UpdateRange(indicators);
                await _dbContext.SaveChangesAsync();
                // delete indicator question whose MonitoringIndicatorId is same as deleted indicator record will delete
                foreach (var item in indicators)
                {
                    var indicatorQuestion = await _dbContext.ProjectMonitoringIndicatorQuestions.Where(x => x.MonitoringIndicatorId == item.MonitoringIndicatorId).ToListAsync();
                    indicatorQuestion.ForEach(x => x.IsDeleted = true);
                    _dbContext.ProjectMonitoringIndicatorQuestions.RemoveRange(indicatorQuestion);
                    await _dbContext.SaveChangesAsync();
                }
                foreach (var item in request.MonitoringReviewModel)
                {
                    ProjectMonitoringIndicatorDetail monitoringIndicatorDetail = new ProjectMonitoringIndicatorDetail();

                    if (item.MonitoringIndicatorId == null)
                    {
                        monitoringIndicatorDetail.CreatedById = request.CreatedById;
                        monitoringIndicatorDetail.CreatedDate = request.CreatedDate;
                        monitoringIndicatorDetail.IsDeleted = false;
                        monitoringIndicatorDetail.ProjectMonitoringReviewId = monitoring.ProjectMonitoringReviewId;
                        monitoringIndicatorDetail.ProjectIndicatorId = item.ProjectIndicatorId;
                        monitoringIndicatorDetail.QuestionTypeId = item.QuestionTypeId;

                        await _dbContext.ProjectMonitoringIndicatorDetail.AddAsync(monitoringIndicatorDetail);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        monitoringIndicatorDetail = await _dbContext.ProjectMonitoringIndicatorDetail
                                                                                    .FirstOrDefaultAsync(x => x.MonitoringIndicatorId == item.MonitoringIndicatorId);

                        monitoringIndicatorDetail.ModifiedById = request.ModifiedById;
                        monitoringIndicatorDetail.IsDeleted = false;
                        monitoringIndicatorDetail.ModifiedDate = request.ModifiedDate;
                        monitoringIndicatorDetail.ProjectIndicatorId = item.ProjectIndicatorId;
                        monitoringIndicatorDetail.QuestionTypeId = item.QuestionTypeId;
                        _dbContext.ProjectMonitoringIndicatorDetail.Update(monitoringIndicatorDetail);
                        await _dbContext.SaveChangesAsync();
                    }

                    if (item.IndicatorQuestions.Any())
                    {

                        foreach (var obj in item.IndicatorQuestions)
                        {
                            ProjectMonitoringIndicatorQuestions monitoringQuestions = new ProjectMonitoringIndicatorQuestions();
                            // add each question for each monitoring inicator id
                            monitoringQuestions.IsDeleted = false;
                            monitoringQuestions.CreatedDate = request.CreatedDate;
                            monitoringQuestions.CreatedById = request.CreatedById;
                            monitoringQuestions.IndicatorQuestionId = obj.IndicatorQuestionId;
                            monitoringQuestions.VerificationSourceName = obj.VerificationSourceName;
                            monitoringQuestions.VerificationSourceId = obj.VerificationSourceId;
                            monitoringQuestions.MonitoringIndicatorId = monitoringIndicatorDetail.MonitoringIndicatorId;
                            monitoringQuestions.Score = obj.Score;
                            await _dbContext.ProjectMonitoringIndicatorQuestions.AddAsync(monitoringQuestions);
                            await _dbContext.SaveChangesAsync();
                           
                        }
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
