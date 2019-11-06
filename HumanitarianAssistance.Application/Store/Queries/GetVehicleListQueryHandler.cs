using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetVehicleListQueryHandler : IRequestHandler<GetVehicleListQuery, VehicleTrackerListModel>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetVehicleListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<VehicleTrackerListModel> Handle(GetVehicleListQuery request, CancellationToken cancellationToken)
        {
            VehicleTrackerListModel vehicle = new VehicleTrackerListModel();

            try
            {

                var purchasedVehiclesQuery = _dbContext.PurchasedVehicleDetail
                                                    .Include(x => x.EmployeeDetail)
                                                    .Include(x => x.StoreItemPurchase)
                                                    .Include(x => x.VehicleMileageDetail)
                                                    .Include(x => x.VehicleItemDetail)
                                                    .ThenInclude(x => x.StoreItemPurchase)
                                                    .ThenInclude(x => x.StoreInventoryItem)
                                                    .Where(x=> x.IsDeleted == false && x.StoreItemPurchase.IsDeleted == false)
                                                    .Select(x=> new VehicleTrackerModel 
                                                  {
                                                      VehicleId = x.Id,
                                                      EmployeeName = x.EmployeeDetail.EmployeeName,
                                                      FuelConsumptionRate = x.FuelConsumptionRate,
                                                      TotalMileage= x.IncurredMileage + x.StartingMileage + x.VehicleMileageDetail.Where(z=> z.IsDeleted == false)
                                                                                                                                         .Select(y=> y.Mileage).DefaultIfEmpty(0).Sum(),
                                                      TotalCost = x.StoreItemPurchase.UnitCost +
                                                                  x.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                                  y.VehiclePurchaseId == x.Id && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleFuel)
                                                                               .Select(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost).DefaultIfEmpty(0).Sum() +
                                                                  x.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                                  y.VehiclePurchaseId == x.Id && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil)
                                                                               .Select(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost).DefaultIfEmpty(0).Sum() +
                                                                  x.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                                  y.VehiclePurchaseId == x.Id && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleSpareParts)
                                                                               .Select(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost).DefaultIfEmpty(0).Sum() +
                                                                  x.VehicleItemDetail.Where(y => y.IsDeleted == false &&
                                                                  y.VehiclePurchaseId == x.Id && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMaintenanceService)
                                                                               .Select(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost).DefaultIfEmpty(0).Sum(),
                                                      OriginalCost = x.StoreItemPurchase.UnitCost,
                                                      PlateNo = x.PlateNo,
                                                      EmployeeId = x.EmployeeDetail.EmployeeID,
                                                      OfficeId = x.OfficeId,
                                                      PurchasedDate= x.StoreItemPurchase.PurchaseDate,
                                                      CurrencyId= x.StoreItemPurchase.Currency
                                                  })
                                                  .AsQueryable();

                if(!string.IsNullOrEmpty(request.PlateNo))
                {
                    purchasedVehiclesQuery=  purchasedVehiclesQuery.Where(x=> x.PlateNo == request.PlateNo);
                }

                if(request.TotalCost != null)
                {
                   purchasedVehiclesQuery= purchasedVehiclesQuery.Where(x=> x.TotalCost == request.TotalCost); 
                }

                if(request.EmployeeId != null)
                {
                   purchasedVehiclesQuery= purchasedVehiclesQuery.Where(x=> x.EmployeeId == request.EmployeeId); 
                }

                if(request.OfficeId != null)
                {
                   purchasedVehiclesQuery= purchasedVehiclesQuery.Where(x=> x.OfficeId == request.OfficeId); 
                }

                vehicle.TotalRecords= await purchasedVehiclesQuery.CountAsync();

                vehicle.VehicleList = await purchasedVehiclesQuery.Skip(request.pageSize.Value * request.pageIndex.Value)
                                                    .Take(request.pageSize.Value)
                                                    .ToListAsync();

                // If Display Currency is selected the display cost after exchange rate
                if(request.DisplayCurrency != null)
                {
                     List<ExchangeRateDetail> exchangeRateList= await _dbContext.ExchangeRateDetail
                                                                     .Where(x=> x.IsDeleted== false &&
                                                                      x.ToCurrency == request.DisplayCurrency.Value &&
                                                                     vehicle.VehicleList.Select(y=> y.PurchasedDate.Date).Contains(x.Date.Date)).ToListAsync();

                foreach(var item in vehicle.VehicleList)
                {
                    ExchangeRateDetail exchangeRate= exchangeRateList.FirstOrDefault(x=> x.Date.Date == item.PurchasedDate.Date &&
                                                                     x.FromCurrency== item.CurrencyId && x.ToCurrency == request.DisplayCurrency.Value);

                    if(exchangeRate== null)
                    {
                        throw new Exception(string.Format(StaticResource.ExchangeRateNotPresent, item.PurchasedDate.Date.ToShortDateString()));
                    }

                    item.OriginalCost = Math.Round(item.OriginalCost * (double)exchangeRate.Rate, 4) ;
                    item.TotalCost = Math.Round(item.TotalCost * (double)exchangeRate.Rate, 4) ;
                
                }
            }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return vehicle;
        }
    }
}