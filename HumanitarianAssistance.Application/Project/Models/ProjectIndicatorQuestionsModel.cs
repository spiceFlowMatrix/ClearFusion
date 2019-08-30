using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProjectIndicatorQuestionsModel
    {
        public long IndicatorQuestionId { get; set; }
        public string IndicatorQuestion { get; set; }
        public int? QuestionType { get; set; }
        public string QuestionTypeName { get; set; }
        public long ProjectIndicatorId { get; set; }
        public bool IsDeleted { get; set; }
        public List<VerificationSourcesModel> VerificationSources { get; set; }

    }
    public class VerificationSourcesModel
    {

        public string VerificationSourceName { get; set; }
        public long VerificationSourceId { get; set; }
        public bool IsDeleted { get; set; }
        public long? IndicatorQuestionId { get; set; }

    }
}
