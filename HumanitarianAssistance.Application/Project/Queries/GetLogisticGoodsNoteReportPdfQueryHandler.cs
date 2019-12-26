using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;


namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetLogisticGoodsNoteReportPdfQueryHandler : IRequestHandler<GetLogisticGoodsNoteReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;
        public GetLogisticGoodsNoteReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService, IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
        }
        public async Task<byte[]> Handle(GetLogisticGoodsNoteReportPdfQuery request, CancellationToken cancellationToken)
        {
            LogisticGoodsNoteReportModel model = new LogisticGoodsNoteReportModel();

            try
            {
                var query = _dbContext.ProjectLogisticItems
                                                        .Include(x => x.StoreInventoryItem)
                                                        .Include(x => x.ProjectLogisticRequests)
                                                        .ThenInclude(x => x.ProjectDetail)
                                                        .Include(x => x.ProjectLogisticRequests)
                                                        .ThenInclude(x => x.ProjectBudgetLineDetail)
                                                        .ThenInclude(x => x.ProjectJobDetail)
                                                        .Where(x => x.IsDeleted == false && x.LogisticRequestsId == request.LogisticRequestId)
                                                        .AsQueryable();

                model = query.Select(x => new LogisticGoodsNoteReportModel
                {
                    Number = x.ProjectLogisticRequests.LogisticRequestsId,
                    ProjectCode = x.ProjectLogisticRequests.ProjectDetail.ProjectCode,
                    JobCode = x.ProjectLogisticRequests.ProjectBudgetLineDetail.ProjectJobDetail.ProjectJobCode,
                    PurchaseDate = x.ProjectLogisticRequests.PurchaseDate != null ? x.ProjectLogisticRequests.PurchaseDate.Value.ToShortDateString() : "",
                    BudgetLine = x.ProjectLogisticRequests.ProjectBudgetLineDetail.BudgetCode,
                    HeaderLogo = _env.WebRootFileProvider.GetFileInfo("ReportLogo/headertop.png").PhysicalPath,
                    ChaDesignLogo = _env.WebRootFileProvider.GetFileInfo("ReportLogo/humanitarianLogo.png").PhysicalPath,
                    ApplySheetText = _env.WebRootFileProvider.GetFileInfo("ReportLogo/goodsText.png").PhysicalPath,
                    InstructionText = _env.WebRootFileProvider.GetFileInfo("ReportLogo/guidelineText.png").PhysicalPath,
                    OfficeName = x.ProjectLogisticRequests.OfficeDetail.OfficeName
                }).FirstOrDefault();


                model.GoodsNoteAmountModel = query.Select(x => new GoodsNoteAmountModel
                {
                    ItemDescription = x.StoreInventoryItem.ItemName,
                    Quantity = x.Quantity,
                    UnitPrice = x.EstimatedUnitCost,
                    ToBePurchased = x.Quantity,
                    EstimatedAmount = (x.Quantity * x.EstimatedUnitCost),


                }).ToList();

                model.TotalQuantity = model.GoodsNoteAmountModel.Sum(x => x.Quantity);
                model.TotalUnitPrice = model.GoodsNoteAmountModel.Sum(x => x.UnitPrice);
                model.TotalEstimatedAmount = model.GoodsNoteAmountModel.Sum(x => x.EstimatedAmount);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return await _pdfExportService.ExportToPdf(model, "Pages/PdfTemplates/LogisticGoodsNoteReport.cshtml");

        }
    }
}