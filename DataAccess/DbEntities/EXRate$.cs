using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class EXRate_ : BaseEntity
    {
        [StringLength(255)]
        public string regCode { get; set; }
        [StringLength(255)]
	    public string DATE { get; set; }
        [StringLength(255)]
        public string CURR { get; set; }
        [StringLength(255)]
        public string RATE { get; set; }
    }
}
