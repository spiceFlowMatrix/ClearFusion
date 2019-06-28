using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using HumanitarianAssistance.ViewModels.Models;
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
    [Route("api/[Controller]/[Action]/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class VoucherTransactionController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private IVoucherNewService _iVoucherNewService;

        public VoucherTransactionController(
            UserManager<AppUser> userManager,
            IVoucherNewService iVoucherNewService
        )
        {
            _userManager = userManager;
            _iVoucherNewService = iVoucherNewService;
        }

        #region "Voucher"

        /// <summary>
        /// Get All Voucher List
        /// </summary>
        /// <param name="voucherNewFilterModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<APIResponse> GetAllVoucherList([FromBody]VoucherNewFilterModel voucherNewFilterModel)
        {
            APIResponse response = await _iVoucherNewService.GetAllNewVoucherList(voucherNewFilterModel);
            return response;
        }

        /// <summary>
        /// Get Voucher Detail By VoucherNo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<APIResponse> GetVoucherDetailByVoucherNo([FromBody]long id)
        {
            APIResponse response = await _iVoucherNewService.GetVoucherDetailByVoucherNo(id);
            return response;
        }

        /// <summary>
        /// Add Voucher Detail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<APIResponse> AddVoucherDetail([FromBody] VoucherDetailModel model)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user != null)
            {
                var id = user.Id;
                model.CreatedById = id;
                model.IsDeleted = false;
                model.CreatedDate = DateTime.UtcNow;
            }
            APIResponse response = await _iVoucherNewService.AddVoucherNewDetail(model);
            return response;
        }

        /// <summary>
        /// Edit Voucher Detail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<APIResponse> EditVoucherDetail([FromBody] VoucherDetailModel model)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (user != null)
            {
                var id = user.Id;
                model.CreatedById = id;
                model.IsDeleted = false;
                model.CreatedDate = DateTime.UtcNow;
            }
            APIResponse response = await _iVoucherNewService.EditVoucherNewDetail(model);
            return response;
        }

        /// <summary>
        /// Verify Voucher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<APIResponse> VerifyVoucher([FromBody]long id)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            APIResponse response = await _iVoucherNewService.VerifyVoucher(id, user.Id);
            return response;
        }

        #endregion

        #region "Transaction"

        /// <summary>
        /// Get All Voucher Transactions List
        /// </summary>
        /// <param name="id"></param>
        /// <returns>voucher transaction list</returns>
        [HttpPost]
        public async Task<APIResponse> GetAllTransactionsByVoucherId([FromBody]long id)
        {
            APIResponse response = await _iVoucherNewService.GetAllTransactionsByVoucherId(id);
            return response;
        }

        /// <summary>
        /// Add/Edit Voucher transaction list
        /// </summary>
        /// <param name="voucherTransactions"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<APIResponse> AddEditTransactionList([FromBody]AddEditTransactionModel voucherTransactions)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            APIResponse response = _iVoucherNewService.AddEditTransactionList(voucherTransactions, user.Id);
            return response;
        }


        #endregion

    }
}
