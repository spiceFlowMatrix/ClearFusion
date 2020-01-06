using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Enums;


namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddLogisticRequestCommandHandler : IRequestHandler<AddLogisticRequestCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddLogisticRequestCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(AddLogisticRequestCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            using (var _dbTransaction = _dbContext.Database.BeginTransaction()){
                try
                {
                    ProjectLogisticRequests obj= new ProjectLogisticRequests{
                        ProjectId = request.ProjectId,
                        Description = request.Description,
                        CurrencyId = request.CurrencyId,
                        BudgetLineId = request.BudgetLineId,
                        OfficeId = request.OfficeId,
                        Status = request.Status,
                        ComparativeStatus = request.ComparativeStatus,
                        TenderStatus = request.TenderStatus,  
                        TotalCost = request.TotalCost,
                        CreatedDate = request.CreatedDate,
                        CreatedById = request.CreatedById,
                        IsDeleted = false
                    };
                    await _dbContext.ProjectLogisticRequests.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    foreach (var item in request.RequestedItems)
                    {
                        ProjectLogisticItems itemobj= new ProjectLogisticItems{
                            ItemId = item.ItemId,
                            EstimatedUnitCost = item.EstimatedUnitCost,
                            PurchaseSubmitted = false,
                            Quantity = item.RequestedUnits,
                            IsDeleted = false,
                            LogisticRequestsId = obj.LogisticRequestsId,
                            CreatedDate = request.CreatedDate,
                            CreatedById = request.CreatedById,
                        };
                        await _dbContext.ProjectLogisticItems.AddAsync(itemobj);
                    }
                    await _dbContext.SaveChangesAsync();
                    _dbTransaction.Commit();
                    
                    response.data.logisticRequestId=obj.LogisticRequestsId;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                catch (Exception ex)
                {
                    _dbTransaction.Rollback();
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = ex.Message;
                }
            }
            return response;
        }
    }
}
