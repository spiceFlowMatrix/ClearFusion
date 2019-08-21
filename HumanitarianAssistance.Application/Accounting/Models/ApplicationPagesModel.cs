namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class ApplicationPagesModel
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string ModuleName { get; set; }
        public int ModuleId { get; set; }
        public bool Edit { get; set; }
        public bool View { get; set; }
        public bool Approve { get; set; }
        public bool Reject { get; set; }
        public bool Agree { get; set; }
        public bool Disagree { get; set; }
        public bool OrderSchedule { get; set; }
    }
}