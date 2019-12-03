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

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
        public class DeleteLogisticRequestItemCommandHandler : IRequestHandler<DeleteLogisticRequestItemCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public DeleteLogisticRequestItemCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(DeleteLogisticRequestItemCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
                try
                {
                    var listitem = await _dbContext.ProjectLogisticItems
                                                    .FirstOrDefaultAsync(x => x.IsDeleted == false && x.LogisticItemId == request.ItemId);

                    if (listitem == null)
                    {
                        throw new Exception("Item not found!");
                    }


                    listitem.ModifiedDate = request.ModifiedDate;
                    listitem.ModifiedById = request.ModifiedById;
                    listitem.IsDeleted = true;

                    await _dbContext.SaveChangesAsync();

                    var totalCost = await _dbContext.ProjectLogisticItems.Where(x=>x.IsDeleted == false && x.LogisticRequestsId == request.RequestId)
                    .SumAsync(x=>x.EstimatedUnitCost * x.Quantity);
                    var logisticRequest = await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=>x.IsDeleted == false && x.LogisticRequestsId == request.RequestId);
                    if (logisticRequest!=null) {
                        logisticRequest.TotalCost = totalCost;
                        await _dbContext.SaveChangesAsync();
                    }

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
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
