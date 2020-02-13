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
using HumanitarianAssistance.Domain.Entities.Accounting;

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

                        if(purchase.VoucherId.HasValue && purchase.VoucherId !=0)
                        {
                            VoucherDetail voucher = await _dbContext.VoucherDetail.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.VoucherNo == purchase.VoucherId);

                            if(voucher!= null)
                            {
                                voucher.OperationalType = (int)OperationType.Store;
                                _dbContext.VoucherDetail.Update(voucher);
                                await _dbContext.SaveChangesAsync();
                            }
                        }

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
                                    ChasisNo= vehicle.ChasisNo,
                                    EngineNo= vehicle.EngineNo,
                                    ManufacturerCountry= vehicle.ManufacturerCountry,
                                    PersonRemarks= vehicle.Remarks,
                                    RegistrationNo= vehicle.RegistrationNo,
                                };

                                await _dbContext.PurchasedVehicleDetail.AddAsync(vehicleDetail);
                                await _dbContext.SaveChangesAsync();

                                //purchasedVehicleList.Add(vehicleDetail);

                                eventType = "Vehicle";
                                string logText = $"{request.Quantity} {unitName} purchased in ";

                                // log details
                                LogStoreInfo(request.CreatedById, eventType, logText, purchaseId, (int)TransportItemCategory.Vehicle, vehicleDetail.Id);
                            }

                            // await _dbContext.PurchasedVehicleDetail.AddRangeAsync(purchasedVehicleList);
                            // await _dbContext.SaveChangesAsync();
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
                                    ChasisNo= generator.ChasisNo,
                                    EngineNo= generator.EngineNo,
                                    PersonRemarks= generator.Remarks,
                                    RegistrationNo= generator.RegistrationNo,
                                    ManufacturerCountry= generator.ManufacturerCountry,
                                    EmployeeID= generator.EmployeeId
                                };

                                await _dbContext.PurchasedGeneratorDetail.AddRangeAsync(generatorDetail);
                                await _dbContext.SaveChangesAsync();

                                // purchasedGeneratorList.Add(generatorDetail);

                                eventType = "Generator";
                                string logText = $"{request.Quantity} {unitName} purchased in ";

                                // log details
                                LogStoreInfo(request.CreatedById, eventType, logText, purchaseId, (int)TransportItemCategory.Generator, generatorDetail.Id);
                            }



                        }

                        //Add purchased vehicle Item
                        if ((request.ItemGroupTransportCategory == (int)TransportItemCategory.Vehicle) &&
                            (request.ItemTransportCategory != (int)TransportItemCategory.Vehicle))
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
                            eventType = GetEventTypeName(request.ItemGroupTransportCategory, request.ItemTransportCategory);
                            string logText = $"{request.Quantity} {unitName} purchased in ";

                            // log details
                            LogStoreInfo(request.CreatedById, eventType, logText, purchaseId, (int)TransportItemCategory.Vehicle, request.TransportItemId.Value);
                        }
                        //Add purchased generator Item
                        else if ((request.ItemGroupTransportCategory == (int)TransportItemCategory.Generator) &&
                            (request.ItemTransportCategory != (int)TransportItemCategory.Generator))
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
                            eventType = GetEventTypeName(request.ItemGroupTransportCategory, request.ItemTransportCategory);
                            string logText = $"{request.Quantity} {unitName} purchased in ";

                            // log details
                            LogStoreInfo(request.CreatedById, eventType, logText, purchaseId, (int)TransportItemCategory.Generator, request.TransportItemId.Value);
                        }

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

        private void LogStoreInfo(string createdById, string eventType, string logText, long purchaseId, int transportType, long entityId)
        {
            try
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
                    TransportType = transportType,
                    TransportTypeEntityId = entityId
                };

                _dbContext.StoreLogger.Add(logger);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}