using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class RemoveEmployeeFromAssignedProjectCommandHandler: IRequestHandler<RemoveEmployeeFromAssignedProjectCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;

        public RemoveEmployeeFromAssignedProjectCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {

            _dbContext= dbContext;
            _mapper= mapper;
        }

        public async Task<ApiResponse> Handle(RemoveEmployeeFromAssignedProjectCommand model, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var ProjectAssignToInfo = await _dbContext.ProjectAssignTo.FirstOrDefaultAsync(c => c.ProjectAssignToId == model.ProjectAssignToId && c.ProjectId == model.ProjectId);
                ProjectAssignToInfo.IsDeleted = true;
                ProjectAssignToInfo.ModifiedById = model.ModifiedById;
                ProjectAssignToInfo.ModifiedDate = model.ModifiedDate;
                _dbContext.ProjectAssignTo.Update(ProjectAssignToInfo);
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