using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditVehicleDetailCommandHandler: IRequestHandler<EditVehicleDetailCommand, bool>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public EditVehicleDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(EditVehicleDetailCommand request, CancellationToken cancellationToken)
        {
            bool isSuccess = false;

            try
            {
                if(request == null)
                {
                    throw new Exception(StaticResource.RequestValuesInAppropriate);
                }

                PurchasedVehicleDetail vehicle= await _dbContext.PurchasedVehicleDetail
                                                          .FirstOrDefaultAsync(x=> x.IsDeleted == false && x.Id == request.VehicleId);

                if(vehicle == null)
                {
                    throw new Exception(StaticResource.RecordNotFound);
                }

                vehicle.PlateNo = request.PlateNo;
                vehicle.EmployeeId= request.EmployeeId;
                vehicle.StartingMileage= request.StartingMileage;
                vehicle.IncurredMileage= request.IncurredMileage;
                vehicle.FuelConsumptionRate= request.FuelConsumptionRate;
                vehicle.MobilOilConsumptionRate= request.MobilOilConsumptionRate;
                vehicle.ModelYear= request.ModelYear;
                vehicle.OfficeId = request.OfficeId;
                vehicle.ModifiedById = request.ModifiedById;
                vehicle.ModifiedDate= DateTime.UtcNow;

                _dbContext.PurchasedVehicleDetail.Update(vehicle);

                 //log details
                StoreLogger logger = new StoreLogger
                {
                    CreatedDate = DateTime.UtcNow,
                    CreatedById = request.ModifiedById,
                    IsDeleted = false,
                    EventType = "Vehicle Edited",
                    LogText = $"Vehicle details were edited",
                    TransportType = (int)TransportItemCategory.Vehicle,
                    TransportTypeEntityId= request.VehicleId
                };

                await _dbContext.StoreLogger.AddAsync(logger);
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