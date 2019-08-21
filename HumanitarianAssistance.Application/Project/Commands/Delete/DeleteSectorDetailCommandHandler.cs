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
    public class DeleteSectorDetailCommandHandler: IRequestHandler<DeleteSectorDetailCommand, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public DeleteSectorDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteSectorDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var SectorInfo = await _dbContext.SectorDetails.FirstOrDefaultAsync(c => c.SectorId == request.SectorId);
                SectorInfo.IsDeleted = true;
                SectorInfo.ModifiedById = request.ModifiedById;
                SectorInfo.ModifiedDate = DateTime.UtcNow;
                _dbContext.SectorDetails.Update(SectorInfo);
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