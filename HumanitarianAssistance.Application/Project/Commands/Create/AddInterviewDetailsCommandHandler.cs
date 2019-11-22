using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Create {

    public class AddInterviewDetailsCommandHandler : IRequestHandler<AddInterviewDetailsCommand, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddInterviewDetailsCommandHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle (AddInterviewDetailsCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                ProjectInterviewDetails interviewDetails = new ProjectInterviewDetails () {
                    Description = request.Description,
                    NoticePeriod = request.NoticePeriod,
                    AvailableDate = request.AvailableDate,
                    WrittenTestMarks = request.WrittenTestMarks,
                    CurrentBase = request.CurrentBase,
                    CurrentOther = request.CurrentOther,
                    ExpectationBase = request.ExpectationBase,
                    ExpectationOther = request.ExpectationOther,
                    Status = request.Status,
                    InterviewQuestionOne = request.InterviewQuestionOne,
                    InterviewQuestionTwo = request.InterviewQuestionTwo,
                    InterviewQuestionThree = request.InterviewQuestionThree,
                    CurrentTransport = request.CurrentTransport,
                    CurrentMeal = request.CurrentMeal,
                    ExpectationTransport = request.ExpectationTransport,
                    ExpectationMeal = request.ExpectationMeal,
                    ProfessionalCriteriaMarks = request.ProfessionalCriteriaMark,
                    MarksObtained = request.MarksObtain,
                    // TotalMarksObtained =request.TotalMarksObtain,
                    CreatedById = request.CreatedById,
                    CreatedDate = request.CreatedDate,
                    IsDeleted = false
                };
                await _dbContext.ProjectInterviewDetails.AddAsync (interviewDetails);
                await _dbContext.SaveChangesAsync ();
                List<RatingBasedCriteria> ratingObj = new List<RatingBasedCriteria> ();
                foreach (var item in request.RatingBasedCriteriaList) {
                    RatingBasedCriteria question = new RatingBasedCriteria () {
                        QuestionId = item.QuestionId,
                        Score = item.Score,
                        CreatedById = request.CreatedById,
                        CreatedDate = request.CreatedDate,
                        IsDeleted = false
                    };

                    ratingObj.Add (question);
                }
                await _dbContext.RatingBasedCriteria.AddRangeAsync (ratingObj);
                await _dbContext.SaveChangesAsync ();

                List<InterviewTechnicalQuestion> technicalObj = new List<InterviewTechnicalQuestion> ();
                foreach (var item in request.TechnicalQuestionList) {
                    InterviewTechnicalQuestion question = new InterviewTechnicalQuestion () {
                        QuestionId = item.QuestionId,
                        Score = item.Score,
                        CreatedById = request.CreatedById,
                        CreatedDate = request.CreatedDate,
                        IsDeleted = false
                    };

                    technicalObj.Add (question);
                }
                await _dbContext.InterviewTechnicalQuestion.AddRangeAsync (technicalObj);
                await _dbContext.SaveChangesAsync ();

                List<InterviewLanguages> languageObj = new List<InterviewLanguages> ();
                foreach (var item in request.LanguageList) {
                    InterviewLanguages details = new InterviewLanguages () {
                        LanguageName = item.LanguageName,
                        Reading = 1,
                        Writing = 1,
                        Listening = 1,
                        Speaking = 1,
                        CreatedById = request.CreatedById,
                        CreatedDate = request.CreatedDate,
                        IsDeleted = false
                    };

                    languageObj.Add (details);
                }
                await _dbContext.InterviewLanguages.AddRangeAsync (languageObj);
                await _dbContext.SaveChangesAsync ();

                List<InterviewTrainings> traningObj = new List<InterviewTrainings> ();
                foreach (var item in request.TraningList) {
                    InterviewTrainings details = new InterviewTrainings () {
                        NewTraininigType = item.TraningType,
                        TrainingName = item.TraningName,
                        StudyingCountry = item.TraningCountryAndCity,
                        StartDate = item.TraningStartDate,
                        EndDate = item.TraningEndDate,
                        CreatedById = request.CreatedById,
                        CreatedDate = request.CreatedDate,
                        IsDeleted = false
                    };
                    traningObj.Add (details);
                }
                await _dbContext.InterviewTrainings.AddRangeAsync (traningObj);
                await _dbContext.SaveChangesAsync ();

                List<HRJobInterviewers> interviewerObj = new List<HRJobInterviewers> ();
                foreach (var item in request.InterviewerList) {
                    HRJobInterviewers details = new HRJobInterviewers () {
                        EmployeeId = item.EmployeeId,
                        CreatedById = request.CreatedById,
                        CreatedDate = request.CreatedDate,
                        IsDeleted = false
                    };
                    interviewerObj.Add (details);
                }
                await _dbContext.HRJobInterviewers.AddRangeAsync (interviewerObj);
                await _dbContext.SaveChangesAsync ();

                var statusDetails = _dbContext.HiringRequestCandidateStatus.Where (x => x.HiringRequestId == request.HiringRequestId && x.CandidateId == request.CandidateId && x.IsDeleted == false).FirstOrDefault ();
                statusDetails.ModifiedById = request.CreatedById;
                statusDetails.ModifiedDate = request.CreatedDate;
                statusDetails.InterviewId = interviewDetails.InterviewId;
                await _dbContext.SaveChangesAsync ();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}