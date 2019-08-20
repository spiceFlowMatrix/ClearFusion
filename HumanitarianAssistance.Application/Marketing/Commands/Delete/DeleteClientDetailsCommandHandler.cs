using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeleteClientDetailsCommandHandler : IRequestHandler<DeleteClientDetailsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public DeleteClientDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(DeleteClientDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var ClientInfo = await _dbContext.ClientDetails.Where(c => c.ClientId == request.ClientId).SingleOrDefaultAsync();
                ClientInfo.IsDeleted = true;
                ClientInfo.ModifiedById = request.ModifiedById;
                ClientInfo.ModifiedDate = request.ModifiedDate;
                _dbContext.ClientDetails.Update(ClientInfo);
                await _dbContext.SaveChangesAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Client Deleted Successfully";
                response.data.jobListTotalCount = await _dbContext.JobDetails.CountAsync(x => x.IsDeleted == false);
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
