using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries {

    public class DownloadCandidateCvByRequestIdQueryHandler : IRequestHandler<DownloadCandidateCvByRequestIdQuery, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IFileManagementService _fileManagement;

        public DownloadCandidateCvByRequestIdQueryHandler (HumanitarianAssistanceDbContext dbContext, IFileManagementService fileManagement) {
            _dbContext = dbContext;
            _fileManagement = fileManagement;
        }

        public async Task<ApiResponse> Handle (DownloadCandidateCvByRequestIdQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            CandidateCvModel attachment = new CandidateCvModel ();

            try {
                var document = await (from es in _dbContext.EntitySourceDocumentDetails
                 join df in _dbContext.DocumentFileDetail 
                 on es.DocumentFileId equals df.DocumentFileId into docs from doc in docs.DefaultIfEmpty () 
                 join u in _dbContext.UserDetails 
                 on doc.CreatedById equals u.AspNetUserId into finaldocs from final in finaldocs.DefaultIfEmpty ()
                 where doc.IsDeleted == false && es.EntityId == request.requestId &&
                    doc.PageId == (int) FileSourceEntityTypes.HiringRequestCandidateCV 
                    select new StoreDocumentModel {
                        DocumentFileId = doc.DocumentFileId,
                            UploadedBy = $"{final.FirstName} {final.LastName}",                        
                    }).FirstOrDefaultAsync ();

                if (document == null) {
                    attachment = null;
                } else {
                    FileModel fileModel = new FileModel () {
                        PageId = (int) FileSourceEntityTypes.HiringRequestCandidateCV,
                        RecordId = request.requestId,
                        DocumentFileId = document.DocumentFileId
                    };

                    //get Saved Document ID and Signed URL For Purchase Image
                    var documentModel = await _fileManagement.GetFilesByDocumentFileId (fileModel);

                    if (documentModel != null) {
                        attachment = new CandidateCvModel {
                        AttachmentName = documentModel.DocumentName,
                        AttachmentUrl = documentModel.SignedURL,
                        UploadedBy = document.UploadedBy
                        };
                    }
                }

                response.data.CandidateCvDownload = attachment;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}