using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class InterviewLanguages: BaseEntity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public int InterviewLanguagesId { get; set; }
        [ForeignKey("InterviewDetailsId")]
		public InterviewDetails InterviewDetails { get; set; }
		public int InterviewDetailsId { get; set; }
		public string LanguageName { get; set; }
		public int? LanguageId { get; set; }
		public int? Reading { get; set; }
		public int? Writing { get; set; }
		public int? Listening { get; set; }
		public int? Speaking { get; set; }

	}
}
