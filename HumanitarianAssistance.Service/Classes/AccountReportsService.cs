﻿using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.Service.interfaces;
using Microsoft.EntityFrameworkCore;
using System.IO;
using HumanitarianAssistance.ViewModels;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Entities;
using System.Data;
using NpgsqlTypes;
using Npgsql;
using HumanitarianAssistance.ViewModels.SPModels;
using System.Diagnostics;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;

namespace HumanitarianAssistance.Service.Classes
{
    public class AccountReportsService : IAccountRecords
    {
        IUnitOfWork _uow;
        UserManager<AppUser> _userManager;

        public AccountReportsService(IUnitOfWork uow, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._userManager = userManager;
        }

        public async Task<APIResponse> GetVoucherSummaryList(VoucherSummaryFilterModel voucherSummaryFilter)
        {
            APIResponse response = new APIResponse();

            try
            {
                var spVoucherSummaryList = await _uow.GetDbContext().LoadStoredProc("get_vouchersummaryreportvouchersbyfilter")
                                      .WithSqlParam("accounts", voucherSummaryFilter.Accounts)
                                      .WithSqlParam("budgetlines", voucherSummaryFilter.BudgetLines)
                                      .WithSqlParam("currencyid", voucherSummaryFilter.Currency)
                                      .WithSqlParam("journals", voucherSummaryFilter.Journals)
                                      .WithSqlParam("offices", voucherSummaryFilter.Offices)
                                      .WithSqlParam("projectjobs", voucherSummaryFilter.ProjectJobs)
                                      .WithSqlParam("projects", voucherSummaryFilter.Projects)
                                      .WithSqlParam("recordtype", voucherSummaryFilter.RecordType)
                                      .ExecuteStoredProc<SPVoucherSummaryReportModel>();

                response.data.TotalCount = spVoucherSummaryList.Count;

                var summaryList = spVoucherSummaryList.Skip(voucherSummaryFilter.PageIndex * voucherSummaryFilter.PageSize).Take(voucherSummaryFilter.PageSize).ToList();

                response.data.VoucherSummaryList = summaryList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + exception.Message;
            }

            return response;
        }

        public async Task<APIResponse> GetVoucherTransactionList(TransactionFilterModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                var data = await _uow.GetDbContext().VoucherDetail
                                                    .Include(x => x.VoucherTransactionDetails)
                                                    .ThenInclude(y => y.ChartOfAccountDetail)
                                                    .Include(x => x.CurrencyDetail)
                                                    .FirstOrDefaultAsync(x => x.IsDeleted == false && x.VoucherNo == model.VoucherNo);

                if (data != null)
                {
                    if (data.VoucherTransactionDetails.Any())
                    {

                        if (model.RecordType == (int)RECORDTYPE.SINGLE)
                        {
                            response.data.VoucherSummaryTransactionList = data.VoucherTransactionDetails.Select(x => new VoucherSummaryTransactionModel
                            {
                                AccountCode = x.ChartOfAccountDetail.ChartOfAccountNewCode,
                                AccountName = x.ChartOfAccountDetail.AccountName,
                                CurrencyName = data.CurrencyDetail.CurrencyName,
                                TransactionDescription = x.Description,
                                Amount = x.Debit == 0 ? x.Credit : x.Debit,
                                TransactionType = x.Debit == 0 ? "Credit" : "Debit"
                            }).ToList();
                        }
                        else //consolidated
                        {
                            response.data.VoucherSummaryTransactionList = new List<VoucherSummaryTransactionModel>();

                            ExchangeRateDetail exchangeRateDetail = exchangeRateDetail = await _uow.GetDbContext().ExchangeRateDetail
                                                                                  .OrderByDescending(x => x.Date)
                                                                                  .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                   x.Date <= data.VoucherDate.Date && x.FromCurrency == data.CurrencyId &&
                                                                                   x.ToCurrency == model.CurrencyId);

                            foreach (var item in data.VoucherTransactionDetails)
                            {
                                VoucherSummaryTransactionModel voucherSummaryTransactionModel = new VoucherSummaryTransactionModel();

                                if (item.CurrencyId != model.CurrencyId)
                                {
                                    if (exchangeRateDetail == null)
                                    {
                                        throw new Exception("Exchange Rate Not Defined");
                                    }

                                    if (item.Debit == 0)
                                    {
                                        voucherSummaryTransactionModel.Amount = Math.Round((double)(item.Credit * (double)exchangeRateDetail.Rate), 2);
                                        voucherSummaryTransactionModel.TransactionType = "Credit";
                                    }
                                    else
                                    {
                                        voucherSummaryTransactionModel.Amount = Math.Round((double)(item.Debit * (double)exchangeRateDetail.Rate), 2);
                                        voucherSummaryTransactionModel.TransactionType = "Debit";
                                    }
                                }
                                else
                                {
                                    if (item.Debit == 0)
                                    {
                                        voucherSummaryTransactionModel.Amount = item.Credit;
                                        voucherSummaryTransactionModel.TransactionType = "Credit";
                                    }
                                    else
                                    {
                                        voucherSummaryTransactionModel.Amount = item.Debit;
                                        voucherSummaryTransactionModel.TransactionType = "Debit";
                                    }
                                }

                                voucherSummaryTransactionModel.AccountCode = item.ChartOfAccountDetail.ChartOfAccountNewCode;
                                voucherSummaryTransactionModel.AccountName = item.ChartOfAccountDetail.AccountName;
                                voucherSummaryTransactionModel.CurrencyName = data.CurrencyDetail.CurrencyName;
                                voucherSummaryTransactionModel.TransactionDescription = item.Description;

                                response.data.VoucherSummaryTransactionList.Add(voucherSummaryTransactionModel);
                            }
                        }
                    }
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + exception.Message;
            }

            return response;
        }
    }
}
