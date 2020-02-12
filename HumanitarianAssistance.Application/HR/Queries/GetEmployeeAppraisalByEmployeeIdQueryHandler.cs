using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeAppraisalByEmployeeIdQueryHandler : IRequestHandler<GetEmployeeAppraisalByEmployeeIdQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeeAppraisalByEmployeeIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(GetEmployeeAppraisalByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            try
            {
                var empAppraisalDetails = await _dbContext.EmployeeAppraisalDetails
                                                        .Include(x => x.EmployeeEvaluation)
                                                        .Include(x => x.EmployeeAppraisalQuestions)
                                                        .ThenInclude(x => x.AppraisalGeneralQuestions)
                                                        .Include(x => x.EmployeeAppraisalTeamMember)
                                                        .ThenInclude(x => x.EmployeeDetail)
                                                        .Include(x => x.StrongandWeakPoints)
                                                        .Include(x => x.EmployeeEvaluationTraining)
                .Where(x => x.EmployeeId == request.EmployeeId && x.IsDeleted == false)
                .OrderByDescending(x => x.CurrentAppraisalDate)
                .Select(x => new EmployeeAppraisalDetailNewModel
                {
                    EmployeeAppraisalDetailsId = x.EmployeeAppraisalDetailsId,
                    EmployeeId = x.EmployeeId,
                    // EmployeeCode = x.EmployeeEvaluation.EmployeeDetail.EmployeeCode,
                    // EmployeeName = x.EmployeeEvaluation.EmployeeDetail.EmployeeName,
                    // FatherName = x.EmployeeEvaluation.EmployeeDetail.FatherName,
                    // Position = x.EmployeeEvaluation.EmployeeDetail.Position,
                    // DepartmentId = x.EmployeeEvaluation.EmployeeDetail.EmployeeProfessionalDetail.DepartmentId,
                    // DepartmentName = x.EmployeeEvaluation.EmployeeDetail.EmployeeProfessionalDetail.DepartmentName,
                    // Qualification = x.Qualification,
                    // DutyStation = x.DutyStation,
                    RecruitmentDate = x.RecruitmentDate,
                    AppraisalPeriod = x.AppraisalPeriod,
                    CurrentAppraisalDate = x.CurrentAppraisalDate,
                    OfficeId = x.OfficeId,
                    AppraisalStatus = x.AppraisalStatus,
                    EvaluationDisplayDate = x.CurrentAppraisalDate.ToShortDateString(),
                    FinalResultQues1 = x.EmployeeEvaluation.FinalResultQues1,
                    FinalResultQues2 = x.EmployeeEvaluation.FinalResultQues2,
                    FinalResultQues3 = x.EmployeeEvaluation.FinalResultQues3,
                    FinalResultQues4 = x.EmployeeEvaluation.FinalResultQues4,
                    FinalResultQues5 = x.EmployeeEvaluation.FinalResultQues5,
                    CommnetByEmployee = x.EmployeeEvaluation.CommentsByEmployee,
                    AppraisalMembers = x.EmployeeAppraisalTeamMember.Select(y => new AppraisalMemberListModel
                    {
                        EmployeeId = y.EmployeeId,
                        EmployeeAppraisalTeamMemberId = y.EmployeeAppraisalTeamMemberId,
                        EmployeeName = y.EmployeeDetail.EmployeeName,
                        Type = y.EmployeeDetail.EmployeeProfessionalDetail.Designation,
                        EmployeeCode = y.EmployeeDetail.EmployeeCode

                    }).ToList(),
                    AppraisalTraining = x.EmployeeEvaluationTraining.Select(z => new AppraisalTrainingListModel
                    {
                        TrainingProgramBasedOn = z.TrainingProgram,
                        Program = z.Program,
                        Participated = z.Participated,
                        CatchLevel = z.CatchLevel,
                        RefresherTrm = z.RefresherTrm,
                        OtherRecommemenedTraining = z.OthRecommendation,
                        EmployeeEvaluationTrainingId = z.EmployeeEvaluationTrainingId,

                    }).ToList(),
                    AppraisalStrongPoints = x.StrongandWeakPoints.Where(s => s.Status == (int)AppriasalStorngWeakPointType.Strong).Select(a => new AppraisalStrongPointsListModel
                    {
                        StrongPoints = a.Point,
                        AppraisalStrongPointsId = a.StrongPointsId,

                    }).ToList(),
                    AppraisalWeakPoints = x.StrongandWeakPoints.Where(w => w.Status == (int)AppriasalStorngWeakPointType.Weak).Select(s => new AppraisalWeakPointsListModel
                    {
                        WeakPoints = s.Point,
                        AppraisalWeakPointsId = s.StrongPointsId
                    }).ToList(),
                    GeneralProfessionalIndicatorQuestion = x.EmployeeAppraisalQuestions.Select(d => new GeneralProfessionalIndicatorQuestionListModel
                    {
                        AppraisalGeneralQuestionsId = d.AppraisalGeneralQuestionsId,
                        Score = d.Score.Value,
                        Remarks = d.Remarks,
                        SequenceNumber = d.AppraisalGeneralQuestions.SequenceNo,
                        QuestionEnglish = d.AppraisalGeneralQuestions.Question,

                        // CurrentAppraisalDate = d.CurrentAppraisalDate,
                    }).ToList(),
                   AppraisalScore = x.EmployeeAppraisalQuestions.Count> 0 ? (x.EmployeeAppraisalQuestions.Sum(sc => sc.Score.Value)) : 0,
                   TotalScore = (double)(x.EmployeeAppraisalQuestions.Count> 0 ? Math.Round((decimal)(x.EmployeeAppraisalQuestions.Average(av=>av.Score.Value)),2) : 0)
                })
                .ToListAsync();
              
                response.Add("AppraisalList", empAppraisalDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }

}