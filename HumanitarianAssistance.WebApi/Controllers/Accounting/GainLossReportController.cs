using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Controllers.Accounting
{
    [Produces("application/json")]
    [Route("api/GainLossReport/[Action]/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GainLossReportController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private IAccountBalance _iaccountBalanceService;
        private IVoucherNewService _iVoucherNewService;

        public GainLossReportController(
            UserManager<AppUser> userManager,
            IAccountBalance iaccountBalanceService,
            IVoucherNewService iVoucherNewService
        )
        {
            _userManager = userManager;
            _iaccountBalanceService = iaccountBalanceService;
            _iVoucherNewService = iVoucherNewService;
        }

        [HttpGet]
        public async Task<APIResponse> GetExchangeGainLossFilterAccountList()
        {
            APIResponse response = new APIResponse();
            response = await _iaccountBalanceService.GetExchangeGainLossFilterAccountList();
            return response;
        }

        [HttpGet]
        public async Task<APIResponse> GetExchangeGainLossVoucherList()
        {
            APIResponse response = await _iVoucherNewService.GetExchangeGainLossVoucherList();
            return response;
        }

        [HttpPost]
        public async Task<APIResponse> AddExchangeGainLossVoucher([FromBody] ExchangeGainLossVoucherDetails model)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user != null)
            {
                var id = user.Id;
                model.CreatedById = id;
                model.IsDeleted = false;
                model.CreatedDate = DateTime.UtcNow;
            }

            APIResponse response = await _iVoucherNewService.CreateGainLossTransaction(model, user.Id);

            return response;
        }

        [HttpPost]
        public async Task<APIResponse> DeleteGainLossVoucherTransaction([FromBody]long id)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            APIResponse response = await _iVoucherNewService.DeleteGainLossVoucherTransaction(id, user.Id);
            return response;
        }

    }
}
