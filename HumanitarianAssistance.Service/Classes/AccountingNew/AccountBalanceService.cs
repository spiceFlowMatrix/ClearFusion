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

        public async Task<APIResponse> GetNoteBalancesByHeadType(int headType)
        {
            APIResponse response = new APIResponse();

            // TODO: get all input-level accounts that match the headType
            try
            {

                // Select all input level accounts where the account's parent exists in the sub-level list
                var inputLevelList = await _uow.GetDbContext().ChartOfAccountNew
                    .Where(x => x.AccountHeadTypeId == headType)
                    .ToListAsync();

                var vTransactions = await _uow.GetDbContext().VoucherTransactions.Where(x =>
                        inputLevelList.Select(a => a.ChartOfAccountNewId).Contains((long) x.ChartOfAccountNewId))
                    .ToListAsync();


                response.data.SubLevelAccountList = inputLevelList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            // TODO: fetch all vouchers that contain transactions towards accounts whose head type matches this headType
            // TODO: get exchange rate for each transaction. Use the currencyId from each transaction's voucher, and the toCurrencyId to get  

            // TODO: get all notes that have accounts whose head type match this.headType

            return response;
        }

        public async Task<double> GetAccountBalanceById(long accountId)
        {
            // check if account exists
            var accountTask = _uow.GetDbContext().ChartOfAccountNew.Where(x => x.ChartOfAccountNewId == accountId).FirstOrDefaultAsync();
            // get all transactions
            var transactions = await _uow.GetDbContext().VoucherTransactions.Where(x => x.ChartOfAccountNewId == accountId)
                .ToListAsync();

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
                balance = (double) totalDebits - (double) totalCredits;
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
