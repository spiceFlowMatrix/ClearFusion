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
                    vehicle = await _dbContext.PurchasedVehicleDetail
                                          .Include(x => x.EmployeeDetail)
                                          .Include(x => x.StoreItemPurchase)
                                          .Include(x => x.VehicleMileageDetail)
                                          .Include(x => x.VehicleItemDetail)
                                          .ThenInclude(x => x.StoreItemPurchase)
                                          .ThenInclude(x => x.StoreInventoryItem)
                                          .ThenInclude(x=> x.StoreItemGroup)
                                          .Where(x => x.IsDeleted == false && x.Id == request.VehicleId)
                                          .Select(x => new VehicleModel
                                          {
                                              VehicleId = x.Id,
                                              PlateNo = x.PlateNo,
                                              EmployeeId = x.EmployeeId,
                                              StandardFuelConsumptionRate = x.FuelConsumptionRate,
                                              StandardMobilOilConsumptionRate = x.MobilOilConsumptionRate,
                                              StartingMileage = x.StartingMileage,
                                              IncurredMileage = x.IncurredMileage,
                                              ModelYear = x.ModelYear,
                                              OfficeId = x.OfficeId,
                                              EmployeeName = x.EmployeeDetail.EmployeeName,
                                              PurchaseName = $"{x.StoreItemPurchase.PurchaseName} - {x.StoreItemPurchase.PurchaseDate.ToShortDateString()}-{x.StoreItemPurchase.PurchaseId}",
                                              PurchaseId = x.StoreItemPurchase.PurchaseId,
                                              OfficeName = x.OfficeId != 0 ? _dbContext.OfficeDetail.FirstOrDefault(y => y.IsDeleted == false && y.OfficeId == x.OfficeId).OfficeName : null,
                                              TotalFuelUsage = x.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                                                  y.VehiclePurchaseId == request.VehicleId &&
                                                                                  y.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                                  y.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                                                               .Select(z => z.StoreItemPurchase.Quantity).DefaultIfEmpty(0).Sum(),
                                              TotalMobilOilUsage = x.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                                                   y.VehiclePurchaseId == request.VehicleId && 
                                                                                   y.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                                   y.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                                                               .Select(z => z.StoreItemPurchase.Quantity).DefaultIfEmpty(0).Sum(),
                                              FuelTotalCost = x.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                                                     y.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                                     y.VehiclePurchaseId == request.VehicleId && y.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                                                               .Select(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost).DefaultIfEmpty(0).Sum(),
                                              MobilOilTotalCost = x.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                                                     y.VehiclePurchaseId == request.VehicleId &&
                                                                                     y.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                                     y.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                                                               .Select(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost).DefaultIfEmpty(0).Sum(),
                                              SparePartsTotalCost = x.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                                                     y.VehiclePurchaseId == request.VehicleId && 
                                                                                      y.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle && 
                                                                                      y.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.SpareParts)
                                                                               .Select(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost).DefaultIfEmpty(0).Sum(),
                                              ServiceAndMaintenanceTotalCost = x.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                                                     y.VehiclePurchaseId == request.VehicleId &&
                                                                                     y.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                                     y.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MaintenanceService)
                                                                               .Select(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost).DefaultIfEmpty(0).Sum(),
                                              CurrentMileage = x.StartingMileage + x.IncurredMileage + x.VehicleMileageDetail.Where(z => z.IsDeleted == false).Select(z => z.Mileage).DefaultIfEmpty(0).Sum(),
                                              VehicleStartingCost = x.StoreItemPurchase.UnitCost,
                                              ChasisNo= x.ChasisNo,
                                              EngineNo= x.EngineNo,
                                              RegistrationNo= x.RegistrationNo,
                                              ManufacturerCountry= x.ManufacturerCountry,
                                              Remarks= x.PersonRemarks
                                          }).FirstOrDefaultAsync();

                if (vehicle == null)
                {
                    throw new Exception(StaticResource.RecordNotFound);
                }

                vehicle.ActualFuelConsumptionRate = vehicle.CurrentMileage != 0 ? Math.Round((vehicle.TotalFuelUsage / vehicle.CurrentMileage) * 100, 4) : 0;
                vehicle.ActualMobilOilConsumptionRate = vehicle.CurrentMileage != 0 ? Math.Round((vehicle.TotalMobilOilUsage / vehicle.CurrentMileage) * 100, 4) : 0;

            }
            catch (Exception ex)
            {
                throw ex;

            }
            return vehicle;
        }
    }
}