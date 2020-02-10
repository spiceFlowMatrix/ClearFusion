using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.ActionLog {
    public class AuditLog : BaseEntity {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Column (Order = 1)]
        public long AuditLogId { get; set; }
        public int? TypeOfEntity { get; set; }      
        public int? EntityId { get; set; } 
        public int? ActionTypeId { get; set; }  
        public string ActionDescription { get; set; }         
    }
}