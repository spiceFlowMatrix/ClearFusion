using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class ProjectHiringCandidateDetailModel
    {
        public int? EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public int GradeId { get; set; }
        public int? EmployeeTypeId { get; set; }

        public string EmployeeTypeName { get; set; }
        public long HiringRequestId { get; set; }
        public string Gender { get; set; }
        public bool IsInterViewed { get; set; }

        public bool IsShortListed { get; set; }
        public bool IsSelected { get; set; }

    }

    public class CandidateInterViewModel {

        public int? InterviewDetailsId { get; set; }
        public int EmployeeID { get; set; }
        public long? JobId { get; set; }
        public string Status { get; set; }
        public string JobDescription { get; set; }
    }

}
