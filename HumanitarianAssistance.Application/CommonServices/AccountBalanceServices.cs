using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.CommonServices
{
    public class AccountBalanceServices : IAccountBalanceServices
    {
        public readonly HumanitarianAssistanceDbContext _dbContext;

        public AccountBalanceServices(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Generate view models for key value pairs of accounts(full objects) and their balances.
        // Dictionaries cannot be converted to json objects properly by the framework so use
        // this helper function to prepare your account balances for json.
        public List<AccountBalanceModel> GenerateBalanceViewModels(Dictionary<ChartOfAccountNew, double> rawBalances)
        {
            List<AccountBalanceModel> vmBalances = new List<AccountBalanceModel>();
            foreach (var balance in rawBalances)
            {
                var iVmBalance = new AccountBalanceModel
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

        public async Task<List<AccountBalanceModel>> GetAccountBalancesById(List<long> accountIds, int toCurrencyId, DateTime reportDate)
        {
            List<AccountBalanceModel> vmNoteBalances = new List<AccountBalanceModel>();

            try
            {
                var inputLevelList = await _dbContext.ChartOfAccountNew
                    .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();


                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var accountBalances = await GetAccountBalances(inputLevelList, reportDate, toCurrencyId);
                vmNoteBalances = GenerateBalanceViewModels(accountBalances);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return vmNoteBalances;
        }

        public async Task<List<AccountBalanceModel>> GetAccountBalancesById(List<long> accountIds, DateTime transactionExchangeDate, int toCurrencyId,
            DateTime reportDate)
        {
            List<AccountBalanceModel> vmNoteBalances = new List<AccountBalanceModel>();

            try
            {
                var inputLevelList = await _dbContext.ChartOfAccountNew
                    .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();


                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var accountBalances = await GetAccountBalances(inputLevelList, reportDate, transactionExchangeDate, toCurrencyId);
                vmNoteBalances = GenerateBalanceViewModels(accountBalances);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return vmNoteBalances;
        }

        public async Task<List<AccountBalanceModel>> GetAccountBalancesById(List<long> accountIds, int toCurrencyId,
            DateTime reportStartDate, DateTime reportEndDate)
        {
            List<AccountBalanceModel> vmNoteBalances = new List<AccountBalanceModel>();

            try
            {
                var inputLevelList = await _dbContext.ChartOfAccountNew
                    .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();


                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var accountBalances = await GetAccountBalances(inputLevelList, toCurrencyId, reportStartDate, reportEndDate);
                vmNoteBalances = GenerateBalanceViewModels(accountBalances);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return vmNoteBalances;
        }

        public async Task<List<AccountBalanceModel>> GetAccountBalancesById(List<long> accountIds, DateTime transactionExchangeDate, int toCurrencyId,
            DateTime reportStartDate, DateTime reportEndDate)
        {
            List<AccountBalanceModel> vmNoteBalances = new List<AccountBalanceModel>();

            try
            {
                var inputLevelList = await _dbContext.ChartOfAccountNew
                    .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();

                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var accountBalances = await GetAccountBalances(inputLevelList, toCurrencyId, reportStartDate, reportEndDate, transactionExchangeDate);
                vmNoteBalances = GenerateBalanceViewModels(accountBalances);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return vmNoteBalances;
        }

        public async Task<List<VoucherTransactions>> GetAccountTransactions(List<ChartOfAccountNew> inputLevelAccounts, DateTime endDate)
        {
            return await _dbContext.VoucherTransactions
                .Where(x => x.TransactionDate != null ? x.TransactionDate.Value.Date <= endDate.Date : x.TransactionDate <= endDate
                            && inputLevelAccounts.Select(y => y.ChartOfAccountNewId).Contains((long)x.ChartOfAccountNewId)
                            && x.IsDeleted == false
                            && x.ChartOfAccountNewId != null)
                .Include(x => x.ChartOfAccountDetail)
                .ToListAsync();
        }

        public async Task<List<VoucherTransactions>> GetAccountTransactions(List<ChartOfAccountNew> inputLevelAccounts, DateTime startDate,
            DateTime endDate)
        {
            return await _dbContext.VoucherTransactions
                .Where(x => x.TransactionDate != null ? x.TransactionDate.Value.Date <= endDate.Date : x.TransactionDate <= endDate
                            && x.TransactionDate != null ? x.TransactionDate.Value.Date >= startDate.Date : x.TransactionDate >= startDate
                            && inputLevelAccounts.Select(y => y.ChartOfAccountNewId).Contains((long)x.ChartOfAccountNewId)
                            && x.IsDeleted == false
                            && x.ChartOfAccountNewId != null)
                .Include(x => x.ChartOfAccountDetail)
                .ToListAsync();
        }

        public double DetermineTransactionExrate(VoucherTransactions transaction,
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

        public double DetermineTransactionExrate(VoucherTransactions transaction,
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
        public async Task<List<VoucherTransactions>> GetTransactionValuesAfterExchange(List<VoucherTransactions> transactions, int toCurrencyId)
        {
            var ratesQuery = _dbContext.ExchangeRateDetail.Where(x => x.ToCurrency == toCurrencyId
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

        public Dictionary<ChartOfAccountNew, double> CalculateAccountBalances(
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
        public async Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts, DateTime transactionsTillDate,
            int toCurrencyId)
        {
            var transactions = await GetAccountTransactions(inputLevelAccounts, transactionsTillDate);
            var exchangeValuedTransactions = await GetTransactionValuesAfterExchange(transactions, toCurrencyId);

            return CalculateAccountBalances(inputLevelAccounts, exchangeValuedTransactions);
        }

        // this override calculates transaction credit/debit values after exchange based on the given transactionCompareDate
        public async Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts, DateTime transactionsTillDate,
            DateTime transactionExchangeDate, int toCurrencyId)
        {
            var transactions = await GetAccountTransactions(inputLevelAccounts, transactionsTillDate);
            var exchangeValuedTransactions = await GetTransactionValuesAfterExchange(transactions, toCurrencyId, transactionExchangeDate);

            return CalculateAccountBalances(inputLevelAccounts, exchangeValuedTransactions);
        }

        public async Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts,
            int toCurrencyId, DateTime transactionsStartingFrom, DateTime transactionsUntil)
        {
            var transactions = await GetAccountTransactions(inputLevelAccounts, transactionsStartingFrom, transactionsUntil);
            var exchangeValuedTransactions = await GetTransactionValuesAfterExchange(transactions, toCurrencyId);

            return CalculateAccountBalances(inputLevelAccounts, exchangeValuedTransactions);
        }

        public async Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts,
            int toCurrencyId, DateTime transactionsStartingFrom, DateTime transactionsUntil, DateTime transactionExchangeDate)
        {
            var transactions = await GetAccountTransactions(inputLevelAccounts, transactionsStartingFrom, transactionsUntil);
            var exchangeValuedTransactions = await GetTransactionValuesAfterExchange(transactions, toCurrencyId, transactionExchangeDate);

            return CalculateAccountBalances(inputLevelAccounts, exchangeValuedTransactions);
        }

        public async Task<List<VoucherTransactions>> GetAccountTransactions(List<ChartOfAccountNew> inputLevelAccounts, DateTime startDate,
            DateTime endDate, List<int?> journalList, List<int?> officeList, List<long?> projectIdList)
        {
            var data = await _dbContext.VoucherTransactions
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

        public async Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts,
           int toCurrencyId, DateTime transactionsStartingFrom, DateTime transactionsUntil, List<int?> journalList, List<int?> officeList, List<long?> projectIdList)
        {
            var transactions = await GetAccountTransactions(inputLevelAccounts, transactionsStartingFrom, transactionsUntil, journalList, officeList, projectIdList);
            var exchangeValuedTransactions = await GetTransactionValuesAfterExchange(transactions, toCurrencyId);

            return CalculateAccountBalances(inputLevelAccounts, exchangeValuedTransactions);
        }

        public async Task<List<AccountBalanceModel>> GetAccountBalancesById(List<long?> accountIds, int toCurrencyId,
            DateTime reportStartDate, DateTime reportEndDate, List<int?> journalList, List<int?> officeList, List<long?> projectIdList)
        {
            List<AccountBalanceModel> vmNoteBalances = new List<AccountBalanceModel>();

            try
            {
                var inputLevelList = await _dbContext.ChartOfAccountNew
                    .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();


                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var accountBalances = await GetAccountBalances(inputLevelList, toCurrencyId, reportStartDate, reportEndDate, journalList, officeList, projectIdList);
                vmNoteBalances = GenerateBalanceViewModels(accountBalances);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return vmNoteBalances;
        }

        public async Task<List<AccountBalanceModel>> GetAccountBalancesById(List<long?> accountIds, DateTime transactionExchangeDate, int toCurrencyId,
           DateTime reportStartDate, DateTime reportEndDate, List<int?> journalList, List<int?> officeList, List<long?> projectIdList)
        {
            List<AccountBalanceModel> vmNoteBalances = new List<AccountBalanceModel>();

            try
            {
                var inputLevelList = await _dbContext.ChartOfAccountNew
                                                     .Where(x => accountIds.Contains(x.ChartOfAccountNewId))
                                                     .ToListAsync();

                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var accountBalances = await GetAccountBalances(inputLevelList, toCurrencyId, reportStartDate, reportEndDate, transactionExchangeDate, journalList, officeList, projectIdList);
                vmNoteBalances = GenerateBalanceViewModels(accountBalances);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return vmNoteBalances;
        }

        public async Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts,
            int toCurrencyId, DateTime transactionsStartingFrom, DateTime transactionsUntil, DateTime transactionExchangeDate, List<int?> journalList, List<int?> officeList, List<long?> projectIdList)
        {
            var transactions = await GetAccountTransactions(inputLevelAccounts, transactionsStartingFrom, transactionsUntil, journalList, officeList, projectIdList);
            var exchangeValuedTransactions = await GetTransactionValuesAfterExchange(transactions, toCurrencyId, transactionExchangeDate);

            return CalculateAccountBalances(inputLevelAccounts, exchangeValuedTransactions);
        }

        // Value after exchange on the given onDate
        public async Task<List<VoucherTransactions>> GetTransactionValuesAfterExchange(List<VoucherTransactions> transactions, int toCurrencyId, DateTime onDate)
        {
            if (!transactions.Any())
            {
                throw new Exception("Transaction not found");
            }

            var ratesQuery = _dbContext.ExchangeRateDetail.Where(x => x.ToCurrency == toCurrencyId
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
    }
}