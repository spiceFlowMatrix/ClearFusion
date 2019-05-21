using AutoMapper;
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
    public class AccountReportsService: IAccountRecords
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;

        public AccountReportsService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<APIResponse> GetBalanceSheet()
        {
            APIResponse response = new APIResponse();

            try
            {
                ////get level 1 Account
                //List<ChartAccountDetail> AccountLevel1 = await _uow.GetDbContext().ChartAccountDetail.Where(x => x.IsDeleted == false && x.AccountLevelId == 1).ToListAsync();

                //foreach (ChartAccountDetail Account1 in AccountLevel1)
                //{
                //    //get level 2 Account
                //    List<ChartAccountDetail> AccountLevel2 = await _uow.GetDbContext().ChartAccountDetail.Where(x => x.IsDeleted == false && x.AccountLevelId == 2 && x.ParentID== Account1.AccountCode).ToListAsync();

                //    foreach (ChartAccountDetail Account2 in AccountLevel2)
                //    {
                //        //get level 3 Account
                //        List<ChartAccountDetail> AccountLevel3 = await _uow.GetDbContext().ChartAccountDetail.Where(x => x.IsDeleted == false && x.AccountLevelId == 3 && x.ParentID == Account2.AccountCode).ToListAsync();

                //        foreach (ChartAccountDetail Account3 in AccountLevel3)
                //        {
                //            //get level 4 Account
                //            List<ChartAccountDetail> AccountLevel4 = await _uow.GetDbContext().ChartAccountDetail.Where(x => x.IsDeleted == false && x.AccountLevelId == 3 && x.ParentID == Account2.AccountCode).ToListAsync();

                //        }
                //    }
                //}

                BalanceSheetReportModel balanceSheetReportModel = new BalanceSheetReportModel();

                var accountDetails = _uow.GetDbContext().ChartOfAccountNew.Where(x => x.IsDeleted == false && x.AccountLevelId == 1).GroupBy(x => x.ChartOfAccountNewId);

                foreach(var obj in accountDetails)
                {
                    balanceSheetReportModel.ChartOfAccountNewId = obj.Key;


                }

                //response.data.ChartAccountList = chartaccountlist;
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

        public async Task<APIResponse> GetVoucherSummaryList(VoucherSummaryFilterModel voucherSummaryFilter)
        {
            APIResponse response = new APIResponse();

            try
            {
                //var voucherDetails = _uow.GetDbContext()
                //                                              .VoucherDetail
                //                                              .Include(x => x.VoucherTransactionDetails)
                //                                              .Where(x => x.IsDeleted == false &&
                //                                              x.VoucherTransactionDetails.Select(z => z.ChartOfAccountNewId).Intersect(voucherSummaryFilter.Accounts).Any() &&
                //                                              x.VoucherTransactionDetails.Select(z => z.BudgetLineId).Intersect(voucherSummaryFilter.BudgetLines).Any() &&
                //                                              x.CurrencyId == voucherSummaryFilter.Currency && voucherSummaryFilter.Journals.Contains(x.JournalCode) &&
                //                                              voucherSummaryFilter.Offices.Contains(x.OfficeId) &&
                //                                              x.VoucherTransactionDetails.Select(z => z.JobId).Intersect(voucherSummaryFilter.ProjectJobs).Any() &&
                //                                              x.VoucherTransactionDetails.Select(z => z.ProjectId).Intersect(voucherSummaryFilter.Projects).Any());

                //if (voucherSummaryFilter.TransactionType == (int)RECORDTYPE.Credit)
                //{
                //    voucherDetails = voucherDetails.Where(x => x.VoucherTransactionDetails.Where(z => z.Credit != 0).Any());
                //}
                //else
                //{
                //    voucherDetails = voucherDetails.Where(x => x.VoucherTransactionDetails.Where(z => z.Debit != 0).Any());
                //}

                // var result = await voucherDetails.ToListAsync();

                var spVoucherSummaryList = await _uow.GetDbContext().LoadStoredProc("get_vouchersummaryreport")
                                      .WithSqlParam("accounts", voucherSummaryFilter.Accounts)
                                      .WithSqlParam("budgetlines", voucherSummaryFilter.BudgetLines)
                                      .WithSqlParam("currencyid", voucherSummaryFilter.Currency)
                                      .WithSqlParam("journals", voucherSummaryFilter.Journals)
                                      .WithSqlParam("offices", voucherSummaryFilter.Offices)
                                      .WithSqlParam("projectjobs", voucherSummaryFilter.ProjectJobs)
                                      .WithSqlParam("projects", voucherSummaryFilter.Projects)
                                      .WithSqlParam("recordtype", voucherSummaryFilter.RecordType)
                                      .ExecuteStoredProc<SPVoucherSummaryReportModel>();
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
