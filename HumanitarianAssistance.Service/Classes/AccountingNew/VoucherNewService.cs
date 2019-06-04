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
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using HumanitarianAssistance.ViewModels.Models.Project;
using HumanitarianAssistance.Common.Enums;
using System.IO;
using HumanitarianAssistance.ViewModels.Models.Store;

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

            string voucherNoValue = null;
            string referenceNoValue = null;
            string descriptionValue = null;
            string journalNameValue = null;
            string dateValue = null;

            if (!string.IsNullOrEmpty(voucherNewFilterModel.FilterValue))
            {
                voucherNoValue = voucherNewFilterModel.VoucherNoFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
                referenceNoValue = voucherNewFilterModel.ReferenceNoFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
                descriptionValue = voucherNewFilterModel.DescriptionFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
                journalNameValue = voucherNewFilterModel.JournalNameFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
                dateValue = voucherNewFilterModel.DateFlag ? voucherNewFilterModel.FilterValue.ToLower().Trim() : null;
            }

            try
            {

                int totalCount = await _uow.GetDbContext().VoucherDetail
                                       .Where(v => v.IsDeleted == false &&
                                               !string.IsNullOrEmpty(voucherNewFilterModel.FilterValue) ? (
                                               v.VoucherNo.ToString().Trim().Contains(voucherNoValue) ||
                                               v.ReferenceNo.Trim().ToLower().Contains(referenceNoValue) ||
                                               v.Description.Trim().ToLower().Contains(descriptionValue) ||
                                               v.JournalDetails.JournalName.Trim().ToLower().Contains(journalNameValue) ||
                                               v.VoucherDate.ToString().Trim().Contains(dateValue)
                                               ) : true
                                       )
                                      .AsNoTracking()
                                      .CountAsync();

                var voucherList = await _uow.GetDbContext().VoucherDetail
                                      .Where(v => v.IsDeleted == false &&
                                                 !string.IsNullOrEmpty(voucherNewFilterModel.FilterValue) ? (
                                                   v.VoucherNo.ToString().Trim().Contains(voucherNoValue) ||
                                                   v.ReferenceNo.Trim().ToLower().Contains(referenceNoValue) ||
                                                   v.Description.Trim().ToLower().Contains(descriptionValue) ||
                                                   v.JournalDetails.JournalName.Trim().ToLower().Contains(journalNameValue) ||
                                                   v.VoucherDate.ToString().Trim().ToLower().Contains(dateValue)
                                                   ) : true
                                       )
                                      .OrderByDescending(x => x.CreatedDate)
                                      .Select(x => new VoucherDetailModel
                                      {
                                          VoucherNo = x.VoucherNo,
                                          CurrencyCode = x.CurrencyDetail.CurrencyCode,
                                          CurrencyId = x.CurrencyDetail.CurrencyId,
                                          VoucherDate = x.VoucherDate,
                                          ChequeNo = x.ChequeNo,
                                          ReferenceNo = x.ReferenceNo,
                                          Description = x.Description,
                                          JournalName = x.JournalDetails.JournalName,
                                          JournalCode = x.JournalDetails.JournalCode,
                                          VoucherTypeId = x.VoucherTypeId,
                                          OfficeId = x.OfficeId,
                                          ProjectId = x.ProjectId,
                                          BudgetLineId = x.BudgetLineId,
                                          OfficeName = x.OfficeDetails.OfficeName,
                                      })
                                      .Skip(voucherNewFilterModel.pageSize.Value * voucherNewFilterModel.pageIndex.Value)
                                      .Take(voucherNewFilterModel.pageSize.Value)
                                      .ToListAsync();
                response.data.VoucherDetailList = voucherList;
                response.data.TotalCount = totalCount;
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

        /// <summary>
        /// Get Voucher Detail By VoucherNo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetVoucherDetailByVoucherNo(long id)
        {
            APIResponse response = new APIResponse();
            try
            {
                var voucherDetail = await _uow.GetDbContext().VoucherDetail
                                              .Include(o => o.OfficeDetails)
                                              .Include(j => j.JournalDetails)
                                              .Include(c => c.CurrencyDetail)
                                              .Include(f => f.FinancialYearDetails)
                                              .FirstOrDefaultAsync(v => v.IsDeleted == false && v.VoucherNo == id);

                if (voucherDetail != null)
                {
                    VoucherDetailModel obj = new VoucherDetailModel();

                    obj.VoucherNo = voucherDetail.VoucherNo;
                    obj.CurrencyCode = voucherDetail.CurrencyDetail?.CurrencyCode ?? null;
                    obj.CurrencyId = voucherDetail.CurrencyDetail?.CurrencyId ?? 0;
                    obj.VoucherDate = voucherDetail.VoucherDate;
                    obj.ChequeNo = voucherDetail.ChequeNo;
                    obj.ReferenceNo = voucherDetail.ReferenceNo;
                    obj.Description = voucherDetail.Description;
                    obj.JournalName = voucherDetail.JournalDetails?.JournalName ?? null;
                    obj.JournalCode = voucherDetail.JournalDetails?.JournalCode ?? null;
                    obj.VoucherTypeId = voucherDetail.VoucherTypeId;
                    obj.OfficeId = voucherDetail.OfficeId;
                    obj.ProjectId = voucherDetail.ProjectId;
                    obj.BudgetLineId = voucherDetail.BudgetLineId;
                    obj.OfficeName = voucherDetail.OfficeDetails?.OfficeName ?? null;
                    obj.FinancialYearId = voucherDetail.FinancialYearId;
                    obj.FinancialYearName = voucherDetail.FinancialYearDetails?.FinancialYearName ?? null;
                    obj.IsVoucherVerified = voucherDetail.IsVoucherVerified;
                    obj.IsExchangeGainLossVoucher = voucherDetail.IsExchangeGainLossVoucher;

                    response.data.VoucherDetail = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.VoucherNotPresent;
                }


            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// check if Exchange Rate is present or not
        /// </summary>
        /// <param name="currencyList"></param>
        /// <param name="exchangeRatePresedsnt"></param>
        /// <returns>false</returns>
        /// <returns>true</returns>
        public bool CheckExchangeRateIsPresent(List<int> currencyList, List<ExchangeRateDetail> exchangeRates)
        {
            var groupedDataCount = exchangeRates.GroupBy(x => new { x.FromCurrency, x.ToCurrency }).ToList().Count;
            if (groupedDataCount >= (int)Math.Pow(currencyList.Count(), 2))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Add Voucher
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddVoucherNewDetail(VoucherDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {

                model.TimezoneOffset = model.TimezoneOffset > 0 ? model.TimezoneOffset * -1 : Math.Abs(model.TimezoneOffset.Value);

                DateTime filterVoucherDate = model.VoucherDate.AddMinutes(model.TimezoneOffset.Value);

                Task<List<CurrencyDetails>> currencyListTask = _uow.GetDbContext().CurrencyDetails.Where(x => x.IsDeleted == false).ToListAsync();

                if (model.IsExchangeGainLossVoucher)
                {
                    model.VoucherDate = DateTime.UtcNow;
                }

                Task<List<ExchangeRateDetail>> exchangeRatePresentTask = _uow.GetDbContext().ExchangeRateDetail.Where(x => x.Date.Date == model.VoucherDate.Date && x.IsDeleted == false).ToListAsync();

                List<CurrencyDetails> currencyList = await currencyListTask;

                List<int> currencyIds = currencyList.Select(x => x.CurrencyId).ToList();

                string currencyCode = currencyList.FirstOrDefault(x => x.CurrencyId == model.CurrencyId).CurrencyCode;

                List<ExchangeRateDetail> exchangeRatePresent = await exchangeRatePresentTask;

                if (CheckExchangeRateIsPresent(currencyIds, exchangeRatePresent))
                {
                    var officeDetail = await _uow.GetDbContext().OfficeDetail.FirstOrDefaultAsync(o => o.OfficeId == model.OfficeId); //use OfficeCode

                    if (officeDetail != null)
                    {
                        Task<FinancialYearDetail> fincancialYearTask = _uow.GetDbContext().FinancialYearDetail.FirstOrDefaultAsync(o => o.IsDefault);
                        Task<CurrencyDetails> currencyDetailTask = _uow.GetDbContext().CurrencyDetails.FirstOrDefaultAsync(o => o.CurrencyId == model.CurrencyId);
                        // NOTE: Dont remove this as we will need journal details in response
                        Task<JournalDetail> journaldetailTask = _uow.GetDbContext().JournalDetail.FirstOrDefaultAsync(o => o.JournalCode == model.JournalCode);
                        int voucherCount = await _uow.GetDbContext().VoucherDetail.Where(x => x.VoucherDate.Month == model.VoucherDate.Month && x.VoucherDate.Year== filterVoucherDate.Year && x.OfficeId== model.OfficeId && x.CurrencyId== model.CurrencyId).CountAsync();

                        FinancialYearDetail fincancialYear = await fincancialYearTask;

                        if (fincancialYear != null)
                        {
                            CurrencyDetails currencyDetail = await currencyDetailTask;

                            if (currencyDetail != null)
                            {
                                JournalDetail journaldetail = await journaldetailTask;

                                VoucherDetail obj = _mapper.Map<VoucherDetail>(model);
                                obj.JournalCode = journaldetail != null ? journaldetail.JournalCode : model.JournalCode;
                                obj.FinancialYearId = fincancialYear.FinancialYearId;
                                obj.CreatedById = model.CreatedById;
                                obj.VoucherDate = model.VoucherDate;
                                obj.CreatedDate = DateTime.UtcNow;
                                obj.IsDeleted = false;

                                // Pattern: Office Code - Currency Code - Month Number - voucher count on selected month - Year
                                string referenceNo = AccountingUtility.GenerateVoucherReferenceCode(model.VoucherDate, voucherCount, currencyDetail.CurrencyCode, officeDetail.OfficeCode);

                                int sameVoucherReferenceNoCount = 0;

                                if (!string.IsNullOrEmpty(referenceNo))
                                {
                                    do
                                    {
                                        sameVoucherReferenceNoCount = await _uow.GetDbContext().VoucherDetail.Where(x => x.ReferenceNo == referenceNo).CountAsync();

                                        if (sameVoucherReferenceNoCount == 0)
                                        {
                                            obj.ReferenceNo = referenceNo;
                                        }
                                        else
                                        {
                                            //DO NOT REMOVE: This is used to get the latest voucher and then we will get the count of vouhcer sequence from it
                                            // VoucherDetail voucherDetail = _uow.GetDbContext().VoucherDetail.OrderByDescending(x => x.VoucherDate).FirstOrDefault(x => x.VoucherDate.Month == filterVoucherDate.Month && x.OfficeId == model.OfficeId && x.VoucherDate.Year == filterVoucherDate.Year);

                                            var refNo = referenceNo.Split('-');
                                            int count = Convert.ToInt32(refNo[3]);
                                            referenceNo = AccountingUtility.GenerateVoucherReferenceCode(model.VoucherDate, count, currencyCode, officeDetail.OfficeCode);
                                        }
                                    }
                                    while (sameVoucherReferenceNoCount != 0);
                                }

                                await _uow.VoucherDetailRepository.AddAsyn(obj);
                                
                                VoucherDetailEntityModel voucherModel = _mapper.Map<VoucherDetail, VoucherDetailEntityModel>(obj);

                                response.data.VoucherDetailEntity = voucherModel;
                                response.StatusCode = StaticResource.successStatusCode;
                                response.Message = "Success";
                            }
                            else
                            {
                                response.StatusCode = StaticResource.failStatusCode;
                                response.Message = StaticResource.CurrencyNotFound;
                            }
                        }
                        else
                        {
                            response.StatusCode = StaticResource.failStatusCode;
                            response.Message = StaticResource.defaultFinancialYearIsNotSet;
                        }
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.officeCodeNotFound;
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.ExchagneRateNotDefined;
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Edit Voucher
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditVoucherNewDetail(VoucherDetailModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                model.TimezoneOffset = model.TimezoneOffset > 0 ? model.TimezoneOffset *-1 : Math.Abs(model.TimezoneOffset.Value);

                DateTime filterVoucherDate = model.VoucherDate.AddMinutes(model.TimezoneOffset.Value);

                Task<List<CurrencyDetails>> currencyListTask = _uow.GetDbContext().CurrencyDetails.Where(x => x.IsDeleted == false).ToListAsync();
                Task<List<ExchangeRateDetail>> exchangeRatePresentTask = _uow.GetDbContext().ExchangeRateDetail.Where(x => x.Date.Date == model.VoucherDate.Date && x.IsDeleted == false).ToListAsync();
                Task<VoucherDetail> voucherDetailTask= _uow.VoucherDetailRepository.FindAsync(c => c.VoucherNo == model.VoucherNo);
                Task<OfficeDetail> officeDetailTask= _uow.OfficeDetailRepository.FindAsync(x => x.IsDeleted == false && x.OfficeId == model.OfficeId);
                List<CurrencyDetails> currencyDetailsList = await currencyListTask;

                var currencyList = currencyDetailsList.Select(x => x.CurrencyId).ToList();

                List<ExchangeRateDetail> exchangeRatePresent = await exchangeRatePresentTask;

                if (CheckExchangeRateIsPresent(currencyList, exchangeRatePresent))
                {
                    VoucherDetail voucherdetailInfo = await voucherDetailTask;

                    string currencyCode = currencyDetailsList.FirstOrDefault(x => x.CurrencyId == model.CurrencyId).CurrencyCode;

                    int voucherCount = await _uow.GetDbContext().VoucherDetail.Where(x => x.VoucherDate.Month == filterVoucherDate.Month && x.OfficeId == model.OfficeId && x.VoucherDate.Year== filterVoucherDate.Year).CountAsync();

                    if (voucherdetailInfo != null)
                    {
                        OfficeDetail officeDetail = await officeDetailTask;

                        if (model.VoucherDate.Date != voucherdetailInfo.VoucherDate.Date || model.OfficeId != voucherdetailInfo.OfficeId || voucherdetailInfo.CurrencyCode != model.CurrencyCode)
                        {
                            string referenceNo = AccountingUtility.GenerateVoucherReferenceCode(filterVoucherDate, voucherCount, currencyCode, officeDetail.OfficeCode);

                            if (!string.IsNullOrEmpty(referenceNo))
                            {
                                //check if same sequence number is already present in the voucher table
                                int sameVoucherReferenceNoCount = 0;

                                do
                                {
                                    sameVoucherReferenceNoCount = await _uow.GetDbContext().VoucherDetail.Where(x => x.ReferenceNo == referenceNo).CountAsync();

                                    if (sameVoucherReferenceNoCount == 0)
                                    {
                                        voucherdetailInfo.ReferenceNo = referenceNo;
                                    }
                                    else
                                    {
                                        var refNo = referenceNo.Split('-');
                                        int count = Convert.ToInt32(refNo[3]);
                                        referenceNo = AccountingUtility.GenerateVoucherReferenceCode(model.VoucherDate, count, currencyCode, officeDetail.OfficeCode);
                                    }
                                }
                                while (sameVoucherReferenceNoCount != 0);
                            }
                        }
                        else if (model.CurrencyId != voucherdetailInfo.CurrencyId)
                        {
                            if (string.IsNullOrEmpty(voucherdetailInfo.ReferenceNo))
                            {
                                var refNo = voucherdetailInfo.ReferenceNo.Split('-');
                                refNo[1] = currencyCode;
                                voucherdetailInfo.ReferenceNo = refNo[0] + "-" + refNo[1] + "-" + refNo[2] + "-" + refNo[3] + "-" + refNo[4];
                            }
                            else
                            {
                                throw new Exception("Reference No cannot be set");
                            }
                        }

                        voucherdetailInfo.CurrencyId = model.CurrencyId;
                        voucherdetailInfo.OfficeId = model.OfficeId;
                        voucherdetailInfo.VoucherDate = model.VoucherDate;
                        voucherdetailInfo.ChequeNo = model.ChequeNo;
                        // voucherdetailInfo.ReferenceNo = voucherdetailInfo.ReferenceNo;
                        voucherdetailInfo.JournalCode = model.JournalCode;
                        voucherdetailInfo.FinancialYearId = model.FinancialYearId;
                        voucherdetailInfo.VoucherTypeId = model.VoucherTypeId;
                        voucherdetailInfo.Description = model.Description;
                        voucherdetailInfo.ModifiedById = model.ModifiedById;
                        voucherdetailInfo.ModifiedDate = model.ModifiedDate;
                        await _uow.VoucherDetailRepository.UpdateAsyn(voucherdetailInfo);

                        if (_uow.GetDbContext().VoucherTransactions.Any(x => x.VoucherNo == voucherdetailInfo.VoucherNo))
                        {
                            var voucherTransactions =
                                await _uow.VoucherTransactionsRepository.FindAllAsync(x =>
                                    x.VoucherNo == voucherdetailInfo.VoucherNo);
                            foreach (var transaction in voucherTransactions)
                            {
                                transaction.TransactionDate = voucherdetailInfo.VoucherDate;
                            }

                            _uow.GetDbContext().VoucherTransactions.UpdateRange(voucherTransactions);
                            await _uow.SaveAsync();
                        }

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.VoucherNotPresent;
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.ExchagneRateNotDefined;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// <summary>
        /// Delete Voucher
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteVoucher(long voucherId, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var voucherdetail = await _uow.VoucherDetailRepository.FindAsync(c => c.VoucherNo == voucherId);

                if (voucherdetail != null)
                {
                    voucherdetail.IsDeleted = true;
                    voucherdetail.ModifiedById = userId;
                    voucherdetail.ModifiedDate = DateTime.UtcNow;

                    await _uow.VoucherDetailRepository.UpdateAsyn(voucherdetail);

                    await DeleteTransaction(voucherId, userId);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }

                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.VoucherNotPresent;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// Get All Voucher Transaction List 
        /// </summary>
        /// <param name="VoucherNo"></param>
        /// <returns>List of Voucher Transactions</returns>
        public async Task<APIResponse> GetAllTransactionsByVoucherId(long VoucherNo)
        {
            APIResponse response = new APIResponse();

            try
            {

                List<VoucherTransactionsModel> voucherTransactionsList = await _uow.GetDbContext().VoucherTransactions
                                   .Where(x => x.IsDeleted == false && x.VoucherNo == VoucherNo)
                                   .Select(x => new VoucherTransactionsModel
                                   {
                                       AccountNo = x.ChartOfAccountNewId,
                                       Debit = (x.Debit != 0 && x.Debit != null) ? x.Debit : 0,
                                       Credit = (x.Credit != 0 && x.Credit != null) ? x.Credit : 0,
                                       BudgetLineId = x.BudgetLineId,
                                       ProjectId = x.ProjectId,
                                       Description = x.Description,
                                       TransactionId = x.TransactionId,
                                       VoucherNo = x.VoucherNo,
                                       JobId = x.JobId,
                                       JobName = _uow.GetDbContext().ProjectJobDetail.FirstOrDefault(j => j.IsDeleted == false && j.ProjectJobId == x.JobId).ProjectJobName,
                                       BudgetLineList = _uow.GetDbContext()
                                                                    .ProjectBudgetLineDetail
                                                                    .Where(p => p.IsDeleted == false && p.ProjectId == x.ProjectId)
                                                                    .Select(s=> new ProjectBudgetLineDetailModel
                                                                    {
                                                                        BudgetLineId= s.BudgetLineId,
                                                                        BudgetName= s.BudgetName
                                                                    }).ToList()
                                   }).ToListAsync();

                response.data.VoucherTransactions = voucherTransactionsList;
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

        /// <summary>
        /// Edit Transaction Record on the basis of Transaction Id
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Success/Failure</returns>
        public async Task<APIResponse> EditTransactionDetail(VoucherTransactionsModel voucherTransactions, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                //get Voucher transaction as per transactionId
                VoucherTransactions transaction = await _uow.VoucherTransactionsRepository.FindAsync(c => c.TransactionId == voucherTransactions.TransactionId);

                if (transaction != null)
                {
                    transaction.ChartOfAccountNewId = voucherTransactions.AccountNo;
                    transaction.Debit = voucherTransactions.Debit;
                    transaction.Credit = voucherTransactions.Credit;
                    transaction.Description = voucherTransactions.Description;
                    transaction.BudgetLineId = voucherTransactions.BudgetLineId;
                    transaction.ProjectId = voucherTransactions.ProjectId;
                    transaction.ModifiedById = userId;
                    transaction.ModifiedDate = DateTime.Now;

                    //update transaction as per new updated values
                    await _uow.VoucherTransactionsRepository.UpdateAsyn(transaction);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Delete Transaction
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns>Success/failure</returns>
        public async Task<APIResponse> DeleteTransactionById(long transactionId)
        {
            APIResponse response = new APIResponse();

            try
            {
                //get Voucher transaction as per transactionId
                VoucherTransactions transaction = await _uow.VoucherTransactionsRepository.FindAsync(c => c.TransactionId == transactionId);

                if (transaction != null)
                {
                    transaction.IsDeleted = true;

                    //update transaction as per new updated values
                    await _uow.VoucherTransactionsRepository.UpdateAsyn(transaction);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Delete Transaction
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns>Success/failure</returns>
        public async Task<APIResponse> DeleteTransaction(long voucherId, string userId)
        {
            APIResponse response = new APIResponse();
            using (var _dbTransaction = _uow.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    var transactions = await _uow.GetDbContext().VoucherTransactions.Where(x => x.VoucherNo == voucherId).ToListAsync();
                    if (transactions.Any())
                    {
                        transactions.ForEach(x =>
                        {
                            x.IsDeleted = true;
                            x.ModifiedDate = DateTime.UtcNow;
                            x.ModifiedById = userId;
                        });

                        _uow.GetDbContext().VoucherTransactions.UpdateRange(transactions);
                        _uow.GetDbContext().SaveChanges();
                        _dbTransaction.Commit();

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                    else
                    {
                        throw new Exception(StaticResource.TransactionNotFound);
                    }
                }
                catch (Exception ex)
                {
                    _dbTransaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            return response;
        }

        /// <summary>
        /// Add Voucher Transaction
        /// </summary>
        /// <param name="voucherTransactions"></param>
        /// <param name="userId"></param>
        /// <returns>Success/Failure</returns>
        public async Task<APIResponse> AddTransactionDetail(List<VoucherTransactionsModel> voucherTransactionsList, string userId)
        {
            APIResponse response = new APIResponse();

            List<VoucherTransactions> transactionsList = new List<VoucherTransactions>();

            try
            {
                if (voucherTransactionsList.Any())
                {

                    var voucherDetail = await _uow.VoucherDetailRepository.FindAsync(x => x.IsDeleted == false && x.VoucherNo == voucherTransactionsList.FirstOrDefault().VoucherNo);

                    foreach (VoucherTransactionsModel voucherTransactions in voucherTransactionsList)
                    {
                        //new voucher transaction object
                        VoucherTransactions transaction = new VoucherTransactions();

                        transaction.ChartOfAccountNewId = voucherTransactions.AccountNo;
                        transaction.Debit = voucherTransactions.Debit;
                        transaction.Credit = voucherTransactions.Credit;
                        transaction.Description = voucherTransactions.Description;
                        transaction.BudgetLineId = voucherTransactions.BudgetLineId;
                        transaction.ProjectId = voucherTransactions.ProjectId;
                        transaction.CreatedById = userId;
                        transaction.CreatedDate = DateTime.Now;
                        transaction.IsDeleted = false;
                        transaction.VoucherNo = voucherTransactions.VoucherNo;
                        transaction.CurrencyId = voucherDetail.CurrencyId;
                        transaction.TransactionDate = voucherDetail.VoucherDate;

                        transactionsList.Add(transaction);
                    }

                    //Add transaction
                    await _uow.GetDbContext().VoucherTransactions.AddRangeAsync(transactionsList);
                    await _uow.SaveAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Add/Edit Voucher Transaction
        /// </summary>
        /// <param name="voucherTransactions"></param>
        /// <param name="userId"></param>
        /// <returns>Success/Failure</returns>
        public APIResponse AddEditTransactionList(AddEditTransactionModel voucherTransactions, string userId)
        {
            APIResponse response = new APIResponse();

            List<VoucherTransactions> transactionsListAdd = new List<VoucherTransactions>();
            List<VoucherTransactions> transactionsListEdit = new List<VoucherTransactions>();

            try
            {
                if (voucherTransactions.VoucherTransactions.Any())
                {

                    var editList = voucherTransactions.VoucherTransactions.Where(w => w.TransactionId != 0)
                                                          .Select(s => s.TransactionId);

                    var editTransactionList = _uow.GetDbContext().VoucherTransactions
                                                                 .Where(x => editList
                                                                            .Contains(x.TransactionId))
                                                                 .ToList();

                    var voucherDetail = _uow.VoucherDetailRepository.Find(x => x.IsDeleted == false && x.VoucherNo == voucherTransactions.VoucherNo);

                    if (voucherDetail != null)
                    {
                        foreach (VoucherTransactionsModel item in voucherTransactions.VoucherTransactions)
                        {
                            // Add
                            if (item.TransactionId == 0 && item.IsDeleted == false)
                            {
                                //new voucher transaction object
                                VoucherTransactions transaction = new VoucherTransactions();

                                transaction.ChartOfAccountNewId = item.AccountNo;
                                transaction.Debit = item.Debit;
                                transaction.Credit = item.Credit;
                                transaction.Description = item.Description;
                                transaction.BudgetLineId = item.BudgetLineId;
                                transaction.ProjectId = item.ProjectId;
                                transaction.CreatedById = userId;
                                transaction.CreatedDate = DateTime.Now;
                                transaction.IsDeleted = false;
                                transaction.VoucherNo = item.VoucherNo;
                                transaction.CurrencyId = voucherDetail.CurrencyId;
                                transaction.TransactionDate = voucherDetail.VoucherDate;
                                transaction.JobId = item.JobId;

                                transactionsListAdd.Add(transaction);
                            }
                            // edit
                            else
                            {
                                VoucherTransactions transaction = editTransactionList.FirstOrDefault(x => x.TransactionId == item.TransactionId);

                                if (transaction != null)
                                {
                                    if (item.IsDeleted == false)
                                    {
                                        transaction.IsDeleted = false;
                                    }
                                    else
                                    {
                                        transaction.IsDeleted = true;
                                    }
                                    transaction.TransactionId = item.TransactionId;
                                    transaction.ChartOfAccountNewId = item.AccountNo;
                                    transaction.Debit = item.Debit;
                                    transaction.Credit = item.Credit;
                                    transaction.Description = item.Description;
                                    transaction.BudgetLineId = item.BudgetLineId;
                                    transaction.ProjectId = item.ProjectId;
                                    transaction.JobId = item.JobId;
                                    transaction.CurrencyId = voucherDetail.CurrencyId;
                                    transaction.TransactionDate = voucherDetail.VoucherDate;
                                    transaction.ModifiedById = userId;
                                    transaction.ModifiedDate = DateTime.Now;
                                    //transaction.VoucherNo = voucherTransactions.VoucherNo;

                                    transactionsListEdit.Add(transaction);
                                }
                            }
                        }

                        using (IDbContextTransaction tran = _uow.GetDbContext().Database.BeginTransaction())
                        {
                            try
                            {
                                _uow.GetDbContext().VoucherTransactions.AddRange(transactionsListAdd);
                                _uow.GetDbContext().VoucherTransactions.UpdateRange(transactionsListEdit);

                                _uow.GetDbContext().SaveChanges();
                                tran.Commit();
                            }

                            catch (Exception ex)
                            {
                                tran.Rollback();
                                response.StatusCode = StaticResource.failStatusCode;
                                response.Message = StaticResource.SomethingWrong + ex.Message;
                                return response;
                            }
                        }

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = StaticResource.SuccessText;
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.VoucherNotPresent;
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Verify Voucher and we will consider only verified vouchers for transactions
        /// </summary>
        /// <param name="voucherNo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<APIResponse> VerifyVoucher(long voucherNo, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var voucherDetail = await _uow.VoucherDetailRepository.FindAsync(x => x.VoucherNo == voucherNo);
                if (voucherDetail != null)
                {
                    voucherDetail.IsVoucherVerified = !voucherDetail.IsVoucherVerified;
                    voucherDetail.ModifiedById = userId;
                    voucherDetail.ModifiedDate = DateTime.Now;

                    await _uow.VoucherDetailRepository.UpdateAsyn(voucherDetail);

                    response.data.IsVoucherVerified = voucherDetail.IsVoucherVerified;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = voucherDetail.IsVoucherVerified ? StaticResource.VoucherVerified : StaticResource.VoucherUnVerified;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Gain Loss Voucher and Transaction generation
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<APIResponse> CreateGainLossTransaction(ExchangeGainLossVoucherDetails model, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                #region "Generate Voucher"
                VoucherDetailModel voucherModel = new VoucherDetailModel
                {
                    VoucherNo = model.VoucherNo,
                    CurrencyId = model.CurrencyId,
                    Description = model.Description,
                    JournalCode = model.JournalId,
                    VoucherTypeId = model.VoucherType,
                    OfficeId = model.OfficeId,
                    ProjectId = model.ProjectId,
                    BudgetLineId = model.BudgetLineId,
                    IsExchangeGainLossVoucher = true
                };

                var responseVoucher = await AddVoucherNewDetail(voucherModel);

                #endregion

                #region "Generate Transaction"

                if (responseVoucher.StatusCode == 200)
                {
                    List<VoucherTransactionsModel> transactions = new List<VoucherTransactionsModel>();

                    // Credit
                    transactions.Add(new VoucherTransactionsModel
                    {
                        TransactionId = 0,
                        VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                        AccountNo = model.CreditAccount,
                        Debit = 0,
                        Credit = Math.Abs(model.Amount),
                        Description = "Gain-Loss-Voucher-Credit",
                        IsDeleted = false
                    });

                    // Debit
                    transactions.Add(new VoucherTransactionsModel
                    {
                        TransactionId = 0,
                        VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                        AccountNo = model.DebitAccount,
                        Debit = Math.Abs(model.Amount),
                        Credit = 0,
                        Description = "Gain-Loss-Voucher-Debit",
                        IsDeleted = false
                    });

                    AddEditTransactionModel transactionVoucherDetail = new AddEditTransactionModel
                    {
                        VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                        VoucherTransactions = transactions
                    };

                    var responseTransaction = AddEditTransactionList(transactionVoucherDetail, userId);

                    if (responseTransaction.StatusCode == 200)
                    {
                        string voucherName = _uow.GetDbContext().VoucherDetail.FirstOrDefault(x => x.JournalCode == responseVoucher.data.VoucherDetailEntity.JournalCode)?.JournalDetails.JournalName;

                        response.data.GainLossVoucherDetail = new GainLossVoucherList
                        {
                            VoucherId = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                            JournalName = voucherName != null ? voucherName : "",
                            VoucherName = responseVoucher.data.VoucherDetailEntity.ReferenceNo,
                            VoucherDate = responseVoucher.data.VoucherDetailEntity.VoucherDate
                        };
                    }
                    else
                    {
                        throw new Exception(responseTransaction.Message);
                    }

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = responseVoucher.Message;
                }

                #endregion
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        ///  Delete Gain Loss Voucher-Transaction
        /// </summary>
        /// <param name="voucherId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteGainLossVoucherTransaction(long voucherId, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (voucherId != 0)
                {
                    var voucherResponse = await DeleteVoucher(voucherId, userId);

                    response.StatusCode = voucherResponse.StatusCode;
                    response.Message = voucherResponse.Message;
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.VoucherNotPresent;
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Get all Vouchers let generated from gain loss report
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetExchangeGainLossVoucherList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var gainLossVouchers = await _uow.GetDbContext().VoucherDetail
                                                                        .Where(x => x.IsDeleted == false &&
                                                                                    x.IsExchangeGainLossVoucher == true)
                                                                        .Select(x => new GainLossVoucherList
                                                                        {
                                                                            VoucherId = x.VoucherNo,
                                                                            VoucherName = x.ReferenceNo,
                                                                            JournalName = x.JournalDetails.JournalName,
                                                                            VoucherDate = x.VoucherDate,
                                                                        })
                                                                        .ToListAsync();

                response.data.GainLossVoucherList = gainLossVouchers;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> VerifyPurchase(ItemPurchaseModel model)
        {
            var response = new APIResponse();
            try
            {
                if (model != null)
                {
                    var purchaseRecord = await _uow.StoreItemPurchaseRepository.FindAsync(x => x.PurchaseId == model.PurchaseId);
                    if (purchaseRecord != null)
                    {
                        _mapper.Map(model, purchaseRecord);

                        if (!string.IsNullOrEmpty(model.ImageFileName))
                        {
                            if (model.ImageFileName.Contains(","))
                            {
                                string[] str = model.ImageFileName.Split(",");
                                byte[] filepath = Convert.FromBase64String(str[1]);
                                string ex = str[0].Split("/")[1].Split(";")[0];
                                string guidname = Guid.NewGuid().ToString();
                                string filename = guidname + "." + ex;
                                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                                File.WriteAllBytes(@"Documents/" + filename, filepath);

                                purchaseRecord.ImageFileName = guidname;
                                purchaseRecord.ImageFileType = "." + ex;
                            }
                        }

                        if (model.InvoiceFileName != null && model.InvoiceFileName != "")
                        {
                            if (model.InvoiceFileName.Contains(","))
                            {
                                string[] str = model.InvoiceFileName.Split(",");
                                byte[] filepath = Convert.FromBase64String(str[1]);
                                string ex = str[0].Split("/")[1].Split(";")[0];
                                string guidname = Guid.NewGuid().ToString();
                                string filename = guidname + "." + ex;
                                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                                File.WriteAllBytes(@"Documents/" + filename, filepath);

                                purchaseRecord.InvoiceFileName = guidname;
                                purchaseRecord.InvoiceFileType = "." + ex;
                            }
                        }

                        purchaseRecord.IsDeleted = false;


                        //List<ExchangeRate> exchangeRate = new List<ExchangeRate>();

                        if (model.IsPurchaseVerified.HasValue && model.IsPurchaseVerified.Value)
                        {
                            var financialYearDetails = _uow.GetDbContext().FinancialYearDetail.FirstOrDefault(x => x.IsDeleted == false && x.StartDate.Date.Year == DateTime.Now.Year);
                            var inventory = _uow.GetDbContext().InventoryItems.Include(x => x.Inventory).FirstOrDefault(x => x.ItemId == model.InventoryItem);
                            var paymentTypes = _uow.GetDbContext().PaymentTypes.FirstOrDefault(x => x.PaymentId == model.PaymentTypeId);
                           
                            #region "Generate Voucher"
                            VoucherDetailModel voucherModel = new VoucherDetailModel
                            {
                                CurrencyId = model.Currency,
                                Description = StaticResource.PurchaseVoucherCreated,
                                JournalCode = model.JournalCode,
                                VoucherTypeId = (int)VoucherTypes.Journal,
                                OfficeId = model.OfficeId,
                                ProjectId = model.ProjectId,
                                BudgetLineId = model.BudgetLineId,
                                IsExchangeGainLossVoucher = false,
                                CreatedById= model.CreatedById,
                                CreatedDate = DateTime.UtcNow,
                                IsDeleted = false,
                                FinancialYearId = financialYearDetails.FinancialYearId,
                                VoucherDate = DateTime.UtcNow,
                                TimezoneOffset= model.TimezoneOffset
                            };

                            var responseVoucher = await AddVoucherNewDetail(voucherModel);
                            #endregion
                            
                            if (responseVoucher.StatusCode == 200)
                            {
                                purchaseRecord.VerifiedPurchaseVoucher = responseVoucher.data.VoucherDetailEntity.VoucherNo;
                                await _uow.StoreItemPurchaseRepository.UpdateAsyn(purchaseRecord);

                                List<VoucherTransactionsModel> transactions = new List<VoucherTransactionsModel>();

                                // Credit
                                transactions.Add(new VoucherTransactionsModel
                                {
                                    TransactionId = 0,
                                    VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                                    AccountNo = paymentTypes.ChartOfAccountNewId,
                                    Debit = 0,
                                    Credit = model.UnitCost * model.Quantity,
                                    Description = StaticResource.PurchaseVoucherCreated,
                                    IsDeleted = false
                                });

                                // Debit
                                transactions.Add(new VoucherTransactionsModel
                                {
                                    TransactionId = 0,
                                    VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                                    AccountNo = inventory.Inventory.InventoryDebitAccount,
                                    Debit = model.UnitCost * model.Quantity,
                                    Credit = 0,
                                    Description = StaticResource.PurchaseVoucherCreated,
                                    IsDeleted = false
                                });

                                AddEditTransactionModel transactionVoucherDetail = new AddEditTransactionModel
                                {
                                    VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                                    VoucherTransactions = transactions
                                };

                                var responseTransaction = AddEditTransactionList(transactionVoucherDetail, model.CreatedById);

                                if (responseTransaction.StatusCode == 200)
                                {
                                    response.StatusCode = StaticResource.successStatusCode;
                                    response.Message = StaticResource.SuccessText;
                                }
                                else
                                {
                                    throw new Exception(responseTransaction.Message);
                                }
                            }
                            else
                            {
                                response.StatusCode = StaticResource.failStatusCode;
                                response.Message = responseVoucher.Message;
                            }
                        }
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Record cannot be updated";
                        return response;
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Model values are inappropriate";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
                return response;
            }
            return response;
        }

        public async Task<APIResponse> UnverifyPurchase(ItemPurchaseModel model)
        {
            var response = new APIResponse();

            try
            {
                if (model != null)
                {
                    var purchaseRecord = await _uow.StoreItemPurchaseRepository.FindAsync(x => x.PurchaseId == model.PurchaseId);

                    if (purchaseRecord != null)
                    {
                        _mapper.Map(model, purchaseRecord);

                        if (!string.IsNullOrEmpty(model.ImageFileName))
                        {
                            if (model.ImageFileName.Contains(","))
                            {
                                string[] str = model.ImageFileName.Split(",");
                                byte[] filepath = Convert.FromBase64String(str[1]);
                                string ex = str[0].Split("/")[1].Split(";")[0];
                                string guidname = Guid.NewGuid().ToString();
                                string filename = guidname + "." + ex;
                                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                                File.WriteAllBytes(@"Documents/" + filename, filepath);

                                purchaseRecord.ImageFileName = guidname;
                                purchaseRecord.ImageFileType = "." + ex;
                            }
                        }

                        if (model.InvoiceFileName != null && model.InvoiceFileName != "")
                        {
                            if (model.InvoiceFileName.Contains(","))
                            {
                                string[] str = model.InvoiceFileName.Split(",");
                                byte[] filepath = Convert.FromBase64String(str[1]);
                                string ex = str[0].Split("/")[1].Split(";")[0];
                                string guidname = Guid.NewGuid().ToString();
                                string filename = guidname + "." + ex;
                                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                                File.WriteAllBytes(@"Documents/" + filename, filepath);

                                purchaseRecord.InvoiceFileName = guidname;
                                purchaseRecord.InvoiceFileType = "." + ex;
                            }
                        }

                        purchaseRecord.IsDeleted = false;

                        if (model.IsPurchaseVerified.HasValue && !model.IsPurchaseVerified.Value)
                        {

                            VoucherDetail voucherDetail = _uow.GetDbContext().VoucherDetail.FirstOrDefault(x => x.IsDeleted == false && x.VoucherNo == model.VerifiedPurchaseVoucher);

                            if (voucherDetail != null)
                            {
                                List<VoucherTransactions> voucherTransactionsList = await _uow.GetDbContext().VoucherTransactions.Where(x => x.IsDeleted == false && x.VoucherNo == model.VerifiedPurchaseVoucher).ToListAsync();

                                var creditTransaction = voucherTransactionsList.FirstOrDefault(x => x.Debit == 0);
                                var debitTransaction = voucherTransactionsList.FirstOrDefault(x => x.Credit == 0);

                                List<VoucherTransactionsModel> transactions = new List<VoucherTransactionsModel>();

                                // Credit
                                transactions.Add(new VoucherTransactionsModel
                                {
                                    TransactionId = 0,
                                    VoucherNo = voucherDetail.VoucherNo,
                                    AccountNo = debitTransaction.ChartOfAccountNewId,
                                    Debit = 0,
                                    Credit = debitTransaction.Debit,
                                    Description = StaticResource.PurchaseVoucherCreated,
                                    IsDeleted = false
                                });

                                // Debit
                                transactions.Add(new VoucherTransactionsModel
                                {
                                    TransactionId = 0,
                                    VoucherNo = voucherDetail.VoucherNo,
                                    AccountNo = creditTransaction.ChartOfAccountNewId,
                                    Debit = creditTransaction.Credit,
                                    Credit = 0,
                                    Description = StaticResource.PurchaseVoucherCreated,
                                    IsDeleted = false
                                });

                                AddEditTransactionModel transactionVoucherDetail = new AddEditTransactionModel
                                {
                                    VoucherNo = voucherDetail.VoucherNo,
                                    VoucherTransactions = transactions
                                };

                                var responseTransaction = AddEditTransactionList(transactionVoucherDetail, model.CreatedById);

                                if (responseTransaction.StatusCode == 200)
                                {
                                    response.StatusCode = StaticResource.successStatusCode;
                                    response.Message = StaticResource.SuccessText;
                                }
                                else
                                {
                                    throw new Exception(responseTransaction.Message);
                                }


                                response.StatusCode = StaticResource.successStatusCode;
                                response.Message = "Success";
                            }
                            else
                            {
                                throw new Exception(" Voucher Not Found on Verified Purchase");
                            }
                        }
                        else
                        {
                            await _uow.StoreItemPurchaseRepository.UpdateAsyn(purchaseRecord);
                        }
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Record cannot be updated";
                        return response;
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Model values are inappropriate";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
                return response;
            }
            return response;
        }

        /// <summary>
        /// Addition of Employee Pension Payment
        /// </summary>
        /// <param name="OfficeId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddEmployeePensionPayment(EmployeePensionPaymentModel EmployeePensionPayment)
        {
            APIResponse response = new APIResponse();

            try
            {
                var officeCode = _uow.OfficeDetailRepository.FindAsync(o => o.OfficeId == EmployeePensionPayment.OfficeId).Result.OfficeCode; //use OfficeCode
                var financialYear = await _uow.GetDbContext().FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDefault == true && x.IsDeleted == false);
                var EmployeeDetails = await _uow.GetDbContext().EmployeeDetail.FirstOrDefaultAsync(x => x.EmployeeID == EmployeePensionPayment.EmployeeId && x.IsDeleted == false);
                PensionDebitAccountMaster pensionDebitAccountMaster = await _uow.GetDbContext().PensionDebitAccountMaster.FirstOrDefaultAsync(x => x.IsDeleted == false);

                if (pensionDebitAccountMaster == null)
                {
                    throw new Exception("Pension Debit Account Not Set");
                }


                #region "Generate Voucher"
                VoucherDetailModel voucherModel = new VoucherDetailModel
                {
                    CurrencyId = EmployeePensionPayment.CurrencyId.Value,
                    Description = string.Format(StaticResource.PensionPaymentCreated, DateTime.Now.Date, EmployeeDetails.EmployeeName),
                    JournalCode = EmployeePensionPayment.JournalCode,
                    VoucherTypeId = EmployeePensionPayment.VoucherTypeId,
                    OfficeId = EmployeePensionPayment.OfficeId,
                    IsExchangeGainLossVoucher = false,
                    CreatedById = EmployeePensionPayment.CreatedById,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    FinancialYearId = financialYear.FinancialYearId,
                    VoucherDate = DateTime.UtcNow,
                    TimezoneOffset= EmployeePensionPayment.TimezoneOffset
                };

                var responseVoucher = await AddVoucherNewDetail(voucherModel);
                #endregion

                if (responseVoucher.StatusCode == 200)
                {

                    List<VoucherTransactionsModel> transactions = new List<VoucherTransactionsModel>();

                    // Credit
                    transactions.Add(new VoucherTransactionsModel
                    {
                        TransactionId = 0,
                        VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                        AccountNo = EmployeePensionPayment.CreditAccount,
                        Debit = 0,
                        Credit = Convert.ToDouble(EmployeePensionPayment.PensionAmount),
                        Description = StaticResource.PensionPayment,
                        IsDeleted = false
                    });

                    // Debit
                    transactions.Add(new VoucherTransactionsModel
                    {
                        TransactionId = 0,
                        VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                        AccountNo = pensionDebitAccountMaster.ChartOfAccountNewId,
                        Debit = Convert.ToDouble(EmployeePensionPayment.PensionAmount),
                        Credit = 0,
                        Description = StaticResource.PensionPayment,
                        IsDeleted = false
                    });

                    AddEditTransactionModel transactionVoucherDetail = new AddEditTransactionModel
                    {
                        VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo,
                        VoucherTransactions = transactions
                    };

                    var responseTransaction = AddEditTransactionList(transactionVoucherDetail, EmployeePensionPayment.CreatedById);

                    if (responseTransaction.StatusCode == 200)
                    {

                        PensionPaymentHistory pensionPayments = new PensionPaymentHistory();
                        pensionPayments.PaymentDate = DateTime.Now;
                        pensionPayments.PaymentAmount = EmployeePensionPayment.PensionAmount;
                        pensionPayments.IsDeleted = false;
                        pensionPayments.CreatedById = EmployeePensionPayment.CreatedById;
                        pensionPayments.EmployeeId = EmployeePensionPayment.EmployeeId.Value;
                        pensionPayments.VoucherNo = responseVoucher.data.VoucherDetailEntity.VoucherNo;
                        pensionPayments.VoucherReferenceNo = responseVoucher.data.VoucherDetailEntity.ReferenceNo;

                        _uow.PensionPaymentHistoryRepository.Add(pensionPayments);

                        var user = await _uow.UserDetailsRepository.FindAsync(x => x.AspNetUserId == EmployeePensionPayment.CreatedById);

                        LoggerDetailsModel loggerObj = new LoggerDetailsModel();
                        loggerObj.NotificationId = (int)Common.Enums.LoggerEnum.VoucherCreated;
                        loggerObj.IsRead = false;
                        loggerObj.UserName = user.FirstName + " " + user.LastName;
                        loggerObj.UserId = EmployeePensionPayment.CreatedById;
                        loggerObj.LoggedDetail = "Voucher " + responseVoucher.data.VoucherDetailEntity.ReferenceNo + " Created";
                        loggerObj.CreatedDate = DateTime.Now;

                        response.LoggerDetailsModel = loggerObj;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = StaticResource.SuccessText;
                    }
                    else
                    {
                        throw new Exception(responseTransaction.Message);
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = responseVoucher.Message;
                }

                ////Creating Voucher for Voucher transaction
                //VoucherDetail obj = new VoucherDetail();
                //obj.CreatedById = EmployeePensionPayment.CreatedById;
                //obj.CreatedDate = DateTime.UtcNow;
                //obj.IsDeleted = false;
                //obj.FinancialYearId = financialYear.FinancialYearId;
                //obj.VoucherTypeId = EmployeePensionPayment.VoucherTypeId;
                //obj.Description = string.Format(StaticResource.PensionPaymentCreated, DateTime.Now.Date, EmployeeDetails.EmployeeName);
                //obj.CurrencyId = EmployeePensionPayment.CurrencyId;
                //obj.VoucherDate = DateTime.Now;
                //obj.JournalCode = EmployeePensionPayment.JournalId;
                //obj.OfficeId = EmployeePensionPayment.OfficeId;

                //await _uow.VoucherDetailRepository.AddAsyn(obj);

                //obj.ReferenceNo = officeCode + "-" + obj.VoucherNo;
                //await _uow.VoucherDetailRepository.UpdateAsyn(obj);

                //List<VoucherTransactions> VoucherTransactionsList = new List<VoucherTransactions>();

                //VoucherTransactions xVoucherTransactionCredit = new VoucherTransactions();
                //VoucherTransactions xVoucherTransactionDebit = new VoucherTransactions();

                ////Creating Voucher Transaction for Credit
                //xVoucherTransactionCredit.IsDeleted = false;
                //xVoucherTransactionCredit.VoucherNo = obj.VoucherNo;
                //xVoucherTransactionCredit.FinancialYearId = financialYear.FinancialYearId;
                //xVoucherTransactionCredit.ChartOfAccountNewId = EmployeePensionPayment.CreditAccount;
                //xVoucherTransactionCredit.CreditAccount = EmployeePensionPayment.CreditAccount;
                //xVoucherTransactionCredit.Credit = Convert.ToDouble(EmployeePensionPayment.PensionAmount);
                //xVoucherTransactionCredit.Debit = 0;
                //xVoucherTransactionCredit.CurrencyId = EmployeePensionPayment.CurrencyId;
                //xVoucherTransactionCredit.Description = string.Format(StaticResource.PensionPaymentCreated, DateTime.Now.Date, EmployeeDetails.EmployeeName); ;
                //xVoucherTransactionCredit.OfficeId = EmployeePensionPayment.OfficeId;

                //VoucherTransactionsList.Add(xVoucherTransactionCredit);

                ////Creating Voucher Transaction for Debit
                //xVoucherTransactionDebit.IsDeleted = false;
                //xVoucherTransactionDebit.VoucherNo = obj.VoucherNo;
                //xVoucherTransactionDebit.FinancialYearId = financialYear.FinancialYearId;
                //xVoucherTransactionDebit.Debit = Convert.ToDouble(EmployeePensionPayment.PensionAmount);
                //xVoucherTransactionDebit.Credit = 0;
                //xVoucherTransactionDebit.ChartOfAccountNewId = EmployeePensionPayment.DebitAccount;
                //xVoucherTransactionDebit.DebitAccount = EmployeePensionPayment.DebitAccount;
                //xVoucherTransactionDebit.CreditAccount = 0;
                //xVoucherTransactionDebit.CurrencyId = EmployeePensionPayment.CurrencyId;
                //xVoucherTransactionDebit.Description = string.Format(StaticResource.PensionPaymentCreated, DateTime.Now.Date, EmployeeDetails.EmployeeName); ;
                //xVoucherTransactionDebit.OfficeId = EmployeePensionPayment.OfficeId;

                //VoucherTransactionsList.Add(xVoucherTransactionDebit);

                ////save voucher transactions to db
                //await _uow.GetDbContext().AddRangeAsync(VoucherTransactionsList);
                //await _uow.GetDbContext().SaveChangesAsync();

                //Adding details to Pension Payment History Table
               

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
