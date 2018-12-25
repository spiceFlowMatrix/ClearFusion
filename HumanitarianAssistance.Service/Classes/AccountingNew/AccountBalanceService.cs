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

        // Get Account Balance
        // get accounts
        // get accounts transactions
        // get exchange rate for each transaction
        // get account balance by id

        public async Task<APIResponse> GetNoteBalancesByHeadType(int headTypeId, int toCurrency)
        {
            APIResponse response = new APIResponse();

            // TODO: get all input-level accounts that match the headTypeId
            try
            {

                // Select all input level accounts where the account's parent exists in the sub-level list
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => x.AccountHeadTypeId == headTypeId && x.AccountLevelId == (int)AccountLevels.InputLevel)
                    .ToListAsync();


                var vTransactions = await _uow.GetDbContext().VoucherTransactions.Where(x =>
                        inputLevelList.Select(a => (long)a.ChartOfAccountNewId).Contains((long)x.ChartOfAccountNewId) 
                        && x.ChartOfAccountNewId != null && x.IsDeleted == false)
                    .ToListAsync();

                var vouchers = await _uow.GetDbContext().VoucherDetail.Where(x =>
                    vTransactions.Select(a => a.VoucherNo).Contains(x.VoucherNo)).ToListAsync();

                // TODO: figure out where to get toCurrency for filtering exchange rates on.
                // each transaction may have a different. we cannot assume a single fromCurrency
                // only a single toCurrency
                var exRates = await _uow.GetDbContext().ExchangeRateDetail.OrderByDescending(x => x.Date).Where(x =>
                    vTransactions.Select(y => y.TransactionDate).Any(y => x.Date <= y)
                    && vTransactions.Select(z => z.CurrencyId).Contains(x.FromCurrency)).ToListAsync();

                var tasks = new List<double>();

                foreach (var account in inputLevelList)
                {
                    tasks.Add(CalculateAccountBalanceByAccount(vTransactions, exRates, toCurrency, account));
                }

                response.data.SubLevelAccountList = inputLevelList;
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


        // thhis override calculates transaction credit/debit value after exchange on the transaction date
        private async Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts, DateTime tillDate, int toCurrencyId)
        {
            var transactions = await GetAccountTransactions(inputLevelAccounts, tillDate);
            var exchangeValuedTransactions = await GetTransactionValuesAfterExchange(transactions, toCurrencyId);

            Dictionary<ChartOfAccountNew, double> accountBalances = new Dictionary<ChartOfAccountNew, double>();

            foreach (var account in inputLevelAccounts)
            {
                var accountTransactions =
                    exchangeValuedTransactions.Where(x => x.ChartOfAccountNewId == account.ChartOfAccountNewId).ToList();
                var totalCredits = accountTransactions.Select(x => x.Credit.GetValueOrDefault()).Sum();
                var totalDebits = accountTransactions.Select(x => x.Debit.GetValueOrDefault()).Sum();
                if(account.IsCreditBalancetype.GetValueOrDefault())
                    accountBalances.Add(account, totalCredits - totalDebits);
                else
                    accountBalances.Add(account, totalDebits - totalCredits);
            }

            return accountBalances;
        }

        // this override calculates transaction credit/debit values after exchange based on the given transactionCompareDate
        private async Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts, DateTime tillDate, 
            DateTime transactionCompareDate, int toCurrencyId, bool isCreditBalanceType)
        {
            var transactions = await GetAccountTransactions(inputLevelAccounts, tillDate);
        }

        private async Task<Dictionary<ChartOfAccountNew, double>> GetAccountBalances(List<ChartOfAccountNew> inputLevelAccounts, DateTime startDate, DateTime endDate)
        {

        }

        private double CalculateAccountBalanceByAccount(List<VoucherTransactions> transactions,
            List<ExchangeRateDetail> exRates, int toCurrency, ChartOfAccountNew account)
        {
            List<VoucherTransactions> transactionsOriginal = new List<VoucherTransactions>();

            // Get value of each transaction (debit or credit) in the given currency on the transaction date
            foreach (var transaction in transactions)
            {
                double? exchangeRate = 0.0;
                if (transaction.CurrencyId == toCurrency)
                {
                    exchangeRate = 1;
                }
                else
                {
                    if(transaction.TransactionDate == null)
                        throw new Exception("Transaction date is not set");
                    if(transaction.CurrencyId == null)
                        throw new Exception("Transaction currency is not set");
                    var interexchangeRate = exRates.OrderByDescending(x => x.Date)
                        .FirstOrDefault(x => x.Date <= transaction.TransactionDate 
                                             && x.FromCurrency == transaction.CurrencyId 
                                             && x.ToCurrency == toCurrency).Rate;
                }

                var oTrans = (transaction);
                oTrans.Credit = exchangeRate * transaction.Credit;
                oTrans.Debit = exchangeRate * transaction.Debit;
                transactionsOriginal.Add(oTrans);
            }

            var totalCredits = transactionsOriginal.Select(x => x.Credit).Sum();
            var totalDebits = transactionsOriginal.Select(x => x.Debit).Sum();
            double balance = 0;
            if ((bool)account.IsCreditBalancetype)
            {
                balance = (double)totalCredits - (double)totalDebits;
            }
            else
            {
                balance = (double)totalDebits - (double)totalCredits;
            }

            return balance;

        }

        private async Task<double> CalculateAccountBalanceByAccount(long accountId)
        {
            // check if account exists
            var accountTask = _uow.GetDbContext().ChartOfAccountNew.Where(x => x.ChartOfAccountNewId == accountId).FirstOrDefaultAsync();
            // get all transactions
            var transactions = await _uow.VoucherTransactionsRepository.FindAllAsync(x => x.ChartOfAccountNewId == accountId);

            //TODO: get the currency value of all transactions at a given toCurrency 
            var account = await accountTask;
            var transactionCredits = transactions.Select(x => x.Credit);
            var transactionDebits = transactions.Select(x => x.Debit);
            var totalCredits = transactionCredits.Sum();
            var totalDebits = transactionDebits.Sum();
            double balance = 0;
            if ((bool)account.IsCreditBalancetype)
            {
                balance = (double)totalCredits - (double)totalDebits;
            }
            else
            {
                balance = (double)totalDebits - (double)totalCredits;
            }

            return balance;
        }


        public async Task<APIResponse> GetNoteBalanceById(int noteType)
        {
            APIResponse response = new APIResponse();

            return response;
        }
    }
}
