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
    public class AddEditProjectSectorCommandHandler: IRequestHandler<AddEditProjectSectorCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public AddEditProjectSectorCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(AddEditProjectSectorCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var existRecord = await _dbContext.ProjectSector.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectId == request.ProjectId);
                if (existRecord == null)
                {
                    ProjectSector obj = new ProjectSector();
                    obj.ProjectId = request.ProjectId;
                    obj.SectorId = request.SectorId;
                    obj.IsDeleted = false;
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = DateTime.UtcNow;
                    await _dbContext.ProjectSector.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    if (existRecord != null)
                    {
                        // _mapper.Map(model, existRecord);
                        existRecord.ProjectId = request.ProjectId;
                        existRecord.SectorId = request.SectorId;
                        existRecord.IsDeleted = false;
                        existRecord.ModifiedById = request.ModifiedById;
                        existRecord.ModifiedDate = DateTime.UtcNow;
                        _dbContext.ProjectSector.Update(existRecord);
                        await _dbContext.SaveChangesAsync();
                    }
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