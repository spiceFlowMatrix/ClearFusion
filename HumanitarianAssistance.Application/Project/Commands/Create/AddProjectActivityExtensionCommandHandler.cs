using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProjectActivityExtensionCommandHandler : IRequestHandler<AddProjectActivityExtensionCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public AddProjectActivityExtensionCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(AddProjectActivityExtensionCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                ProjectActivityExtensions extensionObj = new ProjectActivityExtensions();
                extensionObj.ActivityId = request.ActivityId;
                extensionObj.StartDate = request.StartDate;
                extensionObj.EndDate = request.EndDate;
                extensionObj.Description = request.Description;
                extensionObj.CreatedById = request.CreatedById;
                extensionObj.IsDeleted = false;
                extensionObj.CreatedDate = request.CreatedDate;

                await _dbContext.ProjectActivityExtensions.AddAsync(extensionObj);
                await _dbContext.SaveChangesAsync();

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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
