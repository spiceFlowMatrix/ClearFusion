using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditGeneratorDetailCommandHandler: IRequestHandler<EditGeneratorDetailCommand, bool>
    {
         private HumanitarianAssistanceDbContext _dbContext;
        public EditGeneratorDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(EditGeneratorDetailCommand command, CancellationToken cancellationToken)
        {
            bool isSuccess = false;

            try
            {
                if(command == null)
                {
                    throw new Exception(StaticResource.RequestValuesInAppropriate);
                }

                PurchasedGeneratorDetail generator= await _dbContext.PurchasedGeneratorDetail
                                                          .FirstOrDefaultAsync(x=> x.IsDeleted == false && x.Id == command.GeneratorId);

                generator.Voltage = command.Voltage;
                generator.StartingUsage= command.StartingUsage;
                generator.IncurredUsage= command.IncurredUsage;
                generator.FuelConsumptionRate= command.FuelConsumptionRate;
                generator.MobilOilConsumptionRate= command.MobilOilConsumptionRate;
                generator.ModelYear= command.ModelYear;
                generator.OfficeId = command.OfficeId;

                _dbContext.PurchasedGeneratorDetail.Update(generator);
                await _dbContext.SaveChangesAsync();
                isSuccess= true;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return isSuccess;
        }
    }
}