using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProjectIndicatorCommandHandler : IRequestHandler<AddProjectIndicatorCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddProjectIndicatorCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddProjectIndicatorCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectIndicators indicator = new ProjectIndicators();
                indicator.CreatedById = request.CreatedById;
                indicator.IsDeleted = false;
                indicator.CreatedDate = request.CreatedDate;

                await _dbContext.ProjectIndicators.AddAsync(indicator);
                await _dbContext.SaveChangesAsync();

                indicator.IndicatorCode = ProjectUtility.GenerateCode(indicator.ProjectIndicatorId);
                await _dbContext.SaveChangesAsync();

                int count = await _dbContext.ProjectIndicators.Where(x => x.IsDeleted == false).CountAsync();

                response.data.ProjectIndicator = new ProjectIndicatorViewModel();
                response.data.TotalCount = count;
                response.data.ProjectIndicator.ProjectIndicatorId = indicator.ProjectIndicatorId;
                response.data.ProjectIndicator.IndicatorCode = indicator.IndicatorCode;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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
