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
                    EmployeeAppraisalDetails ifExistEmpRecord = await _dbContext.EmployeeAppraisalDetails.Where(x => x.EmployeeId == request.EmployeeId && x.IsDeleted == false && x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId).FirstOrDefaultAsync();
                    if (ifExistEmpRecord != null)
                    {

                        ifExistEmpRecord.EmployeeId = request.EmployeeId;
                        ifExistEmpRecord.AppraisalPeriod = request.AppraisalPeriod;
                        ifExistEmpRecord.CurrentAppraisalDate = request.CurrentAppraisalDate;
                        ifExistEmpRecord.AppraisalStatus = null;
                        ifExistEmpRecord.ModifiedById = request.ModifiedById;
                        ifExistEmpRecord.ModifiedDate = request.ModifiedDate;
                        _dbContext.EmployeeAppraisalDetails.Update(ifExistEmpRecord);
                        await _dbContext.SaveChangesAsync();

                        List<EmployeeAppraisalQuestions> quesList = new List<EmployeeAppraisalQuestions>();
                        if (request.GeneralProfessionalIndicatorQuestion.Count > 0)
                        {
                            var questionList = await _dbContext.EmployeeAppraisalQuestions.Where(x => x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId && x.IsDeleted == false).ToListAsync();
                            var result = questionList.Select(x => x.EmployeeAppraisalQuestionsId).Except(request.GeneralProfessionalIndicatorQuestion.Select(x => x.EmployeeAppraisalQuestionsId).ToList());


                            var removedList = questionList.Where(x => result.Contains(x.EmployeeAppraisalQuestionsId)).ToList();
                            if (removedList.Count > 0)
                            {
                                removedList.ForEach(x => x.IsDeleted = true);
                                _dbContext.EmployeeAppraisalQuestions.UpdateRange(removedList);
                                await _dbContext.SaveChangesAsync();
                            }


                            foreach (var item in request.GeneralProfessionalIndicatorQuestion)
                            {

                                var questionExist = questionList.FirstOrDefault(x => x.EmployeeAppraisalQuestionsId == item.EmployeeAppraisalQuestionsId);
                                if (questionExist != null)
                                {
                                    questionExist.Score = item.Score;
                                    questionExist.Remarks = item.Remarks;
                                    questionExist.IsDeleted = false;
                                    questionExist.EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId;
                                    questionExist.AppraisalGeneralQuestionsId = item.AppraisalGeneralQuestionsId;
                                    questionExist.EmployeeId = request.EmployeeId;
                                    questionExist.ModifiedById = request.CreatedById;
                                    questionExist.ModifiedDate = request.CreatedDate;
                                    _dbContext.EmployeeAppraisalQuestions.Update(questionExist);
                                    await _dbContext.SaveChangesAsync();
                                }
                                else
                                {
                                    // add new 
                                    EmployeeAppraisalQuestions question = new EmployeeAppraisalQuestions();
                                    question.Score = item.Score;
                                    question.Remarks = item.Remarks;
                                    question.IsDeleted = false;
                                    question.EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId;
                                    question.AppraisalGeneralQuestionsId = item.AppraisalGeneralQuestionsId;
                                    question.EmployeeId = request.EmployeeId;
                                    question.CreatedById = request.CreatedById;
                                    question.CreatedDate = request.CreatedDate;
                                    // quesList.Add(question);

                                    await _dbContext.EmployeeAppraisalQuestions.AddAsync(question);
                                    await _dbContext.SaveChangesAsync();
                                }
                            }


                        }


                        List<EmployeeAppraisalTeamMember> teamMembers = new List<EmployeeAppraisalTeamMember>();

                        if (request.AppraisalMembers.Count > 0)
                        {

                            var employeeAppraisalTeamMember = await _dbContext.EmployeeAppraisalTeamMember.Where(x => x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId && x.IsDeleted == false).ToListAsync();
                            // _dbContext.RemoveRange(employeeAppraisalTeamMember);
                            var result = employeeAppraisalTeamMember.Select(x => x.EmployeeAppraisalTeamMemberId).Except(request.AppraisalMembers.Select(x => x.EmployeeAppraisalTeamMemberId).ToList());
                            var removedMemberList = employeeAppraisalTeamMember.Where(x => result.Contains(x.EmployeeAppraisalTeamMemberId)).ToList();
                            if (removedMemberList.Count > 0)
                            {
                                removedMemberList.ForEach(x => x.IsDeleted = true);
                                _dbContext.EmployeeAppraisalTeamMember.UpdateRange(removedMemberList);
                                await _dbContext.SaveChangesAsync();
                            }
                            foreach (var i in request.AppraisalMembers)
                            {
                                var memberExist = employeeAppraisalTeamMember.FirstOrDefault(x => x.EmployeeAppraisalTeamMemberId == i.EmployeeAppraisalTeamMemberId);
                                if (memberExist != null)
                                {
                                    memberExist.EmployeeId = i.EmployeeId;
                                    memberExist.EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId;
                                    memberExist.ModifiedById = request.ModifiedById;
                                    memberExist.ModifiedDate = request.ModifiedDate;
                                    memberExist.IsDeleted = false;
                                    _dbContext.EmployeeAppraisalTeamMember.Update(memberExist);
                                    await _dbContext.SaveChangesAsync();
                                }
                                else
                                {
                                    EmployeeAppraisalTeamMember eam = new EmployeeAppraisalTeamMember();
                                    eam.EmployeeId = i.EmployeeId;
                                    eam.EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId;
                                    eam.CreatedById = request.CreatedById;
                                    eam.CreatedDate = request.CreatedDate;
                                    eam.IsDeleted = false;
                                    //teamMembers.Add(eam);
                                    await _dbContext.EmployeeAppraisalTeamMember.AddAsync(eam);
                                    await _dbContext.SaveChangesAsync();
                                }

                            }

                        }

                        var eployeeEvaluation = await _dbContext.EmployeeEvaluation.FirstOrDefaultAsync(x => x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId && x.EmployeeEvaluationId == request.EmployeeEvaluationId &&
                                                                                                             x.IsDeleted == false);
                        // _dbContext.RemoveRange(eployeeEvaluation);
                        if (eployeeEvaluation != null)
                        {
                            eployeeEvaluation.FinalResultQues1 = request.FinalResultQues1;
                            eployeeEvaluation.FinalResultQues2 = request.FinalResultQues2;
                            eployeeEvaluation.FinalResultQues3 = request.FinalResultQues3;
                            eployeeEvaluation.FinalResultQues4 = request.FinalResultQues4;
                            eployeeEvaluation.FinalResultQues5 = request.FinalResultQues5;
                            eployeeEvaluation.CommentsByEmployee = request.CommentsByEmployee;
                            eployeeEvaluation.ModifiedById = request.ModifiedById;
                            eployeeEvaluation.ModifiedDate = request.ModifiedDate;
                            eployeeEvaluation.CurrentAppraisalDate = request.CurrentAppraisalDate;
                            eployeeEvaluation.EmployeeId = request.EmployeeId;
                            eployeeEvaluation.EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId;
                            _dbContext.EmployeeEvaluation.Update(eployeeEvaluation);
                            await _dbContext.SaveChangesAsync();
                        }


                        List<StrongandWeakPoints> StrongList = new List<StrongandWeakPoints>();

                        if (request.AppraisalStrongPoints.Count > 0)
                        {
                            var strongPointRecords = await _dbContext.StrongandWeakPoints.Where(x => x.IsDeleted == false && x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId && x.Status == (int)AppriasalStorngWeakPointType.Strong).ToListAsync();
                            // _dbContext.StrongandWeakPoints.RemoveRange(empRecords);
                            var filterdRecord = strongPointRecords.Select(x => x.StrongPointsId).Except(request.AppraisalStrongPoints.Select(x => x.AppraisalStrongPointsId).ToList());

                            var recordToDelete = strongPointRecords.Where(x => filterdRecord.Contains(x.StrongPointsId)).ToList();
                            if (recordToDelete.Count > 0)
                            {
                                recordToDelete.ForEach(x => x.IsDeleted = true);
                                _dbContext.StrongandWeakPoints.UpdateRange(recordToDelete);
                                await _dbContext.SaveChangesAsync();
                            }
                            foreach (var item in request.AppraisalStrongPoints)
                            {
                                var existingRecord = strongPointRecords.FirstOrDefault(x => x.StrongPointsId == item.AppraisalStrongPointsId);
                                if (existingRecord != null)
                                {
                                    existingRecord.ModifiedById = request.ModifiedById;
                                    existingRecord.ModifiedDate = request.ModifiedDate;
                                    // existingRecord.CurrentAppraisalDate = DateTime.Now;
                                    existingRecord.EmployeeId = request.EmployeeId;
                                    existingRecord.Point = item.StrongPoints;
                                    _dbContext.StrongandWeakPoints.Update(existingRecord);
                                    await _dbContext.SaveChangesAsync();

                                }
                                else
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
                                    // StrongList.Add(sp);
                                    await _dbContext.StrongandWeakPoints.AddAsync(sp);
                                    await _dbContext.SaveChangesAsync();

                                }
                            }
                        }
                        List<StrongandWeakPoints> WeakList = new List<StrongandWeakPoints>();

                        if (request.AppraisalWeakPoints.Count > 0)
                        {
                            var weakPointsRecords = await _dbContext.StrongandWeakPoints.Where(x => x.IsDeleted == false && x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId && x.Status == (int)AppriasalStorngWeakPointType.Weak).ToListAsync();
                            // _dbContext.StrongandWeakPoints.RemoveRange(empRecords);
                            var filteredRecords = weakPointsRecords.Select(x => x.StrongPointsId).Except(request.AppraisalWeakPoints.Select(x => x.AppraisalWeakPointsId).ToList());
                            var pointToDelete = weakPointsRecords.Where(x => filteredRecords.Contains(x.StrongPointsId)).ToList();
                            if (pointToDelete.Count > 0)
                            {
                                pointToDelete.ForEach(x => x.IsDeleted = true);
                                _dbContext.StrongandWeakPoints.UpdateRange(pointToDelete);
                                await _dbContext.SaveChangesAsync();
                            }

                            foreach (var item in request.AppraisalWeakPoints)
                            {
                                var ifExistWeakPointList = weakPointsRecords.FirstOrDefault(x => x.StrongPointsId == item.AppraisalWeakPointsId);
                                if (ifExistWeakPointList != null)
                                {
                                    ifExistWeakPointList.CurrentAppraisalDate = DateTime.Now;
                                    ifExistWeakPointList.EmployeeId = request.EmployeeId;
                                    ifExistWeakPointList.Point = item.WeakPoints;
                                    ifExistWeakPointList.IsDeleted = false;
                                    _dbContext.StrongandWeakPoints.Update(ifExistWeakPointList);
                                    await _dbContext.SaveChangesAsync();
                                }
                                else
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
                                    // WeakList.Add(wp);
                                    await _dbContext.StrongandWeakPoints.AddAsync(wp);
                                    await _dbContext.SaveChangesAsync();

                                }
                            }
                        }

                        List<EmployeeEvaluationTraining> empTraining = new List<EmployeeEvaluationTraining>();
                        if (request.AppraisalTraining.Count > 0)
                        {
                            var employeeEvaluationTraining = await _dbContext.EmployeeEvaluationTraining.Where(x => x.IsDeleted == false && x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId).ToListAsync();
                            // _dbContext.EmployeeEvaluationTraining.RemoveRange(employeeEvaluationTraining);
                            var filteredEvaluationRecord = employeeEvaluationTraining.Select(x => x.EmployeeEvaluationTrainingId).Except(request.AppraisalTraining.Select(x => x.EmployeeEvaluationTrainingId).ToList());
                            var recordToDel = employeeEvaluationTraining.Where(x => filteredEvaluationRecord.Contains(x.EmployeeEvaluationTrainingId)).ToList();
                            if (recordToDel.Count > 0)
                            {
                                recordToDel.ForEach(x => x.IsDeleted = true);
                                _dbContext.EmployeeEvaluationTraining.UpdateRange(recordToDel);
                                await _dbContext.SaveChangesAsync();
                            }
                            foreach (var tr in request.AppraisalTraining)
                            {
                                var ifExistTraingRecord = employeeEvaluationTraining.FirstOrDefault(x => x.EmployeeEvaluationTrainingId == tr.EmployeeEvaluationTrainingId);
                                if (ifExistTraingRecord != null)
                                {
                                    ifExistTraingRecord.TrainingProgram = tr.TrainingProgramBasedOn;
                                    ifExistTraingRecord.Program = tr.Program;
                                    ifExistTraingRecord.Participated = tr.Participated;
                                    ifExistTraingRecord.CatchLevel = tr.CatchLevel;
                                    ifExistTraingRecord.RefresherTrm = tr.RefresherTrm;
                                    ifExistTraingRecord.OthRecommendation = tr.OtherRecommemenedTraining;
                                    // ifExistTraingRecord.EmployeeEvaluationTrainingId = tr.EmployeeEvaluationTrainingId;
                                    ifExistTraingRecord.EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId;
                                    ifExistTraingRecord.ModifiedDate = request.ModifiedDate;
                                    ifExistTraingRecord.ModifiedById = request.ModifiedById;
                                    ifExistTraingRecord.IsDeleted = false;
                                    _dbContext.EmployeeEvaluationTraining.Update(ifExistTraingRecord);
                                    await _dbContext.SaveChangesAsync();
                                }
                                else
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
                                    // empTraining.Add(empt);
                                    await _dbContext.EmployeeEvaluationTraining.AddAsync(empt);
                                    await _dbContext.SaveChangesAsync();

                                }
                            }
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