using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.FileManagement.Commands.Update
{
    public class UpdateUploadedFileInfoCommandHandler: IRequestHandler<UpdateUploadedFileInfoCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IFileManagementService _fileManagement;

        public UpdateUploadedFileInfoCommandHandler(HumanitarianAssistanceDbContext dbContext, IFileManagementService fileManagement)
        {
            _dbContext = dbContext;
            _fileManagement = fileManagement;
        }

        public async Task<ApiResponse> Handle(UpdateUploadedFileInfoCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (request != null)
                {
                    DocumentFileDetail docDetail = await _dbContext.DocumentFileDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.DocumentFileId == request.DocumentFileId);

                    docDetail.ModifiedById = request.ModifiedById;
                    docDetail.ModifiedDate = DateTime.UtcNow;
                    docDetail.Name = request.FileName;
                    docDetail.RawFileMimeType = request.FileType;
                    docDetail.RawFileSizeBytes = request.FileSize;
                    docDetail.StorageDirectoryPath = request.FilePath;

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