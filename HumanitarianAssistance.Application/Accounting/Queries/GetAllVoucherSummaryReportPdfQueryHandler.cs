using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Domain.Entities.Accounting;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllVoucherSummaryReportPdfQueryHandler : IRequestHandler<GetAllVoucherSummaryReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;

        public GetAllVoucherSummaryReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService, IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
        }

        public async Task<byte[]> Handle(GetAllVoucherSummaryReportPdfQuery request, CancellationToken cancellationToken)
        {
            try
            {
                VoucherSummaryMainReportPdfModel mainsummary = new VoucherSummaryMainReportPdfModel();

                mainsummary.Logo = _env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath;

                // var spVoucherSummaryList=await _dbContext.LoadStoredProc("get_all_voucher_transaction_list")
                //                                 .WithSqlParam("voucherid", request.VoucherId)   
                //                                 .ExecuteStoredProc<VoucherSummaryReportNewPdfModel>();
                // var groupedlist=spVoucherSummaryList.GroupBy(x=>new {
                //     x.VoucherNo,
                //     x.ReferenceNo,
                //     x.VoucherDate,
                //     x.ChequeNo,
                //     x.VoucherDescription,
                //     x.CurrencyCode,
                //     x.JournalName,
                //     x.OfficeName
                // }).Select(s=>new VoucherSummaryReportPdfModel{
                //     Currency=s.Key.CurrencyCode,
                //     Cheque=s.Key.ChequeNo,
                //     VoucherNo=s.Key.VoucherNo,
                //     ReferenceNo=s.Key.ReferenceNo,
                //     Journal=s.Key.JournalName,
                //     Date=s.Key.VoucherDate.ToString(),
                //     Region=s.Key.OfficeName,
                //     Description=s.Key.VoucherDescription,
                //     TotalCredit=s.Sum(x=>x.Credit).ToString(),
                //     TotalDebit=s.Sum(x=>x.Debit).ToString(),
                //     TransactionDetails=s.Select(ss=>new TransactionSummaryReportPdfModel{
                //         AccountNo=ss.ChartOfAccountNewCode.ToString(),
                //         Description=ss.TransactionDescription,
                //         Debit=ss.Debit.ToString(),
                //         Credit=ss.Credit.ToString(),
                //         BudgetLine=ss.BudgetCode,
                //         Project=ss.ProjectCode,
                //         Job=ss.ProjectJobCode,
                //         Sector=ss.SectorCode
                //         }).ToList()
                //     }).ToList();

                var groupedlist = await _dbContext.VoucherDetail
                                         .Include(x => x.VoucherTransactionDetails)
                                         .ThenInclude(x => x.ChartOfAccountDetail)
                                         .Include(x => x.VoucherTransactionDetails)
                                         .ThenInclude(x => x.ProjectBudgetLineDetail)
                                         .Include(x => x.VoucherTransactionDetails)
                                         .ThenInclude(x => x.ProjectDetail)
                                         .ThenInclude(x => x.ProjectSector)
                                         .ThenInclude(x => x.SectorDetails)
                                         .Include(x => x.VoucherTransactionDetails)
                                         .ThenInclude(x => x.ProjectJobDetail)
                                         .Include(x => x.OfficeDetails)
                                         .Include(x => x.CurrencyDetail)
                                         .Include(x => x.JournalDetails)
                                         .Where(x => x.IsDeleted == false && request.VoucherIdList.Contains(x.VoucherNo))
                                         .Select(x => new VoucherSummaryReportPdfModel
                                         {
                                             Currency = x.CurrencyDetail.CurrencyCode,
                                             Cheque = x.ChequeNo,
                                             VoucherNo = x.VoucherNo,
                                             ReferenceNo = x.ReferenceNo,
                                             Journal = x.JournalDetails.JournalName,
                                             Date = x.VoucherDate.ToShortDateString(),
                                             Region = x.OfficeDetails.OfficeName,
                                             Description = x.Description,
                                             TotalCredit = Math.Round((double)x.VoucherTransactionDetails.Where(y=> y.IsDeleted == false).Select(y => y.Credit).DefaultIfEmpty(0).Sum(), 2).ToString(),
                                             TotalDebit = Math.Round((double)x.VoucherTransactionDetails.Where(y=> y.IsDeleted == false).Select(y => y.Debit).DefaultIfEmpty(0).Sum(), 2).ToString(),
                                             TransactionDetails = x.VoucherTransactionDetails.Where(y=> y.IsDeleted == false).Select(z => new TransactionSummaryReportPdfModel
                                             {
                                                 AccountNo = z.ChartOfAccountDetail.ChartOfAccountNewCode,
                                                 Description = z.Description,
                                                 Debit = z.Debit.ToString(),
                                                 Credit = z.Credit.ToString(),
                                                 BudgetLine = z.ProjectBudgetLineDetail == null ? "" : z.ProjectBudgetLineDetail.BudgetCode,
                                                 Project = z.ProjectDetail == null ? "" : z.ProjectDetail.ProjectCode,
                                                 Job = z.ProjectJobDetail == null ? "" : z.ProjectJobDetail.ProjectJobCode,
                                                 Sector = (z.ProjectDetail != null && z.ProjectDetail.ProjectSector != null &&
                                                             z.ProjectDetail.ProjectSector.SectorDetails != null) ?
                                                                             z.ProjectDetail.ProjectSector.SectorDetails.SectorCode : ""
                                             }).ToList()
                                         }).ToListAsync();


                mainsummary.VoucherDetails = groupedlist;
                return await _pdfExportService.ExportToPdf(mainsummary, "Pages/PdfTemplates/VoucherSummaryReport.cshtml");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}