using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class LogisticsRequestsModel
    {
        public long RequestId { get; set; }
        public string Description { get; set; }
        public string RequestCode { get; set; }
        public string Status { get; set; }
        public double TotalCost { get; set; }
        public long ProjectId { get; set; }
        public string BudgetLine { get; set; }
        public string Currency { get; set; }
        public string Office { get; set; }   
        public int ComparativeStatus { get; set; }
        public string ProcessingType { get; set; }
    }
}
