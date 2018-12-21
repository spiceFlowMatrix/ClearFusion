using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;

namespace HumanitarianAssistance.Service.Classes.AccountingNew
{
    public class VoucherNewService : IVoucherNewService
    {

        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public VoucherNewService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }


        /// <summary>
        /// Get All Voucher List
        /// </summary>
        /// <param name="voucherNewFilterModel"></param>
        /// <returns>Vouchers List</returns>
        public async Task<APIResponse> GetAllNewVoucherList(VoucherNewFilterModel voucherNewFilterModel)
        {
            APIResponse response = new APIResponse();

            string codeValue = null;
            string journalValue = null;
            string dateValue = null;
            string nameValue = null;

            if (!string.IsNullOrEmpty(voucherNewFilterModel.FilterValue))
            {
                codeValue = voucherNewFilterModel.CodeFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
                nameValue = voucherNewFilterModel.NameFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
                dateValue = voucherNewFilterModel.DateFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
                journalValue = voucherNewFilterModel.JournalFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
            }

            try
            {

                var voucherList = await Task.Run(() =>
                    _uow.GetDbContext().VoucherDetail
                                      .Include(o => o.OfficeDetails)
                                      .Include(j => j.JournalDetails)
                                      .Include(c => c.CurrencyDetail)
                                      .Include(f => f.FinancialYearDetails)
                                      .Where(v => v.IsDeleted == false &&
                                               (
                                               v.VoucherNo.ToString().Trim() == codeValue ||
                                               v.ReferenceNo.ToString().Trim() == nameValue ||
                                               v.VoucherDate.ToString().Trim() == dateValue ||
                                               v.JournalDetails.JournalName.ToLower().Trim() == journalValue
                                               )
                                      )
                                      .OrderBy(x => x.VoucherDate).ToList()
                                      );
                var voucherdetaillist = voucherList.Select(v => new VoucherDetailModel
                {
                    VoucherNo = v.VoucherNo,
                    CurrencyCode = v.CurrencyDetail?.CurrencyCode ?? null,
                    CurrencyId = v.CurrencyDetail?.CurrencyId ?? 0,
                    VoucherDate = v.VoucherDate,
                    ChequeNo = v.ChequeNo,
                    ReferenceNo = v.ReferenceNo,
                    Description = v.Description,
                    JournalName = v.JournalDetails?.JournalName ?? null,
                    JournalCode = v.JournalDetails?.JournalCode ?? null,
                    VoucherTypeId = v.VoucherTypeId,
                    OfficeId = v.OfficeId,
                    ProjectId = v.ProjectId,
                    BudgetLineId = v.BudgetLineId,
                    OfficeName = v.OfficeDetails?.OfficeName ?? null,
                    FinancialYearId = v.FinancialYearId,
                    FinancialYearName = v.FinancialYearDetails?.FinancialYearName ?? null
                }).ToList();
                response.data.VoucherDetailList = voucherdetaillist.OrderByDescending(x => x.VoucherDate).ToList();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

    }
}
