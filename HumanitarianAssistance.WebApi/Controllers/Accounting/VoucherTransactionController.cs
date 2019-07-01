using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.Accounting {
    [Produces ("application/json")]
    [Route ("api/VoucherTransaction/[Action]/")]
    [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class VoucherTransactionController : Controller {
        private readonly UserManager<AppUser> _userManager;
        private IVoucherNewService _iVoucherNewService;

        public VoucherTransactionController (
            UserManager<AppUser> userManager,
            IVoucherNewService iVoucherNewService
        ) {
            _userManager = userManager;
            _iVoucherNewService = iVoucherNewService;
        }

        #region "Voucher"

        /// <summary>
        /// Get All Voucher List
        /// </summary>
        /// <remarks>
        /// Sample input:
        /// 
        ///     POST /GetAllVoucherList
        ///     {
        ///         "FilterValue": "",
        ///         "VoucherNoFlag": true,
        ///         "ReferenceNoFlag": true,
        ///         "DescriptionFlag": true,
        ///         "JournalNameFlag": true,
        ///         "DateFlag": true,
        ///         "pageIndex": 0,
        ///         "pageSize": 10,
        ///         "totalCount": 0
        ///     }
        /// 
        /// 
        /// Sample output:
        ///
        ///     {
        ///         "StatusCode": 200,
        ///         "Message": "Success",
        ///         "data": {
        ///             "VoucherDetailList": [
        ///                 {
        ///                     "VoucherNo": 256,
        ///                     "CurrencyId": 1,
        ///                     "CurrencyCode": "AFG",
        ///                     "VoucherDate": "2019-06-27T11:43:30.311353",
        ///                     "ReferenceNo": "A0001-AFG-6-4-19",
        ///                     "Description": "Salary Payment: E13-Employee-5-500",
        ///                     "JournalCode": 1,
        ///                     "JournalName": "Salary Journal",
        ///                     "VoucherTypeId": 1,
        ///                     "OfficeId": 1,
        ///                     "OfficeName": "Afghanistan",
        ///                     "IsExchangeGainLossVoucher": false
        ///                 },
        ///                 {
        ///                     "VoucherNo": 255,
        ///                     "CurrencyId": 1,
        ///                     "CurrencyCode": "AFG",
        ///                     "VoucherDate": "2019-06-17T11:38:19",
        ///                     "ChequeNo": "1212121",
        ///                     "ReferenceNo": "A0001-AFG-6-3-19",
        ///                     "Description": "test",
        ///                     "JournalCode": 1,
        ///                     "JournalName": "Salary Journal",
        ///                     "VoucherTypeId": 1,
        ///                     "OfficeId": 1,
        ///                     "OfficeName": "Afghanistan",
        ///                     "IsExchangeGainLossVoucher": false
        ///                 }
        ///             ]
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <param name="voucherNewFilterModel"></param>
        /// <response code="200">Get all voucher list successsfull</response>
        /// <returns></returns>
        [HttpPost]
        public async Task<APIResponse> GetAllVoucherList ([FromBody] VoucherNewFilterModel voucherNewFilterModel) {
            APIResponse response = await _iVoucherNewService.GetAllNewVoucherList (voucherNewFilterModel);
            return response;
        }

        /// <summary>
        /// Get Voucher Detail By VoucherNo
        /// </summary>
        /// <remarks>
        /// Sample input:
        /// 
        ///     POST /GetVoucherDetailByVoucherNo
        ///     256
        ///
        /// Sample output:
        ///
        ///     {
        ///         "StatusCode": 200,
        ///         "Message": "Success",
        ///         "data": {
        ///             "VoucherDetail": {
        ///                 "VoucherNo": 256,
        ///                 "CurrencyId": 1,
        ///                 "CurrencyCode": "AFG",
        ///                 "VoucherDate": "2019-06-27T11:43:30.311353",
        ///                 "ReferenceNo": "A0001-AFG-6-4-19",
        ///                 "Description": "Salary Payment: E13-Employee-5-500",
        ///                 "JournalCode": 1,
        ///                 "JournalName": "Salary Journal",
        ///                 "VoucherTypeId": 1,
        ///                 "OfficeId": 1,
        ///                 "OfficeName": "Afghanistan",
        ///                 "FinancialYearId": 2,
        ///                 "FinancialYearName": "FY-2019",
        ///                 "IsVoucherVerified": false,
        ///                 "IsExchangeGainLossVoucher": false
        ///             }
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<APIResponse> GetVoucherDetailByVoucherNo ([FromBody] long id) {
            APIResponse response = await _iVoucherNewService.GetVoucherDetailByVoucherNo (id);
            return response;
        }

        /// <summary>
        /// Add Voucher Detail
        /// </summary>
        /// <remarks>
        /// Sample input:
        /// 
        ///     POST /AddVoucherDetail
        ///     {
        ///         "VoucherNo": 0,
        ///         "CurrencyId": 0,
        ///         "CurrencyCode": "string",
        ///         "VoucherDate": "2019-06-25T11:57:42.819Z",
        ///         "ChequeNo": "string",
        ///         "ReferenceNo": "string",
        ///         "Description": "string",
        ///         "JournalCode": 0,
        ///         "JournalName": "string",
        ///         "VoucherTypeId": 0,
        ///         "OfficeId": 0,
        ///         "OfficeName": "string",
        ///         "ProjectId": 0,
        ///         "BudgetLineId": 0,
        ///         "FinancialYearId": 0,
        ///         "FinancialYearName": "string",
        ///         "IsVoucherVerified": true,
        ///         "TimezoneOffset": 0,
        ///         "IsExchangeGainLossVoucher": true,
        ///         "CreatedDate": "2019-06-25T11:57:42.819Z",
        ///         "ModifiedDate": "2019-06-25T11:57:42.819Z",
        ///         "CreatedById": "string",
        ///         "ModifiedById": "string",
        ///         "IsDeleted": false
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<APIResponse> AddVoucherDetail ([FromBody] VoucherDetailModel model) {
            var user = await _userManager.FindByNameAsync (HttpContext.User.FindFirst (ClaimTypes.NameIdentifier).Value);

            if (user != null) {
                var id = user.Id;
                model.CreatedById = id;
                model.IsDeleted = false;
                model.CreatedDate = DateTime.UtcNow;
            }
            APIResponse response = await _iVoucherNewService.AddVoucherNewDetail (model);
            return response;
        }

        /// <summary>
        /// Edit Voucher Detail
        /// </summary>
        /// <remarks>
        /// Sample input:
        ///
        ///     POST /EditVoucherDetail
        ///     {
        ///           "VoucherNo": 0,
        ///           "CurrencyId": 0,
        ///           "CurrencyCode": "string",
        ///           "VoucherDate": "2019-07-01T07:23:18.037Z",
        ///           "ChequeNo": "string",
        ///           "ReferenceNo": "string",
        ///           "Description": "string",
        ///           "JournalCode": 0,
        ///           "JournalName": "string",
        ///           "VoucherTypeId": 0,
        ///           "OfficeId": 0,
        ///           "OfficeName": "string",
        ///           "ProjectId": 0,
        ///           "BudgetLineId": 0,
        ///           "FinancialYearId": 0,
        ///           "FinancialYearName": "string",
        ///           "IsVoucherVerified": true,
        ///          "TimezoneOffset": 0,
        ///          "IsExchangeGainLossVoucher": true,
        ///           "CreatedDate": "2019-07-01T07:23:18.037Z",
        ///           "ModifiedDate": "2019-07-01T07:23:18.037Z",
        ///           "CreatedById": "string",
        ///           "ModifiedById": "string",
        ///           "IsDeleted": false
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<APIResponse> EditVoucherDetail ([FromBody] VoucherDetailModel model) {
            var user = await _userManager.FindByNameAsync (HttpContext.User.FindFirst (ClaimTypes.NameIdentifier).Value);

            if (user != null) {
                var id = user.Id;
                model.CreatedById = id;
                model.IsDeleted = false;
                model.CreatedDate = DateTime.UtcNow;
            }
            APIResponse response = await _iVoucherNewService.EditVoucherNewDetail (model);
            return response;
        }

        /// <summary>
        /// Verify Voucher
        /// </summary>
        /// <remarks>
        /// Sample input:
        ///
        ///     POST /VerifyVoucher
        ///     256
        ///
        /// Sample output:
        ///
        ///    {
        ///       "StatusCode": 200,
        ///       "Message": "Voucher is Unverified",
        ///        "data": {
        ///           "IsVoucherVerified": false
        ///      }
        ///   }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<APIResponse> VerifyVoucher ([FromBody] long id) {
            var user = await _userManager.FindByNameAsync (HttpContext.User.FindFirst (ClaimTypes.NameIdentifier).Value);
            APIResponse response = await _iVoucherNewService.VerifyVoucher (id, user.Id);
            return response;
        }

        #endregion

        #region "Transaction"

        /// <summary>
        /// Get All Voucher Transactions List
        /// </summary>
        /// <remarks>
        /// Sample output:
        ///
        ///     POST /GetAllTransactionsByVoucherId
        ///     256
        ///
        /// Sample output:
        ///
        ///      {
        ///            "StatusCode": 200,
        ///            "Message": "Success",
        ///            "data": {
        ///                "VoucherTransactions": [
        ///                    {
        ///                        "TransactionId": 1436,
        ///                        "AccountNo": 79,
        ///                        "Description": "Capacity Building Deduction has been credited towards Net Salary",
        ///                        "Credit": 40,
        ///                        "Debit": 0,
        ///                        "VoucherNo": 256,
        ///                         "BudgetLineList": []
        ///                     },
        ///                    {
        ///                        "TransactionId": 1437,
        ///                        "AccountNo": 79,
        ///                         "Description": "Fine Deduction has been credited towards Net Salary",
        ///                        "Credit": 10,
        ///                        "Debit": 0,
        ///                        "VoucherNo": 256,
        ///                        "BudgetLineList": []
        ///                    }
        ///                ]
        ///            }
        ///        }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>voucher transaction list</returns>
        [HttpPost]
        public async Task<APIResponse> GetAllTransactionsByVoucherId ([FromBody] long id) {
            APIResponse response = await _iVoucherNewService.GetAllTransactionsByVoucherId (id);
            return response;
        }

        /// <summary>
        /// Add/Edit/Delete Voucher transaction list
        /// </summary>
        /// <remarks>
        ///    Sample input:
        ///
        ///     POST /AddEditTransactionList
        ///
        ///    {
        ///        "VoucherNo": 0,
        ///        "VoucherTransactions": [
        ///            {
        ///            "TransactionId": 0,
        ///            "AccountNo": 0,
        ///            "Description": "string",
        ///            "ProjectId": 0,
        ///            "BudgetLineId": 0,
        ///            "JobId": 0,
        ///            "JobName": "string",
        ///            "Credit": 0,
        ///            "Debit": 0,
        ///            "VoucherNo": 0,
        ///            "IsDeleted": false
        ///            }
        ///        ]
        ///    }
        ///
        /// </remarks>
        /// <param name="voucherTransactions"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<APIResponse> AddEditTransactionList ([FromBody] AddEditTransactionModel voucherTransactions) {
            var user = await _userManager.FindByNameAsync (HttpContext.User.FindFirst (ClaimTypes.NameIdentifier).Value);
            APIResponse response = _iVoucherNewService.AddEditTransactionList (voucherTransactions, user.Id);
            return response;
        }

        #endregion

    }
}