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

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditProjectActivityExtensionCommandHandler : IRequestHandler<EditProjectActivityExtensionCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditProjectActivityExtensionCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditProjectActivityExtensionCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectActivityExtensions extensionDetail = await _dbContext.ProjectActivityExtensions.FirstOrDefaultAsync(x => x.ActivityId == request.ActivityId && x.IsDeleted == false);

                if (extensionDetail != null)
                {
                    extensionDetail.ActivityId = request.ActivityId;
                    extensionDetail.StartDate = request.StartDate;
                    extensionDetail.EndDate = request.EndDate;
                    extensionDetail.Description = request.Description;
                    extensionDetail.IsDeleted = false;
                    extensionDetail.ModifiedById = request.ModifiedById;
                    extensionDetail.ModifiedDate = request.ModifiedDate;

                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else
                {
                    throw new Exception(StaticResource.ActivityExtensionNotFound);
                }
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
