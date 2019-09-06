using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProjectIndicatorQuestionsCommandHandler : IRequestHandler<AddProjectIndicatorQuestionsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddProjectIndicatorQuestionsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddProjectIndicatorQuestionsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    ProjectIndicatorQuestions indicator = new ProjectIndicatorQuestions
                    {
                        //IndicatorQuestionId = request.IndicatorQuestionId,
                        IndicatorQuestion = request.IndicatorQuestion,
                        ProjectIndicatorId = request.ProjectIndicatorId,
                        CreatedById = request.CreatedById,
                        CreatedDate = request.CreatedDate,
                        IsDeleted = false,
                        QuestionType = request.QuestionType,
                    };

                    await _dbContext.ProjectIndicatorQuestions.AddAsync(indicator);
                    await _dbContext.SaveChangesAsync();

                    List<VerificationSources> sourceList = new List<VerificationSources>();

                    if (request.VerificationSources.Any())
                    {
                        foreach (var item in request.VerificationSources)
                        {
                            VerificationSources source = new VerificationSources
                            {
                                VerificationSourceName = item.VerificationSourceName,
                                IndicatorQuestionId = indicator.IndicatorQuestionId
                            };
                            sourceList.Add(source);
                        }

                        await _dbContext.VerificationSources.AddRangeAsync(sourceList);
                        await _dbContext.SaveChangesAsync();

                    }
                    
                    List<VerificationSourcesModel> sourceModel = new List<VerificationSourcesModel>();
                    if (sourceList.Any())
                    {
                        foreach (var item in sourceList)
                        {
                            VerificationSourcesModel model = new VerificationSourcesModel()
                            {
                                VerificationSourceId = item.VerificationSourceId,
                                VerificationSourceName = item.VerificationSourceName
                            };
                            sourceModel.Add(model);
                        }

                    }
                  

                    ProjectIndicatorQuestionsModel questionModel = new ProjectIndicatorQuestionsModel()
                    {
                        ProjectIndicatorId=indicator.ProjectIndicatorId,
                        IndicatorQuestion = indicator.IndicatorQuestion,
                        IndicatorQuestionId = indicator.IndicatorQuestionId,
                        QuestionType = indicator.QuestionType,
                        VerificationSources = sourceModel,
                        QuestionTypeName= indicator.QuestionType == (int)QuestionType.Qualitative ? nameof(QuestionType.Qualitative) : indicator.QuestionType == (int)QuestionType.Quantitative ? nameof(QuestionType.Quantitative): null,

                    };



                    response.ResponseData = questionModel;
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
