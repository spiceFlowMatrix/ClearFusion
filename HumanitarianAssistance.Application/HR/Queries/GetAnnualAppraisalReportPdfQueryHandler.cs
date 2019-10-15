using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAnnualAppraisalReportPdfQueryHandler : IRequestHandler<GetAnnualAppraisalReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;

        public GetAnnualAppraisalReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
        }

        public async Task<byte[]> Handle(GetAnnualAppraisalReportPdfQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // model logic here
                // var emplst = await (from ee in _dbContext.EmployeeAppraisalDetails
                //                     join ed in _dbContext.EmployeeDetail
                //                     on ee.EmployeeId equals ed.EmployeeID
                //                     join epd in _dbContext.EmployeeProfessionalDetail
                //                     on ed.EmployeeID equals epd.EmployeeId
                //                     join dd in _dbContext.DesignationDetail
                //                     on epd.DesignationId equals dd.DesignationId
                //                     into x
                //                     from xd in x.DefaultIfEmpty()
                //                     join eev in _dbContext.EmployeeEvaluation
                //                     on ee.EmployeeAppraisalDetailsId equals eev.EmployeeAppraisalDetailsId
                //                     into y
                //                     from yd in y.DefaultIfEmpty()
                //                     where ee.OfficeId == request.OfficeId && ee.IsDeleted == false
                //                     select new AnnualAppraisalReportPdfModel
                //                     {
                //                         Name = ed.EmployeeName,
                //                         FatherName = ed.FatherName,
                //                         Designation = xd.Designation,
                //                         Department = ee.Department,
                //                         AppraisalStatus = yd.EvaluationStatus,
                //                         EmployeeComments = yd.CommentsByEmployee,
                //                         EmployeeAppraisalDetailId = ee.EmployeeAppraisalDetailsId
                //                     }).ToListAsync();                                                                                               
                List<AnnualAppraisalReportPdfModel> summary = new List<AnnualAppraisalReportPdfModel>();
                // int sNumber = 1;
                // foreach (var item in emplst)
                // {
                //     var TeamMemberName = await (from ed in _dbContext.EmployeeDetail
                //                                 join eat in _dbContext.EmployeeAppraisalTeamMember
                //                                 on ed.EmployeeID equals eat.EmployeeId
                //                                 where eat.EmployeeAppraisalDetailsId == item.EmployeeAppraisalDetailId
                //                                 select new { eat.EmployeeAppraisalDetailsId, ed.EmployeeName }).ToListAsync();
                //     var points = await (from x in _dbContext.StrongandWeakPoints
                //                         where x.EmployeeAppraisalDetailsId == item.EmployeeAppraisalDetailId
                //                         select new { x.Status,x.Point}).ToListAsync();

                //     summary.Add(new AnnualAppraisalReportPdfModel
                //     {
                //         SerialNumber = sNumber,
                //         Name = item.Name,
                //         FatherName = item.FatherName,
                //         Designation = item.Designation,
                //         Department = item.Department,
                //         AppraisalStatus = item.AppraisalStatus,
                //         CommitteeMemberOne = TeamMemberName.Count > 0 ? TeamMemberName[0].EmployeeName : null,
                //         CommitteeMemberTwo = TeamMemberName.Count > 1 ? TeamMemberName[1].EmployeeName : null,
                //         EmployeeComments = item.EmployeeComments,
                //         WeakPoint = points != null? points.Where(x=>x.Status==2).Select(x => x.Point).ToList(): null,
                //         StrongPoint= points != null ? points.Where(x => x.Status == 1).Select(x => x.Point).ToList():null,
                //     });
                //     sNumber = sNumber + 1;
                // }
                var EmployeeList = await _dbContext.LoadStoredProc("get_annual_appraisal_report")
                                     .WithSqlParam("office_id", request.OfficeId)
                                     .ExecuteStoredProc<spAnnualAppraisalReportPdfModel>();

                foreach (var item in EmployeeList)
                {
                    summary.Add(new AnnualAppraisalReportPdfModel
                    {
                        SerialNumber = item.SerialNumber,
                        Name = item.Name,
                        FatherName = item.FatherName,
                        Designation = item.Designation,
                        Department = item.Department,
                        AppraisalStatus = item.AppraisalStatus,
                        CommitteeMemberOne = item.CommitteeMemberOne,
                        CommitteeMemberTwo = item.CommitteeMemberTwo,
                        EmployeeComments = item.EmployeeComments,
                        WeakPoint = item.WeakPoint,
                        StrongPoint = item.StrongPoint
                    });
                }
                return await _pdfExportService.ExportToPdf(summary, "Pages/PdfTemplates/AnnualAppraisalReport.cshtml");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

