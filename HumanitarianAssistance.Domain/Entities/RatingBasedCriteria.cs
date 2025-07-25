﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Domain.Entities.Project;

namespace HumanitarianAssistance.Domain.Entities
{
    public class RatingBasedCriteria : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int RatingBasedCriteriaId { get; set; }
        public int? InterviewDetailsId { get; set; }
        public string CriteriaQuestion { get; set; }
        public int? Rating { get; set; }
        [ForeignKey("InterviewDetailsId")]
        public InterviewDetails InterviewDetails { get; set; }
        
        //new UI changes
        
        [ForeignKey ("InterviewId")]
		public ProjectInterviewDetails ProjectInterviewDetails { get; set; }
		public int? InterviewId { get; set; }
        public int? QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public RatingBasedCriteriaQuestions RatingBasedCriteriaQuestions { get; set; }
        public int? Score { get; set; }
    }
}
