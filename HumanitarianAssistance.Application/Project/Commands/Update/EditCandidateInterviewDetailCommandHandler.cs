using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.Project.Commands.Update {
    public class EditCandidateInterviewDetailCommandHandler : IRequestHandler<EditCandidateInterviewDetailCommand, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditCandidateInterviewDetailCommandHandler (HumanitarianAssistanceDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle (EditCandidateInterviewDetailCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction ()) {
                try {
                    var interviewDetails = await _dbContext.ProjectInterviewDetails.Where (x => x.InterviewId == request.InterviewId && x.IsDeleted == false).FirstOrDefaultAsync ();
                    interviewDetails.Description = request.Description;
                    interviewDetails.NoticePeriod = request.NoticePeriod;
                    interviewDetails.AvailableDate = request.AvailableDate;
                    interviewDetails.WrittenTestMarks = request.WrittenTestMarks;
                    interviewDetails.CurrentBase = request.CurrentBase;
                    interviewDetails.CurrentOther = request.CurrentOther;
                    interviewDetails.ExpectationBase = request.ExpectationBase;
                    interviewDetails.ExpectationOther = request.ExpectationOther;
                    interviewDetails.Status = request.Status;
                    interviewDetails.InterviewQuestionOne = request.InterviewQuestionOne;
                    interviewDetails.InterviewQuestionTwo = request.InterviewQuestionTwo;
                    interviewDetails.InterviewQuestionThree = request.InterviewQuestionThree;
                    interviewDetails.CurrentTransport = request.CurrentTransport;
                    interviewDetails.CurrentMeal = request.CurrentMeal;
                    interviewDetails.ExpectationTransport = request.ExpectationTransport;
                    interviewDetails.ExpectationMeal = request.ExpectationMeal;
                    interviewDetails.ProfessionalCriteriaMarks = request.ProfessionalCriteriaMark;
                    interviewDetails.MarksObtained = request.MarksObtain;
                    interviewDetails.TotalMarksObtain = request.TotalMarksObtain;
                    interviewDetails.ModifiedById = request.ModifiedById;
                    interviewDetails.ModifiedDate = request.ModifiedDate;
                    await _dbContext.SaveChangesAsync ();

                    var RatingData = _dbContext.RatingBasedCriteria.Where (x => x.InterviewId == request.InterviewId && x.IsDeleted == false).ToList ();
                    _dbContext.RatingBasedCriteria.RemoveRange (RatingData);
                    await _dbContext.SaveChangesAsync ();
                    List<RatingBasedCriteria> ratingObj = new List<RatingBasedCriteria> ();
                    foreach (var item in request.RatingBasedCriteriaList) {
                        RatingBasedCriteria question = new RatingBasedCriteria () {
                            InterviewId = interviewDetails.InterviewId,
                            QuestionId = item.QuestionId,
                            Score = item.Selected,
                            CreatedById = request.CreatedById,
                            CreatedDate = request.CreatedDate,
                            IsDeleted = false
                        };

                        ratingObj.Add (question);
                    }
                    await _dbContext.RatingBasedCriteria.AddRangeAsync (ratingObj);
                    await _dbContext.SaveChangesAsync ();

                    var InterviewTechnicalData = _dbContext.InterviewTechnicalQuestion.Where (x => x.InterviewId == request.InterviewId && x.IsDeleted == false).ToList ();
                    _dbContext.InterviewTechnicalQuestion.RemoveRange (InterviewTechnicalData);
                    await _dbContext.SaveChangesAsync ();
                    List<InterviewTechnicalQuestion> technicalObj = new List<InterviewTechnicalQuestion> ();
                    foreach (var item in request.TechnicalQuestionList) {
                        InterviewTechnicalQuestion question = new InterviewTechnicalQuestion () {
                            InterviewId = interviewDetails.InterviewId,
                            QuestionId = item.QuestionId,
                            Score = item.Selected,
                            CreatedById = request.CreatedById,
                            CreatedDate = request.CreatedDate,
                            IsDeleted = false
                        };
                        technicalObj.Add (question);
                    }
                    await _dbContext.InterviewTechnicalQuestion.AddRangeAsync (technicalObj);
                    await _dbContext.SaveChangesAsync ();


                    var LanguageData = _dbContext.InterviewLanguages.Where (x => x.InterviewId == request.InterviewId && x.IsDeleted == false).ToList ();
                    _dbContext.InterviewLanguages.RemoveRange (LanguageData);
                    await _dbContext.SaveChangesAsync ();
                    List<InterviewLanguages> languageObj = new List<InterviewLanguages> ();
                    foreach (var item in request.LanguageList) {
                        InterviewLanguages details = new InterviewLanguages () {
                            InterviewId = interviewDetails.InterviewId,
                            LanguageName = item.LanguageName,
                            Reading = item.LanguageReading == "Poor" ? 1 : item.LanguageReading == "Good" ? 2 : item.LanguageReading == "VeryGood" ? 3 : 4,
                            Writing = item.LanguageWriting == "Poor" ? 1 : item.LanguageWriting == "Good" ? 2 : item.LanguageWriting == "VeryGood" ? 3 : 4,
                            Listening = item.LanguageListining == "Poor" ? 1 : item.LanguageListining == "Good" ? 2 : item.LanguageListining == "VeryGood" ? 3 : 4,
                            Speaking = item.LanguageSpeaking == "Poor" ? 1 : item.LanguageSpeaking == "Good" ? 2 : item.LanguageSpeaking == "VeryGood" ? 3 : 4,
                            CreatedById = request.CreatedById,
                            CreatedDate = request.CreatedDate,
                            IsDeleted = false
                        };
                        languageObj.Add (details);
                    }
                    await _dbContext.InterviewLanguages.AddRangeAsync (languageObj);
                    await _dbContext.SaveChangesAsync ();

                    var TrainingData = _dbContext.InterviewTrainings.Where (x => x.InterviewId == request.InterviewId && x.IsDeleted == false).ToList ();
                    _dbContext.InterviewTrainings.RemoveRange (TrainingData);
                    await _dbContext.SaveChangesAsync ();
                    List<InterviewTrainings> traningObj = new List<InterviewTrainings> ();
                    foreach (var item in request.TraningList) {
                        InterviewTrainings details = new InterviewTrainings () {
                            InterviewId = interviewDetails.InterviewId,
                            NewTraininigType = item.TraningType,
                            TrainingName = item.TraningName,
                            StudyingCountry = item.TraningCountryAndCity,
                            StartDate = Convert.ToDateTime(item.TraningStartDate), 
                            EndDate = Convert.ToDateTime(item.TraningEndDate),
                            CreatedById = request.CreatedById,
                            CreatedDate = request.CreatedDate,
                            IsDeleted = false
                        };
                        traningObj.Add (details);
                    }
                    await _dbContext.InterviewTrainings.AddRangeAsync (traningObj);
                    await _dbContext.SaveChangesAsync ();

                    var InterviewergData = _dbContext.HRJobInterviewers.Where (x => x.InterviewId == request.InterviewId && x.IsDeleted == false).ToList ();
                    _dbContext.HRJobInterviewers.RemoveRange (InterviewergData);
                    await _dbContext.SaveChangesAsync ();
                    List<HRJobInterviewers> interviewerObj = new List<HRJobInterviewers> ();
                    foreach (var item in request.InterviewerList) {
                        HRJobInterviewers details = new HRJobInterviewers () {
                            InterviewId = interviewDetails.InterviewId,
                            EmployeeId = item.EmployeeId,
                            CreatedById = request.CreatedById,
                            CreatedDate = request.CreatedDate,
                            IsDeleted = false
                        };
                        interviewerObj.Add (details);
                    }
                    await _dbContext.HRJobInterviewers.AddRangeAsync (interviewerObj);
                    await _dbContext.SaveChangesAsync ();

                    tran.Commit ();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                } catch (Exception ex) {
                    tran.Rollback ();
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = ex.Message;
                }
            }
            return response;
        }
    }
}