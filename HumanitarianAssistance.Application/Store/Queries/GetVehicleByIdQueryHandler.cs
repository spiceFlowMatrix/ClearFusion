using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleModel>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public GetVehicleByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VehicleModel> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            VehicleModel vehicle = new VehicleModel();

            try
            {
                var vehicles = await _dbContext.PurchasedVehicleDetail
                                          .Include(x => x.EmployeeDetail)
                                          .Include(x => x.StoreItemPurchase)
                                          .Include(x => x.VehicleMileageDetail)
                                          .Include(x => x.VehicleItemDetail)
                                          .ThenInclude(x => x.StoreItemPurchase)
                                          .ThenInclude(x => x.StoreInventoryItem)
                                          .Where(x => x.IsDeleted == false && x.Id == request.VehicleId)
                                          .FirstOrDefaultAsync();

                if (vehicles == null)
                {
                    throw new Exception(StaticResource.RecordNotFound);
                }

                vehicle = new VehicleModel
                {
                    VehicleId = vehicles.Id,
                    PlateNo = vehicles.PlateNo,
                    EmployeeId = vehicles.EmployeeId,
                    StandardFuelConsumptionRate = vehicles.FuelConsumptionRate,
                    StandardMobilOilConsumptionRate = vehicles.MobilOilConsumptionRate,
                    StartingMileage = vehicles.StartingMileage,
                    IncurredMileage = vehicles.IncurredMileage,
                    ModelYear = vehicles.ModelYear,
                    OfficeId = vehicles.OfficeId,
                    EmployeeName = vehicles.EmployeeDetail.EmployeeName,
                    PurchaseName = $"{vehicles.StoreItemPurchase.PurchaseName} - {vehicles.StoreItemPurchase.PurchaseDate.ToShortDateString()}-{vehicles.StoreItemPurchase.PurchaseId}",
                    PurchaseId = vehicles.StoreItemPurchase.PurchaseId,
                    OfficeName = vehicles.OfficeId != 0 ? _dbContext.OfficeDetail.FirstOrDefault(y => y.IsDeleted == false && y.OfficeId == vehicles.OfficeId).OfficeName : null,
                    TotalFuelUsage = vehicles.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                        y.VehiclePurchaseId == request.VehicleId && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleFuel)
                                                                               .Select(z => z.StoreItemPurchase.Quantity).Sum(),
                    TotalMobilOilUsage = vehicles.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                         y.VehiclePurchaseId == request.VehicleId && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil)
                                                                               .Sum(z => z.StoreItemPurchase.Quantity),
                    FuelTotalCost = vehicles.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                           y.VehiclePurchaseId == request.VehicleId && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleFuel)
                                                                               .Sum(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost),
                    MobilOilTotalCost = vehicles.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                           y.VehiclePurchaseId == request.VehicleId && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil)
                                                                               .Sum(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost),
                    SparePartsTotalCost = vehicles.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                           y.VehiclePurchaseId == request.VehicleId && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleSpareParts)
                                                                               .Sum(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost) ,
                    ServiceAndMaintenanceTotalCost = vehicles.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                           y.VehiclePurchaseId == request.VehicleId && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMaintenanceService)
                                                                               .Sum(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost),
                    CurrentMileage= vehicles.StartingMileage+ vehicles.IncurredMileage+ vehicles.VehicleMileageDetail.Where(x=> x.IsDeleted == false).Sum(x=> x.Mileage),
                    VehicleStartingCost= vehicles.StoreItemPurchase.UnitCost,
                };
                
                    vehicle.ActualFuelConsumptionRate= vehicle.TotalFuelUsage !=0 ? Math.Round((vehicle.TotalFuelUsage/vehicle.CurrentMileage)*100, 4) :0;
                    vehicle.ActualMobilOilConsumptionRate= vehicle.TotalMobilOilUsage !=0 ? Math.Round((vehicle.TotalMobilOilUsage/vehicle.CurrentMileage)*100, 4): 0;
                
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return vehicle;
        }
    }
}