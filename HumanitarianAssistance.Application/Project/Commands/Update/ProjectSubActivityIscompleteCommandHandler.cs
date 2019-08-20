using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
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
        public class ProjectSubActivityIscompleteCommandHandler : IRequestHandler<ProjectSubActivityIscompleteCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public ProjectSubActivityIscompleteCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(ProjectSubActivityIscompleteCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                ProjectActivityDetail obj = await _dbContext.ProjectActivityDetail.FirstOrDefaultAsync(x => x.ActivityId == request.activityId && x.IsDeleted == false);

                if (obj != null)
                {

                    obj.IsCompleted = !obj.IsCompleted;
                    obj.ModifiedDate = request.ModifiedDate;
                    obj.IsDeleted = false;
                    obj.ModifiedById = request.ModifiedById;
                    await _dbContext.SaveChangesAsync();
                }
                var parent = await _dbContext.ProjectActivityDetail.AnyAsync(x => x.IsDeleted == false &&
                                                                                      x.ParentId == obj.ParentId &&
                                                                                      x.IsCompleted == false);
                ProjectActivityDetail detail = new ProjectActivityDetail();
                if (!parent)
                {
                    //foreach (var item in parent)
                    //{
                    detail = await _dbContext.ProjectActivityDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ActivityId == obj.ParentId);
                    if (detail != null)
                    {
                        detail.StatusId = (int)ProjectPhaseType.Completed;
                        await _dbContext.SaveChangesAsync();
                    }
                    //}

                }
                response.data.ProjectActivityDetail = detail;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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
