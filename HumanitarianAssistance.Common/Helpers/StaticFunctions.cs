using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Common.Helpers
{
    public static class StaticFunctions
    {
        public static Guid GuidGenerator()
        {
            var buffer = Guid.NewGuid().ToByteArray();

            var time = new DateTime(0x76c, 1, 1);
            var now = DateTime.Now;
            var span = new TimeSpan(now.Ticks - time.Ticks);
            var timeOfDay = now.TimeOfDay;

            var bytes = BitConverter.GetBytes(span.Days);
            var array = BitConverter.GetBytes(
                (long)(timeOfDay.TotalMilliseconds / 3.333333));

            Array.Reverse(bytes);
            Array.Reverse(array);
            Array.Copy(bytes, bytes.Length - 2, buffer, buffer.Length - 6, 2);
            Array.Copy(array, array.Length - 4, buffer, buffer.Length - 4, 4);

            return new Guid(buffer);
        }

        public static double SalaryCalculate(double grossSalary, double exchangeRate)
        {
            // Compare Gross salary only in AFG

            double salaryTax = 0;
            if (grossSalary < 5000)
            {
                salaryTax = 0;
            }
            else if (grossSalary >= 5000 && grossSalary < 12500)
            {
                salaryTax = (grossSalary * exchangeRate - 5000) * 2 / 100;
            }
            else if (grossSalary >= 12500 && grossSalary < 100000)
            {
                salaryTax = (((grossSalary * exchangeRate - 12500) * 10 / 100) + 150) / exchangeRate;
            }
            else
            {
                salaryTax = ((((grossSalary * exchangeRate) - 100000) * 20 / 100) + 8900) / exchangeRate;
            }

            //Returned in AFG
            return salaryTax;
        }

// Project actiivty recurring days 
        public static DateTime GetRecurringDays(int? RecurringCount, int? RecurrinTypeId, DateTime? PlannedStartDate)
        {
            DateTime eventDate = new DateTime();
            string myDate = PlannedStartDate.Value.ToString();
            DateTime date = Convert.ToDateTime(myDate);
            int year = date.Year;
            int month = date.Month;
            int numberOfReccuredDays = 0;
            int numberOfDaysInQuater = 0;
            int quarterdays = 0;
            // daily
            if (RecurrinTypeId == 1)
            {
                numberOfReccuredDays = 1 * RecurringCount.Value;
                eventDate = date.AddDays(numberOfReccuredDays - 1);

            }
            // weekly
            else if (RecurrinTypeId == 2)
            {
                numberOfReccuredDays = 7 * RecurringCount.Value;
                eventDate = date.AddDays(numberOfReccuredDays - 1);
            }
            // monthly
            else if (RecurrinTypeId == 3)
            {

                eventDate = date.AddMonths(RecurringCount.Value);
                //to get previous day 
                eventDate = eventDate.AddDays(-1);
            }
            // yearly
            else if (RecurrinTypeId == 4)
            {
                eventDate = date.AddYears(RecurringCount.Value);
                eventDate = eventDate.AddDays(-1);

            }
            // quarterly
            else if (RecurrinTypeId == 5)
            {
                // to get number of days in quaerterly year i is quarter value(3 months)
                for (int i = 0; i < (3 * RecurringCount.Value); i++)
                {
                    quarterdays = DateTime.DaysInMonth(year, month);
                    numberOfDaysInQuater = numberOfDaysInQuater + quarterdays;
                    if (month >= 12)
                    {
                        year = year + 1;
                        month = 0;
                    }
                    month = month + 1;
                }
                eventDate = date.AddDays(numberOfDaysInQuater);
                // eventDate = eventDate.AddDays(-1);
            }
            return eventDate;
        }


    }

    public static class DefaultValues
    {
        public static float DefaultPensionRate = 4.5f;
    }

    public static class PurchaseCode
    {
        public static string GetPurchaseCode(DateTime purchaseDate, string purchaseName, long purchaseId)
        {

            return purchaseName + "-" + purchaseDate.Date.ToShortDateString() + "-" + purchaseId;
        }
    }

}
