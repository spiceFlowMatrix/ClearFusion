using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.Accounting
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

        /// <summary>
        /// Get Voucher Summary Report
        /// </summary>
        /// <remarks>
        /// Sample input:
        ///
        ///     POST /GetVoucherSummaryReportList
        ///     {
        ///              "Accounts": [ 0 ],
        ///              "BudgetLines": [ 0 ],
        ///              "Currency": 0,
        ///              "Journals": [ 0 ],
        ///              "Offices": [ 0 ],
        ///              "ProjectJobs": [ 0 ],
        ///              "Projects": [ 0 ],
        ///              "RecordType": 0,
        ///              "PageIndex": 0,
        ///              "PageSize": 10,
        ///              "TotalCount": 0
        ///     }            
        ///
        /// Sample output:
        ///
        ///      {
        ///          "StatusCode": 200,
        ///          "Message": "Success",
        ///          "data": {
        ///               VoucherSummaryList: [
        ///                     {
        ///                         VoucherCode: "A0001-AFG-6-1-19"
        ///                         VoucherDate: "2019-06-07 12:22:07.853753"
        ///                         VoucherDescription: "Pension Payment Done On 6/7/2019"
        ///                         VoucherNo: 253
        ///                     }
        ///               ]
        ///           }
        ///     }
        ///
        /// </remarks>
        /// <param name="voucherSummaryFilter"></param>
        /// <returns></returns>
        [HttpPost]
        //[ApiExplorerSettings(GroupName = "accounting")]
        public async Task<APIResponse> GetVoucherSummaryReportList([FromBody] VoucherSummaryFilterModel voucherSummaryFilter)
        {
            APIResponse response = await _iaccountRecords.GetVoucherSummaryList(voucherSummaryFilter);
            return response;
        }


        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<APIResponse> GetVoucherTransactionList([FromBody] TransactionFilterModel model)
        {
            APIResponse response = await _iaccountRecords.GetVoucherTransactionList(model);
            return response;
        }


        /// <summary>
        /// Get Trial Balance Report
        /// </summary>
        /// <remarks>
        /// Sample input:
        ///
        ///     POST /GetTrialBalanceReport
        ///     {
        ///         "OfficesList": [
        ///           0
        ///         ],
        ///         "OfficeIdList": [
        ///           0
        ///         ],
        ///         "CurrencyId": 0,
        ///         "fromdate": "2017-07-01T13:22:23.039Z",
        ///         "todate": "2019-07-01T13:22:23.039Z",
        ///         "RecordType": 0,
        ///         "accountLists": [
        ///           0
        ///         ],
        ///         "Take": 0,
        ///         "Skip": 0
        ///     }
        ///
        ///
        /// Sample output:
        ///
        ///      {
        ///          "StatusCode": 200,
        ///          "Message": "Success",
        ///          "data": {
        ///               TrialBalanceList: [
        ///                  {
        ///                     AccountName: "Owners 210102"
        ///                     Amount: 0
        ///                     ChartOfAccountNewCode: "210102"
        ///                     ChartOfAccountNewId: 8
        ///                     CreditAmount: 271810
        ///                     CurrencyName: "Afghanistan"
        ///                     DebitAmount: 0
        ///                     Description: "Testing 2"
        ///                     TotalCredit: 0
        ///                     TotalDebit: 0
        ///                     TransactionDate: "2018-12-21T00:00:00"
        ///                     TransactionNo: 0                 
        ///                 }
        ///               ]
        ///           }
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[ApiExplorerSettings(GroupName = "accounting")]
        public async Task<APIResponse> GetTrialBalanceReport([FromBody] LedgerModels model)
        {
            APIResponse response = await _ivoucherDetail.GetTrialBalanceDetailsByCondition(model);
            return response;
        }


        /// <summary>
        /// Get Ledger Report Details
        /// </summary>
        /// <remarks>
        /// Sample input:
        ///
        ///     POST /GetAllLedgerDetails
        ///     {
        ///         "OfficesList": [
        ///           0
        ///         ],
        ///         "OfficeIdList": [
        ///           0
        ///         ],
        ///         "CurrencyId": 0,
        ///         "fromdate": "2019-07-01T13:22:23.039Z",
        ///         "todate": "2019-07-01T13:22:23.039Z",
        ///         "RecordType": 0,
        ///         "accountLists": [
        ///           0
        ///         ],
        ///         "Take": 0,
        ///         "Skip": 0
        ///     }
        ///
        ///
        /// Sample output:
        ///
        ///      {
        ///          "StatusCode": 200,
        ///          "Message": "Success",
        ///          "data": {
        ///               LedgerList: [
        ///                   {
        ///                       AccountName: "Assets 110101"
        ///                       Amount: 0
        ///                       ChartAccountName: "Assets 110101"
        ///                       ChartOfAccountNewCode: "110101"
        ///                       ChartOfAccountNewId: 9
        ///                       CreditAmount: 0
        ///                       CurrencyName: "Afghanistan"
        ///                       DebitAmount: 500
        ///                       Description: "Test"
        ///                       TotalCredit: 0
        ///                       TotalDebit: 0
        ///                       TransactionDate: "2018-12-21T00:00:00"
        ///                       TransactionNo: 0
        ///                       VoucherNo: "5"
        ///                       VoucherReferenceNo: "A0001-5"
        ///                   },
        ///                   {
        ///                       AccountName: "Assets 110101"
        ///                       Amount: 0
        ///                       ChartAccountName: "Assets 110101"
        ///                       ChartOfAccountNewCode: "110101"
        ///                       ChartOfAccountNewId: 9
        ///                       CreditAmount: 0
        ///                       CurrencyName: "Afghanistan"
        ///                       DebitAmount: 500
        ///                       Description: "Test"
        ///                       TotalCredit: 0
        ///                       TotalDebit: 0
        ///                       TransactionDate: "2018-12-21T00:00:00"
        ///                       TransactionNo: 0
        ///                       VoucherNo: "5"
        ///                       VoucherReferenceNo: "A0001-5"
        ///                   }
        ///               ]
        ///           }
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[ApiExplorerSettings(GroupName = "accounting")]
        public async Task<APIResponse> GetAllLedgerDetails([FromBody] LedgerModels model)
        {
            APIResponse response = new APIResponse();
            if (model.OfficeIdList.Any())
            {
                response = await _ivoucherDetail.GetAllLedgerDetailsByCondition(model);
            }
            return response;
        }


        /// <summary>
        /// Get Journal Report Details
        /// </summary>
        /// <remarks>
        /// Sample input:
        ///
        ///     POST /GetJournalVoucherDetailsByCondition
        ///     {
        ///        "CurrencyId": 1,
        ///         "fromdate": "2017-07-01T10:12:16.443Z",
        ///        "todate": "2019-07-01T10:12:16.443Z",
        ///         "OfficesList": [
        ///             3,2,1,12
        ///         ],
        ///         "RecordType": 0,
        ///         "JournalCode": [
        ///            1
        ///         ],
        ///         "AccountLists": [
        ///             9,79,81
        ///         ],
        ///         "Take": 10,
        ///         "Skip": 0
        ///     }
        ///
        /// Sample output:
        ///
        ///      {
        ///            "StatusCode": 200,
        ///            "Message": "Success",
        ///            "data": {
        ///                "JournalVoucherViewList": [
        ///                       {
        ///                           "JournalCode": 1,
        ///                           "VoucherNo": 5,
        ///                           "TransactionDate": "2018-12-21T00:00:00",
        ///                           "AccountCode": "110101",
        ///                           "AccountName": "Assets 110101",
        ///                           "TransactionDescription": "Test",
        ///                           "CurrencyId": 1,
        ///                           "CreditAmount": 0,
        ///                           "DebitAmount": 500,
        ///                           "ReferenceNo": "A0001-5",
        ///                           "ChartOfAccountNewId": 9
        ///                        },
        ///                       {
        ///                           "JournalCode": 1,
        ///                           "VoucherNo": 6,
        ///                           "TransactionDate": "2018-12-21T00:00:00",
        ///                           "AccountCode": "110101",
        ///                           "AccountName": "Assets 110101",
        ///                           "TransactionDescription": "Testing",
        ///                           "CurrencyId": 1,
        ///                           "CreditAmount": 0,
        ///                           "DebitAmount": 500,
        ///                           "ReferenceNo": "A0001-6",
        ///                           "ChartOfAccountNewId": 9
        ///                       }
        ///                 ]
        ///            }
        ///        }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<APIResponse> GetJournalVoucherDetailsByCondition([FromBody] JournalViewModel model)
        {
            APIResponse response = await _ivoucherDetail.GetJouranlVoucherDetailsByCondition(model);
            return response;
        }

    }
}