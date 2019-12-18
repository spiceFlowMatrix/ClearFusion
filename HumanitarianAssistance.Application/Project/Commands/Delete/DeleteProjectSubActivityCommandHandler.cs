using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteProjectSubActivityCommandHandler : IRequestHandler<DeleteProjectSubActivityCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public DeleteProjectSubActivityCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteProjectSubActivityCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectActivityDetail activityDetail = await _dbContext.ProjectActivityDetail.FirstOrDefaultAsync(c => c.ActivityId == request.ActivityId);

                List<ProjectActivityExtensions> extensionList = await _dbContext.ProjectActivityExtensions.Where(x => x.IsDeleted == false && x.ActivityId == request.ActivityId)
                .ToListAsync();
                if (activityDetail != null)
                {
                    activityDetail.IsDeleted = true;
                    activityDetail.ModifiedById = request.ModifiedById;
                    activityDetail.ModifiedDate = request.ModifiedDate;

                    await _dbContext.SaveChangesAsync();
                    if (extensionList.Any())
                    {
                         extensionList.ForEach(x=> x.IsDeleted = true);
                        _dbContext.UpdateRange(extensionList);
                        await _dbContext.SaveChangesAsync();
                    }

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.ActivityNotFound;
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