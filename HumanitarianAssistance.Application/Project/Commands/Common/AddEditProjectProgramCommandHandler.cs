using System;
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
    public class AddEditProjectProgramCommandHandler : IRequestHandler<AddEditProjectProgramCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public AddEditProjectProgramCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {

            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddEditProjectProgramCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var existRecord = await _dbContext.ProjectProgram.Where(x => x.IsDeleted == false && x.ProjectId == request.ProjectId).ToListAsync();
                if (request.ProgramIds != null)
                {
                    if (existRecord.Any())
                    {
                        // edit exiting
                        existRecord.ForEach(x => x.IsDeleted = true);
                        _dbContext.ProjectProgram.UpdateRange(existRecord);
                        await _dbContext.SaveChangesAsync();
                        foreach (var item in request.ProgramIds)
                        {
                            ProjectProgram pro = new ProjectProgram();
                            pro.ProjectId = request.ProjectId;
                            pro.ProgramId = item;
                            pro.IsDeleted = false;
                            pro.CreatedById = request.CreatedById;
                            pro.CreatedDate = DateTime.UtcNow;

                            await _dbContext.ProjectProgram.AddAsync(pro);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        foreach (var item in request.ProgramIds)
                        {
                            ProjectProgram obj = new ProjectProgram();
                            obj.ProjectId = request.ProjectId;
                            obj.ProgramId = item;
                            obj.IsDeleted = false;
                            obj.CreatedById = request.CreatedById;
                            obj.CreatedDate = DateTime.UtcNow;
                            await _dbContext.ProjectProgram.AddAsync(obj);
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