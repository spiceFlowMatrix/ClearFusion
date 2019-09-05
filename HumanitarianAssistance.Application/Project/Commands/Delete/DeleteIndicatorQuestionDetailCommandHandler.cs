using AutoMapper;
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
namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteIndicatorQuestionDetailCommandHandler : IRequestHandler<DeleteIndicatorQuestionDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeleteIndicatorQuestionDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(DeleteIndicatorQuestionDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var questionDetail = await _dbContext.ProjectIndicatorQuestions
                                                    .Include(x => x.VerificationSources)
                                                    .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                              x.IndicatorQuestionId == request.IndicatorQuestionId);

                if (questionDetail != null)
                {
                    questionDetail.ModifiedDate = request.ModifiedDate;
                    questionDetail.ModifiedById = request.ModifiedById;
                    questionDetail.IsDeleted = true;

                    await _dbContext.SaveChangesAsync();

                    if (questionDetail.VerificationSources.Any())
                    {
                        questionDetail.VerificationSources.ForEach(x =>
                        {
                            x.IsDeleted = true;
                            x.ModifiedById = request.ModifiedById;
                            x.ModifiedDate = request.ModifiedDate;
                        });
                        await _dbContext.SaveChangesAsync();

                    }

                }
                else
                {
                    throw new Exception(StaticResource.IndicatorQuestionNotfound);

                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}