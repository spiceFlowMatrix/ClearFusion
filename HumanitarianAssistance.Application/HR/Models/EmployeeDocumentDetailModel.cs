using HumanitarianAssistance.Application.Infrastructure;
using System;


namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeeDocumentDetailModel : BaseModel
    {
        public int DocumentID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentFilePath { get; set; }
        public DateTime? DocumentDate { get; set; }
        public int EmployeeID { get; set; }
        public string FilePath { get; set; }
        public string DocumentGUID { get; set; }
        public string Extension { get; set; }
        public int? DocumentType { get; set; }
    }
}
