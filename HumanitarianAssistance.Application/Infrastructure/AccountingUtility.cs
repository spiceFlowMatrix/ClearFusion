using System;
using System.Collections.Generic;
using HumanitarianAssistance.Common.Helpers;

namespace HumanitarianAssistance.Application.Infrastructure
{
    internal static class AccountingUtility
    {

        public static string GenerateVoucherReferenceCode(DateTime voucherDate, int voucherCount, string currencyCode, string officeCode)
        {
            string voucherReferenceNo = string.Empty;

            try
            {
                // Pattern: Office Code - Currency Code - Month Number - voucher count on selected month - Year
                voucherReferenceNo = $"{officeCode}-{currencyCode}-{voucherDate.Month}-{++voucherCount}-{voucherDate.Year.ToString().Substring(2)}";

            }
            catch (Exception exception)
            {
                throw exception;
            }

            return voucherReferenceNo;
        }

        public static string GetSalaryDescription(string employeeCode, string employeeName, int month, decimal? totalSalary)
        {
            string salaryDescription = string.Empty;

            try
            {
                salaryDescription = StaticResource.SalaryPaymentDone + employeeCode + "-" + employeeName + "-" + month + "-" + totalSalary;
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return salaryDescription;
        }

        public static List<DateTime> GetRegularIntervalDates(DateTime startDate, DateTime endDate, int interval)
        {
            List<DateTime> regularIntervalDates = new List<DateTime>();

            try
            {

                TimeSpan timeDiff = (endDate - startDate) / interval;

                TimeSpan timespan = TimeSpan.Zero;

                regularIntervalDates.Add(startDate);

                for (int i = 0; i < interval; i++)
                {
                    timespan += timeDiff;
                    regularIntervalDates.Add(startDate.Add(timespan));
                }

            }
            catch (Exception exception)
            {
                throw exception;
            }

            return regularIntervalDates;
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}