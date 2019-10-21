using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ActivityDocumentDetailModel
    {
        public long ActivityDocumentId { get; set; } 
        public string ActivityDocumentsFilePath { get; set; }
        public string ActivityDocumentsFileName { get; set; }
        public int? StatusId { get; set; }
        public long ActivityId { get; set; }
        public string ActivityDocumentName { get; set; }
    }
}
