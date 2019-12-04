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
    public class EditLogisticRequestCommandHandler : IRequestHandler<EditLogisticRequestCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditLogisticRequestCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditLogisticRequestCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var _logisticReq = await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=>x.IsDeleted == false && x.LogisticRequestsId== request.RequestId);
                if(_logisticReq != null) {
                    _logisticReq.Description = request.Description;
                    _logisticReq.OfficeId = request.OfficeId;
                    _logisticReq.CurrencyId = request.CurrencyId;
                    _logisticReq.BudgetLineId = request.BudgetLineId;
                    _logisticReq.Status = request.Status;
                    _logisticReq.ComparativeStatus = request.ComparativeStatus;
                    _logisticReq.TotalCost = request.TotalCost;
                }

                var _logisticItems = await _dbContext.ProjectLogisticItems.Where(x=>x.IsDeleted == false && x.LogisticRequestsId == request.RequestId).ToListAsync();
                var excludedItemId = _logisticItems.Select(x=>x.LogisticItemId).Except(request.RequestedItems.Select(y=>y.Id));
                foreach(var id in excludedItemId) {
                    var item = _logisticItems.FirstOrDefault(x=>x.LogisticItemId == id);
                    item.IsDeleted = true;
                }
                foreach(var item in request.RequestedItems) {
                    if(item.Id == 0) {
                        ProjectLogisticItems obj =new ProjectLogisticItems{
                            LogisticRequestsId= request.RequestId,
                            ItemId = item.ItemId,
                            Quantity = item.RequestedUnits,
                            EstimatedUnitCost =item.EstimatedUnitCost,
                            CreatedById = request.CreatedById,
                            CreatedDate = request.CreatedDate,
                            IsDeleted = false
                        };
                        _dbContext.ProjectLogisticItems.Add(obj);
                    }
                }
                await _dbContext.SaveChangesAsync();
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
