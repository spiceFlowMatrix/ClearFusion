using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Entities.Models
{
    public partial class BiddingPanelDetail
    {
        public long BiddingPanelId { get; set; }
        public long EmployeeId { get; set; }
        public string Position { get; set; }
        public string Grade { get; set; }
        public string Office { get; set; }
        public string EmailAddress { get; set; }
        public string BiddingAnnouncementAttachment { get; set; }
        public DateTime? BiddingDate { get; set; }
        public short? Status { get; set; }
    }
}
