using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using DataAccess.DbEntities.AccountingNew;
using HumanitarianAssistance.Common.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanitarianAssistance.Service.CommonUtility
{
    public static class CommonFunctions
    {

        public static List<ChartOfAccountNew> GetInputLevelAccountDetails(ChartOfAccountNew accountDetails, List<ChartOfAccountNew> allAccounts)
        {
            List<ChartOfAccountNew> accountsLevelFourth = new List<ChartOfAccountNew>();
            IEnumerable<long> accountsLevelTwo = null;
            IEnumerable<long> accountsLevelThird = null;

            try
            {

                if (accountDetails.AccountLevelId == (int)AccountLevels.MainLevel) //1
                {
                    // Gets the level 2nd accounts
                    accountsLevelTwo = allAccounts.FindAll(x => x.ParentID == accountDetails.ChartOfAccountNewId && x.AccountLevelId == (int)AccountLevels.ControlLevel).Select(x => x.ChartOfAccountNewId); //3

                    // Gets the level 3rd accounts
                    accountsLevelThird = allAccounts.FindAll(x => accountsLevelTwo.Contains(x.ParentID) && x.AccountLevelId == (int)AccountLevels.SubLevel).Select(x => x.ChartOfAccountNewId); //3

                    // Gets the level 4th accounts
                    accountsLevelFourth = allAccounts.FindAll(x => x.AccountLevelId == (int)AccountLevels.InputLevel && accountsLevelThird.Contains(x.ParentID)); //4

                }
                else if (accountDetails.AccountLevelId == (int)AccountLevels.ControlLevel) //2
                {
                    // Gets the level 3rd accounts
                    accountsLevelThird = allAccounts.FindAll(x => x.ParentID == accountDetails.ChartOfAccountNewId && x.AccountLevelId == (int)AccountLevels.SubLevel).Select(x => x.ChartOfAccountNewId); //3

                    // Gets the level 4th accounts
                    accountsLevelFourth = allAccounts.FindAll(x => x.AccountLevelId == (int)AccountLevels.InputLevel && accountsLevelThird.Contains(x.ParentID)); //4
                }
                else if (accountDetails.AccountLevelId == (int)AccountLevels.SubLevel) //3
                {
                    // Gets the level 4th accounts
                    accountsLevelFourth = allAccounts.FindAll(x => x.ParentID == accountDetails.ChartOfAccountNewId && x.AccountLevelId == (int)AccountLevels.InputLevel); //4


                }
                else if (accountDetails.AccountLevelId == (int)AccountLevels.InputLevel) //4
                {
                    // Gets the level 4th accounts
                    accountsLevelFourth = allAccounts.FindAll(x => x.ChartOfAccountNewId == accountDetails.ChartOfAccountNewId && x.AccountLevelId == (int)AccountLevels.InputLevel); //4
                }

            }
            catch (Exception ex)
            {

            }

            return accountsLevelFourth;
        }

        public static double DateDifference(DateTime endDate, DateTime startDate)
        {
            return (endDate.Date - startDate.Date).Days;
        }
    }
}
