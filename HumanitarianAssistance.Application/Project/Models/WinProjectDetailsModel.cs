namespace HumanitarianAssistance.Application.Project.Models
{
    public class WinProjectDetailsModel
    {
        public long WinProjectId { get; set; }
        public long ProjectId { get; set; }
        public string CommentText { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool? IsWin { get; set; }

        
    }
}