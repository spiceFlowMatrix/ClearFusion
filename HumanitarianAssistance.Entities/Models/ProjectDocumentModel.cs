using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class ProjectDocumentModel : BaseModel
    {
        public long ProjectDocumentId { get; set; }
        public string DocumentName { get; set; }
        public DateTime DocumentDate { get; set; }
        public long ProjectId { get; set; }
        public string FilePath { get; set; }
        public string DocumentGUID { get; set; }
        public string Extension { get; set; }
    }
}
