using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
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
    [Route("api/ChartOfAccount/[Action]/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChartOfAccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private IChartOfAccountNewService _iChartOfAccountNewService;

        public ChartOfAccountController(
            UserManager<AppUser> userManager,
            IChartOfAccountNewService iChartOfAccountNew
        )
        {
            _userManager = userManager;
            _iChartOfAccountNewService = iChartOfAccountNew;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<APIResponse> GetMainLevelAccount([FromBody]long id)
        {
            APIResponse response = await _iChartOfAccountNewService.GetMainLevelAccount(id);
            return response;
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<APIResponse> GetAllAccountsByParentId([FromBody]int id)
        {
            APIResponse response = await _iChartOfAccountNewService.GetAllAccountsByParentId(id);
            return response;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<object> AddChartOfAccount([FromBody]ChartOfAccountNewModel model)
        {
            APIResponse response = new APIResponse();
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (user != null)
            {
                model.CreatedById = user.Id;
                model.CreatedDate = DateTime.UtcNow;

                response = await _iChartOfAccountNewService.AddChartOfAccount(model);
            }
            return response;
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<APIResponse> EditChartOfAccount([FromBody]ChartOfAccountNewModel model)
        {
            APIResponse response = new APIResponse();
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (user != null)
            {
                model.ModifiedById = user.Id;
                model.ModifiedDate = DateTime.UtcNow;

                response = await _iChartOfAccountNewService.EditChartOfAccount(model);
            }
            return response;
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<APIResponse> DeleteChartOfAccount([FromBody]long accountId)
        {
            APIResponse response = new APIResponse();
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (user != null)
            {
                response = await _iChartOfAccountNewService.DeleteChartOfAccount(accountId, user.Id);
            }
            return response;
        }


    }
}
