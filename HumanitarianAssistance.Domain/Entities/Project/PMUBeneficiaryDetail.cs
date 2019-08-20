using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class PMUBeneficiaryDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int BeneficiaryID { get; set; }

        public int? PMUProjectID { get; set; }
        public int? SerialNo { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string Province { get; set; }
        [StringLength(50)]
        public string District { get; set; }
        [StringLength(50)]
        public string Village { get; set; }
        [StringLength(50)]
        public string ID { get; set; }
        public int? Age { get; set; }
        [StringLength(50)]
        public string Sex { get; set; }
        [StringLength(50)]
        public string MaritalStatus { get; set; }
        [StringLength(50)]
        public string Referrer { get; set; }
        public DateTime? ReferDate { get; set; }
        [StringLength(50)]
        public string TypeOfCase { get; set; }
        [StringLength(50)]
        public string TelephoneNo { get; set; }
    }
}
