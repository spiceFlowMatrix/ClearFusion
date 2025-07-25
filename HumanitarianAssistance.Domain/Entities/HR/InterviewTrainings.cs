﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Project;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class InterviewTrainings: BaseEntity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public int InterviewTrainingsId { get; set; }
        [ForeignKey("InterviewDetailsId")]
        public InterviewDetails InterviewDetails { get; set; }
		public int? InterviewDetailsId { get; set; }
		public int? TraininigType { get; set; }

		
		[ForeignKey ("InterviewId")]
		public ProjectInterviewDetails ProjectInterviewDetails { get; set; }
		public int? InterviewId { get; set; }
		public string NewTraininigType { get; set; }
		public string TrainingName { get; set; }
		public string StudyingCountry { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

	}
}
