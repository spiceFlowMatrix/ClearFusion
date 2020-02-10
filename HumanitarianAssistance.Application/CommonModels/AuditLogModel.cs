namespace HumanitarianAssistance.Application.CommonModels {
    public class AuditLogModel {
        public long AuditLogId { get; set; }
        public int? TypeOfEntity { get; set; }
        public int? EntityId { get; set; }
        public int? ActionTypeId { get; set; }
        public string ActionDescription { get; set; }
    }
}