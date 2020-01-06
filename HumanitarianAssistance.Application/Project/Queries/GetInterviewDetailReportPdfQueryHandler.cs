using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries {

    public class GetInterviewDetailReportPdfQueryHandler : IRequestHandler<GetInterviewDetailReportPdfQuery, byte[]> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;

        public GetInterviewDetailReportPdfQueryHandler (HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService, IHostingEnvironment env) {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
        }

        public async Task<byte[]> Handle (GetInterviewDetailReportPdfQuery request, CancellationToken cancellationToken) {
            try {
                // model logic here 

                var answerlist = await (from hcs in _dbContext.HiringRequestCandidateStatus
                .Where(x=>x.HiringRequestId==request.HiringRequestId && x.CandidateId==request.CandidateId && x.IsDeleted==false)
                join i in _dbContext.ProjectInterviewDetails
                on hcs.InterviewId equals i.InterviewId into id from i in id.DefaultIfEmpty () 
                join cd in _dbContext.CandidateDetails
                on hcs.CandidateId equals cd.CandidateId into cdl from cd in cdl.DefaultIfEmpty ()
                 join p in _dbContext.ProvinceDetails
                on cd.ProvinceId equals p.ProvinceId into pd from p in pd.DefaultIfEmpty ()
                select new InterviewDetailsPdfModel {
                    TechnicalAnswerList = (from it in _dbContext.InterviewTechnicalQuestion.Where(x=>x.InterviewId == i.InterviewId)
                                           join tq in _dbContext.TechnicalQuestion
                                           on it.QuestionId equals tq.TechnicalQuestionId into tqd from tq in tqd.DefaultIfEmpty ()
                                           select new AnswerList {
                                               Question = tq.Question,
                                               Score = it.Score
                                           } ).ToList(),
                    RatingBasedCriteriaAnswerList = (from rb in _dbContext.RatingBasedCriteria.Where(x=>x.InterviewId == i.InterviewId)
                       join rbq in _dbContext.RatingBasedCriteriaQuestions
                       on rb.QuestionId equals rbq.QuestionsId into rbqd from rbq in rbqd.DefaultIfEmpty ()
                       select new AnswerList {
                           Question = rbq.Question,
                           Score = rb.Score
                       } ).ToList(),      
                    InterviewerList = (from ji in _dbContext.HRJobInterviewers.Where(x=>x.InterviewId == i.InterviewId)
                       join ed in _dbContext.EmployeeDetail
                       on ji.EmployeeId equals ed.EmployeeID into edl from ed in edl.DefaultIfEmpty ()
                       select new InterviewerList {
                          Name = ed.EmployeeName,
                          Position = "-",
                          Signature = "-",
                          Date = DateTime.Now
                       } ).ToList(),        
                       EducationList = (from edl in _dbContext.EducationDegreeMaster.Where(x=>x.IsDeleted == false)
                       select new EducationList {
                           EducationName = edl.Name
                       } ).ToList(),                               
                     Province = p.ProvinceName,
                }
                ).FirstOrDefaultAsync();

                InterviewDetailsPdfModel summary = new InterviewDetailsPdfModel () {
                    CheckRadioPath = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/radio-checked.png")?.PhysicalPath,
                    UncheckRadioPath = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/radio-unchecked.png")?.PhysicalPath,
                    LogoPath = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/logo.jpg")?.PhysicalPath,
                    CheckedIconPath = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/check-box.png")?.PhysicalPath,
                    UnCheckedIconPath = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/uncheck-blank.png")?.PhysicalPath,
                    PersianChaName = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/PersianText.png")?.PhysicalPath,
                    RatingBasedCriteriaAnswerList = answerlist.RatingBasedCriteriaAnswerList,
                    TechnicalAnswerList = answerlist.TechnicalAnswerList,
                    LanguageList = request.LanguageList,
                    TraningList = request.TraningList,
                    InterviewerList = answerlist.InterviewerList,
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
                    ProfessionalCriteriaMark = request.ProfessionalCriteriaMark,
                    MarksObtain = request.MarksObtain,
                    TotalMarksObtain = request.TotalMarksObtain,
                    CandidateName = request.CandidateName,
                    Qualification = request.Qualification,
                    Position = request.Position,
                    DutyStation = request.DutyStation,
                    MaritalStatus = request.MaritalStatus,
                    PassportNumber = request.PassportNumber,
                    NameOfInstitute = request.NameOfInstitute,
                    DateOfBirth = request.DateOfBirth,
                    Province = answerlist.Province,
                    EducationList = answerlist.EducationList
                };
                return await _pdfExportService.ExportToPdf (summary, "Pages/PdfTemplates/InterviewDetailReportPdf.cshtml");
            } catch (Exception ex) {
                throw new Exception (ex.Message);
            }
        }
    }
}