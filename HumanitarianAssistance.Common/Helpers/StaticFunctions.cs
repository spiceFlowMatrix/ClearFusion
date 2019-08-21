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
    }

    public static class DefaultValues
    {
        public static float DefaultPensionRate = 4.5f;
    }
}
