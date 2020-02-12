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
                                                        .Include(x => x.EmployeeAppraisalTeamMember).
                                                        Include(x => x.StrongandWeakPoints)
                                                        .Include(x => x.EmployeeEvaluationTraining)
                .Where(x => x.EmployeeId == request.EmployeeId && x.IsDeleted == false)
                .OrderByDescending(x => x.CurrentAppraisalDate)
                .Select(x => new EmployeeAppraisalDetailNewModel
                {
                    EmployeeAppraisalDetailsId = x.EmployeeAppraisalDetailsId,
                    EmployeeId = x.EmployeeId,
                    EmployeeCode = x.EmployeeCode,
                    EmployeeName = x.EmployeeName,
                    FatherName = x.FatherName,
                    Position = x.Position,
                    Department = x.Department,
                    Qualification = x.Qualification,
                    DutyStation = x.DutyStation,
                    RecruitmentDate = x.RecruitmentDate,
                    AppraisalPeriod = x.AppraisalPeriod,
                    CurrentAppraisalDate = x.CurrentAppraisalDate,
                    OfficeId = x.OfficeId,
                    // TotalScore = x.TotalScore,
                    AppraisalStatus = x.AppraisalStatus,
                    AppraisalScore = (x.AppraisalScore != null ? Math.Round((double)x.AppraisalScore) : 0) + " - " + ((MarkedScores)(x.AppraisalScore != null ? Math.Round((double)x.AppraisalScore) : 0)).ToString(),
                    EvaluationDisplayDate = x.CurrentAppraisalDate.ToShortDateString(),
                    FinalResultQues1 = x.EmployeeEvaluation.FinalResultQues1,
                    FinalResultQues2 = x.EmployeeEvaluation.FinalResultQues2,
                    FinalResultQues3 = x.EmployeeEvaluation.FinalResultQues3,
                    FinalResultQues4 = x.EmployeeEvaluation.FinalResultQues4,
                    FinalResultQues5 = x.EmployeeEvaluation.FinalResultQues5,
                    CommnetByEmployee = x.EmployeeEvaluation.CommentsByEmployee,
                    AppraisalMembers = x.EmployeeAppraisalTeamMember.Select(y => new AppraisalMemberListModel
                    {
                        //EmployeeId = y.EmployeeId,
                        EmployeeAppraisalTeamMemberId = y.EmployeeAppraisalTeamMemberId

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
                    AppraisalStrongPoints = x.StrongandWeakPoints.Select(a => new AppraisalStrongPointsListModel
                    {
                        StrongPoints = a.Point,
                        AppraisalStrongPointsId = a.StrongPointsId,

                    }).ToList(),
                    AppraisalWeakPoints = x.StrongandWeakPoints.Select(s => new AppraisalWeakPointsListModel
                    {
                        WeakPoints = s.Point,
                        AppraisalWeakPointsId = s.StrongPointsId
                    }).ToList(),
                    GeneralProfessionalIndicatorQuestion = x.EmployeeAppraisalQuestions.Select(d => new GeneralProfessionalIndicatorQuestionListModel
                    {
                        AppraisalGeneralQuestionsId = d.AppraisalGeneralQuestionsId,
                        Score = d.Score.Value,
                        Remarks = d.Remarks,
                        // CurrentAppraisalDate = d.CurrentAppraisalDate,
                    }).ToList()

                })
                .ToListAsync();

                // if (empAppraisalDetails != null)
                // {
                //     EmployeeAppraisalDetailsModel model = new EmployeeAppraisalDetailsModel();
                //     var quesLst = await _dbContext.EmployeeAppraisalQuestions.Include(x => x.AppraisalGeneralQuestions).Where(x => x.CurrentAppraisalDate == empAppraisalDetails.CurrentAppraisalDate && x.EmployeeId == empAppraisalDetails.EmployeeId && x.IsDeleted == false).ToListAsync();
                //     model.EmployeeAppraisalDetailsId = empAppraisalDetails.EmployeeAppraisalDetailsId;
                //     model.EmployeeId = empAppraisalDetails.EmployeeId;
                //     model.EmployeeCode = empAppraisalDetails.EmployeeCode;
                //     model.EmployeeName = empAppraisalDetails.EmployeeName;
                //     model.FatherName = empAppraisalDetails.FatherName;
                //     model.Position = empAppraisalDetails.Position;
                //     model.Department = empAppraisalDetails.Department;
                //     model.Qualification = empAppraisalDetails.Qualification;
                //     model.DutyStation = empAppraisalDetails.DutyStation;
                //     model.RecruitmentDate = empAppraisalDetails.RecruitmentDate;
                //     model.AppraisalPeriod = empAppraisalDetails.AppraisalPeriod;
                //     model.CurrentAppraisalDate = empAppraisalDetails.CurrentAppraisalDate;
                //     model.OfficeId = empAppraisalDetails.OfficeId;
                //     model.TotalScore = empAppraisalDetails.TotalScore;
                //     model.AppraisalStatus = empAppraisalDetails.AppraisalStatus;
                //     var scorePoint = empAppraisalDetails.AppraisalScore != null ? Math.Round((double)empAppraisalDetails.AppraisalScore) : 0;
                //     if (empAppraisalDetails.AppraisalScore != null)
                //     {
                //         model.AppraisalScore = scorePoint + " - " + ((MarkedScores)scorePoint).ToString();
                //     }
                //     foreach (var element in quesLst)
                //     {
                //         EmployeeAppraisalQuestionModel questions = new EmployeeAppraisalQuestionModel();
                //         questions.QuestionEnglish = element.AppraisalGeneralQuestions.Question;
                //         questions.QuestionDari = element.AppraisalGeneralQuestions.DariQuestion;
                //         questions.SequenceNo = element.AppraisalGeneralQuestions.SequenceNo.Value;
                //         questions.AppraisalGeneralQuestionsId = element.AppraisalGeneralQuestionsId;
                //         questions.Score = element.Score;
                //         questions.Remarks = element.Remarks;
                //         model.EmployeeAppraisalQuestionList.Add(questions);
                //     }
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