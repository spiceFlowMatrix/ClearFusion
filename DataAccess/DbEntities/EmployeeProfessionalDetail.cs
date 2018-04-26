using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class EmployeeProfessionalDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long EmployeeProfessionalId { get; set; }
        public int? EmployeeId { get; set; }
        public EmployeeDetail EmployeeDetails { get; set; }
        public int? EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        public int? OfficeId { get; set; }
        public OfficeDetail OfficeDetail { get; set; }
        public int? DesignationId { get; set; }
        public DesignationDetail DesignationDetails { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime? HiredOn { get; set; }
        public DateTime? FiredOn { get; set; }
        public string FiredReason { get; set; }
        public DateTime? ResignationOn { get; set; }
        public string ResignationReason { get; set; }
        public string JobDescription { get; set; }
        public string TrainingBenefits { get; set; }

		public int? EmployeeContractTypeId { get; set; }
		public EmployeeContractType EmployeeContractType { get; set; }
	}
}
