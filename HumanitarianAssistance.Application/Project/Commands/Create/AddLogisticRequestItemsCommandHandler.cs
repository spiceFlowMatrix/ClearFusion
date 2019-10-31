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

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddLogisticRequestItemsCommandHandler : IRequestHandler<AddLogisticRequestItemsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddLogisticRequestItemsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddLogisticRequestItemsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var existitem= await _dbContext.ProjectLogisticItems.FirstOrDefaultAsync(x=>x.IsDeleted==false && x.ItemId==request.ItemId && x.LogisticRequestsId == request.RequestId);
                if(existitem != null){
                    throw new Exception("Item already exists!");
                }
                ProjectLogisticItems obj =new ProjectLogisticItems{
                    LogisticRequestsId= request.RequestId,
                    ItemId = request.ItemId,
                    Quantity = request.Quantity,
                    EstimatedCost =request.EstimatedCost,
                    CreatedById = request.CreatedById,
                    CreatedDate = request.CreatedDate,
                    IsDeleted = false
                };
                await _dbContext.ProjectLogisticItems.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
                var returnobj = await _dbContext.ProjectLogisticItems.Where(x=>x.IsDeleted==false && x.LogisticItemId==obj.LogisticItemId)
                .Select(y=> new LogisticItemModel{
                    Id = y.LogisticItemId,
                    Item = y.StoreInventoryItem.ItemName,
                    Quantity = y.Quantity,
                    EstimatedCost = y.EstimatedCost,
                    Availability = y.Quantity,
                    ItemId = y.ItemId
                })
                .FirstOrDefaultAsync();
                response.data.logisticItem = returnobj;
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
