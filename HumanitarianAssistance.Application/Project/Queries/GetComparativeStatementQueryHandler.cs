using System;
using HumanitarianAssistance.Application.Store.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Common.Enums;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetComparativeStatementQueryHandler: IRequestHandler<GetComparativeStatementQuery, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IFileManagementService _fileManagement;

        public GetComparativeStatementQueryHandler(HumanitarianAssistanceDbContext dbContext, IFileManagementService fileManagement)
        {
            _dbContext= dbContext;
            _fileManagement = fileManagement;
        }

        public async Task<ApiResponse> Handle(GetComparativeStatementQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            ComparativeStatementModel model = new ComparativeStatementModel();
            List<StatementAttachmentModel> attachments = new List<StatementAttachmentModel>();
            try
            {
                var compStatus = await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=>x.IsDeleted == false && x.LogisticRequestsId == request.requestId);
                if(compStatus == null) {
                    throw new Exception("Request doesn't exists!");
                }
                if(compStatus.ComparativeStatus == (int)LogisticComparativeStatus.RejectStatement) {
                    var user = await _dbContext.UserDetails.FirstOrDefaultAsync(x=> x.AspNetUserId == compStatus.ModifiedById);
                    model.RejectedBy = user.FirstName + ' ' +  user.LastName;
                }
                else 
                {
                    var statement = await _dbContext.ComparativeStatementSubmission.FirstOrDefaultAsync(x=>x.IsDeleted==false && x.LogisticRequestsId == request.requestId);
                    if( statement == null) {
                        throw new Exception("Statement not submitted yet!");
                    }
                    var documentsAsync = (from es in _dbContext.EntitySourceDocumentDetails 
                                    join df in _dbContext.DocumentFileDetail on es.DocumentFileId equals df.DocumentFileId
                                    into docs
                                    from doc in docs.DefaultIfEmpty()
                                    join u in _dbContext.UserDetails on doc.CreatedById equals u.AspNetUserId
                                    into finaldocs
                                    from final in finaldocs.DefaultIfEmpty()
                                    where doc.IsDeleted == false && es.EntityId == request.requestId &&
                                    doc.PageId == (int)FileSourceEntityTypes.ComparativeStatement
                                    select new StoreDocumentModel 
                                    {
                                        DocumentFileId= doc.DocumentFileId,
                                        // DocumentName= doc.Name,
                                        // DocumentTypeId= doc.DocumentTypeId,
                                        // UploadedBy= $"{final.FirstName} {final.LastName}",
                                        // UploadedOn= doc.CreatedDate,
                                        // DocumentTypeName= doc.DocumentTypeId== (int)DocumentFileTypes.PurchaseImage ? "Image" : "Invoice"
                                    }).ToListAsync();

                    var user = await _dbContext.UserDetails.FirstOrDefaultAsync(x=> x.AspNetUserId == statement.CreatedById);
                    model.SubmittedBy = user.FirstName + ' ' + user.LastName;
                    model.Description = statement.Description;
                    // model.selectedSupplier = await _dbContext.ProjectLogisticSuppliers.Where(x=>x.IsDeleted == false && statement.SupplierIds.Contains(x.SupplierId))
                    //                                         .Select(x=> new SupplierDetailModel{
                    //                                             Id = x.SupplierId,
                    //                                             SupplierName = x.SupplierName
                    //                                         })
                    //                                         .ToListAsync();

                    var documentList = await documentsAsync;
                    foreach(var item in documentList)
                    {
                        FileModel fileModel = new FileModel()
                        {
                            PageId = (int)FileSourceEntityTypes.ComparativeStatement,
                            RecordId = request.requestId,
                            DocumentFileId= item.DocumentFileId
                        };

                        //get Saved Document ID and Signed URL For Purchase Image
                        var documentModel = await _fileManagement.GetFilesByDocumentFileId(fileModel);

                        if (documentModel != null)
                        {
                            attachments.Add(new StatementAttachmentModel{
                                AttachmentName = documentModel.DocumentName,
                                AttachmentUrl = documentModel.SignedURL
                            });
                        }
                    }
                    model.attachments = attachments;
                }
                
                response.data.ComparativeStatement = model;
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