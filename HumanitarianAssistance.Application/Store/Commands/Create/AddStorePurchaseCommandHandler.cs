using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Enums;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddStorePurchaseCommandHandler : IRequestHandler<AddStorePurchaseCommand, long>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddStorePurchaseCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<long> Handle(AddStorePurchaseCommand request, CancellationToken cancellationToken)
        {
            request.PurchaseDate = request.TimezoneOffset == null ? request.PurchaseDate : request.PurchaseDate.AddMinutes(Math.Abs((double)request.TimezoneOffset));
            long purchaseId = 0;
            string unitName = string.Empty;
            string eventType = string.Empty;

            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (request != null)
                    {
                        var exRate = await _dbContext.ExchangeRateDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Date.Date == request.PurchaseDate.Date && x.FromCurrency == request.Currency && x.ToCurrency == (int)Currency.USD);

                        if (exRate == null)
                        {
                            throw new Exception($"Exchange Rates not defined for Date {request.PurchaseDate.Date.ToString("dd/MM/yyyy")}");
                        }

                        StoreItemPurchase purchase = _mapper.Map<StoreItemPurchase>(request);

                        purchase.IsDeleted = false;
                        purchase.CreatedById = request.CreatedById;
                        purchase.CreatedDate = request.CreatedDate;
                        purchase.SerialNo = request.PurchaseOrderNo.ToString();

                        List<PurchaseUnitType> PurchaseUnitTypeList = await _dbContext.PurchaseUnitType.Where(x => x.IsDeleted == false).ToListAsync();
                        unitName = PurchaseUnitTypeList.Any(x => x.UnitTypeId == request.UnitType) ? PurchaseUnitTypeList.First(x => x.UnitTypeId == request.UnitType).UnitTypeName : "";

                        //Add Purchase
                        await _dbContext.StoreItemPurchases.AddAsync(purchase);
                        await _dbContext.SaveChangesAsync();
                        purchaseId = purchase.PurchaseId;

                        //Add Purchased vehicle List
                        if (request.PurchasedVehicleList.Any())
                        {
                            List<PurchasedVehicleDetail> purchasedVehicleList = new List<PurchasedVehicleDetail>();

                            foreach (var vehicle in request.PurchasedVehicleList)
                            {
                                PurchasedVehicleDetail vehicleDetail = new PurchasedVehicleDetail
                                {
                                    CreatedById = request.CreatedById,
                                    CreatedDate = request.CreatedDate,
                                    IsDeleted = false,
                                    PurchaseId = purchase.PurchaseId,
                                    EmployeeId = vehicle.EmployeeId,
                                    FuelConsumptionRate = vehicle.FuelConsumptionRate,
                                    IncurredMileage = vehicle.IncurredMileage,
                                    MobilOilConsumptionRate = vehicle.MobilOilConsumptionRate,
                                    ModelYear = vehicle.ModelYear,
                                    OfficeId = vehicle.OfficeId,
                                    PlateNo = vehicle.PlateNo,
                                    StartingMileage = vehicle.StartingMileage,
                                };

                                purchasedVehicleList.Add(vehicleDetail);
                            }

                            await _dbContext.PurchasedVehicleDetail.AddRangeAsync(purchasedVehicleList);
                            await _dbContext.SaveChangesAsync();
                            eventType="Vehicle";
                        }

                        //Add Purchased generator List
                        if (request.PurchasedGeneratorList.Any())
                        {
                            List<PurchasedGeneratorDetail> purchasedGeneratorList = new List<PurchasedGeneratorDetail>();
                            foreach (var generator in request.PurchasedGeneratorList)
                            {
                                PurchasedGeneratorDetail generatorDetail = new PurchasedGeneratorDetail
                                {
                                    CreatedById = request.CreatedById,
                                    CreatedDate = request.CreatedDate,
                                    IsDeleted = false,
                                    PurchaseId = purchase.PurchaseId,
                                    FuelConsumptionRate = generator.FuelConsumptionRate,
                                    IncurredUsage = generator.IncurredUsage,
                                    MobilOilConsumptionRate = generator.MobilOilConsumptionRate,
                                    ModelYear = generator.ModelYear,
                                    OfficeId = generator.OfficeId,
                                    StartingUsage = generator.StartingUsage,
                                    Voltage = generator.Voltage,
                                };

                                purchasedGeneratorList.Add(generatorDetail);
                            }

                            await _dbContext.PurchasedGeneratorDetail.AddRangeAsync(purchasedGeneratorList);
                            await _dbContext.SaveChangesAsync();
                            eventType="Vehicle";
                        }

                        //Add purchased vehicle Item
                        if (request.InventoryItem == (int)TransportItem.VehicleFuel ||
                        request.InventoryItem == (int)TransportItem.VehicleMaintenanceService ||
                        request.InventoryItem == (int)TransportItem.VehicleMobilOil ||
                        request.InventoryItem == (int)TransportItem.VehicleSpareParts)
                        {
                            VehicleItemDetail vehicleItem = new VehicleItemDetail
                            {
                                CreatedById = request.CreatedById,
                                CreatedDate = request.CreatedDate,
                                IsDeleted = false,
                                PurchaseId = purchase.PurchaseId,
                                VehiclePurchaseId = request.TransportItemId.Value
                            };

                            await _dbContext.VehicleItemDetail.AddAsync(vehicleItem);
                            await _dbContext.SaveChangesAsync();
                            
                            //get EventType
                            eventType = GetEventTypeName(request.InventoryItem);
                        }

                        //Add purchased generator Item
                        else if (request.InventoryItem == (int)TransportItem.GeneratorFuel ||
                        request.InventoryItem == (int)TransportItem.GeneratorMaintenanceService ||
                        request.InventoryItem == (int)TransportItem.GeneratorMobilOil ||
                        request.InventoryItem == (int)TransportItem.GeneratorSpareParts)
                        {
                            GeneratorItemDetail generatorItem = new GeneratorItemDetail
                            {
                                CreatedById = request.CreatedById,
                                CreatedDate = request.CreatedDate,
                                IsDeleted = false,
                                PurchaseId = purchase.PurchaseId,
                                GeneratorPurchaseId = request.TransportItemId.Value
                            };

                            await _dbContext.GeneratorItemDetail.AddAsync(generatorItem);
                            await _dbContext.SaveChangesAsync();

                            //get EventType
                            eventType = GetEventTypeName(request.InventoryItem);
                        }

                        //log details
                        StoreLogger logger = new StoreLogger
                        {
                            CreatedDate = DateTime.UtcNow,
                            CreatedById = request.CreatedById,
                            IsDeleted = false,
                            EventType = $"{eventType} Purchased",
                            LogText = $"{request.Quantity} {unitName} purchased in {purchaseId}",
                            PurchaseId = purchaseId
                        };

                        await _dbContext.StoreLogger.AddAsync(logger);
                        await _dbContext.SaveChangesAsync();

                        tran.Commit();
                    }
                    else
                    {
                        throw new Exception("request values are inappropriate");
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }

            return purchaseId;
        }

        private string GetEventTypeName(long inventoryItem)
        {
            string eventTypeName = "";

            switch (inventoryItem)
            {
                case (int)TransportItem.VehicleFuel:
                    eventTypeName = TransportItem.VehicleFuel.ToString();
                    break;
                case (int)TransportItem.VehicleMaintenanceService:
                    eventTypeName = TransportItem.VehicleMaintenanceService.ToString();
                    break;
                case (int)TransportItem.VehicleMobilOil:
                    eventTypeName = TransportItem.VehicleMobilOil.ToString();
                    break;
                case (int)TransportItem.VehicleSpareParts:
                    eventTypeName = TransportItem.VehicleSpareParts.ToString();
                    break;
                case (int)TransportItem.GeneratorFuel:
                    eventTypeName = TransportItem.GeneratorFuel.ToString();
                    break;
                case (int)TransportItem.GeneratorMaintenanceService:
                    eventTypeName = TransportItem.GeneratorMaintenanceService.ToString();
                    break;
                case (int)TransportItem.GeneratorMobilOil:
                    eventTypeName = TransportItem.GeneratorMobilOil.ToString();
                    break;
                case (int)TransportItem.GeneratorSpareParts:
                    eventTypeName = TransportItem.GeneratorSpareParts.ToString();
                    break;
            }

            return eventTypeName;
        }
    }
}