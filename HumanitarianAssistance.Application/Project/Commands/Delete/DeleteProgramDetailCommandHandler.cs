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
    public class DeleteProgramDetailCommandHandler: IRequestHandler<DeleteProgramDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public DeleteProgramDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteProgramDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var ProgramInfo = await _dbContext.ProgramDetail.FirstOrDefaultAsync(c => c.ProgramId == request.ProgramId);
                ProgramInfo.IsDeleted = true;
                ProgramInfo.ModifiedById = request.ModifiedById;
                ProgramInfo.ModifiedDate = request.ModifiedDate;
                _dbContext.ProgramDetail.Update(ProgramInfo);

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