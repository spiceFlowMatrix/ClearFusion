using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteProjectAreaCommandHandler: IRequestHandler<DeleteProjectAreaCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        public DeleteProjectAreaCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteProjectAreaCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var ProjectSectorInfo = await _dbContext.ProjectArea.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectAreaId == request.ProjectAreaId && x.ProjectId == request.ProjectId);
                
                if (ProjectSectorInfo != null)
                {
                    ProjectSectorInfo.IsDeleted = true;
                    ProjectSectorInfo.ModifiedById = request.ModifiedById;
                    ProjectSectorInfo.ModifiedDate = request.ModifiedDate;
                    _dbContext.SaveChanges();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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