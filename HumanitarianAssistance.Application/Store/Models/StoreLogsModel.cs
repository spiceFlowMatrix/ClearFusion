namespace HumanitarianAssistance.Application.Store.Models
{
    public class StoreLogsModel
    {
        public string EventType { get; set; }
        public string EventBy {get; set;}  
        public string EventOn {get; set;} 
        public string LogText { get; set; }
        public long? PurchaseId { get; set; }
        public string PurchaseName {get; set;}
    }
}