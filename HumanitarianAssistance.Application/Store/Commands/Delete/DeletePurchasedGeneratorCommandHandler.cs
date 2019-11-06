using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeletePurchasedGeneratorCommandHandler : IRequestHandler<DeletePurchasedGeneratorCommand, bool>
    {

        private HumanitarianAssistanceDbContext _dbContext;
        public DeletePurchasedGeneratorCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeletePurchasedGeneratorCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                var purchasedGenerator = await _dbContext.PurchasedGeneratorDetail
                                                         .FirstOrDefaultAsync(x=> x.IsDeleted == false &&
                                                         x.Id == request.PurchasedGeneratorId);

                if(purchasedGenerator != null)
                {
                    purchasedGenerator.IsDeleted = true;
                }

                _dbContext.PurchasedGeneratorDetail.Update(purchasedGenerator);
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