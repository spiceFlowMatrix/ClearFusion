using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Controllers.Accounting
{
    [Produces("application/json")]
    [Route("api/ExchangeRates/[Action]/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExchangeRatesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private IExchangeRate _iExchangeRate;

        public ExchangeRatesController(
            UserManager<AppUser> userManager,
            IExchangeRate iExchangeRate
        )
        {
            _userManager = userManager;
            _iExchangeRate = iExchangeRate;
        }

        [HttpPost]
        public async Task<APIResponse> GetSavedExchangeRates([FromBody] ExchangeRateVerificationFilter filter)
        {
            APIResponse response = await _iExchangeRate.GetSavedExchangeRates(filter);
            return response;
        }

        [HttpPost]
        public async Task<APIResponse> SaveSystemGeneratedExchangeRates([FromBody] List<GenerateExchangeRateViewModel> exchangeRateList)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            APIResponse response = await _iExchangeRate.GenerateExchangeRates(exchangeRateList, user.Id);
            return response;
        }

        [HttpPost]
        public async Task<APIResponse> GetExchangeRatesDetail([FromBody] ExchangeRateDetailModel exchangeRateDetailModel)
        {
            APIResponse response = await _iExchangeRate.GetExchangeRatesDetail(exchangeRateDetailModel);
            return response;
        }

        [HttpPost]
        public async Task<APIResponse> SaveExchangeRatesForAllOffices([FromBody] OfficeExchangeRateViewModel officeExchangeRateViewModel)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            APIResponse response = await _iExchangeRate.SaveExchangeRatesForOffice(officeExchangeRateViewModel, user.Id);
            return response;
        }

        [HttpPost]
        public async Task<APIResponse> VerifyExchangeRates([FromBody] DateTime ExchangeRateDate)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            APIResponse response = await _iExchangeRate.VerifyExchangeRates(ExchangeRateDate, user.Id);
            return response;
        }

        [HttpPost]
        public async Task<APIResponse> DeleteExchangeRates([FromBody] DateTime ExchangeRateDate)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            APIResponse response = await _iExchangeRate.DeleteExchangeRates(ExchangeRateDate, user.Id);
            return response;
        }
    }
}
