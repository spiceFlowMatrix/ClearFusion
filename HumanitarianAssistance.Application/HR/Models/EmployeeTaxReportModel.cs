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
		public DateTime? DatesOfEmployeement { get; set; }
		public double? TotalWages { get; set; }
		public double? TotalTax { get; set; }


		public string OfficerName { get; set; }
		public string Position { get; set; }
		public DateTime Date { get; set; }
		public string LogoPath { get; set; }
		public string ReportDate { get; set; }

		public string Image1Path {get;set;}
		public string Image2Path {get;set;}
		public string Image3Path {get;set;}
		public string Image4Path {get;set;}
		public string Image5Path {get;set;}
		public string Image6Path {get;set;}
		public string Image7Path {get;set;}
		public string Image8Path {get;set;}
		public string Image9Path {get;set;}
		public string Image10Path {get;set;}
		public string Image11Path {get;set;}
		public string Image12Path {get;set;}
		public string Image13Path {get;set;}
		public string Image14Path {get;set;}
		public string Image15Path {get;set;}
		public string Image16Path {get;set;}
		public string Image17Path {get;set;}
		public string Image18Path {get;set;}
		public string Image19Path {get;set;}
		public string Image20Path {get;set;}
		public string Image21Path {get;set;}
		public string Image22Path {get;set;}
		public string Image23Path {get;set;}
		public string Image24Path {get;set;}

    }
}