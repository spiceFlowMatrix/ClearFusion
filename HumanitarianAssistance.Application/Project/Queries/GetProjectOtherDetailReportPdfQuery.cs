using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectOtherDetailReportPdfQuery : IRequest<byte[]>
    {
        public bool opportunitytype {get; set;}
        public bool donor {get; set;}
        public bool opportunityno {get; set;}
        public bool opportunity {get; set;}
        public bool enddate {get; set;}
        public bool opportunitydesc {get; set;}
        public bool country {get; set;}
        public bool province {get; set;}
        public bool district {get; set;}
        public bool office {get; set;}
        public bool sector {get; set;}
        public bool program {get; set;}
        public bool startdate {get; set;}
        public bool projgoal {get; set;}
        public bool projobj {get; set;}
        public bool reoidate {get; set;}
        public bool submissiondate {get; set;}
        public bool mainactivities {get; set;}
        public bool dirbenmale {get; set;}
        public bool dirbenfemale {get; set;}
        public bool indirbenmale {get; set;}
        public bool indirbenfemale {get; set;}
        public bool strengthconsideration {get; set;}
        public bool genderconsideration {get; set;}
        public bool genderremarks {get; set;}
        public bool security {get; set;}
        public bool securityconsideration {get; set;}
        public bool securityremarks {get; set;}
        public int ProjectId { get; set; }
    }
}
