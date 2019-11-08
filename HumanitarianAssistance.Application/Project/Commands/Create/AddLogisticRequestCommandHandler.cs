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
            try
            {
                ProjectLogisticRequests obj= new ProjectLogisticRequests{
                    ProjectId = request.ProjectId,
                    RequestName = request.RequestName,
                    CurrencyId = request.CurrencyId,
                    BudgetLineId = request.BudgetLineId,
                    OfficeId = request.OfficeId,
                    Status = (int)LogisticRequestStatus.NewRequest,
                    TotalCost = 0,
                    CreatedDate = request.CreatedDate,
                    CreatedById = request.CreatedById,
                    IsDeleted = false
                };
                await _dbContext.ProjectLogisticRequests.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
                response.data.logisticRequestId=obj.LogisticRequestsId;
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
