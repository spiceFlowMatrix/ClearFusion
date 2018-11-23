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

                var accountDetails = _uow.GetDbContext().ChartAccountDetail.Where(x => x.IsDeleted == false && x.AccountLevelId == 1).GroupBy(x => x.AccountCode);

                foreach(var obj in accountDetails)
                {
                    balanceSheetReportModel.AccountCode = obj.Key;


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

    }
}
