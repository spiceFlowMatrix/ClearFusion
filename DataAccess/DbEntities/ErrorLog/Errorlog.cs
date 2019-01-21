using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.ErrorLog
{
  public  class Errorlog : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long ExceptionId { get; set; }
        public int? Status { get; set; }
        public string stackTrace { get; set; }
        public string UserName { get; set; }
        public int? Section { get; set; }
        public string ModuleName { get; set; }
        public bool? IsActive { get; set; }
        public string FileName { get; set; }
        public string DataXml { get; set; }
    }
}
