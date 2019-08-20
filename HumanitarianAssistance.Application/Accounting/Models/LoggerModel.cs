using System;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class LoggerModel
    {
        public int loggerDetailsId { get; set; }
        public int notificationId { get; set; }
        public bool isRead { get; set; }
        public string userName { get; set; }
        public string userId { get; set; }
        public string loggedDetail { get; set; }
        public DateTime createdDate { get; set; }
    }
}