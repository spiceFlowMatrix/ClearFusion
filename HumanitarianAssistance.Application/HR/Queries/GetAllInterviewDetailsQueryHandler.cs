using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllInterviewDetailsQueryHandler : IRequestHandler<GetAllInterviewDetailsQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllInterviewDetailsQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(GetAllInterviewDetailsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<InterviewDetailModel> lst = new List<InterviewDetailModel>();
                var recordLst = await _dbContext.InterviewDetails
                                                         .Include(x => x.EmployeeDetail)
                                                         .Include(x => x.RatingBasedCriteriaList)
                                                         .Include(x => x.InterviewLanguagesList)
                                                         .Include(x => x.InterviewTrainingsList)
                                                         .Include(x => x.HRJobInterviewersList)
                                                         .Include(x => x.InterviewTechnicalQuestionList)
                                                         .Where(x => x.IsDeleted == false)
                                                         .ToListAsync();
                foreach (var model in recordLst)
                {

                    InterviewDetailModel obj = new InterviewDetailModel();

                    //rating based criteria

                    if (model.RatingBasedCriteriaList != null && model.RatingBasedCriteriaList.Any())
                    {
                        obj.RatingBasedCriteriaList = new List<RatingBasedCriteriaModel>();

                        foreach (var item in model.RatingBasedCriteriaList)
                        {
                            RatingBasedCriteriaModel criteriaModel = new RatingBasedCriteriaModel()
                            {
                                CriteriaQuestion = item.CriteriaQuestion,
                                Rating = item.Rating
                            };

                            obj.RatingBasedCriteriaList.Add(criteriaModel);
                        }
                    }

                    if (model.InterviewTechnicalQuestionList != null && model.InterviewTechnicalQuestionList.Any())
                    {
                        obj.InterviewTechQuesModelList = new List<InterviewTechQuesModel>();

                        foreach (var item in model.InterviewTechnicalQuestionList)
                        {
                            InterviewTechQuesModel technicalModel = new InterviewTechQuesModel()
                            {
                                Question = item.Question,
                                Answer = item.Answer
                            };

                            obj.InterviewTechQuesModelList.Add(technicalModel);
                        }
                    }

                    if (model.InterviewLanguagesList != null && model.InterviewLanguagesList.Any())
                    {
                        obj.InterviewLanguageModelList = new List<InterviewLanguageModel>();

                        foreach (var item in model.InterviewLanguagesList)
                        {
                            InterviewLanguageModel languageModel = new InterviewLanguageModel()
                            {
                                LanguageName = item.LanguageName,
                                LanguageId = item.LanguageId,
                                Reading = item.Reading,
                                Writing = item.Writing,
                                Listening = item.Listening,
                                Speaking = item.Speaking,
                            };

                            obj.InterviewLanguageModelList.Add(languageModel);
                        }
                    }

                    if (model.InterviewTrainingsList != null && model.InterviewTrainingsList.Any())
                    {

                        obj.InterviewTrainingModelList = new List<InterviewTrainingModel>();

                        foreach (var item in model.InterviewTrainingsList)
                        {
                            InterviewTrainingModel trainingModel = new InterviewTrainingModel()
                            {
                                TraininigType = item.TraininigType,
                                TrainingName = item.TrainingName,
                                StudyingCountry = item.StudyingCountry,
                                StartDate = item.StartDate,
                                EndDate = item.EndDate
                            };

                            obj.InterviewTrainingModelList.Add(trainingModel);
                        }
                    }

                    if (model.HRJobInterviewersList != null && model.HRJobInterviewersList.Any())
                    {
                        obj.Interviewers = new List<Interviewers>();

                        foreach (var item in model.HRJobInterviewersList)
                        {
                            Interviewers xInterviewer = new Interviewers()
                            {
                                Interviewer = item.EmployeeId
                            };

                            obj.Interviewers.Add(xInterviewer);
                        }
                    }

                    var empDetail = await _dbContext.EmployeeDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.EmployeeID == model.EmployeeID);
                    var jobDetail = await _dbContext.JobHiringDetails.Include(x => x.OfficeDetails).FirstOrDefaultAsync(x => x.IsDeleted == false && x.JobId == model.JobId);

                    obj.EmployeeID = model.EmployeeID;
                    obj.CandidateName = empDetail.EmployeeName;

                    obj.JobId = model.JobId;
                    obj.DutyStation = jobDetail.OfficeDetails?.OfficeName;

                    obj.InterviewDetailsId = model.InterviewDetailsId;
                    obj.PassportNo = model.PassportNo;
                    obj.University = model.University;
                    obj.PlaceOfBirth = model.PlaceOfBirth;
                    obj.TazkiraIssuePlace = model.TazkiraIssuePlace;
                    obj.MaritalStatus = model.MaritalStatus;
                    obj.Experience = model.Experience;
                    obj.ProfessionalCriteriaMarks = model.ProfessionalCriteriaMarks;
                    obj.MarksObtained = model.MarksObtained;
                    obj.WrittenTestMarks = model.WrittenTestMarks;
                    obj.Ques1 = model.Ques1;
                    obj.Ques2 = model.Ques2;
                    obj.Ques3 = model.Ques3;
                    obj.PreferedLocation = model.PreferedLocation;
                    obj.NoticePeriod = model.NoticePeriod;
                    obj.JoiningDate = model.JoiningDate;

                    obj.CurrentBase = model.CurrentBase;
                    obj.CurrentTransportation = model.CurrentTransportation;
                    obj.CurrentMeal = model.CurrentMeal;
                    obj.CurrentOther = model.CurrentOther;
                    obj.ExpectationBase = model.ExpectationBase;
                    obj.ExpectationTransportation = model.ExpectationTransportation;
                    obj.ExpectationMeal = model.ExpectationMeal;
                    obj.ExpectationOther = model.ExpectationOther;
                    obj.TotalMarksObtained = model.TotalMarksObtained;
                    obj.Status = model.Status;
                    obj.InterviewStatus = model.InterviewStatus;

                    lst.Add(obj);
                }
                response.data.InterviewDetailList = lst;
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
