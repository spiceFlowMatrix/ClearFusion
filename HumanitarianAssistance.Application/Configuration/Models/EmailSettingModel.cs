namespace HumanitarianAssistance.Application.Configuration.Models
{
    public class EmailSettingModel
    {
        public long EmailId { get; set; }
        public string SenderEmail { get; set; }
        public int EmailTypeId { get; set; }
        public string EmailTypeName { get; set; }
        public string SenderPassword { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpServer { get; set; }
        public bool? EnableSSL { get; set; }
    }
}