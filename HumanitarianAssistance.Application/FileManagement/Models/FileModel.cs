namespace HumanitarianAssistance.Application.FileManagement.Models
{
    public class FileModel
    {
        public int PageId { get; set; }
        public long? RecordId { get; set; }
        public long? DocumentFileId { get; set; }
        public int? DocumentTypeId { get; set; }
    }
}