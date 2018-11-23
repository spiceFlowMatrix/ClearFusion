using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes
{
    public class ExchangeRateService : IExchangeRate
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public ExchangeRateService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<APIResponse> AddExchangeRate(ExchangeRateModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                ExchangeRateDetail obj = new ExchangeRateDetail();

                obj.Date = model.Date.Value;
                obj.FromCurrency = model.FromCurrency.Value;
                obj.ToCurrency = model.ToCurrency.Value;
                obj.Rate = Convert.ToDecimal(model.Rate.Value);
                obj.OfficeId = model.OfficeId;

                obj.IsDeleted = false;
                await _uow.ExchangeRateDetailRepository.AddAsyn(obj);
                await _uow.SaveAsync();
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

        public async Task<APIResponse> EditExchangeRate(ExchangeRateModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var exchangerateinfo = await _uow.GetDbContext().ExchangeRateDetail.FirstOrDefaultAsync(x => x.ExchangeRateId == model.ExchangeRateId && x.IsDeleted == false);
                exchangerateinfo.ModifiedById = model.ModifiedById;
                exchangerateinfo.ModifiedDate = DateTime.UtcNow;
                exchangerateinfo.Date = model.Date.Value;
                exchangerateinfo.FromCurrency = model?.FromCurrency??0;
                exchangerateinfo.ToCurrency = model?.ToCurrency??0;
                exchangerateinfo.OfficeId = model.OfficeId;
                exchangerateinfo.Rate = Convert.ToDecimal(model.Rate);
                _uow.GetDbContext().ExchangeRateDetail.Update(exchangerateinfo);
                _uow.GetDbContext().SaveChanges();
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

        #region Exchangerate dependent on base currency

        //public async Task<APIResponse> GetAllExchangeRate()
        //{
        //    APIResponse response = new APIResponse();
        //    try
        //    {
        //       var exchangeratelist = await _uow.GetDbContext().ExchangeRates.Include(x => x.CurrencyFrom).Include(y => y.CurrencyTo).Where(x => x.IsDeleted == false)
        //            .Select(x => new ExchangeRateModel
        //            {
        //                ExchangeRateId = x.ExchangeRateId,
        //                Date = x.Date.Value.Date.ToLocalTime(),
        //                FromCurrencyName = x.CurrencyFrom.CurrencyCode,
        //                ToCurrencyName = x.CurrencyTo.CurrencyCode,
        //                Rate = x.Rate,
        //                FromCurrency = x.FromCurrency,
        //                ToCurrency = x.ToCurrency
        //            }).ToListAsync();

        //        response.data.ExchangeRateList = exchangeratelist;
        //        response.StatusCode = StaticResource.successStatusCode;
        //        response.Message = "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        response.StatusCode = StaticResource.failStatusCode;
        //        response.Message = StaticResource.SomethingWrong + ex.Message;
        //    }
        //    return response;
        //}

        #endregion

        public async Task<APIResponse> GetAllExchangeRate()
        {
            APIResponse response = new APIResponse();
            try
            {
                var exchangeratelist = await _uow.GetDbContext().ExchangeRateDetail.Where(x => x.IsDeleted == false)
                                                                                    .Select(x => new ExchangeRateModel
                                                                                    {
                                                                                        ExchangeRateId = x.ExchangeRateId,
                                                                                        Date = x.Date,
                                                                                        //FromCurrencyName = x.FromCurrency,
                                                                                        //ToCurrencyName = x.ToCurrency,
                                                                                        Rate = Convert.ToDouble(x.Rate),
                                                                                        FromCurrency = x.FromCurrency,
                                                                                        ToCurrency = x.ToCurrency,
                                                                                        OfficeId= x.OfficeId
                                                                                    }).ToListAsync();

                response.data.ExchangeRateList = exchangeratelist;
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

        public async Task<APIResponse> GetExchangeRateByDate(int currencyFromCode, int currenctToCode, DateTime? date)
        {
            APIResponse response = new APIResponse();
            try
            {
                var exchangerate = await Task.Run(() =>
                    _uow.GetDbContext().
                    ExchangeRates.Include(x => x.CurrencyFrom).Include(y => y.CurrencyTo)
                                 .Where(x => x.ToCurrency == currenctToCode && x.FromCurrency == currencyFromCode && x.IsDeleted == false).FirstOrDefault()
                );


                response.data.CurrenctExchangeRate = exchangerate.Rate;
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
        /// Get Exchange Gain Or Loss Amount For the Date Selected
        /// </summary>
        /// <param name="model">Contains Exchange Gain Loss Filter Fields</param>
        /// <returns>APIResponse</returns>
        public async Task<APIResponse> GetExchangeGainOrLossAmount(ExchangeGainOrLossFilterModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                //Get Exchange rate defined onm date of comparison
                ICollection<ExchangeRate> dateOfComparisonExRate = await _uow.ExchangeRateRepository.FindAllAsync(x => x.IsDeleted == false && x.Date.Value.Date == model.DateOfComparison.Value.Date);

                if (dateOfComparisonExRate.Count == 0)// if exchange rate not found then find the recently updated Exchange rate within a year
                {
                    dateOfComparisonExRate = await _uow.ExchangeRateRepository.FindAllAsync(x => x.IsDeleted == false && x.Date.Value.Date >= new DateTime(model.DateOfComparison.Value.Date.Year, model.DateOfComparison.Value.Date.Month - 6, 1)
                                                                                                                      && x.Date.Value.Date <= model.DateOfComparison.Value.Date);

                }

                var allCurrencies = await _uow.CurrencyDetailsRepository.FindAllAsync(x => x.IsDeleted == false);
                var baseCurrency = allCurrencies.FirstOrDefault(x => x.Status == true);

                ICollection<ExchangeRate> exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.Date.Value.Date <= model.ToDate.Value.Date).OrderByDescending(x => x.Date).ToListAsync();

                //NOTE: Select transaction from voucher
                //      Later we will add project filter

                ICollection<VoucherTransactions> xtransactionDetails = await _uow.VoucherTransactionsRepository
                                                //.FindAllAsync(x => model.VoucherList.Contains(x.VoucherNo) &&
                                                .FindAllAsync(x =>
                                                                 x.OfficeId == model.OfficeId &&
                                                                 //model.VoucherList.Contains(x.VoucherNo) &&
                                                                 x.IsDeleted == false &&
                                                                 x.TransactionDate.Value.Date >= model.FromDate.Value.Date &&
                                                                 x.TransactionDate.Value.Date <= model.ToDate.Value.Date
                                                );

                ICollection<VoucherTransactions> transactionDetails = null;

                List<ChartAccountDetail> allAccounts = new List<ChartAccountDetail>();

                List<ChartAccountDetail> InputLevelAccounts = new List<ChartAccountDetail>();

                allAccounts = await _uow.GetDbContext().ChartAccountDetail
                        .Select(x => new ChartAccountDetail
                        {
                            AccountCode = x.AccountCode,
                            AccountLevelId = x.AccountLevelId,
                            AccountLevels = x.AccountLevels,
                            AccountName = x.AccountName,
                            AccountType = x.AccountType,
                            AccountTypeId = x.AccountTypeId,
                            ChartOfAccountCode = x.ChartOfAccountCode,
                            CreatedById = x.CreatedById,
                            CreatedDate = x.CreatedDate,
                            ParentID = x.ParentID,

                        }).ToListAsync();

                foreach (var accounts in model.AccountList)
                {
                    var accountDetail = allAccounts.Find(x => x.ChartOfAccountCode == accounts);

                    if (accountDetail != null)
                    {
                        InputLevelAccounts.AddRange(CommonUtility.CommonFunctions.GetInputLevelAccountDetails(accountDetail, allAccounts));
                    }
                }

                //If VoucherList and AccountList
                if (model.VoucherList != null && model.AccountList != null)
                {
                    transactionDetails = xtransactionDetails.Where(x => model.VoucherList.Contains(x.VoucherNo) && InputLevelAccounts.Select(y => y.AccountCode).ToList().Contains(x.AccountNo.Value)).ToList();
                }
                else
                {
                    transactionDetails = xtransactionDetails.Where(x => InputLevelAccounts.Select(y => y.AccountCode).ToList().Contains(x.AccountNo.Value)).ToList();
                }

                List<TransactionsModel> exchangeGainLossList = new List<TransactionsModel>();


                //NOTE: 
                //      TransactionAmount => Original Transaction
                //      OriginalExchangeValue => Comparison Currency
                //      CurrentExchangeValue => Use compare currency and date of comparison


                foreach (var item in transactionDetails)
                {
                    if (item.Credit != 0)
                    {

                        //Credit
                        TransactionsModel objCredit = new TransactionsModel();

                        objCredit.TransactionDate = item.TransactionDate;
                        objCredit.OriginalCurrency = item.CurrencyId;
                        objCredit.TransactionAmount = item.Credit;
                        objCredit.CreditOrDebit = "Credit";

                        //NOTE: model.currencyId == base ? * : * and / by model.currencyId 

                        objCredit.OriginalExchangeValue = model.ComparisonCurrencyId == baseCurrency.CurrencyId ?
                                                        Math.Round(Convert.ToDouble(item.Credit * exchangeRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId && x.Date.Value.Date <= item.TransactionDate.Value.Date)?.Rate), 2) :
                                                        Math.Round(Convert.ToDouble((item.Credit *
                                                                     exchangeRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId && x.Date.Value.Date <= item.TransactionDate.Value.Date)?.Rate) /
                                                                     exchangeRate.FirstOrDefault(x => x.FromCurrency == model.ComparisonCurrencyId && x.Date.Value.Date <= item.TransactionDate.Value.Date)?.Rate), 2);
                        objCredit.CurrentExchangeValue = model.ComparisonCurrencyId == baseCurrency.CurrencyId ?
                                                    Math.Round(Convert.ToDouble(item.Credit * dateOfComparisonExRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId)?.Rate), 2) :
                                                    Math.Round(Convert.ToDouble((item.Credit *
                                                                 dateOfComparisonExRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId)?.Rate) /
                                                                 dateOfComparisonExRate.FirstOrDefault(x => x.FromCurrency == model.ComparisonCurrencyId)?.Rate), 2);

                        objCredit.GainLossAmount = objCredit.OriginalExchangeValue - objCredit.CurrentExchangeValue;


                        exchangeGainLossList.Add(objCredit);
                    }
                    else if (item.Debit != 0)
                    {
                        //Debit
                        TransactionsModel objDebit = new TransactionsModel();

                        objDebit.TransactionDate = item.TransactionDate;
                        objDebit.OriginalCurrency = item.CurrencyId;
                        objDebit.TransactionAmount = item.Debit;
                        objDebit.CreditOrDebit = "Debit";

                        objDebit.OriginalExchangeValue = model.ComparisonCurrencyId == baseCurrency.CurrencyId ?
                                                 Math.Round(Convert.ToDouble(item.Debit * exchangeRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId && x.Date.Value.Date <= item.TransactionDate.Value.Date)?.Rate), 2) :
                                                 Math.Round(Convert.ToDouble((item.Debit *
                                                              exchangeRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId && x.Date.Value.Date <= item.TransactionDate.Value.Date)?.Rate) /
                                                              exchangeRate.FirstOrDefault(x => x.FromCurrency == model.ComparisonCurrencyId && x.Date.Value.Date <= item.TransactionDate.Value.Date)?.Rate), 2);

                        objDebit.CurrentExchangeValue = model.ComparisonCurrencyId == baseCurrency.CurrencyId ?
                                                     Math.Round(Convert.ToDouble(item.Debit * dateOfComparisonExRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId)?.Rate), 2) :
                                                     Math.Round(Convert.ToDouble((item.Debit *
                                                                  dateOfComparisonExRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId)?.Rate) /
                                                                  dateOfComparisonExRate.FirstOrDefault(x => x.FromCurrency == model.ComparisonCurrencyId)?.Rate), 2);

                        objDebit.GainLossAmount = Math.Round(Convert.ToDouble(objDebit.OriginalExchangeValue - objDebit.CurrentExchangeValue), 2);

                        exchangeGainLossList.Add(objDebit);
                    }
                }

                response.data.ExchangeGainOrLossModel = exchangeGainLossList.ToList();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "success";


                #region "Old code"

                //if (model != null)
                //{

                //    var baseCurrency = await _uow.CurrencyDetailsRepository.FindAsync(x => x.Status == true);

                //    ExchangeGainOrLossModel responseModel = new ExchangeGainOrLossModel();
                //    List<TransactionsModel> lst = new List<TransactionsModel>();

                //    //var exchangeRateList = await _uow.ExchangeRateRepository.FindAllAsync(x => x.IsDeleted == false && x.OfficeId == model.OfficeId && x.Date.Value.Date <= model.EndDate.Date);

                //    var exchangeRateList = await _uow.GetDbContext().LoadStoredProc("get_ExchangeRates")
                //                           .WithSqlParam("modelEndDate", model.EndDate.ToString())
                //                           .WithSqlParam("modelOfficeId", model.OfficeId)
                //                           .ExecuteStoredProc<SP_ExchangeRate>();



                //    //CASE 1
                //    ////Note: Take all the debits towards an account
                //    //var records = await _uow.GetDbContext().ChartAccountDetail
                //    //                    .Include(x => x.CreditAccountlist)
                //    //                    .Where(x => x.IsDeleted == false && model.AccountCodes.Contains(x.AccountCode))
                //    //                    .ToListAsync();

                //    //CASE 2
                //    //Note: Take all the debits towards an account
                //    //var records = await _uow.GetDbContext().ChartAccountDetail
                //    //                    .Where(x => x.IsDeleted == false && model.AccountCodes.Contains(x.AccountCode))
                //    //                    .ToListAsync();

                //    // CASE 3
                //    ////Note: Take all the debits towards an account
                //    //var records = await _uow.GetDbContext().LoadStoredProc("get_exchangeVoucherTransactionDetails")
                //    //             .WithSqlParam("modelAccountCode", model.AccountCodes.ToArray()) //{511022,1}
                //    //             .WithSqlParam("modelStartDate", model.StartDate.ToString())
                //    //             .WithSqlParam("modelEndDate", model.EndDate.ToString())
                //    //             .WithSqlParam("modelOfficeId", model.OfficeId)
                //    //             .ExecuteStoredProc<ExchangeGainOrLossTransaction>();


                //    var records = await _uow.GetDbContext().VoucherTransactions
                //                      .Where(x => x.IsDeleted == false &&
                //                            model.AccountCodes.Contains(x.AccountNo) &&
                //                            x.TransactionDate.Value.Date >= model.StartDate.Date &&
                //                            x.TransactionDate.Value.Date <= model.EndDate.Date
                //                            )
                //                      .Skip(model.Skip)
                //                      .Take(model.Take)
                //                      .ToListAsync();

                //    var totalCount = await _uow.GetDbContext().VoucherTransactions
                //                   .Where(x => x.IsDeleted == false &&
                //                         model.AccountCodes.Contains(x.AccountNo) &&
                //                         x.TransactionDate.Value.Date >= model.StartDate.Date &&
                //                         x.TransactionDate.Value.Date <= model.EndDate.Date
                //                         ).CountAsync();



                //    foreach (var items in records)
                //    {
                //        double? accountTransactionTotal = 0, accountCurrentTotal = 0;

                //        if (items.CurrencyId == baseCurrency.CurrencyId)
                //        {
                //            accountTransactionTotal += Math.Round(Convert.ToDouble(items.Debit), 2);
                //            accountCurrentTotal += Math.Round(Convert.ToDouble(items.Debit), 2);
                //        }
                //        else
                //        {
                //            //var excRateOfTransaction = (from s in _uow.GetDbContext().ExchangeRates
                //            //                            where s.IsDeleted == false &&
                //            //                                  s.FromCurrency == items.CurrencyId &&
                //            //                                  s.Date.Value.Date.Month <= items.TransactionDate.Month &&
                //            //                                  s.Date.Value.Date.Year <= items.TransactionDate.Year
                //            //                            orderby s.Date descending
                //            //                            select s)
                //            //                            .FirstOrDefault();


                //            //var excRateForCurrentDate = (from s in _uow.GetDbContext().ExchangeRates
                //            //                             where s.IsDeleted == false &&
                //            //                                   s.FromCurrency == items.CurrencyId &&
                //            //                                   s.Date.Value.Date.Month <= DateTime.Now.Date.Month &&
                //            //                                   s.Date.Value.Date.Year <= DateTime.Now.Date.Year
                //            //                             orderby s.Date descending
                //            //                             select s)
                //            //                            .FirstOrDefault();



                //            //var excRateOfTransaction = exchangeRateList.Where(x => x.FromCurrency == items.CurrencyId && x.Date.Month <= items.TransactionDate.Month && x.Date.Year <= items.TransactionDate.Year).OrderByDescending(x => x.Date).FirstOrDefault();
                //            //var excRateForCurrentDate = exchangeRateList.Where(x => x.FromCurrency == items.CurrencyId && x.Date.Month <= DateTime.Now.Date.Month && x.Date.Year <= DateTime.Now.Date.Year).OrderByDescending(x => x.Date).FirstOrDefault();

                //            //NOTE: Use AsEnumerable , It will store dast in memory //Much faster
                //            var excRateOfTransaction = exchangeRateList.AsEnumerable().Where(x => x.FromCurrency == items.CurrencyId && x.Date.Month <= items.TransactionDate.Value.Month && x.Date.Year <= items.TransactionDate.Value.Year).OrderByDescending(x => x.Date).FirstOrDefault();
                //            var excRateForCurrentDate = exchangeRateList.AsEnumerable().Where(x => x.FromCurrency == items.CurrencyId && x.Date.Month <= DateTime.Now.Date.Month && x.Date.Year <= DateTime.Now.Date.Year).OrderByDescending(x => x.Date).FirstOrDefault();

                //            accountTransactionTotal += Math.Round(Convert.ToDouble(items.Debit * excRateOfTransaction?.Rate), 2);
                //            accountCurrentTotal += Math.Round(Convert.ToDouble(items.Debit * excRateForCurrentDate?.Rate), 2);

                //            //accountTransactionTotal += Math.Round(Convert.ToDouble(items.Debit * excRateOfTransaction?.Rate));
                //            //accountCurrentTotal += Math.Round(Convert.ToDouble(items.Debit * excRateForCurrentDate?.Rate));
                //        }

                //        TransactionsModel obj = new TransactionsModel();
                //        obj.OriginalAmount = Math.Round(Convert.ToDouble(accountTransactionTotal), 2);
                //        obj.CurrentAmount = Math.Round(Convert.ToDouble(accountCurrentTotal), 2);
                //        obj.Balance = Math.Round(Convert.ToDouble(accountCurrentTotal - accountTransactionTotal), 2);
                //        obj.ChartOfAccountCode = Convert.ToInt64(items.AccountNo);
                //        lst.Add(obj);
                //    }



                //    response.data.TotalCount = totalCount;
                //    responseModel.TransactionsModel = lst;
                //    responseModel.Total = lst.Sum(x => x.Balance);
                //    response.data.ExchangeGainOrLossModel = responseModel;
                //    response.StatusCode = StaticResource.successStatusCode;
                //    response.Message = "Success";
                //}
                //else
                //{
                //    response.data.ExchangeGainOrLossModel = null;
                //    response.data.TotalCount = 0;
                //    response.StatusCode = StaticResource.failStatusCode;
                //    response.Message = "No record Found";
                //}

                #endregion

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Get Exchange Gain Or Loss Transaction Amount For the Date Selected
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetExchangeGainOrLossTransactionAmount(ExchangeGainOrLossTransactionFilterModel model)
        {
            APIResponse response = new APIResponse();

            List<TransactionsModel> exchangeGainLossList = new List<TransactionsModel>();

            try
            {
                //Get Exchange rate defined on date of comparison
                var dateOfComparisonExRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.Date.Value.Date == model.DateOfComparison.Value.Date).OrderByDescending(x => x.Date).ToListAsync();

                ICollection<VoucherTransactions> xtransactionDetails = await _uow.VoucherTransactionsRepository
                                               .FindAllAsync(x =>
                                                                x.OfficeId == model.OfficeId &&
                                                                x.IsDeleted == false &&
                                                                x.TransactionDate.Value.Date >= model.FromDate.Value.Date &&
                                                                x.TransactionDate.Value.Date <= model.ToDate.Value.Date
                                               );

                if (xtransactionDetails.Count > 0)
                {
                    if (dateOfComparisonExRate.Count == 0)// if exchange rate not found then find the recently updated Exchange rate within a year
                    {
                        dateOfComparisonExRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false && x.Date.Value.Date >= new DateTime(model.DateOfComparison.Value.Date.Year, model.DateOfComparison.Value.Date.Month - 6, 1)
                                                                                                                          && x.Date.Value.Date <= model.DateOfComparison.Value.Date).OrderByDescending(x => x.Date).ToListAsync();
                    }

                    ICollection<CurrencyDetails> allCurrencies = await _uow.CurrencyDetailsRepository.FindAllAsync(x => x.IsDeleted == false);
                    CurrencyDetails baseCurrency = allCurrencies.FirstOrDefault(x => x.Status == true);

                    List<ChartAccountDetail> allAccounts = new List<ChartAccountDetail>();

                    allAccounts = await _uow.GetDbContext().ChartAccountDetail/*.Include(x => x.CreditAccountlist).Include(x => x.DebitAccountlist)*/
                            .Select(x => new ChartAccountDetail
                            {
                                AccountCode = x.AccountCode,
                                AccountLevelId = x.AccountLevelId,
                                AccountLevels = x.AccountLevels,
                                AccountName = x.AccountName,
                                AccountType = x.AccountType,
                                AccountTypeId = x.AccountTypeId,
                                ChartOfAccountCode = x.ChartOfAccountCode,
                                CreatedById = x.CreatedById,
                                CreatedDate = x.CreatedDate,
                                ParentID = x.ParentID,
                                //CreditAccountDetails = x.CreditAccountDetails,
                                //CreditAccountlist = x.CreditAccountlist.Where(o => o.TransactionDate.Value.Date >= model.StartDate.Value.Date && o.TransactionDate.Value.Date <= model.EndDate.Value.Date).ToList(),
                                //DebitAccountlist = x.DebitAccountlist.Where(o => o.TransactionDate.Value.Date >= model.StartDate.Value.Date && o.TransactionDate.Value.Date <= model.EndDate.Value.Date).ToList()
                            }).ToListAsync();

                    ICollection<ExchangeRate> exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false &&
                                                                                                                (x.Date.Value.Year > model.FromDate.Value.Year && x.Date.Value.Month > model.FromDate.Value.Month - 6) &&
                                                                                                                x.Date.Value.Date <= model.ToDate.Value.Date
                                                                                                           )
                                                                                                    .OrderByDescending(x => x.Date).ToListAsync();



                    if (exchangeRate.Count == 0)// if exchange rate not found then find the recently updated Exchange rate within a year
                    {
                        exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false &&
                                                                                                               (x.Date.Value.Year <= model.ToDate.Value.Year && x.Date.Value.Month <= model.ToDate.Value.Month)).OrderByDescending(x => x.Date).ToListAsync();
                    }

                    foreach (long chartOfAccountCode in model.AccountList)
                    {
                        var accountDetails = allAccounts.Find(x => x.ChartOfAccountCode == chartOfAccountCode);


                        //start from here
                        List<ChartAccountDetail> accountTransactionsList = CommonUtility.CommonFunctions.GetInputLevelAccountDetails(accountDetails, allAccounts);

                        foreach (ChartAccountDetail xChartAccountDetail in accountTransactionsList)
                        {
                            IEnumerable<VoucherTransactions> fourLevelTransactions = null;

                            if (model.TransactionType == (int)Common.Enums.TransactionType.Credit)
                            {
                                fourLevelTransactions = xtransactionDetails.Where(x => x.AccountNo == xChartAccountDetail.ChartOfAccountCode && x.Credit != 0);
                            }
                            else
                            {
                                fourLevelTransactions = xtransactionDetails.Where(x => x.AccountNo == xChartAccountDetail.ChartOfAccountCode && x.Debit != 0);
                            }

                            foreach (var item in fourLevelTransactions)
                            {
                                if (item.Credit != 0)
                                {
                                    //Credit
                                    TransactionsModel objCredit = new TransactionsModel();

                                    objCredit.TransactionDate = item.TransactionDate;
                                    objCredit.OriginalCurrency = item.CurrencyId;
                                    objCredit.TransactionAmount = item.Credit;
                                    objCredit.CreditOrDebit = "Credit";

                                    //NOTE: model.currencyId == base ? * : * and / by model.currencyId 

                                    objCredit.OriginalExchangeValue = model.ComparisonCurrencyId == baseCurrency.CurrencyId ?
                                                                    Math.Round(Convert.ToDouble(item.Credit * exchangeRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId && x.Date.Value.Date <= item.TransactionDate.Value.Date)?.Rate), 2) :
                                                                    Math.Round(Convert.ToDouble((item.Credit *
                                                                                 exchangeRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId && x.Date.Value.Date <= item.TransactionDate.Value.Date)?.Rate) /
                                                                                 exchangeRate.FirstOrDefault(x => x.FromCurrency == model.ComparisonCurrencyId && x.Date.Value.Date <= item.TransactionDate.Value.Date)?.Rate), 2);
                                    objCredit.CurrentExchangeValue = model.ComparisonCurrencyId == baseCurrency.CurrencyId ?
                                                                Math.Round(Convert.ToDouble(item.Credit * dateOfComparisonExRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId)?.Rate), 2) :
                                                                Math.Round(Convert.ToDouble((item.Credit *
                                                                             dateOfComparisonExRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId)?.Rate) /
                                                                             dateOfComparisonExRate.FirstOrDefault(x => x.FromCurrency == model.ComparisonCurrencyId)?.Rate), 2);

                                    objCredit.GainLossAmount = objCredit.OriginalExchangeValue - objCredit.CurrentExchangeValue;


                                    exchangeGainLossList.Add(objCredit);
                                }
                                else if (item.Debit != 0)
                                {
                                    //Debit
                                    TransactionsModel objDebit = new TransactionsModel();

                                    objDebit.TransactionDate = item.TransactionDate;
                                    objDebit.OriginalCurrency = item.CurrencyId;
                                    objDebit.TransactionAmount = item.Debit;
                                    objDebit.CreditOrDebit = "Debit";

                                    objDebit.OriginalExchangeValue = model.ComparisonCurrencyId == baseCurrency.CurrencyId ?
                                                             Math.Round(Convert.ToDouble(item.Debit * exchangeRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId && x.Date.Value.Date <= item.TransactionDate.Value.Date)?.Rate), 2) :
                                                             Math.Round(Convert.ToDouble((item.Debit *
                                                                          exchangeRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId && x.Date.Value.Date <= item.TransactionDate.Value.Date)?.Rate) /
                                                                          exchangeRate.FirstOrDefault(x => x.FromCurrency == model.ComparisonCurrencyId && x.Date.Value.Date <= item.TransactionDate.Value.Date)?.Rate), 2);

                                    objDebit.CurrentExchangeValue = model.ComparisonCurrencyId == baseCurrency.CurrencyId ?
                                                                 Math.Round(Convert.ToDouble(item.Debit * dateOfComparisonExRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId)?.Rate), 2) :
                                                                 Math.Round(Convert.ToDouble((item.Debit *
                                                                              dateOfComparisonExRate.FirstOrDefault(x => x.FromCurrency == item.CurrencyId)?.Rate) /
                                                                              dateOfComparisonExRate.FirstOrDefault(x => x.FromCurrency == model.ComparisonCurrencyId)?.Rate), 2);

                                    objDebit.GainLossAmount = Math.Round(Convert.ToDouble(objDebit.OriginalExchangeValue - objDebit.CurrentExchangeValue), 2);

                                    exchangeGainLossList.Add(objDebit);
                                }
                            }

                        }

                        //ICollection<VoucherTransactions> transactionDetails = xtransactionDetails.Where(x => model.data1.Contains(x.AccountNo)).ToList();
                        response.data.ExchangeGainOrLossModel = exchangeGainLossList.ToList();
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "success";
                    }
                }
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
