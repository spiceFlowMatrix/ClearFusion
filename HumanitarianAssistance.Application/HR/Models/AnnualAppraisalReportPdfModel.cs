using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class AnnualAppraisalReportPdfModel
    {
        public int SerialNumber { get; set; }    
        public string Name { get; set; }
        public string FatherName { get; set; }  
        public string Designation { get; set; }
        public string Department { get; set; }
        public List<string> WeakPoint { get; set; }
        public List<string> StrongPoint { get; set; }
        public string AppraisalStatus { get; set; }  
        public string RequiredTraining { get; set; }
        public string EmployeeComments { get; set; }
        public string SuperviserComment { get; set; }
        public string CommitteeMemberOne { get; set; }
        public string CommitteeMemberTwo { get; set; }
        public string FinalReview { get; set; }
        public int EmployeeAppraisalDetailId { get; set; }  
    }
}
