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
    public class StartProjectSubActivityCommandHandler : IRequestHandler<StartProjectSubActivityCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public StartProjectSubActivityCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(StartProjectSubActivityCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectActivityDetail obj = await _dbContext.ProjectActivityDetail.FirstOrDefaultAsync(x => x.ActivityId == request.activityId && x.IsDeleted == false);

                ProjectActivityDetail parent = await _dbContext.ProjectActivityDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ActivityId == obj.ParentId);

                if (obj != null)
                {
                    obj.StatusId = (int)ProjectPhaseType.Implementation;
                    obj.ActualStartDate = DateTime.UtcNow;
                    obj.ModifiedDate = request.ModifiedDate;
                    obj.IsDeleted = false;
                    obj.ModifiedById = request.ModifiedById;
                    await _dbContext.SaveChangesAsync();
                }

                if (parent != null)
                {
                    parent.StatusId = (int)ProjectPhaseType.Implementation;
                    await _dbContext.SaveChangesAsync();
                }

                response.data.ProjectActivityDetail = obj;
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
