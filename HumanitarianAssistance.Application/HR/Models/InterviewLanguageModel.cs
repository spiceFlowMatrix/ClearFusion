using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class InterviewLanguageModel
    {
        public string LanguageName { get; set; }
        public int? LanguageId { get; set; }
        public int? Reading { get; set; }
        public int? Writing { get; set; }
        public int? Listening { get; set; }
        public int? Speaking { get; set; }
    }
}
