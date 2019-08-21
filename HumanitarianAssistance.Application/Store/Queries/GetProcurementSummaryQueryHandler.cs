using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetProcurementSummaryQueryHandler : IRequestHandler<GetProcurementSummaryQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetProcurementSummaryQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetProcurementSummaryQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                CurrencyDetails currencyDetails = await _dbContext.CurrencyDetails.FirstOrDefaultAsync(x => x.IsDeleted == false && x.CurrencyId == request.CurrencyId);

                List<StorePurchaseOrder> ProcurmentData = new List<StorePurchaseOrder>();

                ProcurmentData = await _dbContext.StorePurchaseOrders
               .Include(x => x.StoreItemPurchase).ThenInclude(c => c.CurrencyDetails)
               .Include(x => x.EmployeeDetail)
               .Include(x => x.StoreItemPurchase.PurchaseUnitType)
               .Include(x => x.StoreItemPurchase.StoreInventoryItem).ThenInclude(x => x.Inventory)
               .Where(x => x.IssuedToEmployeeId == request.EmployeeId).ToListAsync();

                List<ProcurmentSummaryModel> lst = new List<ProcurmentSummaryModel>();

                if (ProcurmentData != null)
                {
                    foreach (var item in ProcurmentData)
                    {
                        ProcurmentSummaryModel obj = new ProcurmentSummaryModel();
                        obj.ProcurementId = item.OrderId;
                        obj.ProcurementDate = item.IssueDate;
                        obj.EmployeeName = item.EmployeeDetail?.EmployeeName;
                        obj.Store = item.StoreInventoryItem?.Inventory?.AssetType ?? 0;
                        obj.Inventory = item.StoreInventoryItem?.Inventory?.InventoryName ?? null;
                        obj.Item = item.StoreInventoryItem?.ItemName ?? null;
                        obj.TotalCost = (item.IssuedQuantity) * (item.StoreItemPurchase?.UnitCost ?? 0);
                        obj.MustReturn = item.MustReturn == true ? "Yes" : "No";
                        obj.Returned = item.Returned == true ? "Yes" : "No";
                        obj.TotalCostDetails.UnitType = item.StoreItemPurchase?.PurchaseUnitType?.UnitTypeName ?? null;
                        obj.TotalCostDetails.Amount = item.IssuedQuantity; //item.StoreItemPurchase?.Quantity ?? 0;
                        obj.TotalCostDetails.UnitCost = item.StoreItemPurchase?.UnitCost ?? 0;
                        obj.TotalCostDetails.Currency = item.StoreItemPurchase?.CurrencyDetails?.CurrencyName ?? null;
                        obj.VoucherDate = item.StoreItemPurchase.VoucherDate;
                        obj.VoucherNo = item.StoreItemPurchase.VoucherId;
                        obj.CurrencyId = item.StoreItemPurchase.Currency;
                        lst.Add(obj);
                    }

                    if (request.CurrencyId != 0)
                    {
                        //if procurement summary contains all items of currency selected in the drop down
                        bool isProcurementCurrencySame = lst.Any(x => x.CurrencyId == request.CurrencyId);

                        if (!isProcurementCurrencySame)
                        {
                            var dates = lst.Select(y => y.VoucherDate.Date).ToList();

                            List<ExchangeRateDetail> exchangeRateDetail =_dbContext.ExchangeRateDetail.Where(x => x.IsDeleted == false && x.ToCurrency == request.CurrencyId && dates.Contains(x.Date.Date)).ToList();

                            //If Exchange rate is available on voucher date
                            if (exchangeRateDetail.Any())
                            {
                                lst.ForEach(x => x.TotalCostDetails.UnitCost *= exchangeRateDetail.FirstOrDefault(y => y.Date.Date == x.VoucherDate.Date && y.FromCurrency == x.CurrencyId && y.ToCurrency == request.CurrencyId).Rate);
                                lst.ForEach(x => x.TotalCost = x.TotalCostDetails.Amount * x.TotalCostDetails.UnitCost);
                                lst.ForEach(x => x.TotalCostDetails.Currency = currencyDetails.CurrencyName);
                            }
                            else //If Exchange rate is not available on voucher date then take the latest exchange rate updated previously from voucher date
                            {
                                foreach (var obj in lst)
                                {
                                    var exchangeRate = await _dbContext.ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefaultAsync(x => x.IsDeleted == false && x.FromCurrency == obj.CurrencyId && x.ToCurrency == request.CurrencyId && x.Date.Date <= obj.VoucherDate.Date);
                                    if (exchangeRate != null)
                                    {
                                        obj.TotalCostDetails.UnitCost *= exchangeRate.Rate;
                                        obj.TotalCost = obj.TotalCostDetails.Amount * obj.TotalCostDetails.UnitCost;
                                        lst.ForEach(x => x.TotalCostDetails.Currency = currencyDetails.CurrencyName);
                                    }
                                    else
                                    {

                                        throw new Exception("Exchange Rate Not Defined");

                                    }
                                }
                            }
                        }
                    }
                }

                response.data.ProcurmentSummaryModelList = lst;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + " " + ex.Message;
                return response;
            }
            return response;
        }
    }
}
