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

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteProjectActivityExtensionCommandHandler : IRequestHandler<DeleteProjectActivityExtensionCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeleteProjectActivityExtensionCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(DeleteProjectActivityExtensionCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectActivityExtensions extensionDetail = await _dbContext.ProjectActivityExtensions.FirstOrDefaultAsync(x => x.ExtensionId == request.extensionId && x.IsDeleted == false);

                if (extensionDetail != null)
                {
                    extensionDetail.IsDeleted = true;
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
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
