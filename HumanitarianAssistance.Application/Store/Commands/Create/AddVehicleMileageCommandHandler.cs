using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddVehicleMileageCommandHandler : IRequestHandler<AddVehicleMileageCommand, bool>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddVehicleMileageCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(AddVehicleMileageCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                if (request != null)
                {

                    VehicleMileageDetail mileage = await _dbContext.VehicleMileageDetail.Include(x => x.PurchasedVehicleDetail)
                                                                        .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                        x.VehicleId == request.VehicleId &&
                                                                                        x.MileageMonth.Month == request.Month.Month);


                    if (mileage == null)
                    {

                        var vehicle = await _dbContext.PurchasedVehicleDetail.FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                        x.Id == request.VehicleId);

                        if (vehicle != null)
                        {
                            if (request.Month.Month < vehicle.CreatedDate.Value.Month &&
                            request.Month.Year <= vehicle.CreatedDate.Value.Year)
                            {
                                throw new Exception(StaticResource.MileageMonthNotValid);
                            }
                        }

                        mileage = new VehicleMileageDetail
                        {
                            IsDeleted = false,
                            CreatedDate = DateTime.UtcNow,
                            CreatedById = request.CreatedById,
                            VehicleId = request.VehicleId,
                            MileageMonth = request.Month,
                            Mileage = request.Mileage
                        };

                        await _dbContext.VehicleMileageDetail.AddAsync(mileage);

                        //log details
                        StoreLogger logger = new StoreLogger
                        {
                            CreatedDate = DateTime.UtcNow,
                            CreatedById = request.CreatedById,
                            IsDeleted = false,
                            EventType = "Mileage Added",
                            LogText = $"{request.Mileage} Mileage added to this vehicle",
                            TransportType = (int)TransportItemCategory.Vehicle,
                            TransportTypeEntityId = request.VehicleId
                        };

                        await _dbContext.StoreLogger.AddAsync(logger);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        mileage.ModifiedDate = DateTime.UtcNow;
                        mileage.ModifiedById = request.CreatedById;
                        mileage.VehicleId = request.VehicleId;
                        mileage.MileageMonth = request.Month;
                        mileage.Mileage = request.Mileage;

                        _dbContext.VehicleMileageDetail.Update(mileage);

                        //log details
                        StoreLogger logger = new StoreLogger
                        {
                            CreatedDate = DateTime.UtcNow,
                            CreatedById = request.CreatedById,
                            IsDeleted = false,
                            EventType = "Mileage Updated",
                            LogText = $"{request.Mileage} Mileage added to this Vehcile",
                            TransportType = (int)TransportItemCategory.Vehicle,
                            TransportTypeEntityId = request.VehicleId
                        };

                        await _dbContext.StoreLogger.AddAsync(logger);
                        await _dbContext.SaveChangesAsync();
                    }

                    success = true;
                }
                else
                {
                    throw new Exception(StaticResource.RequestValuesInAppropriate);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}