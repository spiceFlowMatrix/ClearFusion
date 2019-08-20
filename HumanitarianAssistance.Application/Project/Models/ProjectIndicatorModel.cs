using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProjectIndicatorViewModel
    {
        public bool IsDeleted { get; set; }
        public long ProjectIndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public string IndicatorCode { get; set; }
    }

    public class ProjectIndicatorModel
    {
        public ProjectIndicatorModel()
        {
            ProjectIndicators = new List<ProjectIndicatorViewModel>();
        }

        public List<ProjectIndicatorViewModel> ProjectIndicators { get; set; }
        public long IndicatorRecordCount { get; set; }
    }
    public class EditIndicatorModel
    {
        public EditIndicatorModel()
        {
            IndicatorQuestions = new List<IndicatorQuestions>();
        }

        public bool? IsDeleted { get; set; }
        public long IndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public string IndicatorCode { get; set; }
        public List<IndicatorQuestions> IndicatorQuestions { get; set; }

    }
    public class IndicatorQuestions
    {
        public long? QuestionId { get; set; }
        public string QuestionText { get; set; }
        public bool? IsDeleted { get; set; }
    }

}
