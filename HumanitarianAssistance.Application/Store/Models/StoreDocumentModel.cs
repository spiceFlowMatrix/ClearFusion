namespace HumanitarianAssistance.Application.Store.Models
{
    public class StoreDocumentModel
    {
        public string SignedURL { get; set; }
        public long DocumentFileId { get; set; }
        public string DocumentName {get; set;}
        public int? DocumentTypeId {get; set;}
    }
}