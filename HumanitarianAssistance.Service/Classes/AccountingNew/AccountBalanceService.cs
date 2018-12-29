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

        public async Task<APIResponse> GetNoteBalancesByHeadType(int headTypeId, int toCurrency, DateTime reportDate)
        {
            APIResponse response = new APIResponse();

            try
            {
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => x.AccountHeadTypeId == headTypeId && x.AccountLevelId == (int)AccountLevels.InputLevel)
                    .Include(x => x.AccountType)
                    .ToListAsync();

                var accountBalances = await GetAccountBalances(inputLevelList, reportDate, toCurrency);

                var notes = inputLevelList.Select(x => x.AccountType);
                List<NoteAccountBalances> noteAccountBalances = new List<NoteAccountBalances>();

                foreach (var note in notes)
                {
                    var currNoteBalances = (Dictionary<ChartOfAccountNew, double>)accountBalances.Where(x => x.Key.AccountTypeId == note.AccountTypeId);

                    var currNoteAccountBalances = new NoteAccountBalances();

                    currNoteAccountBalances.NoteId = note.AccountTypeId;
                    currNoteAccountBalances.NoteName = note.AccountTypeName;
                    currNoteAccountBalances.AccountBalances = currNoteBalances;
                }

                response.data.NoteAccountBalances = noteAccountBalances;
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

                var accountBalances = await GetAccountBalances(inputLevelList, toCurrencyId, reportEndDate, reportEndDate);

                var notes = inputLevelList.Select(x => x.AccountType);
                List<NoteAccountBalances> noteAccountBalances = new List<NoteAccountBalances>();

                foreach (var note in notes)
                {
                    var currNoteBalances = (Dictionary<ChartOfAccountNew, double>)accountBalances.Where(x => x.Key.AccountTypeId == note.AccountTypeId);

                    var currNoteAccountBalances = new NoteAccountBalances();

                    currNoteAccountBalances.NoteId = note.AccountTypeId;
                    currNoteAccountBalances.NoteName = note.AccountTypeName;
                    currNoteAccountBalances.AccountBalances = currNoteBalances;
                }

                response.data.NoteAccountBalances = noteAccountBalances;
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

        public async Task<APIResponse> GetAccountBalancesById(List<long> accountIds, int toCurrencyId,
            DateTime reportDate)
        {
            APIResponse response = new APIResponse();

            try
            {
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();

                var accountBalances = await GetAccountBalances(inputLevelList, reportDate, toCurrencyId);

                response.data.AccountBalances = accountBalances;
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

        public async Task<APIResponse> GetAccountBalancesById(List<long> accountIds, DateTime transactionExchangeDate, int toCurrencyId,
            DateTime reportDate)
        {
            APIResponse response = new APIResponse();

            try
            {
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();

                var accountBalances = await GetAccountBalances(inputLevelList, reportDate, transactionExchangeDate, toCurrencyId);

                response.data.AccountBalances = accountBalances;
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

        public async Task<APIResponse> GetAccountBalancesById(List<long> accountIds, int toCurrencyId,
            DateTime reportStartDate, DateTime reportEndDate)
        {
            APIResponse response = new APIResponse();

            try
            {
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();

                var accountBalances = await GetAccountBalances(inputLevelList, toCurrencyId, reportStartDate, reportEndDate);

                response.data.AccountBalances = accountBalances;
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

        public async Task<APIResponse> GetAccountBalancesById(List<long> accountIds, DateTime transactionExchangeDate, int toCurrencyId,
            DateTime reportStartDate, DateTime reportEndDate)
        {
            APIResponse response = new APIResponse();

            try
            {
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => accountIds.Contains(x.ChartOfAccountNewId)).ToListAsync();

                var accountBalances = await GetAccountBalances(inputLevelList, toCurrencyId, reportStartDate, reportEndDate, transactionExchangeDate);

                response.data.AccountBalances = accountBalances;
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

        private async Task<List<VoucherTransactions>> GetAccountTransactions(List<ChartOfAccountNew> inputLevelAccounts, DateTime endDate)
        {
            return await _uow.GetDbContext().VoucherTransactions
                .Where(x => x.TransactionDate <= endDate 
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
                .Where(x => x.TransactionDate <= endDate
                            && x.TransactionDate >= startDate
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
                var interxExchangeRate = (double)rates.OrderByDescending(x => x.Date)
                    .FirstOrDefault(x => x.Date <= transaction.TransactionDate.GetValueOrDefault()
                                         && x.FromCurrency == transaction.CurrencyId
                                         && x.ToCurrency == toCurrencyId).Rate;
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
                var interxExchangeRate = (double)rates.OrderByDescending(x => x.Date)
                    .FirstOrDefault(x => x.Date <= onDate
                                         && x.FromCurrency == transaction.CurrencyId
                                         && x.ToCurrency == toCurrencyId).Rate;
            }

            return xExchangeRate;

        }

        // Value after exchange on the transaction date
        private async Task<List<VoucherTransactions>> GetTransactionValuesAfterExchange(List<VoucherTransactions> transactions, int toCurrencyId)
        {
            var ratesQuery = _uow.GetDbContext().ExchangeRateDetail.Where(x => x.ToCurrency == toCurrencyId);
            ratesQuery = ratesQuery.Where(x => transactions.Select(y => y.CurrencyId).Contains(x.FromCurrency));
            ratesQuery = ratesQuery.Where(x => transactions.Select(y => y.TransactionDate).Any(z => z >= x.Date));
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
            var ratesQuery = _uow.GetDbContext().ExchangeRateDetail.Where(x => x.ToCurrency == toCurrencyId);
            ratesQuery = ratesQuery.Where(x => transactions.Select(y => y.CurrencyId).Contains(x.FromCurrency));
            ratesQuery = ratesQuery.Where(x => x.Date == onDate);
            var ratesList = await ratesQuery.ToListAsync();

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
    }
}
