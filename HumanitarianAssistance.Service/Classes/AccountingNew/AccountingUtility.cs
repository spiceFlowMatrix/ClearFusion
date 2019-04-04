using HumanitarianAssistance.Common.Helpers;
using System;
namespace HumanitarianAssistance.Service.Classes.AccountingNew
{
    public static class AccountingUtility
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
    }
}
