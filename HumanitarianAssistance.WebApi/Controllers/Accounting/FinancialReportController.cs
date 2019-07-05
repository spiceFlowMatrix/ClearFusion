using System.Collections.Generic;
using System.Threading.Tasks;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.Accounting
{
    [Produces("application/json")]
    [Route("api/FinancialReport/[Action]/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FinancialReportController : Controller
    {
        private IAccountBalance _financialReportService;
        private IAccountBalance _accountBalance;

        public FinancialReportController(
            IAccountBalance financialReportService,
            IAccountBalance accountBalance
        )
        {
            _financialReportService = financialReportService;
            _accountBalance = accountBalance;
        }


        /// <summary>
        /// Get All Account Balances (Balance Sheet Report)
        /// </summary>
        /// <remarks>
        /// Sample input:
        ///
        ///     POST /GetAllAccountBalancesByCategory
        ///        {
        ///              "id": 0,
        ///              "asOfDate": "2019-07-02T07:07:00.860Z",
        ///              "upToDate": "2019-07-02T07:07:00.860Z",
        ///              "currency": 0
        ///        }
        ///
        /// Sample output:
        ///
        ///      {
        ///          "StatusCode": 200,
        ///          "Message": "Success",
        ///          "data": {
        ///               NoteAccountBalances: [
        ///                     {
        ///                          AccountBalances: [
        ///                              {
        ///                                  AccountId: 86, 
        ///                                  AccountName: "Assets 110301", 
        ///                                  Balance: 8096.8, 
        ///                                  AccountCode: "110301"
        ///                              }
        ///                          ],
        ///                          NoteHeadId: 1,
        ///                          NoteId: 45,
        ///                          NoteName: "Assets 8"
        ///                     }
        ///               ]
        ///           }
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[ApiExplorerSettings(GroupName = "accounting")]
        public async Task<APIResponse> GetAllAccountBalancesByCategory([FromBody]BalanceRequestModel model)
        {
            APIResponse response = new APIResponse();
            response = await _accountBalance.GetNoteBalancesByHeadType(model.id, model.currency, model.asOfDate);
            return response;
        }


        /// <summary>
        /// Get All Account Income-Expenses (Income Expense Report)
        /// </summary>
        /// <remarks>
        /// Sample input:
        ///
        ///     POST /GetAllAccountIncomeExpensesByCategory
        ///     {
        ///         "id": 0,
        ///         "asOfDate": "2019-07-02T07:12:45.401Z",
        ///         "upToDate": "2019-07-02T07:12:45.401Z",
        ///         "currency": 0
        ///     }
        /// 
        /// Sample output:
        ///
        ///      {
        ///          "StatusCode": 200,
        ///          "Message": "Success",
        ///         "data": {
        ///               NoteAccountBalances: [
        ///                     {
        ///                          AccountBalances: [
        ///                              {
        ///                                  AccountId: 86, 
        ///                                  AccountName: "Assets 110301", 
        ///                                  Balance: 8096.8, 
        ///                                  AccountCode: "110301"
        ///                              }
        ///                          ],
        ///                          NoteHeadId: 1,
        ///                          NoteId: 45,
        ///                          NoteName: "Assets 8"
        ///                     }
        ///               ]
        ///           }
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[ApiExplorerSettings(GroupName = "accounting")]
        public async Task<APIResponse> GetAllAccountIncomeExpensesByCategory([FromBody]BalanceRequestModel model)
        {
            APIResponse response = new APIResponse();
            response = await _accountBalance.GetNoteBalancesByHeadType(model.id, model.currency, model.asOfDate, model.upToDate);
            return response;
        }

        //[HttpGet]
        //public async Task<APIResponse> Get()
        //{
        //    APIResponse response = new APIResponse();
        //    response = await _financialReportService.GetNoteBalancesByHeadType(1, 3, DateTime.Now);
        //    return response;
        //}


        /// <summary>
        /// Get Exchange Gain-Loss Report
        /// </summary>
        /// <remarks>
        /// Sample input:
        ///
        ///     POST /GetExchangeGainLossReport
        ///     {
        ///          "ToCurrencyId": 0,
        ///          "ComparisionDate": "2019-07-02T06:53:23.327Z",
        ///          "ToDate": "2019-07-02T06:53:23.327Z",
        ///          "FromDate": "2019-07-02T06:53:23.327Z",
        ///          "OfficeIdList": [0],
        ///          "JournalIdList": [0],
        ///          "ProjectIdList": [0],
        ///          "AccountIdList": [0]
        ///     }           
        ///
        /// Sample output:
        ///
        ///      {
        ///          "StatusCode": 200,
        ///          "Message": "Success",
        ///          "data": {
        ///               ExchangeGainLossReportList: [
        ///                     {
        ///                         AccountCode : "string",
        ///                         AccountName :  "string",
        ///                         AccountCodeName :  "string",
        ///                         BalanceOnOriginalDate : 1.22221,
        ///                         BalanceOnCurrentDate : 1.22221,
        ///                         GainLossAmount : 1.22221
        ///                     }
        ///               ]
        ///           }
        ///     }
        ///
        /// </remarks>
        /// <param name="exchangeGainLossReport"></param>
        /// <returns></returns>
        [HttpPost]
        //[ApiExplorerSettings(GroupName = "accounting")]
        public async Task<APIResponse> GetExchangeGainLossReport([FromBody]ExchangeGainLossFilterModel exchangeGainLossReport)
        {
            APIResponse response = new APIResponse();
            response = await _financialReportService.GetExchangeGainLossReport(exchangeGainLossReport);
            return response;
        }


        /// <summary>
        /// Save Gain-Loss AccountList
        /// </summary>
        /// <remarks>
        /// Sample input:
        ///
        ///     POST /SaveGainLossAccountList
        ///         [1, 10, 2]
        ///
        ///
        /// </remarks>
        /// <param name="accountIds"></param>
        /// <returns></returns>
        [HttpPost]
        //[ApiExplorerSettings(GroupName = "accounting")]
        public async Task<APIResponse> SaveGainLossAccountList([FromBody] List<long> accountIds)
        {
            APIResponse response = new APIResponse();
            response = await _financialReportService.SaveGainLossAccountList(accountIds);
            return response;
        }


        /// <summary>
        /// Get Detail Of Notes Report
        /// </summary>
        /// <remarks>
        /// Sample input:
        ///
        ///     POST /GetDetailOfNotes
        ///     {
        ///           "CurrencyId": 0,
        ///           "TillDate": "2019-07-02T06:41:52.174Z"
        ///     }
        ///
        /// Sample output:
        ///
        ///      {
        ///          "StatusCode": 200,
        ///          "Message": "Success",
        ///          "data": {
        ///               DetailsOfNotesFinalList: [
        ///                     {
        ///                         Balance: 6040000
        ///                         NoteName: "Expense 2"
        ///                         TotalCredits: 0
        ///                         TotalDebits: 6040000
        ///                     }
        ///               ]
        ///           }
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[ApiExplorerSettings(GroupName = "accounting")]
        public async Task<APIResponse> GetDetailOfNotes([FromBody] DetailOfNotesFilterModel model)
        {
            APIResponse response = new APIResponse();
            response = await _financialReportService.GetDetailOfNotes(model);
            return response;
        }
    }
}
