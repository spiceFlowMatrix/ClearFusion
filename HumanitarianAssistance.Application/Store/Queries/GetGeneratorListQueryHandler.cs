using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetGeneratorListQueryHandler: IRequestHandler<GetGeneratorListQuery, GeneratorTrackerListModel>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetGeneratorListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GeneratorTrackerListModel> Handle(GetGeneratorListQuery request, CancellationToken cancellationToken)
        {
            GeneratorTrackerListModel generator = new GeneratorTrackerListModel();

            try
            {

                var purchasedGeneratorsQuery = _dbContext.PurchasedGeneratorDetail
                                                  .Include(x=> x.StoreItemPurchase)
                                                  .Where(x=> x.IsDeleted == false && x.StoreItemPurchase.IsDeleted == false)
                                                  .OrderByDescending(x=> x.Id)
                                                  .Select(x=> new GeneratorTrackerModel 
                                                  {
                                                      GeneratorId = x.Id,
                                                      Voltage= x.Voltage,
                                                      FuelConsumptionRate = x.FuelConsumptionRate,
                                                      StartingUsage= x.StartingUsage,
                                                      IncurredUsage= x.IncurredUsage,
                                                      TotalUsage= x.StartingUsage + x.IncurredUsage,
                                                      MobilOilConsumptionRate= x.MobilOilConsumptionRate,
                                                      ModelYear= x.ModelYear,
                                                      PurchaseId= x.PurchaseId,
                                                      TotalCost = x.StoreItemPurchase.UnitCost,
                                                      OriginalCost = x.StoreItemPurchase.UnitCost,
                                                      OfficeId = x.OfficeId,
                                                    CurrencyId= x.StoreItemPurchase.Currency,
                                                      PurchasedDate= x.StoreItemPurchase.PurchaseDate
                                                  })
                                                  .AsQueryable();

                if(request.Voltage != null)
                {
                    purchasedGeneratorsQuery=  purchasedGeneratorsQuery.Where(x=> x.Voltage == request.Voltage);
                }

                if(request.TotalCost!= null)
                {
                   purchasedGeneratorsQuery= purchasedGeneratorsQuery.Where(x=> x.TotalCost == request.TotalCost); 
                }

                if(request.OfficeId != null)
                {
                   purchasedGeneratorsQuery= purchasedGeneratorsQuery.Where(x=> x.OfficeId == request.OfficeId); 
                }

                if(request.ModelYear != null)
                {
                   purchasedGeneratorsQuery= purchasedGeneratorsQuery.Where(x=> x.ModelYear == request.ModelYear); 
                }

                generator.TotalRecords= await purchasedGeneratorsQuery.CountAsync();

                generator.GeneratorTrackerList = await purchasedGeneratorsQuery.Skip(request.pageSize.Value * request.pageIndex.Value)
                                                    .Take(request.pageSize.Value)
                                                    .ToListAsync();

                // If Display Currency is selected the display cost after exchange rate
                if(request.DisplayCurrency != null)
                {
                     List<ExchangeRateDetail> exchangeRateList= await _dbContext.ExchangeRateDetail
                                                                     .Where(x=> x.IsDeleted== false &&
                                                                      x.ToCurrency == request.DisplayCurrency.Value &&
                                                                     generator.GeneratorTrackerList.Select(y=> y.PurchasedDate.Date).Contains(x.Date.Date)).ToListAsync();

                foreach(var item in generator.GeneratorTrackerList)
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

            return generator;
        }
    }
}