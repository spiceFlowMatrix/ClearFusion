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

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
        public class EndProjectSubActivityCommandHandler : IRequestHandler<EndProjectSubActivityCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public EndProjectSubActivityCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(EndProjectSubActivityCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                ProjectActivityDetail obj = await _dbContext.ProjectActivityDetail.FirstOrDefaultAsync(x => x.ActivityId == request.activityId && x.IsDeleted == false);

                if (obj != null)
                {

                    obj.ActualEndDate = DateTime.UtcNow;
                    obj.ModifiedDate = request.ModifiedDate;
                    obj.IsDeleted = false;
                    obj.ModifiedById = request.ModifiedById;
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
