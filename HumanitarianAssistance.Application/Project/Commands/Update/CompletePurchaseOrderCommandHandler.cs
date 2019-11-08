using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class CompletePurchaseOrderCommandHandler : IRequestHandler<CompletePurchaseOrderCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public CompletePurchaseOrderCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(CompletePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var list = await _dbContext.ProjectLogisticItems.Where(x=>x.IsDeleted==false && request.Id.Contains(x.LogisticItemId)).ToListAsync();
                foreach(var item in list) {
                    item.PurchaseSubmitted = true;
                }
                var requestId = list.Where(x=>x.IsDeleted==false).Select(x=>x.LogisticRequestsId).FirstOrDefault();
                var logRequest = await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=>x.IsDeleted==false && x.LogisticRequestsId == requestId);
                logRequest.Status = (int)LogisticRequestStatus.CompletePurchase;
                await _dbContext.SaveChangesAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
