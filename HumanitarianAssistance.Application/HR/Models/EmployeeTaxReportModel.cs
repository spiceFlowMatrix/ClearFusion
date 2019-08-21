using System;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeeTaxReportModel
    {
        public long TaxPayerIdentificationNumber { get; set; }
		public string NameOfBusiness { get; set; }
		public string AddressOfBusiness { get; set; }
		public string TelephoneNumber { get; set; }
		public string EmailAddressEmployer { get; set; }


		public string EmployeeName { get; set; }
		public string EmployeeTaxpayerIdentification { get; set; }
		public string EmployeeAddress { get; set; }
		public string TelephoneNumberEmployee { get; set; }
		public string EmailAddressEmployee { get; set; }
        
		public int AnnualTaxPeriod { get; set; }
		public int DatesOfEmployeement { get; set; }
		public double? TotalWages { get; set; }
		public double? TotalTax { get; set; }


		public string OfficerName { get; set; }
		public string Position { get; set; }
		public DateTime Date { get; set; }
    }
}