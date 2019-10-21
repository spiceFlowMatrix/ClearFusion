using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using DataAccess.DbEntities.AccountingNew;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using HumanitarianAssistance.ViewModels.SPModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Service.Classes.AccountingNew
{
    public class AccountBalanceService : IAccountBalance
    {
        private IUnitOfWork _uow;
        private UserManager<AppUser> _userManager;
        private IMapper _mapper;

        public AccountBalanceService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        // Generate view models for key value pairs of accounts(full objects) and their balances.
        // Dictionaries cannot be converted to json objects properly by the framework so use
        // this helper function to prepare your account balances for json.
        private List<AccountBalance> GenerateBalanceViewModels(Dictionary<ChartOfAccountNew, double> rawBalances)
        {
            List<AccountBalance> vmBalances = new List<AccountBalance>();
            foreach (var balance in rawBalances)
            {
                var iVmBalance = new AccountBalance
                {
                    AccountId = balance.Key.ChartOfAccountNewId,
                    AccountName = balance.Key.AccountName,
                    Balance = new Decimal(balance.Value),
                    AccountCode = balance.Key.ChartOfAccountNewCode
                };
                vmBalances.Add(iVmBalance);
            }

            return vmBalances;
        }

        public async Task<APIResponse> GetNoteBalancesByHeadType(int headTypeId, int toCurrency, DateTime reportDate)
        {
            APIResponse response = new APIResponse();

            try
            {
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => x.IsDeleted == false && x.AccountHeadTypeId == headTypeId && x.AccountLevelId == (int)AccountLevels.InputLevel)
                    .Include(x => x.AccountType)
                    .ToListAsync();

                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var accountBalances = await GetAccountBalances(inputLevelList, reportDate, toCurrency);

                var notes = inputLevelList.Select(x => x.AccountType).Distinct().ToList();
                List<NoteAccountBalances> noteAccountBalances = new List<NoteAccountBalances>();

                foreach (var note in notes)
                {
                    var currNoteBalances = accountBalances.Where(x => x.Key.AccountTypeId == note.AccountTypeId).ToDictionary(x => x.Key, x => x.Value);

                    var vmNoteBalances = GenerateBalanceViewModels(currNoteBalances);

                    var currNoteAccountBalances = new NoteAccountBalances();

                    currNoteAccountBalances.NoteId = note.AccountTypeId;
                    currNoteAccountBalances.NoteName = note.AccountTypeName;
                    currNoteAccountBalances.NoteHeadId = note.AccountHeadTypeId;
                    currNoteAccountBalances.AccountBalances = vmNoteBalances;
                    noteAccountBalances.Add(currNoteAccountBalances);
                }

                response.data.NoteAccountBalances = noteAccountBalances;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<APIResponse> GetNoteBalancesByHeadType(int headTypeId, int toCurrencyId,
            DateTime reportStartDate, DateTime reportEndDate)
        {
            APIResponse response = new APIResponse();

            try
            {
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => x.AccountHeadTypeId == headTypeId && x.AccountLevelId == (int)AccountLevels.InputLevel)
                    .Include(x => x.AccountType)
                    .ToListAsync();


                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var accountBalances = await GetAccountBalances(inputLevelList, toCurrencyId, reportEndDate, reportEndDate);

                var notes = inputLevelList.Select(x => x.AccountType).Distinct().ToList();
                List<NoteAccountBalances> noteAccountBalances = new List<NoteAccountBalances>();

                foreach (var note in notes)
                {
                    var currNoteBalances = accountBalances.Where(x => x.Key.AccountTypeId == note.AccountTypeId).ToDictionary(x => x.Key, x => x.Value);

                    var vmNoteBalances = GenerateBalanceViewModels(currNoteBalances);

                    var currNoteAccountBalances = new NoteAccountBalances();

                    currNoteAccountBalances.NoteId = note.AccountTypeId;
                    currNoteAccountBalances.NoteName = note.AccountTypeName;
                    currNoteAccountBalances.AccountBalances = vmNoteBalances;
                    currNoteAccountBalances.NoteHeadId = note.AccountHeadTypeId;
                    noteAccountBalances.Add(currNoteAccountBalances);
                }

                response.data.NoteAccountBalances = noteAccountBalances;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<APIResponse> GetAccountBalancesById(List<long> accountIds, int toCurrencyId,
            DateTime reportDate)
        {
            APIResponse response = new APIResponse();

            try
            {
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();


                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var accountBalances = await GetAccountBalances(inputLevelList, reportDate, toCurrencyId);
                var vmNoteBalances = GenerateBalanceViewModels(accountBalances);

                response.data.AccountBalances = vmNoteBalances;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<APIResponse> GetAccountBalancesById(List<long> accountIds, DateTime transactionExchangeDate, int toCurrencyId,
            DateTime reportDate)
        {
            APIResponse response = new APIResponse();

            try
            {
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();


                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var accountBalances = await GetAccountBalances(inputLevelList, reportDate, transactionExchangeDate, toCurrencyId);
                var vmNoteBalances = GenerateBalanceViewModels(accountBalances);

                response.data.AccountBalances = vmNoteBalances;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<APIResponse> GetAccountBalancesById(List<long> accountIds, int toCurrencyId,
            DateTime reportStartDate, DateTime reportEndDate)
        {
            APIResponse response = new APIResponse();

            try
            {
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();


                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var accountBalances = await GetAccountBalances(inputLevelList, toCurrencyId, reportStartDate, reportEndDate);
                var vmNoteBalances = GenerateBalanceViewModels(accountBalances);

                response.data.AccountBalances = vmNoteBalances;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<APIResponse> GetAccountBalancesById(List<long> accountIds, DateTime transactionExchangeDate, int toCurrencyId,
            DateTime reportStartDate, DateTime reportEndDate)
        {
            APIResponse response = new APIResponse();

            try
            {
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();


                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var accountBalances = await GetAccountBalances(inputLevelList, toCurrencyId, reportStartDate, reportEndDate, transactionExchangeDate);
                var vmNoteBalances = GenerateBalanceViewModels(accountBalances);

                response.data.AccountBalances = vmNoteBalances;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        private async Task<List<VoucherTransactions>> GetAccountTransactions(List<ChartOfAccountNew> inputLevelAccounts, DateTime endDate)
        {
            return await _uow.GetDbContext().VoucherTransactions
                .Where(x => x.TransactionDate != null ? x.TransactionDate.Value.Date <= endDate.Date : x.TransactionDate <= endDate
                            && inputLevelAccounts.Select(y => y.ChartOfAccountNewId).Contains((long)x.ChartOfAccountNewId)
                            && x.IsDeleted == false
                            && x.ChartOfAccountNewId != null)
                .Include(x => x.ChartOfAccountDetail)
                .ToListAsync();
        }

        private async Task<List<VoucherTransactions>> GetAccountTransactions(List<ChartOfAccountNew> inputLevelAccounts, DateTime startDate,
            DateTime endDate)
        {
            return await _uow.GetDbContext().VoucherTransactions
                .Where(x => x.TransactionDate != null ? x.TransactionDate.Value.Date <= endDate.Date : x.TransactionDate <= endDate
                            && x.TransactionDate != null ? x.TransactionDate.Value.Date >= startDate.Date : x.TransactionDate >= startDate
                            && inputLevelAccounts.Select(y => y.ChartOfAccountNewId).Contains((long)x.ChartOfAccountNewId)
                            && x.IsDeleted == false
                            && x.ChartOfAccountNewId != null)
                .Include(x => x.ChartOfAccountDetail)
                .ToListAsync();
        }

        private double DetermineTransactionExrate(VoucherTransactions transaction,
            List<ExchangeRateDetail> rates, int toCurrencyId)
        {
            double xExchangeRate = 0.0;
            if (transaction.CurrencyId == toCurrencyId)
                xExchangeRate = 1.0;
            else
            {
                if (transaction.TransactionDate == null)
                    throw new Exception("Transaction date is not set");
                if (transaction.CurrencyId == null)
                    throw new Exception("Transaction currency is not set");
                var interxExchangeRate = rates.OrderByDescending(x => x.Date)
                    .FirstOrDefault(x => x.Date <= transaction.TransactionDate.GetValueOrDefault()
                                         && x.FromCurrency == transaction.CurrencyId
                                         && x.ToCurrency == toCurrencyId);
                if (interxExchangeRate == null)
                    throw new Exception("No valid exchange rate exists for the given report's currency");
                xExchangeRate = (double)interxExchangeRate.Rate;
            }

            return xExchangeRate;

        }

        private double DetermineTransactionExrate(VoucherTransactions transaction,
            List<ExchangeRateDetail> rates, int toCurrencyId, DateTime onDate)
        {
            double xExchangeRate = 0.0;
            if (transaction.CurrencyId == toCurrencyId)
                xExchangeRate = 1.0;
            else
            {
                if (transaction.TransactionDate == null)
                    throw new Exception("Transaction date is not set");
                if (transaction.CurrencyId == null)
                    throw new Exception("Transaction currency is not set");
                var interxExchangeRate = rates.OrderByDescending(x => x.Date)
                    .FirstOrDefault(x => x.Date <= onDate
                                         && x.FromCurrency == transaction.CurrencyId
                                         && x.ToCurrency == toCurrencyId);
                if (interxExchangeRate == null)
                    throw new Exception("No valid exchange rate exists for the given report's currency");
                xExchangeRate = (double)interxExchangeRate.Rate;
            }

            return xExchangeRate;

        }

        // Value after exchange on the transaction date
        private async Task<List<VoucherTransactions>> GetTransactionValuesAfterExchange(List<VoucherTransactions> transactions, int toCurrencyId)
        {
            var ratesQuery = _uow.GetDbContext().ExchangeRateDetail.Where(x => x.ToCurrency == toCurrencyId
                                                                               && transactions.Select(y => y.CurrencyId).Contains(x.FromCurrency)
                                                                               && transactions.Select(y => y.TransactionDate).Any(z => z >= x.Date));
            var ratesList = await ratesQuery.ToListAsync();

            List<VoucherTransactions> outputTransactions = new List<VoucherTransactions>();

            foreach (var transaction in transactions)
            {
                var rate = DetermineTransactionExrate(transaction, ratesList, toCurrencyId);

                var outputTransaction = (transaction);
                outputTransaction.Credit = rate * transaction.Credit;
                outputTransaction.Debit = rate * transaction.Debit;
                outputTransactions.Add(outputTransaction);
            }

            return outputTransactions;
        }

        // Value after exchange on the given onDate
        private async Task<List<VoucherTransactions>> GetTransactionValuesAfterExchange(List<VoucherTransactions> transactions, int toCurrencyId, DateTime onDate)
        {
            if (!transactions.Any())
            {
                throw new Exception("Transaction not found");
            }

            var ratesQuery = _uow.GetDbContext().ExchangeRateDetail.Where(x => x.ToCurrency == toCurrencyId
                                                                               && transactions.Select(y => y.CurrencyId).Contains(x.FromCurrency)
                                                                               && x.Date.ToShortDateString() == onDate.ToShortDateString());
            var ratesList = await ratesQuery.ToListAsync();

            if (!ratesList.Any())
            {
                throw new Exception("Exchange Rate Not Defined On Selected Comparision Date");
            }

            List<VoucherTransactions> outputTransactions = new List<VoucherTransactions>();

            foreach (var transaction in transactions)
            {
                var rate = DetermineTransactionExrate(transaction, ratesList, toCurrencyId, onDate);

                var outputTransaction = (transaction);
                outputTransaction.Credit = rate * transaction.Credit;
                outputTransaction.Debit = rate * transaction.Debit;
                outputTransactions.Add(outputTransaction);
            }

            return outputTransactions;
        }

        private Dictionary<ChartOfAccountNew, double> CalculateAccountBalances(
            List<ChartOfAccountNew> inputLevelAccounts, List<VoucherTransactions> accountTransactions)
        {
            Dictionary<ChartOfAccountNew, double> accountBalances = new Dictionary<ChartOfAccountNew, double>();

            foreach (var account in inputLevelAccounts)
            {
                var currAccountTransactions =
                    accountTransactions.Where(x => x.ChartOfAccountNewId == account.ChartOfAccountNewId).ToList();
                var totalCredits = currAccountTransactions.Select(x => x.Credit.GetValueOrDefault()).Sum();
                var totalDebits = currAccountTransactions.Select(x => x.Debit.GetValueOrDefault()).Sum();
                if (account.IsCreditBalancetype.GetValueOrDefault())
                    accountBalances.Add(account, totalCredits - totalDebits);
                else
                    accountBalances.Add(account, totalDebits - totalCredits);
            }

            return accountBalances;
        }

        // thhis override calculates transaction credit/debit value after exchange on the transaction date
        private async Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts, DateTime transactionsTillDate,
            int toCurrencyId)
        {
            var transactions = await GetAccountTransactions(inputLevelAccounts, transactionsTillDate);
            var exchangeValuedTransactions = await GetTransactionValuesAfterExchange(transactions, toCurrencyId);

            return CalculateAccountBalances(inputLevelAccounts, exchangeValuedTransactions);
        }

        // this override calculates transaction credit/debit values after exchange based on the given transactionCompareDate
        private async Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts, DateTime transactionsTillDate,
            DateTime transactionExchangeDate, int toCurrencyId)
        {
            var transactions = await GetAccountTransactions(inputLevelAccounts, transactionsTillDate);
            var exchangeValuedTransactions = await GetTransactionValuesAfterExchange(transactions, toCurrencyId, transactionExchangeDate);

            return CalculateAccountBalances(inputLevelAccounts, exchangeValuedTransactions);
        }

        private async Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts,
            int toCurrencyId, DateTime transactionsStartingFrom, DateTime transactionsUntil)
        {
            var transactions = await GetAccountTransactions(inputLevelAccounts, transactionsStartingFrom, transactionsUntil);
            var exchangeValuedTransactions = await GetTransactionValuesAfterExchange(transactions, toCurrencyId);

            return CalculateAccountBalances(inputLevelAccounts, exchangeValuedTransactions);
        }

        private async Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts,
            int toCurrencyId, DateTime transactionsStartingFrom, DateTime transactionsUntil, DateTime transactionExchangeDate)
        {
            var transactions = await GetAccountTransactions(inputLevelAccounts, transactionsStartingFrom, transactionsUntil);
            var exchangeValuedTransactions = await GetTransactionValuesAfterExchange(transactions, toCurrencyId, transactionExchangeDate);

            return CalculateAccountBalances(inputLevelAccounts, exchangeValuedTransactions);
        }

        private async Task<List<VoucherTransactions>> GetAccountTransactions(List<ChartOfAccountNew> inputLevelAccounts, DateTime startDate,
            DateTime endDate, List<int?> journalList, List<int?> officeList, List<long?> projectIdList)
        {
            var data = await _uow.GetDbContext().VoucherTransactions
                .Where(x => x.TransactionDate <= endDate
                            && x.TransactionDate >= startDate
                            && inputLevelAccounts.Select(y => y.ChartOfAccountNewId).Contains((long)x.ChartOfAccountNewId)
                            && x.IsDeleted == false
                            && x.ChartOfAccountNewId != null
                            && officeList.Contains(x.VoucherDetails.OfficeId)
                            && journalList.Contains(x.VoucherDetails.JournalCode)
                            //&& projectIdList.Contains(x.VoucherDetails.ProjectId)
                            && projectIdList.Contains(x.ProjectId)
                            )
                .Include(x => x.ChartOfAccountDetail)
                .Include(x => x.VoucherDetails)
                .ToListAsync();
            return data;
        }

        private async Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts,
           int toCurrencyId, DateTime transactionsStartingFrom, DateTime transactionsUntil, List<int?> journalList, List<int?> officeList, List<long?> projectIdList)
        {
            var transactions = await GetAccountTransactions(inputLevelAccounts, transactionsStartingFrom, transactionsUntil, journalList, officeList, projectIdList);
            var exchangeValuedTransactions = await GetTransactionValuesAfterExchange(transactions, toCurrencyId);

            return CalculateAccountBalances(inputLevelAccounts, exchangeValuedTransactions);
        }

        public async Task<APIResponse> GetAccountBalancesById(List<long?> accountIds, int toCurrencyId,
            DateTime reportStartDate, DateTime reportEndDate, List<int?> journalList, List<int?> officeList, List<long?> projectIdList)
        {
            APIResponse response = new APIResponse();

            try
            {
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();


                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var accountBalances = await GetAccountBalances(inputLevelList, toCurrencyId, reportStartDate, reportEndDate, journalList, officeList, projectIdList);
                var vmNoteBalances = GenerateBalanceViewModels(accountBalances);

                response.data.AccountBalances = vmNoteBalances;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<APIResponse> GetAccountBalancesById(List<long?> accountIds, DateTime transactionExchangeDate, int toCurrencyId,
           DateTime reportStartDate, DateTime reportEndDate, List<int?> journalList, List<int?> officeList, List<long?> projectIdList)
        {
            APIResponse response = new APIResponse();

            try
            {
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();


                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var accountBalances = await GetAccountBalances(inputLevelList, toCurrencyId, reportStartDate, reportEndDate, transactionExchangeDate, journalList, officeList, projectIdList);
                var vmNoteBalances = GenerateBalanceViewModels(accountBalances);

                response.data.AccountBalances = vmNoteBalances;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        private async Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts,
            int toCurrencyId, DateTime transactionsStartingFrom, DateTime transactionsUntil, DateTime transactionExchangeDate, List<int?> journalList, List<int?> officeList, List<long?> projectIdList)
        {
            var transactions = await GetAccountTransactions(inputLevelAccounts, transactionsStartingFrom, transactionsUntil, journalList, officeList, projectIdList);
            var exchangeValuedTransactions = await GetTransactionValuesAfterExchange(transactions, toCurrencyId, transactionExchangeDate);

            return CalculateAccountBalances(inputLevelAccounts, exchangeValuedTransactions);
        }

        public async Task<APIResponse> GetExchangeGainLossReport(ExchangeGainLossFilterModel exchangeGainLossFilterModel)
        {
            // var dgff = await GetAllTransaction(exchangeGainLossFilterModel.AccountIdList, exchangeGainLossFilterModel.FromDate, exchangeGainLossFilterModel.ToDate, exchangeGainLossFilterModel.OfficeIdList, exchangeGainLossFilterModel.JournalIdList, exchangeGainLossFilterModel.ProjectIdList);

            List<ExchangeGainLossReportViewModel> exchangeGainLossReportData = new List<ExchangeGainLossReportViewModel>();

            APIResponse response = new APIResponse();

            if (exchangeGainLossFilterModel != null)
            {
                try
                {
                    var originalBalance = await GetAccountBalancesById(
                                                            exchangeGainLossFilterModel.AccountIdList,
                                                            exchangeGainLossFilterModel.ToCurrencyId,
                                                            exchangeGainLossFilterModel.FromDate,
                                                            exchangeGainLossFilterModel.ToDate,
                                                            exchangeGainLossFilterModel.JournalIdList,
                                                            exchangeGainLossFilterModel.OfficeIdList,
                                                            exchangeGainLossFilterModel.ProjectIdList
                                                );

                    var currentBalance = await GetAccountBalancesById(
                                                            exchangeGainLossFilterModel.AccountIdList,
                                                            exchangeGainLossFilterModel.ComparisionDate,
                                                            exchangeGainLossFilterModel.ToCurrencyId,
                                                            exchangeGainLossFilterModel.FromDate,
                                                            exchangeGainLossFilterModel.ToDate,
                                                            exchangeGainLossFilterModel.JournalIdList,
                                                            exchangeGainLossFilterModel.OfficeIdList,
                                                            exchangeGainLossFilterModel.ProjectIdList
                                               );

                    if (originalBalance.StatusCode == 200 && currentBalance.StatusCode == 200)
                    {
                        foreach (var balance in originalBalance.data.AccountBalances)
                        {
                            ExchangeGainLossReportViewModel exchangeGainLossReport = new ExchangeGainLossReportViewModel();
                            var currentDateBalance = currentBalance.data.AccountBalances.FirstOrDefault(x => x.AccountId == balance.AccountId);


                            exchangeGainLossReport.AccountCode = balance.AccountCode;
                            exchangeGainLossReport.AccountCodeName = balance.AccountCode + "-" + balance.AccountName;
                            exchangeGainLossReport.AccountName = balance.AccountName;
                            exchangeGainLossReport.BalanceOnCurrentDate = currentDateBalance.Balance;
                            exchangeGainLossReport.BalanceOnOriginalDate = balance.Balance;
                            exchangeGainLossReport.GainLossAmount = currentDateBalance.Balance - balance.Balance;
                            exchangeGainLossReportData.Add(exchangeGainLossReport);
                        }

                        response.data.ExchangeGainLossReportList = exchangeGainLossReportData;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "success";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = originalBalance.StatusCode != 200 ? originalBalance.Message : currentBalance.Message;
                    }

                }
                catch (Exception exception)
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = exception.Message;
                }
            }

            return response;
        }


        public async Task<APIResponse> SaveGainLossAccountList(List<long> accountIds)
        {

            APIResponse response = new APIResponse();

            try
            {
                if (accountIds.Any())
                {
                    //Get all Accounts that are already saved
                    List<GainLossSelectedAccounts> gainLossSelectedAccountsList = await _uow.GetDbContext().GainLossSelectedAccounts.Where(x => x.IsDeleted == false).ToListAsync();

                    if (gainLossSelectedAccountsList.Any())
                    {

                        //Get List of Removed Accounts
                        List<GainLossSelectedAccounts> removedGainLossSelectedAccounts = gainLossSelectedAccountsList.Where(x => !accountIds.Contains(x.ChartOfAccountNewId)).ToList();

                        if (removedGainLossSelectedAccounts.Any())
                        {
                            removedGainLossSelectedAccounts.ForEach(x => x.IsDeleted = true);

                            //Delete and update the table with the accounts already deleted
                            _uow.GetDbContext().UpdateRange(removedGainLossSelectedAccounts);
                            await _uow.GetDbContext().SaveChangesAsync();
                        }

                        //Get List of Accounts that are to be added
                        List<long> addGainLossSelectedAccounts = accountIds.Where(x => !gainLossSelectedAccountsList.Select(y => y.ChartOfAccountNewId).Contains(x)).ToList();

                        gainLossSelectedAccountsList = new List<GainLossSelectedAccounts>();

                        foreach (long accountId in addGainLossSelectedAccounts)
                        {
                            GainLossSelectedAccounts gainLossSelectedAccounts = new GainLossSelectedAccounts
                            {
                                IsDeleted = false,
                                CreatedDate = DateTime.Now,
                                ChartOfAccountNewId = accountId,
                            };

                            gainLossSelectedAccountsList.Add(gainLossSelectedAccounts);

                        }
                    }
                    else //table is empty so it is safe to save all the accounts
                    {
                        gainLossSelectedAccountsList = new List<GainLossSelectedAccounts>();

                        foreach (long accountId in accountIds)
                        {
                            GainLossSelectedAccounts gainLossSelectedAccounts = new GainLossSelectedAccounts
                            {
                                IsDeleted = false,
                                CreatedDate = DateTime.Now,
                                ChartOfAccountNewId = accountId,
                            };

                            gainLossSelectedAccountsList.Add(gainLossSelectedAccounts);
                        }
                    }

                    //Save Accounts to the DB
                    if (gainLossSelectedAccountsList.Any())
                    {
                       await _uow.GetDbContext().GainLossSelectedAccounts.AddRangeAsync(gainLossSelectedAccountsList);
                       await _uow.GetDbContext().SaveChangesAsync();
                    }

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "success";
                }
                else
                {
                    throw new Exception("No Account Selected");
                }
            }
            catch (Exception exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = exception.Message;
            }

            return response;
        }

        public async Task<APIResponse> GetExchangeGainLossFilterAccountList()
        {

            APIResponse response = new APIResponse();

            try
            {
                List<GainLossSelectedAccounts> gainLossSelectedAccountsList = await _uow.GetDbContext().GainLossSelectedAccounts.Where(x => x.IsDeleted == false).ToListAsync();
                response.data.GainLossSelectedAccounts = gainLossSelectedAccountsList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "success";
            }
            catch (Exception exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = exception.Message;
            }

            return response;
        }

        #region "Test code"

        #region "GetAllTransaction"
        public async Task<APIResponse> GetAllTransaction(
                                        List<long?> accountIds,
                                        DateTime fromDate,
                                        DateTime toDate,
                                        List<int?> officeIds,
                                        List<int?> journalIds,
                                        List<long?> projectsIds
            )
        {

            APIResponse response = new APIResponse();
            try
            {
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                  .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();

                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var transactionList = await _uow.GetDbContext().VoucherTransactions
                                       .Where(x => x.IsDeleted == false &&
                                                    x.TransactionDate.Value.Date >= fromDate.Date &&
                                                    x.TransactionDate.Value.Date <= toDate.Date &&
                                                    accountIds.Contains(x.ChartOfAccountNewId) &&
                                                    officeIds.Contains(x.VoucherDetails.OfficeId) &&
                                                    journalIds.Contains(x.VoucherDetails.JournalCode) &&
                                                    projectsIds.Contains(x.ProjectId)
                                             )
                                      .Select(x => new VoucherTransactionModel
                                      {
                                          TransactionId = x.TransactionId,
                                          VoucherNo = x.VoucherNo,
                                          AccountNo = x.ChartOfAccountNewId,
                                          AccountCode = x.ChartOfAccountDetail.ChartOfAccountNewCode,
                                          AccountName = x.ChartOfAccountDetail.AccountName,
                                          OfficeId = x.OfficeId,
                                          JournalId = x.VoucherDetails.JournalCode,
                                          FinancialYearId = x.FinancialYearId,
                                          ProjectId = x.ProjectId,
                                          BudgetLineId = x.BudgetLineId,
                                          TransactionDate = x.TransactionDate,
                                          CurrencyId = x.CurrencyId,
                                          Debit = x.Debit,
                                          Credit = x.Credit,
                                      })
                                      .ToListAsync();

                response.data.VoucherTransactionList = transactionList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
        #endregion

        #region "FetchAllDatesFromTransactions"
        public List<DateTime?> FetchAllDatesFromTransactions(IList<VoucherTransactionModel> transactions)
        {
            return transactions.GroupBy(p => new { p.TransactionDate })
                                      .Select(g => g.First().TransactionDate)
                                      .ToList();
        }
        #endregion

        #region "GetAllExchageRates"
        public async Task<APIResponse> GetAllExchageRatesByDates(List<DateTime?> dateList)
        {

            APIResponse response = new APIResponse();
            try
            {

                var exchangeRates = await _uow.GetDbContext().ExchangeRateDetail
                                       .Where(x => x.IsDeleted == false &&
                                                    dateList.Contains(x.Date)
                                             )
                                      .Select(x => new ExchangeRateDetail
                                      {
                                          Date = x.Date,
                                          ExchangeRateId = x.ExchangeRateId,
                                          FromCurrency = x.FromCurrency,
                                          ToCurrency = x.ToCurrency,
                                          Rate = x.Rate,
                                          OfficeId = x.OfficeId
                                      })
                                      .ToListAsync();

                response.data.ExchangeRateDetailList = exchangeRates;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
        #endregion

        #region "GroupByAccount"
        public List<VoucherTransactionModel> GroupByAccount(List<VoucherTransactionModel> transactions)
        {
            var detail = transactions.GroupBy(p => new { p.AccountNo })
                                      .Select(g => new VoucherTransactionModel
                                      {
                                          AccountNo = g.First().AccountNo,
                                          AccountName = g.First().AccountName,
                                          BudgetLineId = g.First().BudgetLineId,
                                          Credit = g.Sum(x => x.Credit),
                                          Debit = g.Sum(x => x.Debit),
                                      })
                                      .ToList();
            return detail;
        }
        #endregion
     
        #endregion

        public async Task<APIResponse> GetDetailOfNotes(DetailOfNotesFilterModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                List<SPDetailOfNotes> spNotesDetail = await _uow.GetDbContext().LoadStoredProc("get_detailofnote_pdf")
                                                              .WithSqlParam("to_currency_id", model.CurrencyId)
                                                              .WithSqlParam("till_date", model.TillDate.ToString())
                                                              .ExecuteStoredProc<SPDetailOfNotes>();

                List<DetailsOfNotesFinalModel> notesDetail = spNotesDetail.GroupBy(x => new { x.NoteId, x.NoteName })
                                               .Select(x => new DetailsOfNotesFinalModel
                                               {
                                                   NoteName = x.First().NoteName,
                                                   TotalDebits = Math.Round(x.Sum(y => y.Debit), 3),
                                                   TotalCredits = Math.Round(x.Sum(y => y.Credit), 3),
                                                   Balance = Math.Round(x.Sum(y => y.Debit) - x.Sum(y => y.Credit), 3),
                                                   AccountSummary = x.Select(s => new DetailsOfNotesModel
                                                   {
                                                       AccountCode = s.AccountCode,
                                                       AccountName = s.AccountName,
                                                       Debit = Math.Round(s.Debit, 3),
                                                       Credit = Math.Round(s.Credit,3)
                                                   }).ToList()
                                               }).ToList();



                response.data.DetailsOfNotesFinalList = notesDetail;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

    }
}