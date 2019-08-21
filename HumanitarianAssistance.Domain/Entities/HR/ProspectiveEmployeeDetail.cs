using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class ProspectiveEmployeeDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int EmployeeID { get; set; }
        [StringLength(20)]
        public string EmployeeCode { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        [StringLength(100)]
        public string FatherName { get; set; }
        [StringLength(5)]
        public string Sex { get; set; }
        [StringLength(50)]
        public string Passport { get; set; }
        [StringLength(50)]
        public string ReferBy { get; set; }
        [StringLength(50)]
        public string Nationality { get; set; }
        [StringLength(20)]
        public string IDCard { get; set; }
        [StringLength(50)]
        public string Qualification { get; set; }
        [StringLength(50)]
        public string CurrentDesignation { get; set; }
        public DateTime? CVDate { get; set; }
        [StringLength(100)]
        public string Profession { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(20)]
        public string Fax { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(200)]
        public string PermanentAddress { get; set; }
        [StringLength(200)]
        public string CurrentAddress { get; set; }
        public string Remarks { get; set; }
        public float? DemandPay { get; set; }
        [StringLength(5)]
        public string DemandCurrency { get; set; }
        [StringLength(10)]
        public string RegCode { get; set; }
    }
}
