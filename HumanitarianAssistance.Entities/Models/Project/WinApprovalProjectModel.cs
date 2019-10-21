using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
  public  class WinApprovalProjectModel
    {
            public long WinProjectId { get; set; }            
            public long ProjectId { get; set; }
            public string CommentText { get; set; }
            public string FileName { get; set; }
            public string FilePath { get; set; }
            public bool IsWin { get; set; }
        public string UploadedFile { get; set; }
    }
}
