namespace HumanitarianAssistance.Application.Project.Models
{
    public class ApproveProjectDetailsModel
    {
        public long ApproveProjrctId { get; set; }
        public long ProjectId { get; set; }
        public string CommentText { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool? IsApproved { get; set; }
        public byte[] UploadedFile { get; set; }
        public bool? IsProposalRejected { get; set; }        
    }
}