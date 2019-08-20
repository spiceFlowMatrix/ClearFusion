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
    public class AddEditProjectProgramCommandHandler: IRequestHandler<AddEditProjectProgramCommand, ApiResponse>
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
                if (request != null)
                {
                    var existRecord = await _dbContext.ProjectProgram.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectId == request.ProjectId);
                    
                    if (existRecord == null)
                    {
                        ProjectProgram obj = new ProjectProgram();
                        obj.ProjectId = request.ProjectId;
                        obj.ProgramId = request.ProgramId;
                        obj.IsDeleted = false;
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = DateTime.UtcNow;
                        await _dbContext.ProjectProgram.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        if (existRecord != null)
                        {
                            existRecord.ProjectId = request.ProjectId;
                            existRecord.ProgramId = request.ProgramId;
                            existRecord.IsDeleted = false;
                            existRecord.ModifiedById = request.ModifiedById;
                            existRecord.ModifiedDate = DateTime.UtcNow;
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    response.StatusCode = 200;
                    response.Message = "Success";

                }
                else
                {
                    throw new Exception("Project Program Not Selected");
                }
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