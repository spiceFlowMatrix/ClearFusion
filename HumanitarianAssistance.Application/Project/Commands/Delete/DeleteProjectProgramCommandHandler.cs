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
    public class DeleteProjectProgramCommandHandler: IRequestHandler<DeleteProjectProgramCommand, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;
        public DeleteProjectProgramCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteProjectProgramCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var ProjectProgramInfo = await _dbContext.ProjectProgram.FirstOrDefaultAsync(c => c.ProjectProgramId == request.ProjectProgramId && c.ProjectId == request.ProjectId);
                ProjectProgramInfo.IsDeleted = true;
                ProjectProgramInfo.ModifiedById = request.ModifiedById;
                ProjectProgramInfo.ModifiedDate = request.ModifiedDate;
                _dbContext.ProjectProgram.Update(ProjectProgramInfo);
                await _dbContext.SaveChangesAsync();

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