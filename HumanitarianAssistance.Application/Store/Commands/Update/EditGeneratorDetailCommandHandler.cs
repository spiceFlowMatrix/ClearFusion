using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditGeneratorDetailCommandHandler : IRequestHandler<EditGeneratorDetailCommand, bool>
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
                if (command == null)
                {
                    throw new Exception(StaticResource.RequestValuesInAppropriate);
                }

                PurchasedGeneratorDetail generator = await _dbContext.PurchasedGeneratorDetail
                                                          .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == command.GeneratorId);
                generator.ModifiedDate = DateTime.UtcNow;
                generator.ModifiedById = command.ModifiedById;
                generator.Voltage = command.Voltage;
                generator.StartingUsage = command.StartingUsage;
                generator.IncurredUsage = command.IncurredUsage;
                generator.FuelConsumptionRate = command.FuelConsumptionRate;
                generator.MobilOilConsumptionRate = command.MobilOilConsumptionRate;
                generator.ModelYear = command.ModelYear;
                generator.OfficeId = command.OfficeId;

                _dbContext.PurchasedGeneratorDetail.Update(generator);

                //log details
                StoreLogger logger = new StoreLogger
                {
                    CreatedDate = DateTime.UtcNow,
                    CreatedById = command.ModifiedById,
                    IsDeleted = false,
                    EventType = "Generator Edited",
                    LogText = $"Generator details were edited",
                    TransportType= (int)TransportItemCategory.Generator,
                    TransportTypeEntityId= command.GeneratorId
                };

                await _dbContext.StoreLogger.AddAsync(logger);
                await _dbContext.SaveChangesAsync();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isSuccess;
        }
    }
}