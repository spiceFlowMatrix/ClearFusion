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

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProjectSectorCommandHandler : IRequestHandler<AddEditProjectSectorCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public AddEditProjectSectorCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddEditProjectSectorCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var existRecord = await _dbContext.ProjectSector.Where(x => x.IsDeleted == false && x.ProjectId == request.ProjectId).ToListAsync();
                if (request.SectorIds != null)
                {
                    if (existRecord.Any())
                    {
                        // edit exiting
                        existRecord.ForEach(x => x.IsDeleted = true);
                        _dbContext.ProjectSector.UpdateRange(existRecord);
                        await _dbContext.SaveChangesAsync();
                        foreach (var item in request.SectorIds)
                        {
                            ProjectSector sec = new ProjectSector();
                            sec.ProjectId = request.ProjectId;
                            sec.SectorId = item;
                            sec.IsDeleted = false;
                            sec.CreatedById = request.CreatedById;
                            sec.CreatedDate = DateTime.UtcNow;

                            await _dbContext.ProjectSector.AddAsync(sec);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    else
                    {
                       foreach (var item in request.SectorIds)
                        {
                            ProjectSector obj = new ProjectSector();
                            obj.ProjectId = request.ProjectId;
                            obj.SectorId = item;
                            obj.IsDeleted = false;
                            obj.CreatedById = request.CreatedById;
                            obj.CreatedDate = DateTime.UtcNow;
                            await _dbContext.ProjectSector.AddAsync(obj);
                            await _dbContext.SaveChangesAsync();
                        }
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