using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<APIResponse> GetNoteBalancesByHeadType(int headTypeId, int toCurrency)
        {
            APIResponse response = new APIResponse();

            // TODO: get all input-level accounts that match the headTypeId
            try
            {

                // Select all input level accounts where the account's parent exists in the sub-level list
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => x.AccountHeadTypeId == headTypeId)
                    .ToListAsync();

                var vTransactions = await _uow.GetDbContext().VoucherTransactions.Where(x =>
                        inputLevelList.Select(a => a.ChartOfAccountNewId).Contains((long)x.ChartOfAccountNewId))
                    .ToListAsync();

                // TODO: figure out where to get toCurrency for filtering exchange rates on.
                // each transaction may have a different. we cannot assume a single fromCurrency
                // only a single toCurrency
                var exRates = await _uow.GetDbContext().ExchangeRates.OrderByDescending(x => x.Date).Where(x =>
                    vTransactions.Select(y => y.TransactionDate).Any(y => x.Date <= y)
                    && vTransactions.Select(z => z.CurrencyId).Contains(x.FromCurrency))
                    .ToListAsync();

                var tasks = new List<Task<double>>();

                foreach (var account in inputLevelList)
                {
                    tasks.Add(GetAccountBalanceById(account.ChartOfAccountNewId));
                }

                foreach (var task in tasks)
                {
                    var balance = await task;
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
            // TODO: fetch all vouchers that contain transactions towards accounts whose head type matches this headTypeId
            // TODO: get exchange rate for each transaction. Use the currencyId from each transaction's voucher, and the toCurrencyId to get  

            // TODO: get all notes that have accounts whose head type match this.headTypeId

            return response;
        }

        private double GetAccountBalanceById(List<VoucherTransactions> transactions,
            List<ExchangeRate> exRates, int toCurrency, ChartOfAccountNew account)
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
                    exchangeRate = exRates.OrderByDescending(x => x.Date).FirstOrDefault(x =>
                        x.ToCurrency == toCurrency && x.FromCurrency == transaction.CurrencyId &&
                        x.Date <= transaction.TransactionDate && x.OfficeId == transaction.OfficeId).Rate;
                }

                var oTrans = new VoucherTransactions();
                oTrans = transaction;
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

        private async Task<double> GetAccountBalanceById(long accountId)
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


        /*
        // Use this when getting transaction value after exchange on the transaction date.
        private double GetTransactionValue(VoucherTransactions transaction, 
            List<ExchangeRate> exRates, int toCurrencyId)
        {

        }

        // Use this when getting transaction value after exchange on a specific date.
        private double GetTransactionValue(VoucherTransactions transaction,
            List<ExchangeRate> exRates, int toCurrencyId, DateTimeOffset exRateDate)
        {

        }
        */
        public async Task<APIResponse> GetNoteBalanceById(int noteType)
        {
            APIResponse response = new APIResponse();

            return response;
        }
    }
}
