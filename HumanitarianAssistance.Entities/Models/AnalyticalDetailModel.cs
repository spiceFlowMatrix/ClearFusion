using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class AnalyticalDetailModel
    {
        public long AnalyticalId { get; set; }
        public string MemoCode { get; set; }
        public byte MemoType { get; set; }
        public string Program { get; set; }
        public string Project { get; set; }
        public string Job { get; set; }
        public string Sector { get; set; }
        public string Area { get; set; }
        public string MDCode { get; set; }
        public string MemoName { get; set; }
        public float BLAmount { get; set; }
        public string BLCurrCode { get; set; }
        public string CostBook { get; set; }
        public byte Status { get; set; }
        public string DonorCode { get; set; }
        public byte BLType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float ReceivedAmount { get; set; }
        public string Attachment { get; set; }
    }
}
