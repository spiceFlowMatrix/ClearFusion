using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Delete
{
    public class DeleteJournalDetailCommandHandler : IRequestHandler<DeleteJournalDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public DeleteJournalDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteJournalDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var journalInfo = await _dbContext.JournalDetail.FirstOrDefaultAsync(c => c.JournalCode == request.JournalCode);

                if (journalInfo == null)
                {
                    throw new Exception(StaticResource.JournalNotFound);
                }

                journalInfo.IsDeleted = true;
                journalInfo.ModifiedById = request.ModifiedById;
                journalInfo.ModifiedDate = request.ModifiedDate;

                await _dbContext.SaveChangesAsync();

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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