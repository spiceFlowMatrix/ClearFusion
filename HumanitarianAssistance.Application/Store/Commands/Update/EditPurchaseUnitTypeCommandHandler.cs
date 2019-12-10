using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditPurchaseUnitTypeCommandHandler : IRequestHandler<EditPurchaseUnitTypeCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public EditPurchaseUnitTypeCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(EditPurchaseUnitTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (request.IsDefault)
                {
                    PurchaseUnitType unitType = await _dbContext.PurchaseUnitType.FirstOrDefaultAsync(x => x.IsDeleted == false && x.IsDefault == true);

                    if (unitType != null)
                    {
                        unitType.IsDefault = false;
                        await _dbContext.SaveChangesAsync();
                    }
                }

                var editUnitType = await _dbContext.PurchaseUnitType.FirstOrDefaultAsync(x => x.UnitTypeId == request.UnitTypeId);
                if (editUnitType != null)
                {
                    editUnitType.UnitTypeName = request.UnitTypeName;
                    editUnitType.IsDeleted = false;

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
                return response;
            }

            return response;
        }
    }
}
