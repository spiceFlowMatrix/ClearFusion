using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class RatingBasedCriteriaModel
    {
        public int InterviewDetailsId { get; set; }
        public string CriteriaQuestion { get; set; }
        public int? Rating { get; set; }
    }
}
