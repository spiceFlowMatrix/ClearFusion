using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class ActivityDocumentDetailModel
    {
        public long ActtivityDocumentId { get; set; }
        public string ActivityDocumentsFilePath { get; set; }
        public string ActivityDocumentsFileName { get; set; }
        public int? StatusId { get; set; }
        public long ActivityId { get; set; }
        public string ActivityDocumentName { get; set; }
    }

    public class ProjectActivityDocumentViewModel
    {
        public long ActivityId { get; set; }
        public long? MonitoringId { get; set; }
        public long? ProjectPhaseId { get; set; }
    }
}
