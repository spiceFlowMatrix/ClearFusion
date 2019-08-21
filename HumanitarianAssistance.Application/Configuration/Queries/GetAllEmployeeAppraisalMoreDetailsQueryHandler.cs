using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllEmployeeAppraisalMoreDetailsQueryHandler : IRequestHandler<GetAllEmployeeAppraisalMoreDetailsQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmployeeAppraisalMoreDetailsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllEmployeeAppraisalMoreDetailsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<EmployeeAppraisalDetailsModel> lst = new List<EmployeeAppraisalDetailsModel>();

                var emplst = await _dbContext.EmployeeAppraisalDetails.Where(x => x.OfficeId == request.OfficeId && x.AppraisalStatus == true && x.IsDeleted == false).ToListAsync();
                foreach (var item in emplst)
                {
                    EmployeeAppraisalDetailsModel objAppraisal = new EmployeeAppraisalDetailsModel();

                    var empDetails = await _dbContext.EmployeeEvaluation.Where(x => x.EmployeeAppraisalDetailsId == item.EmployeeAppraisalDetailsId && (x.EvaluationStatus == null || x.EvaluationStatus == "approved")).ToListAsync();

                    List<EmployeeEvaluationTrainingModel> trainingList = new List<EmployeeEvaluationTrainingModel>();
                    List<int> appraisalTeamMemberList = new List<int>();

                    List<string> strong = new List<string>();
                    List<string> weak = new List<string>();

                    foreach (var elements in empDetails)
                    {
                        //Training
                        var trainingData = await _dbContext.EmployeeEvaluationTraining.Where(x => x.EmployeeAppraisalDetailsId == item.EmployeeAppraisalDetailsId && x.IsDeleted == false).ToListAsync();

                        foreach (var ele in trainingData)
                        {
                            EmployeeEvaluationTrainingModel objTraining = new EmployeeEvaluationTrainingModel();

                            objTraining.TrainingProgram = ele.TrainingProgram;
                            objTraining.RefresherTrm = ele.RefresherTrm;
                            objTraining.Program = ele.Program;
                            objTraining.Participated = ele.Participated;
                            objTraining.OthRecommendation = ele.OthRecommendation;
                            objTraining.EmployeeEvaluationTrainingId = ele.EmployeeEvaluationTrainingId;
                            objTraining.EmployeeAppraisalDetailsId = ele.EmployeeAppraisalDetailsId;
                            objTraining.CatchLevel = ele.CatchLevel;

                            trainingList.Add(objTraining);
                        }

                        //AppraisalTeamMemberList
                        var appraisalTeamMemberData = await _dbContext.EmployeeAppraisalTeamMember.Where(x => x.EmployeeAppraisalDetailsId == item.EmployeeAppraisalDetailsId && x.IsDeleted == false).ToListAsync();

                        foreach (var teamElement in appraisalTeamMemberData)
                        {      
                            appraisalTeamMemberList.Add(teamElement.EmployeeId);
                        }

                        //Strong n Weak
                        var strongAndWeakPointList = await _dbContext.StrongandWeakPoints.Where(x => x.EmployeeAppraisalDetailsId == item.EmployeeAppraisalDetailsId && x.IsDeleted == false).ToListAsync();

                        foreach (var count in strongAndWeakPointList)
                        {
                            if (count.Status == 1)
                            {
                                strong.Add(count.Point);
                            }
                            if (count.Status == 2)
                            {
                                weak.Add(count.Point);
                            }
                        }

                        objAppraisal.EmployeeEvaluationId = elements.EmployeeEvaluationId;
                        objAppraisal.EmployeeAppraisalDetailsId = elements.EmployeeAppraisalDetailsId;
                        objAppraisal.EmployeeId = elements.EmployeeId;
                        objAppraisal.FinalResultQues1 = elements.FinalResultQues1;
                        objAppraisal.FinalResultQues2 = elements.FinalResultQues2;
                        objAppraisal.FinalResultQues3 = elements.FinalResultQues3;
                        objAppraisal.FinalResultQues4 = elements.FinalResultQues4;
                        objAppraisal.FinalResultQues5 = elements.FinalResultQues5;
                        objAppraisal.DirectSupervisor = elements.DirectSupervisor;
                        objAppraisal.CommentsByEmployee = elements.CommentsByEmployee;
                        objAppraisal.CurrentAppraisalDate = elements.CurrentAppraisalDate;
                        objAppraisal.EvaluationStatus = elements.EvaluationStatus;
                        objAppraisal.EmployeeEvaluationModelList = trainingList;
                        objAppraisal.StrongPoints = strong;
                        objAppraisal.WeakPoints = weak;
                        objAppraisal.EmployeeAppraisalTeamMemberList = appraisalTeamMemberList;
                        lst.Add(objAppraisal);

                    }
                }

                response.data.EmployeeAppraisalDetailsModelLst = lst;
                //var finalLst = lst.GroupBy(x => x.EmployeeId).ToList();
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
