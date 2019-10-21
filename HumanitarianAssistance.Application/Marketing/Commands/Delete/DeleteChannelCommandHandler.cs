using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeleteChannelCommandHandler : IRequestHandler<DeleteChannelCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public DeleteChannelCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteChannelCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var channelDetails = await _dbContext.Channel.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ChannelId == request.model);
                if (channelDetails != null)
                {
                    channelDetails.ModifiedById = request.ModifiedById;
                    channelDetails.ModifiedDate = DateTime.UtcNow;
                    channelDetails.IsDeleted = true;
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Channel deleted successfully";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
