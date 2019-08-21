using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.FileManagement.Commands.Create
{
    public class SaveUploadedFileInfoCommandHandler : IRequestHandler<SaveUploadedFileInfoCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public SaveUploadedFileInfoCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(SaveUploadedFileInfoCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    DocumentFileDetail fileDetail = new DocumentFileDetail();
                    fileDetail.IsDeleted = false;
                    fileDetail.CreatedDate = request.CreatedDate;
                    fileDetail.CreatedById = request.CreatedById;
                    fileDetail.Name = request.FileName;
                    fileDetail.RawFileSizeBytes = request.FileSize;
                    fileDetail.RawFileMimeType = request.FileType;
                    fileDetail.Description = request.FileName;
                    fileDetail.PageId = request.PageId;
                    fileDetail.StorageDirectoryPath = request.FilePath;

                    await _dbContext.DocumentFileDetail.AddAsync(fileDetail);
                    await _dbContext.SaveChangesAsync();
                    EntitySourceDocumentDetail docDetail = new EntitySourceDocumentDetail();
                    docDetail.CreatedDate = request.CreatedDate;
                    docDetail.CreatedById = request.CreatedById;
                    docDetail.DocumentFileId = fileDetail.DocumentFileId;
                    docDetail.EntityId = request.RecordId;
                    docDetail.IsDeleted = false;
                    await _dbContext.EntitySourceDocumentDetails.AddAsync(docDetail);
                    await _dbContext.SaveChangesAsync();
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
