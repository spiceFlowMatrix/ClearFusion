using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Domain.Entities.Accounting;

namespace HumanitarianAssistance.Application.CommonServicesInterface
{
    public interface IAccountBalanceServices
    {
        List<AccountBalanceModel> GenerateBalanceViewModels(Dictionary<ChartOfAccountNew, double> rawBalances);
        Task<List<AccountBalanceModel>> GetAccountBalancesById(List<long> accountIds, int toCurrencyId, DateTime reportDate);
        Task<List<AccountBalanceModel>> GetAccountBalancesById(List<long> accountIds, DateTime transactionExchangeDate, int toCurrencyId, DateTime reportDate);
        Task<List<AccountBalanceModel>> GetAccountBalancesById(List<long> accountIds, int toCurrencyId, DateTime reportStartDate, DateTime reportEndDate);
        Task<List<AccountBalanceModel>> GetAccountBalancesById(List<long> accountIds, DateTime transactionExchangeDate, int toCurrencyId, DateTime reportStartDate, DateTime reportEndDate);
        Task<List<VoucherTransactions>> GetAccountTransactions(List<ChartOfAccountNew> inputLevelAccounts, DateTime endDate);
        Task<List<VoucherTransactions>> GetAccountTransactions(List<ChartOfAccountNew> inputLevelAccounts, DateTime startDate, DateTime endDate);
        double DetermineTransactionExrate(VoucherTransactions transaction, List<ExchangeRateDetail> rates, int toCurrencyId);
        double DetermineTransactionExrate(VoucherTransactions transaction, List<ExchangeRateDetail> rates, int toCurrencyId, DateTime onDate);
        Dictionary<ChartOfAccountNew, double> CalculateAccountBalances(List<ChartOfAccountNew> inputLevelAccounts, List<VoucherTransactions> accountTransactions);
        Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts, DateTime transactionsTillDate, int toCurrencyId);
        Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts, DateTime transactionsTillDate, DateTime transactionExchangeDate, int toCurrencyId);
        Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts, int toCurrencyId, DateTime transactionsStartingFrom, DateTime transactionsUntil, DateTime transactionExchangeDate);
        Task<List<VoucherTransactions>> GetAccountTransactions(List<ChartOfAccountNew> inputLevelAccounts, DateTime startDate, DateTime endDate, List<int?> journalList, List<int?> officeList, List<long?> projectIdList);
        Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts, int toCurrencyId, DateTime transactionsStartingFrom, DateTime transactionsUntil, List<int?> journalList, List<int?> officeList, List<long?> projectIdList);
        Task<List<AccountBalanceModel>> GetAccountBalancesById(List<long?> accountIds, int toCurrencyId, DateTime reportStartDate, DateTime reportEndDate, List<int?> journalList, List<int?> officeList, List<long?> projectIdList);
        Task<List<AccountBalanceModel>> GetAccountBalancesById(List<long?> accountIds, DateTime transactionExchangeDate, int toCurrencyId, DateTime reportStartDate, DateTime reportEndDate, List<int?> journalList, List<int?> officeList, List<long?> projectIdList);
        Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts, int toCurrencyId, DateTime transactionsStartingFrom, DateTime transactionsUntil, DateTime transactionExchangeDate, List<int?> journalList, List<int?> officeList, List<long?> projectIdList);
        Task<List<VoucherTransactions>> GetTransactionValuesAfterExchange(List<VoucherTransactions> transactions, int toCurrencyId, DateTime onDate);
        Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts,
            int toCurrencyId, DateTime transactionsStartingFrom, DateTime transactionsUntil);
    }
}