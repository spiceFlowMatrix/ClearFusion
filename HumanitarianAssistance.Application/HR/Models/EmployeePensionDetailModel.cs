using System;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeePensionDetailModel
    {
        public long? PensionId { get; set; }
        public string CurrencyName { get; set; }
        public double? Amount { get; set; }
        public DateTime? Date { get; set; }
        public int? CurrencyId { get; set; }
    }
}