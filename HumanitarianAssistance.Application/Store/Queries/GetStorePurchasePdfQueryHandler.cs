using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Store.Models;
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

                model.LogoPath = _env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath;

                model.StorePurchaseList = query.Select(x => new StorePurchasePdf
                {
                    PurchaseId = x.PurchaseId,
                    PurchaseDate = x.PurchaseDate.ToShortDateString(),
                    PurchasedQuantity= x.Quantity,
                    ReceivedFromEmployee= x.EmployeeDetail.EmployeeName,
                    ItemCode= x.StoreInventoryItem != null ? x.StoreInventoryItem.ItemCode :"",
                    ItemName = x.StoreInventoryItem != null ? (x.StoreInventoryItem.ItemCode + "-" + x.StoreInventoryItem.ItemName) : "",
                    ProjectId = x.ProjectId,
                    ProjectName = x.ProjectDetail == null ? "" : x.ProjectDetail.ProjectCode + "-" + x.ProjectDetail.ProjectName,
                    OriginalCost = x.UnitCost * x.Quantity,
                    ItemCodeDescription = x.StoreInventoryItem != null ? (x.StoreInventoryItem.ItemCode + "-" + x.StoreInventoryItem.Description): ""  ,
                    Description = x.StoreInventoryItem != null ? x.StoreInventoryItem.Description: ""  ,
                    BudgetLineName= x.ProjectBudgetLineDetail ==null? "" : x.ProjectBudgetLineDetail.BudgetName,
                    CurrencyName= x.CurrencyDetails == null? "":  x.CurrencyDetails.CurrencyName,
                    DepreciationRate= x.DepreciationRate,
                    MasterInventoryCode= x.StoreInventoryItem.MasterInventoryCode,
                    OfficeCode= x.OfficeDetail.OfficeCode,
                    ReceiptDate= x.DeliveryDate != null ? x.DeliveryDate.ToShortDateString() :"",
                    InvoiceDate= x.InvoiceDate != null ? x.InvoiceDate.Value.ToShortDateString(): "",
                    ReceivedFromLocationName= x.StoreSourceCodeDetail != null? (x.StoreSourceCodeDetail.Code+"-"+x.StoreSourceCodeDetail.Description): "" ,
                    Status= x.StatusAtTimeOfIssue != null? x.StatusAtTimeOfIssue.StatusName : "",
                    DepreciatedCost = (x.UnitCost * x.Quantity) - (Math.Ceiling(DateTime.Now.Date.Subtract(x.PurchaseDate).TotalDays) * x.Quantity * x.DepreciationRate * x.UnitCost / 100),
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await _pdfExportService.ExportToPdf(model, "Pages/PdfTemplates/StorePurchaseReport.cshtml");
        }
    }
}