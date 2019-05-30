using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Common
{
    public class FileManagementModel : BaseModel
    {
        public string FileType { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public int PageId { get; set; }
        public long RecordId { get; set; }
        public string FilePath { get; set; }
    }

    public class ObjectData
    {
        public string ObjectName { get; set; }
    }
}
