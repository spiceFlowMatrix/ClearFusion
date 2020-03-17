namespace HumanitarianAssistance.Application.CommonModels {
    public class AuditLogModel {
        public int EmployeeId { get; set; }
        public string PerformedBy { get; set; }
        public int? TypeOfEntity { get; set; }
        public string TypeOfEntityName { get; set; }
        public int? EntityId { get; set; }
        public int? ActionTypeId { get; set; }
        public string ActionTypeIdName { get; set; }
        public string ActionDescription { get; set; }
    }
}