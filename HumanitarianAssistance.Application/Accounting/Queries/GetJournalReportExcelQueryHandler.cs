using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetJournalReportExcelQueryHandler : IRequestHandler<GetJournalReportExcelQuery, byte[]>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IAccountingServices _iAccountingServices;
        private readonly IExcelExportService _excelExportService;
        private readonly IMapper _mapper;

        public GetJournalReportExcelQueryHandler(IAccountingServices iAccountingServices, IMapper mapper,
                HumanitarianAssistanceDbContext dbContext, IExcelExportService excelExportService)
        {
            _iAccountingServices = iAccountingServices;
            _mapper = mapper;
            _dbContext= dbContext;
            _excelExportService= excelExportService;
        }

        public async Task<byte[]> Handle(GetJournalReportExcelQuery request, CancellationToken cancellationToken)
        {
            byte[] result;
            try
            {
                JournalReportModel model = _mapper.Map<JournalReportModel>(request);
                ApiResponse response= await _iAccountingServices.GetJournalReport(model);

                List<CurrencyDetails> currencyList = await _dbContext.CurrencyDetails.Where(x=> x.IsDeleted == false).ToListAsync();

               List<ExpandoObject> reportModel= new List<ExpandoObject>();

               foreach(var item in response.data.JournalVoucherViewList)
               {
                   dynamic obj = new ExpandoObject();
                   obj.AccountName = item.AccountName;
                   obj.AccountCode= item.AccountCode;
                   obj.BudgetLineDescription= item.BudgetLineDescription;
                   obj.JobCode = item.JobCode;
                   obj.ReferenceNo = item.ReferenceNo;
                   obj.TransactionDate = item.TransactionDate;
                   obj.TransactionDescription= item.TransactionDescription;
                   obj.Currency = currencyList.FirstOrDefault(y=> y.CurrencyId == item.CurrencyId).CurrencyName;
                   obj.DebitAmount= item.DebitAmount;
                   obj.CreditAmount = item.CreditAmount;
                   obj.Project= item.Project;

                   reportModel.Add(obj);
               }

               string headerString = "Journal Report";

               List<int> calculateSumOnKeyIndex= new List<int>
               {
                   8,
                   9
               };

               result = _excelExportService.ExportToExcel(reportModel, "JournalReport", headerString, true, calculateSumOnKeyIndex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}