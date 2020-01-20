namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeeLanguageModel
    {
        public int? SpeakLanguageId { get; set; }
        public string LanguageName { get; set; }
        public int? LanguageId { get; set; }
        public int? Reading { get; set; }
        public int? Writing { get; set; }
        public int? Speaking { get; set; }
        public int? Listening { get; set; }
        public int? EmployeeId { get; set; }
    }
}