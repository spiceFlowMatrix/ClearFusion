using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class EmployeeLanguages : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int SpeakLanguageId { get; set; }
        public string LanguageName { get; set; }
        public string Reading { get; set; }
        public string Writing { get; set; }
        public string Speaking { get; set; }
        public string Listening { get; set; }
        public int EmployeeId { get; set; }

    }
}
