using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditInterviewDetailsCommandHandler : IRequestHandler<EditInterviewDetailsCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public EditInterviewDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(EditInterviewDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    var record = await _dbContext.InterviewDetails.FirstOrDefaultAsync(x => x.InterviewDetailsId == request.InterviewDetailsId);
                    if (record != null)
                    {
                        record.JobId = request.JobId;
                        record.PassportNo = request.PassportNo;
                        record.University = request.University;
                        record.PlaceOfBirth = request.PlaceOfBirth;
                        record.TazkiraIssuePlace = request.TazkiraIssuePlace;
                        record.MaritalStatus = request.MaritalStatus;
                        record.Experience = request.Experience;
                        record.ProfessionalCriteriaMarks = request.ProfessionalCriteriaMarks;
                        record.MarksObtained = request.MarksObtained;
                        record.WrittenTestMarks = request.WrittenTestMarks;
                        record.Ques1 = request.Ques1;
                        record.Ques2 = request.Ques2;
                        record.Ques3 = request.Ques3;
                        record.PreferedLocation = request.PreferedLocation;
                        record.NoticePeriod = request.NoticePeriod;
                        record.JoiningDate = request.JoiningDate;

                        record.CurrentBase = request.CurrentBase;
                        record.CurrentTransportation = request.CurrentTransportation;
                        record.CurrentMeal = request.CurrentMeal;
                        record.CurrentOther = request.CurrentOther;

                        record.ExpectationBase = request.ExpectationBase;
                        record.ExpectationTransportation = request.ExpectationTransportation;
                        record.ExpectationMeal = request.ExpectationMeal;
                        record.ExpectationOther = request.ExpectationOther;

                        record.TotalMarksObtained = request.TotalMarksObtained;

                        record.Status = request.Status;
                        record.ModifiedDate = request.ModifiedDate;
                        record.ModifiedById = request.ModifiedById;
                        await _dbContext.SaveChangesAsync();
                    }

                    //Rating based Criteria
                    var criteriaRecord = await _dbContext.RatingBasedCriteria.Where(x => x.InterviewDetailsId == request.InterviewDetailsId).ToListAsync();
                    _dbContext.RatingBasedCriteria.RemoveRange(criteriaRecord);

                    foreach (var item in request.RatingBasedCriteriaList)
                    {
                        RatingBasedCriteria ratingObj = new RatingBasedCriteria();
                        ratingObj.InterviewDetailsId = request.InterviewDetailsId;
                        ratingObj.CriteriaQuestion = item.CriteriaQuestion;
                        ratingObj.Rating = item.Rating;
                        ratingObj.CreatedById = request.CreatedById;
                        ratingObj.CreatedDate = request.CreatedDate;
                        ratingObj.IsDeleted = false;
                        await _dbContext.RatingBasedCriteria.AddAsync(ratingObj);
                    }


                    var languageRecord = await _dbContext.InterviewLanguages.Where(x => x.InterviewDetailsId == request.InterviewDetailsId).ToListAsync();
                    _dbContext.InterviewLanguages.RemoveRange(languageRecord);

                    foreach (var item in request.InterviewLanguageModelList)
                    {
                        InterviewLanguages IL = new InterviewLanguages();
                        IL.InterviewDetailsId = request.InterviewDetailsId;
                        IL.LanguageName = item.LanguageName;
                        IL.LanguageId = item.LanguageId;
                        IL.Reading = item.Reading;
                        IL.Writing = item.Writing;
                        IL.Listening = item.Listening;
                        IL.Speaking = item.Speaking;
                        IL.CreatedById = request.CreatedById;
                        IL.CreatedDate = request.CreatedDate;
                        IL.IsDeleted = false;
                        await _dbContext.InterviewLanguages.AddAsync(IL);
                    }

                    var technicalRecord = await _dbContext.InterviewTechnicalQuestion.Where(x => x.InterviewDetailsId == request.InterviewDetailsId).ToListAsync();
                    _dbContext.InterviewTechnicalQuestion.RemoveRange(technicalRecord);

                    foreach (var item in request.InterviewTechQuesModelList)
                    {
                        InterviewTechnicalQuestion itq = new InterviewTechnicalQuestion();
                        itq.InterviewDetailsId = request.InterviewDetailsId;
                        itq.Question = item.Question;
                        itq.Answer = item.Answer;
                        itq.CreatedById = request.CreatedById;
                        itq.CreatedDate = request.CreatedDate;
                        itq.IsDeleted = false;
                        await _dbContext.InterviewTechnicalQuestion.AddAsync(itq);
                    }

                    var trainingRecords = await _dbContext.InterviewTrainings.Where(x => x.InterviewDetailsId == request.InterviewDetailsId).ToListAsync();
                    _dbContext.InterviewTrainings.RemoveRange(trainingRecords);

                    foreach (var item in request.InterviewTrainingModelList)
                    {
                        InterviewTrainings it = new InterviewTrainings();
                        it.InterviewDetailsId = request.InterviewDetailsId;
                        it.TraininigType = item.TraininigType;
                        it.TrainingName = item.TrainingName;
                        it.StudyingCountry = item.StudyingCountry;
                        it.StartDate = item.StartDate;
                        it.EndDate = item.EndDate;
                        it.CreatedById = request.CreatedById;
                        it.CreatedDate = request.CreatedDate;
                        it.IsDeleted = false;
                        await _dbContext.InterviewTrainings.AddAsync(it);
                    }

                    if (request.Interviewers.Any())
                    {
                        ICollection<HRJobInterviewers> hRJobInterviewersList = await _dbContext.HRJobInterviewers.Where(x => x.IsDeleted == false && x.InterviewDetailsId == request.InterviewDetailsId).ToListAsync();
                        _dbContext.HRJobInterviewers.RemoveRange(hRJobInterviewersList);

                        foreach (var item in request.Interviewers)
                        {
                            HRJobInterviewers hRJobInterviewers = new HRJobInterviewers();
                            hRJobInterviewers.CreatedDate = request.CreatedDate;
                            hRJobInterviewers.CreatedById = request.CreatedById;
                            hRJobInterviewers.EmployeeId = item.Interviewer;
                            hRJobInterviewers.InterviewDetailsId = request.InterviewDetailsId;
                            hRJobInterviewers.IsDeleted = false;
                            await _dbContext.HRJobInterviewers.AddAsync(hRJobInterviewers);
                        }
                    }

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
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
