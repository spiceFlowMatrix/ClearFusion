using AutoMapper;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.CommonServices
{
    public class StoreServices: IStoreServices
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public StoreServices(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ApiResponse AddEditTransactionList(AddEditTransactionModel voucherTransactions, string userId)
        {
            ApiResponse response = new ApiResponse();

            List<VoucherTransactions> transactionsListAdd = new List<VoucherTransactions>();
            List<VoucherTransactions> transactionsListEdit = new List<VoucherTransactions>();

            try
            {
                if (voucherTransactions.VoucherTransactions.Any())
                {

                    var editList = voucherTransactions.VoucherTransactions.Where(w => w.TransactionId != 0)
                                                          .Select(s => s.TransactionId);

                    var editTransactionList = _dbContext.VoucherTransactions
                                                                 .Where(x => editList
                                                                            .Contains(x.TransactionId))
                                                                 .ToList();

                    var voucherDetail = _dbContext.VoucherDetail.FirstOrDefault(x => x.IsDeleted == false && x.VoucherNo == voucherTransactions.VoucherNo);

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
                                transaction.JobId = item.JobId == 0 ? null : item.JobId;

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
                                    transaction.JobId = item.JobId == 0 ? null : item.JobId;
                                    transaction.CurrencyId = voucherDetail.CurrencyId;
                                    transaction.TransactionDate = voucherDetail.VoucherDate;
                                    transaction.ModifiedById = userId;
                                    transaction.ModifiedDate = DateTime.Now;
                                    //transaction.VoucherNo = voucherTransactions.VoucherNo;

                                    transactionsListEdit.Add(transaction);
                                }
                            }
                        }

                        using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
                        {
                            try
                            {
                                _dbContext.VoucherTransactions.AddRange(transactionsListAdd);
                                _dbContext.VoucherTransactions.UpdateRange(transactionsListEdit);

                                _dbContext.SaveChanges();
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

        public async Task<ApiResponse> AddVoucherNewDetail(VoucherDetailModel model)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                model.TimezoneOffset = model.TimezoneOffset > 0 ? model.TimezoneOffset * -1 : Math.Abs(model.TimezoneOffset.Value);

                DateTime filterVoucherDate = model.VoucherDate.AddMinutes(model.TimezoneOffset.Value);

                Task<List<CurrencyDetails>> currencyListTask = _dbContext.CurrencyDetails.Where(x => x.IsDeleted == false).ToListAsync();

                if (model.IsExchangeGainLossVoucher)
                {
                    model.VoucherDate = DateTime.UtcNow;
                }

                Task<List<ExchangeRateDetail>> exchangeRatePresentTask = _dbContext.ExchangeRateDetail.Where(x => x.Date.Date == model.VoucherDate.Date && x.IsDeleted == false).ToListAsync();

                List<CurrencyDetails> currencyList = await currencyListTask;

                List<int> currencyIds = currencyList.Select(x => x.CurrencyId).ToList();

                string currencyCode = currencyList.FirstOrDefault(x => x.CurrencyId == model.CurrencyId).CurrencyCode;

                List<ExchangeRateDetail> exchangeRatePresent = await exchangeRatePresentTask;

                if (CheckExchangeRateIsPresent(currencyIds, exchangeRatePresent))
                {
                    var officeDetail = await _dbContext.OfficeDetail.FirstOrDefaultAsync(o => o.OfficeId == model.OfficeId); //use OfficeCode

                    if (officeDetail != null)
                    {
                        Task<FinancialYearDetail> fincancialYearTask = _dbContext.FinancialYearDetail.FirstOrDefaultAsync(o => o.IsDefault);
                        Task<CurrencyDetails> currencyDetailTask = _dbContext.CurrencyDetails.FirstOrDefaultAsync(o => o.CurrencyId == model.CurrencyId);
                        // NOTE: Dont remove this as we will need journal details in response
                        Task<JournalDetail> journaldetailTask = _dbContext.JournalDetail.FirstOrDefaultAsync(o => o.JournalCode == model.JournalCode);
                        int voucherCount = await _dbContext.VoucherDetail.Where(x => x.VoucherDate.Month == model.VoucherDate.Month && x.VoucherDate.Year == filterVoucherDate.Year && x.OfficeId == model.OfficeId && x.CurrencyId == model.CurrencyId).CountAsync();

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
                                        sameVoucherReferenceNoCount = await _dbContext.VoucherDetail.Where(x => x.ReferenceNo == referenceNo).CountAsync();

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

                                await _dbContext.VoucherDetail.AddAsync(obj);

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

        public bool CheckExchangeRateIsPresent(List<int> currencyList, List<ExchangeRateDetail> exchangeRates)
        {
            var groupedDataCount = exchangeRates.GroupBy(x => new { x.FromCurrency, x.ToCurrency }).ToList().Count;
            if (groupedDataCount >= (int)Math.Pow(currencyList.Count(), 2))
            {
                return true;
            }
            return false;
        }
    }
}