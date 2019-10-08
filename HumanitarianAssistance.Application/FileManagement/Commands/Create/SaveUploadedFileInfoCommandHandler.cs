using System;
using System.Linq;
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
                    DocumentFileDetail fileDetail = await _dbContext.DocumentFileDetail
                                                                    .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                     x.Name == request.FileName && x.PageId == request.PageId &&
                                                                     x.DocumentTypeId == request.DocumentTypeId);

                    //Delete file If file with same name already exists for same page and same document type.
                    if (fileDetail != null)
                    {
                        fileDetail.IsDeleted = true;
                        _dbContext.DocumentFileDetail.Update(fileDetail);
                        await _dbContext.SaveChangesAsync();
                    }
                                                               

                    fileDetail = new DocumentFileDetail
                    {
                        IsDeleted = false,
                        CreatedDate = DateTime.UtcNow,
                        CreatedById = request.CreatedById,
                        Name = request.FileName,
                        RawFileSizeBytes = request.FileSize,
                        RawFileMimeType = request.FileType,
                        Description = request.FileName,
                        PageId = request.PageId,
                        StorageDirectoryPath = request.FilePath,
                        DocumentTypeId= request.DocumentTypeId
                    };

                    await _dbContext.DocumentFileDetail.AddAsync(fileDetail);
                    await _dbContext.SaveChangesAsync();

                     EntitySourceDocumentDetail docDetail = new EntitySourceDocumentDetail
                     {
                        CreatedDate = DateTime.UtcNow,
                        CreatedById = request.CreatedById,
                        DocumentFileId = fileDetail.DocumentFileId,
                        EntityId = request.RecordId,
                        IsDeleted = false
                     };

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
