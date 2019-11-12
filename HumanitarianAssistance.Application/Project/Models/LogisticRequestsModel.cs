using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class LogisticsRequestsModel
    {
        public long RequestId { get; set; }
        public string RequestName { get; set; }
        public int Status { get; set; }
        public double TotalCost { get; set; }
        public long ProjectId { get; set; }
        public string BudgetLine { get; set; }
        public string Currency { get; set; }
        public string Office { get; set; }   
    }
}
