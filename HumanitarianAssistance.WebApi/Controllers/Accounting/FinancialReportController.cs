using System.Collections.Generic;
using System.Threading.Tasks;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebAPI.Controllers.Accounting
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



        [HttpPost]
        public async Task<APIResponse> GetAllAccountBalancesByCategory([FromBody]BalanceRequestModel model)
        {
            APIResponse response = new APIResponse();
            response = await _accountBalance.GetNoteBalancesByHeadType(model.id, model.currency, model.asOfDate);
            return response;
        }

        [HttpPost]
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

        [HttpPost]
        public async Task<APIResponse> GetExchangeGainLossReport([FromBody]ExchangeGainLossFilterModel exchangeGainLossReport)
        {
            APIResponse response = new APIResponse();
            response = await _financialReportService.GetExchangeGainLossReport(exchangeGainLossReport);
            return response;
        }

        [HttpPost]
        public async Task<APIResponse> SaveGainLossAccountList([FromBody] List<long> accountIds)
        {
            APIResponse response = new APIResponse();
            response = await _financialReportService.SaveGainLossAccountList(accountIds);
            return response;
        }


        [HttpPost]
        public async Task<APIResponse> GetDetailOfNotes([FromBody] DetailOfNotesFilterModel model)
        {
            APIResponse response = new APIResponse();
            response = await _financialReportService.GetDetailOfNotes(model);
            return response;
        }
    }
}
