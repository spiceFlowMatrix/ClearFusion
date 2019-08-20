using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteProjectDetailCommandHandler: IRequestHandler<DeleteProjectDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public DeleteProjectDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteProjectDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            DbContext db = _dbContext;
            using (IDbContextTransaction tran = db.Database.BeginTransaction())
            {
                try
                {

                    ProjectPhaseTime _ProjectPhase = new ProjectPhaseTime();
                    var ProjectInfo = await _dbContext.ProjectDetail.FirstOrDefaultAsync(c => c.ProjectId == request.ProjectId);
                    ProjectInfo.IsDeleted = true;
                    ProjectInfo.ModifiedById = request.ModifiedById;
                    ProjectInfo.ModifiedDate = DateTime.UtcNow;
                    _dbContext.ProjectDetail.Update(ProjectInfo);
                    await _dbContext.SaveChangesAsync();
                    _ProjectPhase.IsDeleted = true;
                    _ProjectPhase.ModifiedById = request.ModifiedById;
                    _ProjectPhase.ModifiedDate = DateTime.UtcNow;
                    _dbContext.ProjectPhaseTime.Update(_ProjectPhase);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                    tran.Commit();
                }

                catch (Exception ex)
                {
                    tran.Rollback();
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong + ex.Message;
                }
            }
            return response;
        }
        
    }
}