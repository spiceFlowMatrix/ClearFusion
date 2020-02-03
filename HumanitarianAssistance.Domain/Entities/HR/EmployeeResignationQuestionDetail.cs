using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
  public class EmployeeResignationQuestionDetail:BaseEntity
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(Order = 1)]
    public int ResignationQuestionId { get; set; }
    public int QuestionId { get; set; }
    public int Answer { get; set; }
    public long ResignationId { get; set; }
    [ForeignKey("QuestionId")]
    public ExitInterviewQuestionsMaster ExitInterviewQuestionsMaster { get; set; }
    [ForeignKey("ResignationId")]
    public EmployeeResignationDetail EmployeeResignationDetail { get; set; }
  
  }
}