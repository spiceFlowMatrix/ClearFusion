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
    public class DeleteProducerCommandHandler : IRequestHandler<DeleteProducerCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public DeleteProducerCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteProducerCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var producerDetails = await _dbContext.Producers.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProducerId == request.Id);
                if (producerDetails != null)
                {
                    producerDetails.ModifiedById = request.ModifiedById;
                    producerDetails.ModifiedDate = DateTime.UtcNow;
                    producerDetails.IsDeleted = true;
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Producer deleted successfully";
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
