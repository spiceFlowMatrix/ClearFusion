using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetStorePurchasePdfQueryHandler : IRequestHandler<GetStorePurchasePdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;
        public GetStorePurchasePdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService, IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
        }

        public async Task<byte[]> Handle(GetStorePurchasePdfQuery request, CancellationToken cancellationToken)
        {

            StorePurchasePdfModel model = new StorePurchasePdfModel();

            try
            {
                var query = _dbContext.StoreItemPurchases
                                   .Include(x => x.ProjectDetail)
                                   .ThenInclude(x => x.ProjectJobDetail)
                                  .Include(x => x.PurchaseOrders)
                                  .ThenInclude(x => x.EmployeeDetail)
                                  .Include(x => x.StoreInventoryItem)
                                  .ThenInclude(x => x.StoreItemGroup)
                                  .Include(x => x.StoreInventoryItem)
                                  .ThenInclude(x => x.Inventory)
                                  .Include(x => x.EmployeeDetail)
                                  .Include(x => x.ProjectDetail)
                                  .Include(x=> x.ProjectBudgetLineDetail)
                                  .Include(x=> x.OfficeDetail)
                                  .Where(x => x.IsDeleted == false &&
                                        x.StoreInventoryItem.Inventory.AssetType == request.InventoryTypeId &&
                                        x.ReceiptTypeId == request.ReceiptTypeId &&
                                        x.OfficeId == request.OfficeId &&
                                        x.Currency == request.CurrencyId);

                if (request.InventoryId != 0)
                {
                    query= query.Where(x => x.StoreInventoryItem.Inventory.InventoryId == request.InventoryId);
                }

                if (request.ProjectId != 0)
                {
                   query= query.Where(x => x.ProjectId == request.ProjectId);
                }

                if (request.JobId != 0)
                {
                   query= query.Where(x => x.ProjectDetail.ProjectJobDetail.Select(z => z.ProjectJobId).Contains(request.JobId));
                }

                if (request.PurchaseStartDate != null)
                {
                   query= query.Where(x => x.PurchaseDate.Date >= request.PurchaseStartDate);
                }

                if (request.PurchaseEndDate != null)
                {
                   query= query.Where(x => x.PurchaseDate.Date <= request.PurchaseEndDate);
                }

                if (request.ItemGroupId != 0)
                {
                   query= query.Where(x => x.StoreInventoryItem.StoreItemGroup.ItemGroupId == request.ItemGroupId);
                }

                if (request.ItemId != 0)
                {
                   query= query.Where(x => x.StoreInventoryItem.ItemId == request.ItemId);
                }

                // looping selected columns and 
                foreach (var item in request.SelectedColumns)
                {
                    switch (item)
                    {
                        case nameof(model.StorePurchasePdfFlags.Id):
                            model.StorePurchasePdfFlags.Id = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.Item):
                            model.StorePurchasePdfFlags.Item = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.PurchasedBy):
                            model.StorePurchasePdfFlags.PurchasedBy = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.OriginalCost):
                            model.StorePurchasePdfFlags.OriginalCost = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.DepreciatedCost):
                            model.StorePurchasePdfFlags.DepreciatedCost = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.PurchaseDate):
                            model.StorePurchasePdfFlags.PurchaseDate = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.Currency):
                            model.StorePurchasePdfFlags.Currency = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.PurchasedQuantity):
                            model.StorePurchasePdfFlags.PurchasedQuantity = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.ItemCode):
                            model.StorePurchasePdfFlags.ItemCode = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.ProjectId):
                            model.StorePurchasePdfFlags.ProjectId = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.ItemCodeDescription):
                            model.StorePurchasePdfFlags.ItemCodeDescription = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.Description):
                            model.StorePurchasePdfFlags.Description = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.BudgetLineName):
                            model.StorePurchasePdfFlags.BudgetLineName = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.DepreciationRate):
                            model.StorePurchasePdfFlags.DepreciationRate = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.MasterInventoryCode):
                            model.StorePurchasePdfFlags.MasterInventoryCode = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.OfficeCode):
                            model.StorePurchasePdfFlags.OfficeCode = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.ReceiptDate):
                            model.StorePurchasePdfFlags.ReceiptDate = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.CurrencyName):
                            model.StorePurchasePdfFlags.CurrencyName = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.InvoiceDate):
                            model.StorePurchasePdfFlags.InvoiceDate = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.ReceivedFromLocationName):
                            model.StorePurchasePdfFlags.ReceivedFromLocationName = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.Status):
                            model.StorePurchasePdfFlags.Status = true;
                            break;
                        case nameof(model.StorePurchasePdfFlags.Project):
                            model.StorePurchasePdfFlags.Project = true;
                            break;
                    }
                }

                model.LogoPath = _env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath;

                model.StorePurchaseList = query.Select(x => new StorePurchasePdf
                {
                    PurchaseId = x.PurchaseId,
                    PurchaseDateDisplay = x.PurchaseDate.ToShortDateString(),
                    PurchaseDate= x.PurchaseDate,
                    PurchasedQuantity= x.Quantity,
                    ReceivedFromEmployee= x.EmployeeDetail.EmployeeName,
                    ItemCode= x.StoreInventoryItem != null ? x.StoreInventoryItem.ItemCode :"",
                    ItemName = x.StoreInventoryItem != null ? (x.StoreInventoryItem.ItemCode + "-" + x.StoreInventoryItem.ItemName) : "",
                    ProjectId = x.ProjectId,
                    ProjectName = x.ProjectDetail == null ? "" : x.ProjectDetail.ProjectCode + "-" + x.ProjectDetail.ProjectName,
                    OriginalCost = x.UnitCost * x.Quantity,
                    ItemCodeDescription = x.StoreInventoryItem != null ? (x.StoreInventoryItem.ItemCode + "-" + (x.StoreInventoryItem.Description == null? "": x.StoreInventoryItem.Description)): ""  ,
                    Description = x.StoreInventoryItem != null ? x.StoreInventoryItem.Description: ""  ,
                    BudgetLineName= x.ProjectBudgetLineDetail ==null? "" : x.ProjectBudgetLineDetail.BudgetName,
                    CurrencyName= x.CurrencyDetails == null? "":  x.CurrencyDetails.CurrencyName,
                    DepreciationRate= x.DepreciationRate,
                    MasterInventoryCode= x.StoreInventoryItem.Inventory.InventoryCode,
                    OfficeCode= x.OfficeDetail.OfficeCode,
                    ApplyDepreciation= x.ApplyDepreciation,
                    CurrencyId = x.Currency,
                    ReceiptDate= x.DeliveryDate != null ? x.DeliveryDate.ToShortDateString() :"",
                    InvoiceDate= x.InvoiceDate != null ? x.InvoiceDate.Value.ToShortDateString(): "",
                    ReceivedFromLocationName= x.StoreSourceCodeDetail != null? (x.StoreSourceCodeDetail.Code+"-"+x.StoreSourceCodeDetail.Description): "" ,
                    Status= x.StatusAtTimeOfIssue != null? x.StatusAtTimeOfIssue.StatusName : "",
                    DepreciatedCost = x.UnitCost * x.Quantity,
                    UnitCost= x.UnitCost
                }).ToList();

                // Calculate Depreciation Cost when Depreciation Comparision Date is not null
                if(model.StorePurchaseList.Any())
                {
                    foreach(var item in model.StorePurchaseList)
                    {
                        if(item.ApplyDepreciation && request.DepreciationComparisionDate != null)
                        {
                            // If comparision date is greater than the date item is purchased on
                            if(request.DepreciationComparisionDate.Value.Date > item.PurchaseDate.Date)
                            {
                                item.DepreciatedCost = item.DepreciatedCost - (Math.Ceiling(request.DepreciationComparisionDate.Value.Date.Subtract(item.PurchaseDate.Date).TotalDays) * item.PurchasedQuantity * item.DepreciationRate * item.UnitCost / 100);
                            }
                        }
                    }
                }

                // If Display Currency is selected the display cost after exchange rate
                if(request.DisplayCurrency != null)
                {
                     List<ExchangeRateDetail> exchangeRateList= await _dbContext.ExchangeRateDetail
                                                                     .Where(x=> x.IsDeleted== false &&
                                                                      x.ToCurrency == request.DisplayCurrency.Value &&
                                                                     model.StorePurchaseList.Select(y=> y.PurchaseDate.Date).Contains(x.Date.Date)).ToListAsync();

                foreach(var item in model.StorePurchaseList)
                {
                    ExchangeRateDetail exchangeRate= exchangeRateList.FirstOrDefault(x=> x.Date.Date == item.PurchaseDate.Date &&
                                                                     x.FromCurrency== item.CurrencyId && x.ToCurrency == request.DisplayCurrency.Value);

                    if(exchangeRate== null)
                    {
                        throw new Exception(string.Format(StaticResource.ExchangeRateNotPresent, item.PurchaseDate.Date.ToShortDateString()));
                    }

                    item.OriginalCost = Math.Round(item.OriginalCost * (double)exchangeRate.Rate, 4) ;
                    item.DepreciatedCost = Math.Round(item.DepreciatedCost * (double)exchangeRate.Rate, 4) ;
                
                }
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await _pdfExportService.ExportToPdf(model, "Pages/PdfTemplates/StorePurchaseReport.cshtml");
        }
    }
}