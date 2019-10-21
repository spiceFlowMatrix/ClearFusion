using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities
{
   public class ApplicationPages: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string ModuleName { get; set; }
        public int ModuleId { get; set; }
    }
}
