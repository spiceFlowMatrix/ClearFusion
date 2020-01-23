using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeePensionReportPdfQuery : BaseModel, IRequest<byte[]>
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Currency { get; set; }
        public List<PensionReportModel> PensionReportModel { get; set; }
        public double PensionDeductionTotal { get; set; }
        public double PensionProfitTotal { get; set; }
        public double Total { get; set; }

    }

    public class PensionReportModel
    {
        public int Year { get; set; }
        public List<PensionReportList> PensionReportList { get; set; }
    }
    public class PensionReportList
    {
        public string Date { get; set; }
        public double GrossSalary { get; set; }
        public double PensionRate { get; set; }
        public double PensionDeduction { get; set; }
        public double Profit { get; set; }
        public double Total { get; set; }
    }
}