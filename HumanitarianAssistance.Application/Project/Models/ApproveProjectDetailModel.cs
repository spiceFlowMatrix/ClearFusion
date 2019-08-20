using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ApproveProjectDetailModel
    {
        public long ApproveProjrctId { get; set; }
        public long ProjectId { get; set; }
        public string CommentText { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsApproved { get; set; }
        public string UploadedFile { get; set; }
        public DateTime? ReviewCompletionDate { get; set; }

    }
}
