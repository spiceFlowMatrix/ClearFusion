using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class EmployeeSalaryPaymentHistory : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int SalaryPaymentId { get; set; }
        public int EmployeeId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public long VoucherNo { get; set; }
        public bool IsSalaryReverse { get; set; }

        [ForeignKey("EmployeeId")]
        public EmployeeDetail EmployeeDetail { get; set; }
        [ForeignKey("VoucherNo")]
        public VoucherDetail VoucherDetail { get; set; }


    }
}
