using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProjectAreaCommandHandler: IRequestHandler<AddEditProjectAreaCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public AddEditProjectAreaCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddEditProjectAreaCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var existRecord = await _dbContext.ProjectArea.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectId == request.ProjectId);
                
                if (existRecord == null)
                {
                    ProjectArea obj = new ProjectArea();
                    obj.ProjectId = request.ProjectId;
                    obj.AreaId = request.AreaId;
                    obj.IsDeleted = false;
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = DateTime.UtcNow;
                    await _dbContext.ProjectArea.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    existRecord.ProjectId = request.ProjectId;
                    existRecord.AreaId = request.AreaId;
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = request.ModifiedById;
                    existRecord.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();

                }
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