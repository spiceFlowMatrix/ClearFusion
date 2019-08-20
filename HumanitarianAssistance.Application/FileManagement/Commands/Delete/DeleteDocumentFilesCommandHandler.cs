using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.FileManagement.Commands.Delete
{
    public class DeleteDocumentFilesCommandHandler : IRequestHandler<DeleteDocumentFilesCommand, ApiResponse>
        {
            private readonly HumanitarianAssistanceDbContext _dbContext;

            public DeleteDocumentFilesCommandHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<ApiResponse> Handle(DeleteDocumentFilesCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                if (request.DocumentFileId != null)
                {
                    var result = await _dbContext
                               .EntitySourceDocumentDetails
                               .Include(x => x.DocumentFileDetail)
                               .FirstOrDefaultAsync(x => x.IsDeleted == false && x.DocumentFileId == request.DocumentFileId);

                    result.IsDeleted = true;
                    result.DocumentFileDetail.IsDeleted = true;
                    result.ModifiedById = request.ModifiedById;
                    result.ModifiedDate = request.ModifiedDate; 
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex;
            }
            return response;
        }
    }
}
