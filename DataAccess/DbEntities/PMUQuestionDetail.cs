using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class PMUQuestionDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int QuestionID { get; set; }
        public int? PMUProjectID { get; set; }
        [StringLength(100)]
        public string Question { get; set; }
        [StringLength(20)]
        public string Rating1Text { get; set; }
        [StringLength(20)]
        public string Rating2Text { get; set; }
        [StringLength(20)]
        public string Rating3Text { get; set; }
        [StringLength(20)]
        public string Rating4Text { get; set; }
        [StringLength(20)]
        public string Rating5Text { get; set; }
    }
}
