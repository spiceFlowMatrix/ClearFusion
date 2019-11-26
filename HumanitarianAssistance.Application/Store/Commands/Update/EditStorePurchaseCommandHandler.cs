using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditStorePurchaseCommandHandler : IRequestHandler<EditStorePurchaseCommand, bool>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditStorePurchaseCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<bool> Handle(EditStorePurchaseCommand request, CancellationToken cancellationToken)
        {
            bool success = false;
            string unitName = string.Empty;
            string eventType = string.Empty;

            request.PurchaseDate = request.TimezoneOffset == null ? request.PurchaseDate : request.PurchaseDate.AddMinutes(Math.Abs((double)request.TimezoneOffset));

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

                        List<PurchaseUnitType> PurchaseUnitTypeList = await _dbContext.PurchaseUnitType.Where(x => x.IsDeleted == false).ToListAsync();
                        unitName = PurchaseUnitTypeList.Any(x => x.UnitTypeId == request.UnitType) ? PurchaseUnitTypeList.First(x => x.UnitTypeId == request.UnitType).UnitTypeName : "";

                        StoreItemPurchase purchase = await _dbContext.StoreItemPurchases
                                                                     .Include(x=> x.VehicleItemDetail)
                                                                     .Include(x=> x.GeneratorItemDetail)
                                                                     .FirstOrDefaultAsync(x => x.IsDeleted == false && x.PurchaseId == request.PurchaseId);
                       
                        purchase.IsDeleted = false;
                        purchase.ModifiedById = request.ModifiedById;
                        purchase.ModifiedDate = request.ModifiedDate;
                        purchase.SerialNo = request.PurchaseOrderNo.ToString();
                        purchase.Currency= request.Currency;
                        purchase.AssetTypeId = request.AssetTypeId;
                        purchase.DeliveryDate= request.DeliveryDate;
                        purchase.DepreciationRate= request.DepreciationRate;
                        purchase.InventoryItem= request.InventoryItem;
                        purchase.InvoiceDate= request.InvoiceDate;
                        purchase.InvoiceNo= request.InvoiceNo;
                        purchase.JournalCode= request.JournalCode;
                        purchase.OfficeId= request.OfficeId;
                        purchase.ProjectId= request.ProjectId;
                        purchase.PurchaseDate= request.PurchaseDate;
                        purchase.PurchasedById= request.PurchasedById;
                        purchase.PurchaseName= request.PurchaseName;
                        purchase.Quantity= request.Quantity;
                        purchase.ReceiptTypeId= request.ReceiptTypeId;
                        purchase.ReceivedFromLocation= request.ReceivedFromLocation;
                        purchase.Status= request.Status;
                        purchase.UnitCost= request.UnitCost;
                        purchase.UnitType= request.UnitType;
                        purchase.ApplyDepreciation= request.ApplyDepreciation;

                        //Update Purchase
                        _dbContext.StoreItemPurchases.Update(purchase);
                        await _dbContext.SaveChangesAsync();


                        // NOTE: Vehicle and Generator not to be added/updated form store purchase

                        // //Add Purchased vehicle List
                        // if (request.PurchasedVehicleList.Any())
                        // {
                        //      List<PurchasedVehicleDetail> ExistingVehicleList = await _dbContext.PurchasedVehicleDetail
                        //                                                                  .Where(x => x.IsDeleted == false && x.PurchaseId == request.PurchaseId).ToListAsync();

                        //     foreach (var vehicle in request.PurchasedVehicleList)
                        //     {
                        //         if (vehicle.Id == null || vehicle.Id == 0)
                        //         {
                        //             PurchasedVehicleDetail vehicleDetail = new PurchasedVehicleDetail
                        //             {
                        //                 CreatedById = request.CreatedById,
                        //                 CreatedDate = request.CreatedDate,
                        //                 IsDeleted = false,
                        //                 PurchaseId = purchase.PurchaseId,
                        //                 EmployeeId = vehicle.EmployeeId,
                        //                 FuelConsumptionRate = vehicle.FuelConsumptionRate,
                        //                 IncurredMileage = vehicle.IncurredMileage,
                        //                 MobilOilConsumptionRate = vehicle.MobilOilConsumptionRate,
                        //                 ModelYear = vehicle.ModelYear,
                        //                 OfficeId = vehicle.OfficeId,
                        //                 PlateNo = vehicle.PlateNo,
                        //                 StartingMileage = vehicle.StartingMileage,
                        //             };
                        //             await _dbContext.PurchasedVehicleDetail.AddAsync(vehicleDetail);
                        //         }
                        //         else
                        //         {
                        //             PurchasedVehicleDetail vehicleToUpdate = ExistingVehicleList.FirstOrDefault(x => x.Id == vehicle.Id);

                        //             if (vehicleToUpdate != null)
                        //             {
                        //                 vehicleToUpdate.ModifiedById = request.ModifiedById;
                        //                 vehicleToUpdate.ModifiedDate = request.ModifiedDate;
                        //                 vehicleToUpdate.PurchaseId = purchase.PurchaseId;
                        //                 vehicleToUpdate.EmployeeId = vehicle.EmployeeId;
                        //                 vehicleToUpdate.FuelConsumptionRate = vehicle.FuelConsumptionRate;
                        //                 vehicleToUpdate.IncurredMileage = vehicle.IncurredMileage;
                        //                 vehicleToUpdate.MobilOilConsumptionRate = vehicle.MobilOilConsumptionRate;
                        //                 vehicleToUpdate.ModelYear = vehicle.ModelYear;
                        //                 vehicleToUpdate.OfficeId = vehicle.OfficeId;
                        //                 vehicleToUpdate.PlateNo = vehicle.PlateNo;
                        //                 vehicleToUpdate.StartingMileage = vehicle.StartingMileage;
                        //                 _dbContext.PurchasedVehicleDetail.Update(vehicleToUpdate);
                        //             }
                        //         }
                        //     }

                        //     await _dbContext.SaveChangesAsync();
                        //     eventType="Vehicle";
                        //     string logText = $"{purchase.PurchaseName} Purchase Edited";

                        //     // log details
                        //     LogStoreInfo(request.CreatedById, eventType, logText, null, (int)TransportItemCategory.Vehicle);
                        // }

                        // //Add Purchased generator List
                        // if (request.PurchasedGeneratorList.Any())
                        // {
                           
                        //      List<PurchasedGeneratorDetail> ExistingGeneratorList = await _dbContext.PurchasedGeneratorDetail
                        //                                                                 .Where(x => x.IsDeleted == false && x.PurchaseId == request.PurchaseId).ToListAsync();

                        //     foreach (var generator in request.PurchasedGeneratorList)
                        //     {
                        //         if (generator.Id == null || generator.Id == 0)
                        //         {
                        //             PurchasedGeneratorDetail generatorDetail = new PurchasedGeneratorDetail
                        //             {
                        //                 CreatedById = request.ModifiedById,
                        //                 CreatedDate = request.ModifiedDate,
                        //                 IsDeleted = false,
                        //                 PurchaseId = purchase.PurchaseId,
                        //                 FuelConsumptionRate = generator.FuelConsumptionRate,
                        //                 IncurredUsage = generator.IncurredUsage,
                        //                 MobilOilConsumptionRate = generator.MobilOilConsumptionRate,
                        //                 ModelYear = generator.ModelYear,
                        //                 OfficeId = generator.OfficeId,
                        //                 StartingUsage = generator.StartingUsage,
                        //                 Voltage = generator.Voltage,
                        //             };
                        //             await _dbContext.PurchasedGeneratorDetail.AddAsync(generatorDetail);
                        //         }
                        //         else
                        //         {
                        //             PurchasedGeneratorDetail generatorToUpdate = ExistingGeneratorList.FirstOrDefault(x => x.Id == generator.Id);

                        //             if (generatorToUpdate != null)
                        //             {
                        //                 generatorToUpdate.ModifiedById = request.ModifiedById;
                        //                 generatorToUpdate.ModifiedDate = request.ModifiedDate;
                        //                 generatorToUpdate.PurchaseId = purchase.PurchaseId;
                        //                 generatorToUpdate.FuelConsumptionRate = generator.FuelConsumptionRate;
                        //                 generatorToUpdate.IncurredUsage = generator.IncurredUsage;
                        //                 generatorToUpdate.MobilOilConsumptionRate = generator.MobilOilConsumptionRate;
                        //                 generatorToUpdate.ModelYear = generator.ModelYear;
                        //                 generatorToUpdate.OfficeId = generator.OfficeId;
                        //                 generatorToUpdate.Voltage = generator.Voltage;
                        //                 generatorToUpdate.StartingUsage = generator.StartingUsage;
                        //                 _dbContext.PurchasedGeneratorDetail.Update(generatorToUpdate);
                        //             }
                        //         }
                        //     }
                        //     await _dbContext.SaveChangesAsync();
                        //     eventType="Generator";
                        //     string logText = $"{purchase.PurchaseName} Purchase Edited";

                        //     // log details
                        //     LogStoreInfo(request.CreatedById, eventType, logText, null, (int)TransportItemCategory.Generator);
                        // }

                        //Update purchased vehicle Item
                        if ((request.ItemGroupTransportCategory == (int)TransportItemCategory.Vehicle) &&
                            (request.ItemTransportCategory != (int)TransportItemCategory.Vehicle))
                        {

                            purchase.VehicleItemDetail.ModifiedById = request.ModifiedById;
                            purchase.VehicleItemDetail.ModifiedDate = request.ModifiedDate;
                            purchase.VehicleItemDetail.VehiclePurchaseId = request.TransportItemId.Value;
                            await _dbContext.SaveChangesAsync();

                            //get EventType
                            eventType = GetEventTypeName(request.ItemGroupTransportCategory, request.ItemTransportCategory);
                            string logText = $"{purchase.PurchaseName} Purchase Edited";

                            // log details
                            LogStoreInfo(request.CreatedById, eventType, logText, null, (int)TransportItemCategory.Vehicle, request.TransportItemId.Value);
                        }

                        //Update purchased generator Item
                        else if (request.InventoryItem == (int)TransportItem.GeneratorFuel ||
                        request.InventoryItem == (int)TransportItem.GeneratorMaintenanceService ||
                        request.InventoryItem == (int)TransportItem.GeneratorMobilOil ||
                        request.InventoryItem == (int)TransportItem.GeneratorSpareParts)
                        {

                            purchase.GeneratorItemDetail.ModifiedById = request.ModifiedById;
                            purchase.GeneratorItemDetail.ModifiedDate = request.ModifiedDate;
                            purchase.GeneratorItemDetail.GeneratorPurchaseId = request.TransportItemId.Value;
                            await _dbContext.SaveChangesAsync();

                            //get EventType
                            eventType = GetEventTypeName(request.ItemGroupTransportCategory, request.ItemTransportCategory);
                            string logText = $"{purchase.PurchaseName} Purchase Edited";

                            // log details
                            LogStoreInfo(request.CreatedById, eventType, logText, null, (int)TransportItemCategory.Generator, request.TransportItemId.Value);
                        }

                        tran.Commit();
                        success = true;
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

            return success;
        }

        private string GetEventTypeName(int? itemGroupTransportCategory, int? itemTransportCategory)
        {
            string eventTypeName = "";

            if (itemGroupTransportCategory == (int)TransportItemCategory.Vehicle)
            {
                if (itemTransportCategory == (int)TransportItemCategory.Vehicle)
                {
                    eventTypeName = TransportItemCategory.Vehicle.ToString();
                }
                else if (itemTransportCategory == (int)TransportItemCategory.Fuel)
                {
                    eventTypeName = TransportItemCategory.Vehicle.ToString() + " " + TransportItemCategory.Fuel.ToString();
                }
                else if (itemTransportCategory == (int)TransportItemCategory.MaintenanceService)
                {
                    eventTypeName = TransportItemCategory.Vehicle.ToString() + " " + TransportItemCategory.MaintenanceService.ToString();
                }
                else if (itemTransportCategory == (int)TransportItemCategory.MobilOil)
                {
                    eventTypeName = TransportItemCategory.Vehicle.ToString() + " " + TransportItemCategory.MobilOil.ToString();
                }
                else if (itemTransportCategory == (int)TransportItemCategory.SpareParts)
                {
                    eventTypeName = TransportItemCategory.Vehicle.ToString() + " " + TransportItemCategory.SpareParts.ToString();
                }
            }
            else if (itemGroupTransportCategory == (int)TransportItemCategory.Generator)
            {
                if (itemTransportCategory == (int)TransportItemCategory.Generator)
                {
                    eventTypeName = TransportItemCategory.Generator.ToString();
                }
                else if (itemTransportCategory == (int)TransportItemCategory.Fuel)
                {
                    eventTypeName = TransportItemCategory.Generator.ToString() + " " + TransportItemCategory.Fuel.ToString();
                }
                else if (itemTransportCategory == (int)TransportItemCategory.MaintenanceService)
                {
                    eventTypeName = TransportItemCategory.Generator.ToString() + " " + TransportItemCategory.MaintenanceService.ToString();
                }
                else if (itemTransportCategory == (int)TransportItemCategory.MobilOil)
                {
                    eventTypeName = TransportItemCategory.Generator.ToString() + " " + TransportItemCategory.MobilOil.ToString();
                }
                else if (itemTransportCategory == (int)TransportItemCategory.SpareParts)
                {
                    eventTypeName = TransportItemCategory.Generator.ToString() + " " + TransportItemCategory.SpareParts.ToString();
                }
            }

            return eventTypeName;
        }

        private void LogStoreInfo(string createdById, string eventType, string logText, long? purchaseId, int transportType, long entityId)
        {
            //log details
            StoreLogger logger = new StoreLogger
            {
                CreatedDate = DateTime.UtcNow,
                CreatedById = createdById,
                IsDeleted = false,
                EventType = $"{eventType} Purchased",
                LogText = logText,
                PurchaseId = purchaseId,
                TransportType= transportType,
                TransportTypeEntityId= entityId
            };

            _dbContext.StoreLogger.Add(logger);
            _dbContext.SaveChanges();
        }
    }
}