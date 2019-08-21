using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
        public class DeleteActivityDocumentCommandHandler : IRequestHandler<DeleteActivityDocumentCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public DeleteActivityDocumentCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(DeleteActivityDocumentCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                string bucketName = Environment.GetEnvironmentVariable("GOOGLE_BUCKET_NAME");
                if (bucketName != null)
                {
                    // Get document
                    var documentDetail = await _dbContext.ActivityDocumentsDetail.FirstOrDefaultAsync(x => x.ActtivityDocumentId == request.activityDocumentId);

                    if (documentDetail != null)
                    {
                        if (await GCBucket.DeleteObject(bucketName, documentDetail.ActivityDocumentsFilePath))
                        {
                            documentDetail.IsDeleted = true;
                            documentDetail.ModifiedById = request.ModifiedById;
                            documentDetail.ModifiedDate = request.ModifiedDate;
                            await _dbContext.SaveChangesAsync();                            
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = StaticResource.SuccessText;
                        }
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.BucketNameNotFound;
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
