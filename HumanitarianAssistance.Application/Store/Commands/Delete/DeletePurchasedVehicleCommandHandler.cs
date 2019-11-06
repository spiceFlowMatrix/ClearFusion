using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeletePurchasedVehicleCommandHandler: IRequestHandler<DeletePurchasedVehicleCommand, bool>
    {

        private HumanitarianAssistanceDbContext _dbContext;
        public DeletePurchasedVehicleCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeletePurchasedVehicleCommand request, CancellationToken cancellationToken)
        {
            bool success = false;
            
            try
            {
                var purchasedVehicle = await _dbContext.PurchasedVehicleDetail
                                                         .FirstOrDefaultAsync(x=> x.IsDeleted == false &&
                                                         x.Id == request.PurchasedVehicleId);

                if(purchasedVehicle != null)
                {
                    purchasedVehicle.IsDeleted = true;
                }

                _dbContext.PurchasedVehicleDetail.Update(purchasedVehicle);
                await _dbContext.SaveChangesAsync();

                success= true;
            }
            catch(Exception ex)
            {
                throw ex;
                
            }
            return success;
        }
        
    }
}