namespace HumanitarianAssistance.Application.Project.Models
{
    public class ExistingCandidateDetailsModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public int? Gender { get; set; }
        public int CandidateStatus { get; set; }
    }
}