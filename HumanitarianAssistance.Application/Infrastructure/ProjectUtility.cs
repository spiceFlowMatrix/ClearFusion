using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Infrastructure
{
    public static class ProjectUtility
    {
        public static string GetClientCode(this string ClientId)
        {
            string code = string.Empty;
            if (ClientId.Length == 1)
                return code = "0000" + ClientId;
            else if (ClientId.Length == 2)
                return code = "000" + ClientId;
            else if (ClientId.Length == 3)
                return code = "00" + ClientId;
            else if (ClientId.Length == 4)
                return code = "0" + ClientId;
            else
                return code = "" + ClientId;
        }
        public static string GetContractCode(this string ContractId)
        {
            string code = string.Empty;
            if (ContractId.Length == 1)
                return code = "0000" + ContractId;
            else if (ContractId.Length == 2)
                return code = "000" + ContractId;
            else if (ContractId.Length == 3)
                return code = "00" + ContractId;
            else if (ContractId.Length == 4)
                return code = "0" + ContractId;
            else
                return code = "" + ContractId;
        }
        public static string GetJobCode(this string JobId)
        {
            string code = string.Empty;
            if (JobId.Length == 1)
                return code = "0000" + JobId;
            else if (JobId.Length == 2)
                return code = "000" + JobId;
            else if (JobId.Length == 3)
                return code = "00" + JobId;
            else if (JobId.Length == 4)
                return code = "0" + JobId;
            else
                return code = "" + JobId;
        }
        public static string GetPolicyCode(this string PolicyId)
        {
            string code = string.Empty;
            if (PolicyId.Length == 1)
                return code = "0000" + PolicyId;
            else if (PolicyId.Length == 2)
                return code = "000" + PolicyId;
            else if (PolicyId.Length == 3)
                return code = "00" + PolicyId;
            else if (PolicyId.Length == 4)
                return code = "0" + PolicyId;
            else
                return code = "" + PolicyId;
        }

        public static string GenerateCode(long id)
        {
            return string.Format("{0:D5}", id);
        }

        /// <summary>
        /// Format: P0001, P0011 & so on ..
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GenerateProjectCode(long id)
        {
            //return "P" + string.Format("{0:D4}", id);
            return "P" + id.ToString("D4");
        }

        /// <summary>
        /// Format: Project#1 Job#1: P0001-J001
        ///         Project#1 Job#2: P0001-J002
        ///         
        ///         Project#2 Job#1: P0002-J001
        ///         Project#2 Job#2: P0002-J002
        /// </summary>
        /// <param name="projectCode"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GenerateProjectBudgetLineCode(string projectCode, long id)
        {
            return projectCode + "-B" + string.Format("{0:D5}", id);
        }

        public static string GenerateProjectJobCode(string projectCode, long id)
        {
            return projectCode + "-J" + string.Format("{0:D3}", id);
        }
        public static string getScheduleCode(this string ScheduleId)
        {
            string code = string.Empty;
            if (ScheduleId.Length == 1)
                return code = "0000" + ScheduleId;
            else if (ScheduleId.Length == 2)
                return code = "000" + ScheduleId;
            else if (ScheduleId.Length == 3)
                return code = "00" + ScheduleId;
            else if (ScheduleId.Length == 4)
                return code = "0" + ScheduleId;
            else
                return code = "" + ScheduleId;
        }
    }
}

