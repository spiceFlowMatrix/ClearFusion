using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System;
using HumanitarianAssistance.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Domain.Entities.HR;
using System.Collections.Generic;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class EmployeeAppraisalDetailCommandHandler : IRequestHandler<EmployeeAppraisalDetailCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EmployeeAppraisalDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(EmployeeAppraisalDetailCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    EmployeeAppraisalDetails ifExistEmpRecord = await _dbContext.EmployeeAppraisalDetails.Where(x => x.EmployeeId == request.EmployeeId  && x.IsDeleted == false && x.CurrentAppraisalDate.Date.Day == request.CurrentAppraisalDate.Date.Day).FirstOrDefaultAsync();
                    if (ifExistEmpRecord == null)
                    {
                        EmployeeAppraisalDetails empArraidalDetial = new EmployeeAppraisalDetails()
                        {
                            EmployeeId = request.EmployeeId,
                            AppraisalPeriod = request.AppraisalPeriod,
                            CurrentAppraisalDate = request.CurrentAppraisalDate,
                            AppraisalStatus = null,
                            CreatedById = request.CreatedById,
                            CreatedDate = request.CreatedDate,
                        };
                        await _dbContext.EmployeeAppraisalDetails.AddAsync(empArraidalDetial);
                        await _dbContext.SaveChangesAsync();


                        List<EmployeeAppraisalQuestions> appraisalQues = new List<EmployeeAppraisalQuestions>();
                        if (request.GeneralProfessionalIndicatorQuestion.Count > 0)
                        {
                            foreach (var item in request.GeneralProfessionalIndicatorQuestion)
                            {
                                EmployeeAppraisalQuestions eaq = new EmployeeAppraisalQuestions();
                                eaq.AppraisalGeneralQuestionsId = item.AppraisalGeneralQuestionsId;
                                eaq.Score = item.Score;
                                eaq.Remarks = item.Remarks;
                                eaq.EmployeeId = request.EmployeeId;
                                eaq.CurrentAppraisalDate = request.CurrentAppraisalDate;
                                eaq.CreatedDate = DateTime.Now;
                                eaq.CreatedById = request.CreatedById;
                                eaq.EmployeeAppraisalDetailsId = empArraidalDetial.EmployeeAppraisalDetailsId;
                                appraisalQues.Add(eaq);
                            }
                            await _dbContext.EmployeeAppraisalQuestions.AddRangeAsync(appraisalQues);
                            await _dbContext.SaveChangesAsync();
                        }

                        List<EmployeeAppraisalTeamMember> teamMembers = new List<EmployeeAppraisalTeamMember>();

                        if (request.AppraisalMembers.Count > 0)
                        {
                            foreach (var i in request.AppraisalMembers)
                            {
                                EmployeeAppraisalTeamMember eam = new EmployeeAppraisalTeamMember();
                                eam.EmployeeId = i.EmployeeId;
                                eam.EmployeeAppraisalDetailsId = empArraidalDetial.EmployeeAppraisalDetailsId;
                                eam.CreatedById = request.CreatedById;
                                eam.CreatedDate = request.CreatedDate;
                                eam.IsDeleted = false;
                                teamMembers.Add(eam);
                            }
                            await _dbContext.EmployeeAppraisalTeamMember.AddRangeAsync(teamMembers);
                            await _dbContext.SaveChangesAsync();
                        }


                        EmployeeEvaluation obj = new EmployeeEvaluation();
                        obj.FinalResultQues1 = request.FinalResultQues1;
                        obj.FinalResultQues2 = request.FinalResultQues2;
                        obj.FinalResultQues3 = request.FinalResultQues3;
                        obj.FinalResultQues4 = request.FinalResultQues4;
                        obj.FinalResultQues5 = request.FinalResultQues5;
                        obj.CommentsByEmployee = request.CommentsByEmployee;
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = request.CreatedDate;
                        obj.CurrentAppraisalDate = DateTime.Now;
                        obj.EmployeeId = request.EmployeeId;
                        obj.EmployeeAppraisalDetailsId = empArraidalDetial.EmployeeAppraisalDetailsId;
                        await _dbContext.EmployeeEvaluation.AddAsync(obj);

                        List<StrongandWeakPoints> StrongList = new List<StrongandWeakPoints>();

                        if (request.AppraisalStrongPoints.Count > 0)
                        {

                            foreach (var item in request.AppraisalStrongPoints)
                            {
                                StrongandWeakPoints sp = new StrongandWeakPoints();
                                sp.CreatedById = request.CreatedById;
                                sp.CreatedDate = request.CreatedDate;
                                sp.CurrentAppraisalDate = DateTime.Now;
                                sp.EmployeeId = request.EmployeeId;
                                sp.Point = item.StrongPoints;
                                sp.Status = (int)AppriasalStorngWeakPointType.Strong;  // 1 for strong points
                                sp.IsDeleted = false;
                                sp.EmployeeAppraisalDetailsId = empArraidalDetial.EmployeeAppraisalDetailsId;
                                StrongList.Add(sp);
                            }
                            await _dbContext.StrongandWeakPoints.AddRangeAsync(StrongList);
                        }
                        List<StrongandWeakPoints> WeakList = new List<StrongandWeakPoints>();

                        if (request.AppraisalWeakPoints.Count > 0)
                        {
                            foreach (var item in request.AppraisalWeakPoints)
                            {
                                StrongandWeakPoints wp = new StrongandWeakPoints();
                                wp.CreatedById = request.CreatedById;
                                wp.CreatedDate = request.CreatedDate;
                                wp.CurrentAppraisalDate = DateTime.Now;
                                wp.EmployeeId = request.EmployeeId;
                                wp.Point = item.WeakPoints;
                                wp.Status = (int)AppriasalStorngWeakPointType.Weak; // 2 for Weak points
                                wp.IsDeleted = false;
                                wp.EmployeeAppraisalDetailsId = empArraidalDetial.EmployeeAppraisalDetailsId;
                                WeakList.Add(wp);
                            }
                            await _dbContext.StrongandWeakPoints.AddRangeAsync(WeakList);
                        }

                        List<EmployeeEvaluationTraining> empTraining = new List<EmployeeEvaluationTraining>();
                        if (request.AppraisalTraining.Count > 0)
                        {
                            foreach (var tr in request.AppraisalTraining)
                            {
                                EmployeeEvaluationTraining empt = new EmployeeEvaluationTraining()
                                {
                                    TrainingProgram = tr.TrainingProgramBasedOn,
                                    Program = tr.Program,
                                    Participated = tr.Participated,
                                    CatchLevel = tr.CatchLevel,
                                    RefresherTrm = tr.RefresherTrm,
                                    OthRecommendation = tr.OtherRecommemenedTraining,
                                    EmployeeEvaluationTrainingId = tr.EmployeeEvaluationTrainingId,
                                    EmployeeAppraisalDetailsId = empArraidalDetial.EmployeeAppraisalDetailsId,
                                    CreatedById = request.CreatedById,
                                    CreatedDate = request.CreatedDate,
                                    IsDeleted = false,
                                };
                                empTraining.Add(empt);
                            }
                            await _dbContext.EmployeeEvaluationTraining.AddRangeAsync(empTraining);
                        }

                        await _dbContext.SaveChangesAsync();
                        success = true;
                        transaction.Commit();

                    }
                    else
                    {
                        throw new Exception("Appraisal already exist for the selected date");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;

                }
                return success;
            }
        }
    }
}