using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeletePurchaseUnitTypeCommandHandler : IRequestHandler<DeletePurchaseUnitTypeCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public DeletePurchaseUnitTypeCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(DeletePurchaseUnitTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var deletePurchaseUnitType = await _dbContext.PurchaseUnitType.FirstOrDefaultAsync(x => x.UnitTypeId == request.UnitTypeId);
                if (deletePurchaseUnitType != null)
                {
                    deletePurchaseUnitType.IsDeleted = true;

                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong;
                }
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
