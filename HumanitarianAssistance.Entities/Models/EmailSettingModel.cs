using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmailSettingModel : BaseModel
    {
        public long EmailId { get; set; }
        public string SenderEmail { get; set; }
        public int EmailTypeId { get; set; }
        public string EmailTypeName { get; set; }
        public string SenderPassword { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpServer { get; set; }
        public Boolean? EnableSSL { get; set; }
    }

    public class EmailTypeModel
    {
        public int EmailTypeId { get; set; }
        public string EmailTypeName { get; set; }
    }
}
