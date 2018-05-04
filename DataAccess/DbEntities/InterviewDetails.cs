using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DbEntities
{
    public class InterviewDetails: BaseEntityWithoutId
    {
		public string CandidateName { get; set; }
		public string CandidatePosition { get; set; }
		public string ResidingProvince { get; set; }
		public string DutyStation { get; set; }
		public int Gender { get; set; }

	}
}
