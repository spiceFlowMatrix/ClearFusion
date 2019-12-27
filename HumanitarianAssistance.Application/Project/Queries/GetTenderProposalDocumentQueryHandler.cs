using System;
using HumanitarianAssistance.Application.Store.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Queries {
    public class GetTenderProposalDocumentQueryHandler : IRequestHandler<GetTenderProposalDocumentQuery, ApiResponse> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IFileManagementService _fileManagement;
        public GetTenderProposalDocumentQueryHandler (HumanitarianAssistanceDbContext dbContext, IFileManagementService fileManagement) 
        {
            _dbContext = dbContext;
            _fileManagement = fileManagement;
        }

        public async Task<ApiResponse> Handle (GetTenderProposalDocumentQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            List<TenderProposalDocumentModel> attachments = new List<TenderProposalDocumentModel>();
            List<int> documentTypes = new List<int>() {  
                (int)FileSourceEntityTypes.TenderProposalDocument,
                (int)FileSourceEntityTypes.TenderRFPDocument,
                (int)FileSourceEntityTypes.TenderAnnouncementDocument,
                };
            try 
            {
                var documentsAsync = await (from es in _dbContext.EntitySourceDocumentDetails 
                                    join df in _dbContext.DocumentFileDetail on es.DocumentFileId equals df.DocumentFileId
                                    into docs
                                    from doc in docs.DefaultIfEmpty()
                                    join u in _dbContext.UserDetails on doc.CreatedById equals u.AspNetUserId
                                    into finaldocs
                                    from final in finaldocs.DefaultIfEmpty()
                                    where doc.IsDeleted == false && es.EntityId == request.RequestId &&
                                    documentTypes.Contains(doc.PageId)
                                    select new StoreDocumentModel 
                                    {
                                        DocumentFileId= doc.DocumentFileId,
                                        DocumentTypeId= doc.DocumentTypeId,
                                    }).ToListAsync();
                
                foreach(var doc in documentsAsync)
                {
                    FileModel fileModel =new FileModel()
                    {
                        PageId = Convert.ToInt32(doc.DocumentTypeId),
                        RecordId = request.RequestId,
                        DocumentFileId= doc.DocumentFileId
                    };

                    //get Saved Document ID and Signed URL For Purchase Image
                    var documentModel = await _fileManagement.GetFilesByDocumentFileId(fileModel);

                    if (documentModel != null)
                    {
                        attachments.Add(new TenderProposalDocumentModel
                        {
                            AttachmentName = documentModel.DocumentName,
                            AttachmentUrl = documentModel.SignedURL,
                            DocumentType = Convert.ToInt32(doc.DocumentTypeId),
                            DocumentFileId = doc.DocumentFileId
                        });
                    }
                }
                response.data.TenderProposalDoc = attachments;
                response.StatusCode = 200;
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