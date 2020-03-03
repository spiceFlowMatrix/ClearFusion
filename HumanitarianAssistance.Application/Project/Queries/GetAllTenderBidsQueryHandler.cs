using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllTenderBidsQueryHandler: IRequestHandler<GetAllTenderBidsQuery, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        private readonly IFileManagementService _fileManagement;

        public GetAllTenderBidsQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, 
        IFileManagementService fileManagement)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileManagement = fileManagement;
        }

        public async Task<ApiResponse> Handle(GetAllTenderBidsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var _tenderBids = await _dbContext.TenderBidSubmission.Where(x=>x.IsDeleted == false && x.LogisticRequestsId == request.RequestId).ToListAsync();
                
                List<TenderBidsModel> obj = _mapper.Map<List<TenderBidsModel>>(_tenderBids);

                var contractDocument = await (from es in _dbContext.EntitySourceDocumentDetails 
                                    join df in _dbContext.DocumentFileDetail on es.DocumentFileId equals df.DocumentFileId
                                    into docs
                                    from doc in docs.DefaultIfEmpty()
                                    join u in _dbContext.UserDetails on doc.CreatedById equals u.AspNetUserId
                                    into finaldocs
                                    from final in finaldocs.DefaultIfEmpty()
                                    where doc.IsDeleted == false && _tenderBids.Select(x=>x.BidId).Contains(es.EntityId) &&
                                    doc.PageId == (int)FileSourceEntityTypes.TenderBidContractLetter
                                    select new StoreDocumentModel 
                                    {
                                        DocumentFileId= doc.DocumentFileId,
                                        UploadedBy= $"{final.FirstName} {final.LastName}",
                                        EntityId = es.EntityId
                                    }).ToListAsync();

                foreach (var doc in contractDocument)
                {
                    FileModel fileModel = new FileModel()
                    {
                        PageId = (int)FileSourceEntityTypes.TenderBidContractLetter,
                        RecordId = doc.EntityId,
                        DocumentFileId= doc.DocumentFileId
                    };

                    var bid = obj.FirstOrDefault(x=>x.BidId == fileModel.RecordId);
                    if(bid != null) {
                        var documentModel = await _fileManagement.GetFilesByDocumentFileId(fileModel);
                        bid.ContractLetterUrl = documentModel.SignedURL;
                        bid.ContractLetterName = documentModel.DocumentName;
                    }
                }
                foreach (var item in obj)
                {
                    var sum = item.Profile_Experience + item.OfferValidity +
                    item.TOR_SOWAcceptance + item.Securities_BankGuarantee +
                    item.OfferDocumentation +
                    item.Company_GoodsSpecification + item.Service_Warranty +
                    item.Certification_GMP_COPP + item.WorkExperience +
                    item.DeliveryDateScore;
                    var percentage = (((Convert.ToDouble(sum))/100)*100);
                    item.TotalScore = getTotalScoreText(percentage);
                }

                response.data.TenderBids = obj;
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

        private string getTotalScoreText(double percentage) 
        {
            var returnText = "";
            if(percentage <= 10)
            {
                returnText = percentage + "% " + "Very Poor";
            }
            else if (percentage > 10 && percentage <= 40)
            {
                returnText = percentage + "% " + "Poor";
            }
            else if (percentage > 40 && percentage <= 70)
            {
                returnText = percentage + "% " + "Satisfactory";
            }
            else if (percentage > 70 && percentage <= 90)
            {
                returnText = percentage + "% " + "Good";
            }
            else if (percentage > 90 && percentage <= 100)
            {
                returnText = percentage + "% " + "Excellent";
            }
            return returnText;
        }
        
    }
}