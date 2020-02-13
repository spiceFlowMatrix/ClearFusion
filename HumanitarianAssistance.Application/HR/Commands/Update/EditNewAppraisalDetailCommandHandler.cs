using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Domain.Entities;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditNewAppraisalDetailCommandHandler : IRequestHandler<EditNewAppraisalDetailCommand, bool>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EditNewAppraisalDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(EditNewAppraisalDetailCommand request, CancellationToken cancellationToken)
        {
            bool success;
            using (var transaction = _dbContext.Database.BeginTransaction())
            {

                try
                {
                    EmployeeAppraisalDetails ifExistEmpRecord = await _dbContext.EmployeeAppraisalDetails.Where(x => x.EmployeeId == request.EmployeeId && x.AppraisalStatus == false && x.IsDeleted == false && x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId).FirstOrDefaultAsync();
                    if (ifExistEmpRecord != null)
                    {

                        ifExistEmpRecord.EmployeeId = request.EmployeeId;
                        ifExistEmpRecord.AppraisalPeriod = request.AppraisalPeriod;
                        ifExistEmpRecord.CurrentAppraisalDate = request.CurrentAppraisalDate;
                        ifExistEmpRecord.AppraisalStatus = false;
                        ifExistEmpRecord.ModifiedById = request.ModifiedById;
                        ifExistEmpRecord.ModifiedDate = request.ModifiedDate;
                        // await _dbContext.EmployeeAppraisalDetails.AddAsync(empArraidalDetial);
                        await _dbContext.SaveChangesAsync();

                        List<EmployeeAppraisalQuestions> quesList = new List<EmployeeAppraisalQuestions>();
                        if (request.GeneralProfessionalIndicatorQuestion.Count > 0)
                        {
                            var questionList = await _dbContext.EmployeeAppraisalQuestions.Where(x => x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId).ToListAsync();
                            _dbContext.RemoveRange(questionList);
                            foreach (var item in request.GeneralProfessionalIndicatorQuestion)
                            {

                                EmployeeAppraisalQuestions question = new EmployeeAppraisalQuestions();
                                question.Score = item.Score;
                                question.Remarks = item.Remarks;
                                question.IsDeleted = false;
                                question.EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId;
                                question.AppraisalGeneralQuestionsId = item.AppraisalGeneralQuestionsId;
                                question.EmployeeId = request.EmployeeId;
                                question.CreatedById = request.CreatedById;
                                question.CreatedDate = request.CreatedDate;
                                quesList.Add(question);

                            }
                            await _dbContext.EmployeeAppraisalQuestions.AddRangeAsync(quesList);
                            await _dbContext.SaveChangesAsync();

                        }


                        List<EmployeeAppraisalTeamMember> teamMembers = new List<EmployeeAppraisalTeamMember>();

                        if (request.AppraisalMembers.Count > 0)
                        {

                            var employeeAppraisalTeamMember = await _dbContext.EmployeeAppraisalTeamMember.Where(x => x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId).ToListAsync();
                            _dbContext.RemoveRange(employeeAppraisalTeamMember);

                            foreach (var i in request.AppraisalMembers)
                            {
                                EmployeeAppraisalTeamMember eam = new EmployeeAppraisalTeamMember();
                                eam.EmployeeId = i.EmployeeId;
                                eam.EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId;
                                eam.CreatedById = request.CreatedById;
                                eam.CreatedDate = request.CreatedDate;
                                eam.IsDeleted = false;
                                teamMembers.Add(eam);
                            }
                            await _dbContext.EmployeeAppraisalTeamMember.AddRangeAsync(teamMembers);
                            await _dbContext.SaveChangesAsync();
                        }

                        var eployeeEvaluation = await _dbContext.EmployeeEvaluation.Where(x => x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId).ToListAsync();
                        _dbContext.RemoveRange(eployeeEvaluation);


                        EmployeeEvaluation obj = new EmployeeEvaluation();
                        obj.FinalResultQues1 = request.FinalResultQues1;
                        obj.FinalResultQues2 = request.FinalResultQues2;
                        obj.FinalResultQues3 = request.FinalResultQues3;
                        obj.FinalResultQues4 = request.FinalResultQues4;
                        obj.FinalResultQues5 = request.FinalResultQues5;
                        obj.CommentsByEmployee = request.CommnetByEmployee;
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = request.CreatedDate;
                        obj.CurrentAppraisalDate = DateTime.Now;
                        obj.EmployeeId = request.EmployeeId;
                        obj.EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId;
                        await _dbContext.EmployeeEvaluation.AddAsync(obj);

                        List<StrongandWeakPoints> StrongList = new List<StrongandWeakPoints>();

                        if (request.AppraisalStrongPoints.Count > 0)
                        {
                            var empRecords = await _dbContext.StrongandWeakPoints.Where(x => x.IsDeleted == false && x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId && x.Status == (int)AppriasalStorngWeakPointType.Strong).ToListAsync();
                            _dbContext.StrongandWeakPoints.RemoveRange(empRecords);
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
                                sp.EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId;
                                StrongList.Add(sp);
                            }
                            await _dbContext.StrongandWeakPoints.AddRangeAsync(StrongList);
                        }
                        List<StrongandWeakPoints> WeakList = new List<StrongandWeakPoints>();

                        if (request.AppraisalWeakPoints.Count > 0)
                        {
                            var empRecords = await _dbContext.StrongandWeakPoints.Where(x => x.IsDeleted == false && x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId && x.Status == (int)AppriasalStorngWeakPointType.Weak).ToListAsync();
                            _dbContext.StrongandWeakPoints.RemoveRange(empRecords);
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
                                wp.EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId;
                                WeakList.Add(wp);
                            }
                            await _dbContext.StrongandWeakPoints.AddRangeAsync(WeakList);
                        }

                        List<EmployeeEvaluationTraining> empTraining = new List<EmployeeEvaluationTraining>();
                        if (request.AppraisalTraining.Count > 0)
                        {
                            var employeeEvaluationTraining = await _dbContext.EmployeeEvaluationTraining.Where(x => x.IsDeleted == false && x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId).ToListAsync();
                            _dbContext.EmployeeEvaluationTraining.RemoveRange(employeeEvaluationTraining);

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
                                    EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId,
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
                        throw new Exception("Appraisal already done");
                    }
                    success = true;
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