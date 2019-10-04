using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllVoucherSummaryReportPdfQuery : IRequest<byte[]>
    {
        // public List<long> Accounts { get; set; }
        // public List<long> BudgetLines { get; set; }
        // public int Currency { get; set; }
        // public List<int> Journals { get; set; }
        // public List<int> Offices { get; set; }
        // public List<long> ProjectJobs { get; set; }
        // public List<long> Projects { get; set; }
        // public int RecordType { get; set; }
        public string FilterValue { get; set; }
        public bool VoucherNoFlag { get; set; }
        public bool ReferenceNoFlag { get; set; }
        public bool DescriptionFlag { get; set; }
        public bool JournalNameFlag { get; set; }
        public bool DateFlag { get; set; }
    }
}