using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class EditLogisticRequestItemsCommandHandler : IRequestHandler<EditLogisticRequestItemsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditLogisticRequestItemsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditLogisticRequestItemsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var itemcheck = await  _dbContext.ProjectLogisticItems.Where(x=>x.IsDeleted==false && x.ItemId== request.ItemId && x.LogisticItemId!=request.Id && x.LogisticRequestsId==request.RequestId).ToListAsync();
                if(itemcheck.Count()>0){
                    throw new Exception("Item already exists!");
                }
                var existitem = await _dbContext.ProjectLogisticItems.FirstOrDefaultAsync(x=>x.IsDeleted==false && x.LogisticItemId == request.Id);
                if(existitem == null){
                    throw new Exception("Item doesnot exists!");
                }
                existitem.ItemId = request.ItemId;
                existitem.Quantity = request.RequestedUnits;
                existitem.EstimatedUnitCost = request.EstimatedUnitCost;

                existitem.ModifiedDate = request.ModifiedDate;
                existitem.ModifiedById = request.ModifiedById;
                existitem.IsDeleted = false;

                await _dbContext.SaveChangesAsync();
                
                var totalCost = await _dbContext.ProjectLogisticItems.Where(x=>x.IsDeleted == false && x.LogisticRequestsId == request.RequestId)
                .SumAsync(x=>x.EstimatedUnitCost * x.Quantity);
                var logisticRequest = await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=>x.IsDeleted == false && x.LogisticRequestsId == request.RequestId);
                if (logisticRequest!=null) {
                    logisticRequest.TotalCost = totalCost;
                    if(totalCost>=200 && totalCost<=9999)
                    {
                        logisticRequest.ComparativeStatus = (int)LogisticComparativeStatus.Pending;
                    }
                    await _dbContext.SaveChangesAsync();
                }
                // var returnobj = await _dbContext.ProjectLogisticItems.Where(x=>x.IsDeleted==false && x.LogisticItemId==request.Id)
                // .Select(y=> new LogisticItemModel{
                //     Id = y.LogisticItemId,
                //     Item = y.StoreInventoryItem.ItemName,
                //     Quantity = y.Quantity,
                //     EstimatedCost = y.EstimatedUnitCost,
                //     Availability = y.Quantity,
                //     ItemId = y.ItemId
                // })
                // .FirstOrDefaultAsync();
                // response.data.logisticItem = returnobj;
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
