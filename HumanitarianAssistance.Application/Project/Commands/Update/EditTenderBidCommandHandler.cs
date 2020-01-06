using System;
using HumanitarianAssistance.Common.Enums;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Linq;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class EditTenderBidCommandHandler : IRequestHandler<EditTenderBidCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        public EditTenderBidCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(EditTenderBidCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var bid = await _dbContext.TenderBidSubmission.FirstOrDefaultAsync(x=>x.IsDeleted == false && x.BidId == request.BidId);
                if (bid == null) {
                    throw new Exception("No bid found!");
                }
                _mapper.Map(request, bid);
                await _dbContext.SaveChangesAsync();

                if(request.isContractLetterUpdated) 
                {
                    var contractDocuments = await _dbContext.EntitySourceDocumentDetails
                    .Include(x=>x.DocumentFileDetail)
                    .Where(x=>x.IsDeleted == false && x.EntityId == bid.BidId && x.DocumentFileDetail.PageId == (int)FileSourceEntityTypes.TenderBidContractLetter).ToListAsync();
                    foreach (var doc in contractDocuments) {
                        doc.IsDeleted = true;
                        doc.DocumentFileDetail.IsDeleted = true;
                    }
                    await _dbContext.SaveChangesAsync();
                }
                response.CommonId.LongId = bid.BidId;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch(Exception ex) 
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

    }
}