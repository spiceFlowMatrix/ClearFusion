using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using HumanitarianAssistance.ViewModels.Models;

namespace HumanitarianAssistance.WebAPI.Controllers.Accounting
{
    [Produces("application/json")]
    [Route("api/AccountReports/[Action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountReportsController : Controller
    {
        private IAccountRecords _iaccountRecords;
        private IVoucherDetail _ivoucherDetail;

        public AccountReportsController(
            IAccountRecords iaccountRecords,
            IVoucherDetail ivoucherDetail
        )
        {
            _iaccountRecords = iaccountRecords;
            _ivoucherDetail = ivoucherDetail;
        }

        [HttpGet]
        public async Task<APIResponse> GetBalanceSheetReport()
        {
            APIResponse response = await _iaccountRecords.GetBalanceSheet();
            return response;
        }

        // Voucher Summary Report
        [HttpPost]
        public async Task<APIResponse> GetVoucherSummaryReportList([FromBody] VoucherSummaryFilterModel voucherSummaryFilter)
        {
            APIResponse response = await _iaccountRecords.GetVoucherSummaryList(voucherSummaryFilter);
            return response;
        }

        [HttpPost]
        public async Task<APIResponse> GetVoucherTransactionList([FromBody] TransactionFilterModel model)
        {
            APIResponse response = await _iaccountRecords.GetVoucherTransactionList(model);
            return response;
        }

        // Trial Balance Report
        [HttpPost]
        public async Task<APIResponse> GetTrailBlanceDetailsByCondition([FromBody] LedgerModels model)
        {
            APIResponse response = await _ivoucherDetail.GetTrailBlanceDetailsByCondition(model);
            return response;
        }

        // Ledger Report
        [HttpPost]
        public async Task<APIResponse> GetAllLedgerDetails([FromBody] LedgerModels model)
        {
            APIResponse response = new APIResponse();
            if (model.OfficeIdList.Any())
            {
                response = await _ivoucherDetail.GetAllLedgerDetailsByCondition(model);
            }
            return response;
        }

        // Journal Report
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<APIResponse> GetJournalVoucherDetailsByCondition([FromBody] JournalViewModel model)
        {
            APIResponse response = await _ivoucherDetail.GetJouranlVoucherDetailsByCondition(model);
            return response;
        }

    }
}
