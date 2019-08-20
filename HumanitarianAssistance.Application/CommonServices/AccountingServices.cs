using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Accounting.Commands.Common;
using HumanitarianAssistance.Application.Accounting.Commands.Create;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.CommonServices
{
    public class AccountingServices : IAccountingServices
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountingServices(HumanitarianAssistanceDbContext dbContext, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        #region "Voucher"

        public async Task<VoucherDetailEntityModel> AddVoucherDetail(AddVoucherDetailCommand request)
        {
            try
            {
                request.TimezoneOffset = request.TimezoneOffset > 0 ? request.TimezoneOffset * -1 : Math.Abs(request.TimezoneOffset.Value);

                DateTime filterVoucherDate = request.VoucherDate.AddMinutes(request.TimezoneOffset.Value);

                Task<List<CurrencyDetails>> currencyListTask = _dbContext.CurrencyDetails.Where(x => x.IsDeleted == false).ToListAsync();

                if (request.IsExchangeGainLossVoucher)
                {
                    request.VoucherDate = DateTime.UtcNow;
                }

                Task<List<ExchangeRateDetail>> exchangeRatePresentTask = _dbContext.ExchangeRateDetail.Where(x => x.Date.Date == request.VoucherDate.Date && x.IsDeleted == false).ToListAsync();

                List<CurrencyDetails> currencyList = await currencyListTask;

                List<int> currencyIds = currencyList.Select(x => x.CurrencyId).ToList();

                string currencyCode = currencyList.FirstOrDefault(x => x.CurrencyId == request.CurrencyId).CurrencyCode;

                List<ExchangeRateDetail> exchangeRatePresent = await exchangeRatePresentTask;

                if (!CheckExchangeRateIsPresent(currencyIds, exchangeRatePresent))
                {
                    throw new Exception(StaticResource.ExchagneRateNotDefined);
                }

                var officeDetail = await _dbContext.OfficeDetail.FirstOrDefaultAsync(o => o.OfficeId == request.OfficeId); //use OfficeCode

                if (officeDetail == null)
                {
                    throw new Exception(StaticResource.officeCodeNotFound);
                }

                Task<FinancialYearDetail> financialYearTask = _dbContext.FinancialYearDetail.FirstOrDefaultAsync(o => o.IsDefault);
                Task<CurrencyDetails> currencyDetailTask = _dbContext.CurrencyDetails.FirstOrDefaultAsync(o => o.CurrencyId == request.CurrencyId);

                // NOTE: Dont remove this as we will need journal details in response
                Task<JournalDetail> journaldetailTask = _dbContext.JournalDetail.FirstOrDefaultAsync(o => o.JournalCode == request.JournalCode);
                int voucherCount = await _dbContext.VoucherDetail.Where(x => x.VoucherDate.Month == request.VoucherDate.Month && x.VoucherDate.Year == filterVoucherDate.Year && x.OfficeId == request.OfficeId && x.CurrencyId == request.CurrencyId).CountAsync();

                FinancialYearDetail financialYear = await financialYearTask;

                if (financialYear == null)
                {
                    throw new Exception(StaticResource.defaultFinancialYearIsNotSet);
                }

                CurrencyDetails currencyDetail = await currencyDetailTask;

                if (currencyDetail == null)
                {
                    throw new Exception(StaticResource.CurrencyNotFound);
                }

                JournalDetail journaldetail = await journaldetailTask;

                VoucherDetail obj = _mapper.Map<VoucherDetail>(request);
                obj.JournalCode = journaldetail != null ? journaldetail.JournalCode : request.JournalCode;
                obj.FinancialYearId = financialYear.FinancialYearId;
                obj.CreatedById = request.CreatedById;
                obj.VoucherDate = request.VoucherDate;
                obj.CreatedDate = DateTime.UtcNow;
                obj.IsDeleted = false;

                // Pattern: Office Code - Currency Code - Month Number - voucher count on selected month - Year
                string referenceNo = AccountingUtility.GenerateVoucherReferenceCode(request.VoucherDate, voucherCount, currencyDetail.CurrencyCode, officeDetail.OfficeCode);

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
                            // VoucherDetail voucherDetail = _dbContext.VoucherDetail.OrderByDescending(x => x.VoucherDate).FirstOrDefault(x => x.VoucherDate.Month == filterVoucherDate.Month && x.OfficeId == request.OfficeId && x.VoucherDate.Year == filterVoucherDate.Year);

                            var refNo = referenceNo.Split('-');
                            int count = Convert.ToInt32(refNo[3]);
                            referenceNo = AccountingUtility.GenerateVoucherReferenceCode(request.VoucherDate, count, currencyCode, officeDetail.OfficeCode);
                        }
                    }
                    while (sameVoucherReferenceNoCount != 0);
                }

                await _dbContext.VoucherDetail.AddAsync(obj);
                await _dbContext.SaveChangesAsync();

                VoucherDetailEntityModel voucherModel = _mapper.Map<VoucherDetail, VoucherDetailEntityModel>(obj);

                return voucherModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
        /// <summary>
        /// Delete Voucher
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> DeleteVoucher(long voucherId)
        {
            bool isVoucherDeleted = false;

            try
            {
                var voucherdetail = await _dbContext.VoucherDetail.FirstOrDefaultAsync(c => c.VoucherNo == voucherId);

                if (voucherdetail != null)
                {
                    voucherdetail.IsDeleted = true;
                    // voucherdetail.ModifiedById = userId;
                    voucherdetail.ModifiedDate = DateTime.UtcNow;

                    _dbContext.VoucherDetail.Update(voucherdetail);
                    await _dbContext.SaveChangesAsync();

                    await DeleteTransaction(voucherId, "");

                    isVoucherDeleted = true;
                }
                else
                {
                    isVoucherDeleted = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                isVoucherDeleted = false;
            }
            return isVoucherDeleted;
        }

        #endregion


        #region "Transaction"

        /// <summary>
        /// Add/Edit Transaction
        /// </summary>
        /// <param name="request"></param>
        /// <param name="_dbContext"></param>
        /// <returns>true/Exception</returns>
        public bool AddEditTransactionList(AddEditTransactionListCommand request)
        {

            List<VoucherTransactions> transactionsListAdd = new List<VoucherTransactions>();
            List<VoucherTransactions> transactionsListEdit = new List<VoucherTransactions>();

            try
            {
                if (!request.VoucherTransactions.Any())
                {
                    throw new Exception(StaticResource.NoTransactionToUpDate);
                }

                var editList = request.VoucherTransactions.Where(w => w.TransactionId != 0)
                                                          .Select(s => s.TransactionId);

                var editTransactionList = _dbContext.VoucherTransactions
                                                             .Where(x => editList.Contains(x.TransactionId))
                                                             .ToList();

                var voucherDetail = _dbContext.VoucherDetail.FirstOrDefault(x => x.IsDeleted == false && x.VoucherNo == request.VoucherNo);

                if (voucherDetail == null)
                {
                    throw new Exception(StaticResource.VoucherNotPresent);
                }

                foreach (VoucherTransactionsModel item in request.VoucherTransactions)
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
                        transaction.CreatedById = request.ModifiedById;
                        transaction.CreatedDate = DateTime.UtcNow;
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
                            transaction.ModifiedById = request.ModifiedById;
                            transaction.ModifiedDate = DateTime.UtcNow;

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

                        throw new Exception(ex.Message);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Delete Transaction
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns>Success/failure</returns>
        public async Task<bool> DeleteTransaction(long voucherId, string userId)
        {
            bool transactionDeleted = false;

            using (var _dbTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var transactions = await _dbContext.VoucherTransactions.Where(x => x.VoucherNo == voucherId).ToListAsync();
                    if (transactions.Any())
                    {
                        transactions.ForEach(x =>
                        {
                            x.IsDeleted = true;
                            x.ModifiedDate = DateTime.UtcNow;
                            x.ModifiedById = userId;
                        });

                        _dbContext.VoucherTransactions.UpdateRange(transactions);
                        _dbContext.SaveChanges();
                        _dbTransaction.Commit();

                        transactionDeleted = true;
                    }
                    else
                    {
                        throw new Exception(StaticResource.TransactionNotFound);
                    }
                }
                catch (Exception ex)
                {
                    _dbTransaction.Rollback();
                    Console.WriteLine(ex.Message);
                    transactionDeleted = false;
                }
            }
            return transactionDeleted;
        }

        #endregion


        public async Task<bool> ReverseEmployeeSalaryVoucher(ReverseEmployeeSalaryVoucherCommand request)
        {
            VoucherDetail voucherDetail = new VoucherDetail();

            try
            {
                //Retrieving the list of transactions based on voucher no
                List<VoucherTransactions> voucherTransactionDetails = await _dbContext.VoucherTransactions
                                                                                      .Where(x => x.IsDeleted == false &&
                                                                                                  x.VoucherNo == request.VoucherNo)
                                                                                      .ToListAsync();

                //var exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.OfficeId == 16).OrderByDescending(x => x.Date).ToListAsync();

                if (voucherTransactionDetails.Any())
                {
                    //looping each transaction and reversing it.
                    foreach (VoucherTransactions transaction in voucherTransactionDetails)
                    {
                        VoucherTransactions reverseTransactions = new VoucherTransactions();

                        reverseTransactions.Debit = transaction.Credit;
                        reverseTransactions.CreditAccount = transaction.DebitAccount;
                        reverseTransactions.DebitAccount = transaction.CreditAccount;
                        reverseTransactions.Credit = transaction.Debit;
                        reverseTransactions.CurrencyId = transaction.CurrencyId;
                        reverseTransactions.FinancialYearId = transaction.FinancialYearId;
                        reverseTransactions.ChartOfAccountNewId = transaction.ChartOfAccountNewId;
                        reverseTransactions.OfficeId = transaction.OfficeId;
                        reverseTransactions.VoucherNo = transaction.VoucherNo;
                        reverseTransactions.IsDeleted = false;
                        reverseTransactions.TransactionDate = voucherDetail.VoucherDate;

                        await _dbContext.VoucherTransactions.AddAsync(reverseTransactions);
                        await _dbContext.SaveChangesAsync();

                        //APIResponse apiResponse = await AddVoucherTransactionConvertedToExchangeRate(reverseTransactions, exchangeRate);
                    }
                }

                //Getting the Salary Payment history record and updating the flag isSalaryReversed to true
                var employeeSalaryPaymentHistory = await _dbContext.EmployeeSalaryPaymentHistory
                                                                   .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                             x.IsSalaryReverse == false &&
                                                                                             x.VoucherNo == request.VoucherNo);
                employeeSalaryPaymentHistory.IsSalaryReverse = true;

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddRole(string RoleName)
        {

            IdentityResult identityResult = new IdentityResult();

            try
            {
                var roleExists = await _roleManager.FindByNameAsync(RoleName);

                if (roleExists == null)
                {
                    var role = new IdentityRole();
                    role.Name = RoleName;
                    identityResult = await _roleManager.CreateAsync(role);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return identityResult.Succeeded;
        }

    }
}