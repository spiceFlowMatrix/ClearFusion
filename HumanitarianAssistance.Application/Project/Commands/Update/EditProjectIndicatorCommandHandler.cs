using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
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
        public class EditProjectIndicatorCommandHandler : IRequestHandler<EditProjectIndicatorCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public EditProjectIndicatorCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(EditProjectIndicatorCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    ProjectIndicators indicator = await _dbContext.ProjectIndicators.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectIndicatorId == request.IndicatorId);

                    if (indicator != null)
                    {
                        if (!string.IsNullOrEmpty(request.IndicatorName))
                        {
                            indicator.ModifiedById = request.ModifiedById;
                            indicator.ModifiedDate = request.ModifiedDate;
                            indicator.IndicatorName = request.IndicatorName;

                            await _dbContext.SaveChangesAsync();

                            if (request.IndicatorQuestions.Any())
                            {
                                List<ProjectIndicatorQuestions> projectIndicatorQuestions = await _dbContext
                                                                                                      .ProjectIndicatorQuestions
                                                                                                      .Where(x => x.IsDeleted == false && x.ProjectIndicatorId == request.IndicatorId)
                                                                                                      .ToListAsync();

                                projectIndicatorQuestions.ForEach(x => x.IsDeleted = true);

                                _dbContext.ProjectIndicatorQuestions.UpdateRange(projectIndicatorQuestions);
                                await _dbContext.SaveChangesAsync();

                                projectIndicatorQuestions = new List<ProjectIndicatorQuestions>();

                                foreach (var item in request.IndicatorQuestions)
                                {
                                    ProjectIndicatorQuestions question = new ProjectIndicatorQuestions();
                                    question.IsDeleted = false;
                                    question.CreatedById = request.CreatedById;
                                    question.CreatedDate = request.CreatedDate;
                                    question.ProjectIndicatorId = request.IndicatorId;
                                    question.IndicatorQuestion = item.QuestionText;
                                    projectIndicatorQuestions.Add(question);
                                }

                                await _dbContext.ProjectIndicatorQuestions.AddRangeAsync(projectIndicatorQuestions);
                                await _dbContext.SaveChangesAsync();
                            }


                            ProjectIndicatorViewModel pIndicatorModel = new ProjectIndicatorViewModel();

                            pIndicatorModel.IndicatorName = indicator.IndicatorName;
                            pIndicatorModel.IndicatorCode = indicator.IndicatorCode;
                            pIndicatorModel.ProjectIndicatorId = indicator.ProjectIndicatorId;

                            response.data.ProjectIndicator = pIndicatorModel;
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = StaticResource.SuccessText;
                        }
                        else
                        {
                            response.StatusCode = StaticResource.failStatusCode;
                            response.Message = StaticResource.IndicatorNameEmpty;
                        }
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.ProjectIndicatorNotFound;
                    }
                }
                else
                {
                    throw new Exception("request is null");
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
