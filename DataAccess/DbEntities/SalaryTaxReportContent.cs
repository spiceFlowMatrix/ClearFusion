using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class SalaryTaxReportContent : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int SalaryTaxReportContentId { get; set; }
        public string EmployerAuthorizedOfficerName { get; set; }
        public string PositionAuthorizedOfficer { get; set; }
        public int OfficeId { get; set; }
    }
}
