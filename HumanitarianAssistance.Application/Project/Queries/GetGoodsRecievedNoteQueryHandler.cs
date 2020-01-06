using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetGoodsRecievedNoteQueryHandler: IRequestHandler<GetGoodsRecievedNoteQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IFileManagementService _fileManagement;

        public GetGoodsRecievedNoteQueryHandler(HumanitarianAssistanceDbContext dbContext, IFileManagementService fileManagement)
        {
            _dbContext = dbContext;
            _fileManagement = fileManagement;
        }
        
        public async Task<ApiResponse> Handle(GetGoodsRecievedNoteQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            GoodsRecievedNoteModel attachment = new GoodsRecievedNoteModel();
            
            try
            {
                var document = await (from es in _dbContext.EntitySourceDocumentDetails 
                                    join df in _dbContext.DocumentFileDetail on es.DocumentFileId equals df.DocumentFileId
                                    into docs
                                    from doc in docs.DefaultIfEmpty()
                                    join u in _dbContext.UserDetails on doc.CreatedById equals u.AspNetUserId
                                    into finaldocs
                                    from final in finaldocs.DefaultIfEmpty()
                                    where doc.IsDeleted == false && es.EntityId == request.requestId &&
                                    doc.PageId == (int)FileSourceEntityTypes.GoodsRecievedDocument
                                    select new StoreDocumentModel 
                                    {
                                        DocumentFileId= doc.DocumentFileId,
                                        // DocumentName= doc.Name,
                                        // DocumentTypeId= doc.DocumentTypeId,
                                        UploadedBy= $"{final.FirstName} {final.LastName}",
                                        // UploadedOn= doc.CreatedDate,
                                        // DocumentTypeName= doc.DocumentTypeId== (int)DocumentFileTypes.PurchaseImage ? "Image" : "Invoice"
                                    }).FirstOrDefaultAsync();

                if(document == null) {
                    attachment = null;
                } else {
                    FileModel fileModel = new FileModel()
                    {
                        PageId = (int)FileSourceEntityTypes.GoodsRecievedDocument,
                        RecordId = request.requestId,
                        DocumentFileId= document.DocumentFileId
                    };

                    //get Saved Document ID and Signed URL For Purchase Image
                    var documentModel = await _fileManagement.GetFilesByDocumentFileId(fileModel);

                    if (documentModel != null)
                    {
                        attachment = new GoodsRecievedNoteModel{
                            AttachmentName = documentModel.DocumentName,
                            AttachmentUrl = documentModel.SignedURL,
                            UploadedBy = document.UploadedBy
                        };
                    }
                }
                
                response.data.GoodsRecievedNote = attachment;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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