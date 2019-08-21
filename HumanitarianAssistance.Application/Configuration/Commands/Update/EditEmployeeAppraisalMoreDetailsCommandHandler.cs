using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditEmployeeAppraisalMoreDetailsCommandHandler : IRequestHandler<EditEmployeeAppraisalMoreDetailsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditEmployeeAppraisalMoreDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditEmployeeAppraisalMoreDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var recordList = await _dbContext.EmployeeEvaluation.FirstOrDefaultAsync(x => x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId);

                if (recordList != null)
                {
                    List<EmployeeEvaluationTraining> trainingList = new List<EmployeeEvaluationTraining>();
                    List<EmployeeAppraisalTeamMember> appraisalTeamMemberList = new List<EmployeeAppraisalTeamMember>();
                    List<StrongandWeakPoints> StrongList = new List<StrongandWeakPoints>();
                    List<StrongandWeakPoints> WeakList = new List<StrongandWeakPoints>();

                    var trainingData = await _dbContext.EmployeeEvaluationTraining.Where(x => x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId).ToListAsync();
                    _dbContext.RemoveRange(trainingData);


                    foreach (var item in request.EmployeeEvaluationModelList)
                    {
                        EmployeeEvaluationTraining obj = new EmployeeEvaluationTraining();
                        obj.TrainingProgram = item.TrainingProgram;
                        obj.Program = item.Program;
                        obj.Participated = item.Participated;
                        obj.CatchLevel = item.CatchLevel;
                        obj.RefresherTrm = item.RefresherTrm;
                        obj.OthRecommendation = item.OthRecommendation;

                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = request.CreatedDate;

                        obj.EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId;
                        trainingList.Add(obj);
                    }
                    await _dbContext.EmployeeEvaluationTraining.AddRangeAsync(trainingList);

                    //AppraisalTeamMemberList
                    var appraisalTeamMemberData = await _dbContext.EmployeeAppraisalTeamMember.Where(x => x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId).ToListAsync();
                    _dbContext.RemoveRange(appraisalTeamMemberData);

                    foreach (var item in request.EmployeeAppraisalTeamMemberList)
                    {
                        EmployeeAppraisalTeamMember obj = new EmployeeAppraisalTeamMember();
                        obj.EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId;
                        //obj.EmployeeAppraisalTeamMemberId = item;
                        obj.EmployeeId = item;

                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = request.CreatedDate;

                        appraisalTeamMemberList.Add(obj);
                    }
                    await _dbContext.EmployeeAppraisalTeamMember.AddRangeAsync(appraisalTeamMemberList);

                    recordList.FinalResultQues1 = request.FinalResultQues1;
                    recordList.FinalResultQues2 = request.FinalResultQues2;
                    recordList.FinalResultQues3 = request.FinalResultQues3;
                    recordList.FinalResultQues4 = request.FinalResultQues4;
                    recordList.FinalResultQues5 = request.FinalResultQues5;
                    recordList.DirectSupervisor = request.DirectSupervisor;
                    recordList.CommentsByEmployee = request.CommentsByEmployee;
                    recordList.CurrentAppraisalDate = request.CurrentAppraisalDate;
                    recordList.EmployeeId = request.EmployeeId;

                    await _dbContext.SaveChangesAsync();

                    var empRecords = await _dbContext.StrongandWeakPoints.Where(x => x.IsDeleted == false && x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId).ToListAsync();
                    _dbContext.StrongandWeakPoints.RemoveRange(empRecords);

                    foreach (var item in request.StrongPoints)
                    {
                        StrongandWeakPoints obj = new StrongandWeakPoints();
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = request.CreatedDate;
                        obj.CurrentAppraisalDate = DateTime.Now;
                        obj.EmployeeId = request.EmployeeId;
                        obj.Point = item;
                        obj.Status = 1;         // 1 for strong points
                        obj.EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId;
                        StrongList.Add(obj);
                    }
                    await _dbContext.StrongandWeakPoints.AddRangeAsync(StrongList);

                    foreach (var item in request.WeakPoints)
                    {
                        StrongandWeakPoints obj = new StrongandWeakPoints();
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = request.CreatedDate;
                        obj.CurrentAppraisalDate = DateTime.Now;
                        obj.EmployeeId = request.EmployeeId;
                        obj.Point = item;
                        obj.Status = 2;         // 2 for Weak points
                        obj.EmployeeAppraisalDetailsId = request.EmployeeAppraisalDetailsId;
                        WeakList.Add(obj);
                    }
                    await _dbContext.StrongandWeakPoints.AddRangeAsync(WeakList);
                }
                await _dbContext.SaveChangesAsync();

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
