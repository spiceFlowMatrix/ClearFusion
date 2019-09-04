using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteProjectIndicatorDetailCommandHandler : IRequestHandler<DeleteProjectIndicatorDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeleteProjectIndicatorDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(DeleteProjectIndicatorDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectIndicators projectIndicatorDetail = await _dbContext.ProjectIndicators
                                                                            .Include(x => x.ProjectIndicatorQuestions)
                                                                            .ThenInclude(x => x.VerificationSources)
                                                                            .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                       x.ProjectIndicatorId == request.IndicatorId);


                if (projectIndicatorDetail != null)
                {
                    projectIndicatorDetail.ModifiedDate = request.ModifiedDate;
                    projectIndicatorDetail.ModifiedById = request.ModifiedById;
                    projectIndicatorDetail.IsDeleted = true;
                    await _dbContext.SaveChangesAsync();
                }
                // to delete question detail and verification sources

                if (projectIndicatorDetail.ProjectIndicatorQuestions.Any())
                {
                    projectIndicatorDetail.ProjectIndicatorQuestions.ForEach(x =>
                    {
                        x.IsDeleted = true;
                        x.ModifiedDate = request.ModifiedDate;
                        x.ModifiedById = request.ModifiedById;
                        x.VerificationSources.ForEach(y =>
                                {
                                    y.IsDeleted = true;
                                    y.ModifiedDate = request.ModifiedDate;
                                    y.ModifiedById = request.ModifiedById;
                                });
                    });

                    await _dbContext.SaveChangesAsync();

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