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
                var editUnitType = await _dbContext.PurchaseUnitType.FirstOrDefaultAsync(x => x.UnitTypeId == request.UnitTypeId);
                if (editUnitType != null)
                {
                    //_mapper.Map(model, editUnitType);
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
