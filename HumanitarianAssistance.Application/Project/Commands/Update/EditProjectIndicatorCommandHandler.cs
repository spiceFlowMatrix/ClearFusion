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
                    ProjectIndicators indicator = await _dbContext.ProjectIndicators.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectIndicatorId == request.ProjectIndicatorId);

                    if (indicator != null)
                    {
                        indicator.ModifiedById = request.ModifiedById;
                        indicator.ModifiedDate = request.ModifiedDate;
                        indicator.IndicatorName = request.IndicatorName;
                        indicator.Description = request.Description;
                         _dbContext.ProjectIndicators.Update(indicator);
                        await _dbContext.SaveChangesAsync();

                    }
                    ProjectIndicatorViewModel pIndicatorModel = new ProjectIndicatorViewModel();

                    pIndicatorModel.IndicatorName = indicator.IndicatorName;
                    pIndicatorModel.IndicatorCode = indicator.IndicatorCode;
                    pIndicatorModel.Description = indicator.Description;
                    pIndicatorModel.ProjectIndicatorId = indicator.ProjectIndicatorId;

                    response.ResponseData = pIndicatorModel;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.IndicatorNameEmpty;
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
