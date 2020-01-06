using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Models
{

    public class ModelList
    {
        public List<DesignationTechnicalQuestionModel> DesignationList  {get; set;}
        public long RecordCount {get; set;}
    }
    public class DesignationTechnicalQuestionModel
    {
        public DesignationTechnicalQuestionModel()
        {
            TechnicalQuestionList = new List<TechnicalQuestionModel>();
        }

        public int DesignationId { get; set; }
        public string Designation { get; set; }
        public string Description { get; set; }
        public List<TechnicalQuestionModel> TechnicalQuestionList { get; set; }
    }

    public class TechnicalQuestionModel
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
    }
}