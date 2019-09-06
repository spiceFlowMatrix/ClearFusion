using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditProjectIndicatorQuestionsCommandHandler : IRequestHandler<EditProjectIndicatorQuestionsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditProjectIndicatorQuestionsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditProjectIndicatorQuestionsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            List<VerificationSourcesModel> sourceModel = new List<VerificationSourcesModel>();

            try
            {
                if (request != null)
                {
                    ProjectIndicatorQuestions indicatorQuestion = await _dbContext.ProjectIndicatorQuestions
                                                                        .Include(x => x.VerificationSources)
                                                                        .FirstOrDefaultAsync(x => x.IndicatorQuestionId == request.IndicatorQuestionId &&
                                                                                                   x.IsDeleted == false);
                    if (indicatorQuestion != null)
                    {
                        indicatorQuestion.IndicatorQuestion = request.IndicatorQuestion;
                        indicatorQuestion.ProjectIndicatorId = request.ProjectIndicatorId;
                        indicatorQuestion.ModifiedById = request.ModifiedById;
                        indicatorQuestion.ModifiedDate = request.ModifiedDate;
                        indicatorQuestion.IsDeleted = false;
                        indicatorQuestion.QuestionType = request.QuestionType;

                        await _dbContext.SaveChangesAsync();

                        if (indicatorQuestion.VerificationSources.Any())
                        {
                            // check sources are they present in new requested list
                            var sourcesNotPresent = indicatorQuestion.VerificationSources.Where(x => !request.VerificationSources.Select(y => y.VerificationSourceId).Contains(x.VerificationSourceId));

                            // to delete the existing verification resource if not present in new list
                            if (sourcesNotPresent.Any())
                            {
                                foreach (var item in sourcesNotPresent)
                                {
                                    item.IsDeleted = true;
                                    item.ModifiedById = request.ModifiedById;
                                    item.ModifiedDate = request.ModifiedDate;
                                    _dbContext.VerificationSources.UpdateRange(item);
                                }
                                await _dbContext.SaveChangesAsync();
                            }
                            // to add and update new vrification sources 
                            List<VerificationSources> newSourceList = new List<VerificationSources>();

                            foreach (var item in request.VerificationSources)
                            {
                                VerificationSources ifExistsource = indicatorQuestion.VerificationSources.FirstOrDefault(x => x.VerificationSourceId == item.VerificationSourceId &&
                                                                                                                                x.IsDeleted == false);
                                // update existing v s and map to model
                                if (item.VerificationSourceId > 0 && ifExistsource != null)
                                {
                                    VerificationSourcesModel model = new VerificationSourcesModel
                                    {
                                        VerificationSourceId = item.VerificationSourceId,
                                        VerificationSourceName = item.VerificationSourceName,
                                        IndicatorQuestionId = item.IndicatorQuestionId
                                    };

                                    sourceModel.Add(model);

                                    ifExistsource.VerificationSourceName = item.VerificationSourceName;
                                    ifExistsource.IndicatorQuestionId = indicatorQuestion.IndicatorQuestionId;
                                }
                                else // if not exists then  add new verification source
                                {
                                    VerificationSources vsource = new VerificationSources
                                    {
                                        VerificationSourceName = item.VerificationSourceName,
                                        IndicatorQuestionId = indicatorQuestion.IndicatorQuestionId
                                    };
                                    newSourceList.Add(vsource);

                                };

                            }
                            await _dbContext.VerificationSources.AddRangeAsync(newSourceList);
                            await _dbContext.SaveChangesAsync();

                            // maping to model 
                            if (newSourceList.Any())
                            {
                                foreach (var item in newSourceList)
                                {
                                    VerificationSourcesModel model = new VerificationSourcesModel
                                    {
                                        VerificationSourceId = item.VerificationSourceId,
                                        VerificationSourceName = item.VerificationSourceName,
                                        IndicatorQuestionId = item.IndicatorQuestionId
                                    };
                                    sourceModel.Add(model);
                                }
                            }

                            ProjectIndicatorQuestionsModel questionModel = new ProjectIndicatorQuestionsModel()
                            {
                                IndicatorQuestion = indicatorQuestion.IndicatorQuestion,
                                IndicatorQuestionId = indicatorQuestion.IndicatorQuestionId,
                                QuestionType = indicatorQuestion.QuestionType,
                                VerificationSources = sourceModel,
                                QuestionTypeName = indicatorQuestion.QuestionType == (int)QuestionType.Qualitative ? "Qualitative" : indicatorQuestion.QuestionType == (int)QuestionType.Quantitative ? "Quantitative" : null,

                            };

                            response.ResponseData = questionModel;

                        }

                    }

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }

                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.ProjectIndicatorNotFound;
                }

            }
            catch (Exception exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + exception.Message;
            }

            return response;
        }
    }
}