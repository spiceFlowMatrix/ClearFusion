using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class InterviewDetails : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int InterviewDetailsId { get; set; }

        public int EmployeeID { get; set; }
        public long JobId { get; set; }

        //public string CandidateName { get; set; }
        //public string CandidatePosition { get; set; }
        //public string ResidingProvince { get; set; }
        //public string DutyStation { get; set; }
        //public int Gender { get; set; }

        public string PassportNo { get; set; }
        //public string Qualification { get; set; }
        public string University { get; set; }
        //public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string TazkiraIssuePlace { get; set; }
        public string MaritalStatus { get; set; }

        public string Experience { get; set; }

        public string ProfessionalCriteriaMarks { get; set; }

        public string MarksObtained { get; set; }
        public string WrittenTestMarks { get; set; }
        public string Ques1 { get; set; }
        public string Ques2 { get; set; }
        public string Ques3 { get; set; }
        public string PreferedLocation { get; set; }
        public string NoticePeriod { get; set; }
        public DateTime JoiningDate { get; set; }

        public long CurrentBase { get; set; }
        public bool CurrentTransportation { get; set; }
        public bool CurrentMeal { get; set; }
        public long CurrentOther { get; set; }

        public long ExpectationBase { get; set; }
        public bool ExpectationTransportation { get; set; }
        public bool ExpectationMeal { get; set; }
        public long ExpectationOther { get; set; }

        public string TotalMarksObtained { get; set; }

        public string Status { get; set; }

        public string Interviewer1 { get; set; }
        public string Interviewer2 { get; set; }
        public string Interviewer3 { get; set; }
        public string Interviewer4 { get; set; }

        public string InterviewStatus { get; set; }
    }
}
