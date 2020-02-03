using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using HumanitarianAssistance.Common.Helpers;
using Microsoft.AspNetCore.Hosting;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class ExportEmployeeLeavePdfQueryHandler: IRequestHandler<ExportEmployeeLeavePdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;

        public ExportEmployeeLeavePdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService, IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
        }


        public async Task<byte[]> Handle(ExportEmployeeLeavePdfQuery request, CancellationToken cancellationToken)
        {
            EmployeeLeaveReportPdfModel model = new EmployeeLeaveReportPdfModel();
            try
            {
                //Get Employee Details
                EmployeeDetail employeeDetail = await _dbContext.EmployeeDetail
                                                      .Include(x => x.EmployeeProfessionalDetail)
                                                      .ThenInclude(x=> x.OfficeDetail)
                                                      .Include(x => x.EmployeeProfessionalDetail)
                                                      .ThenInclude(x=> x.AttendanceGroupMaster)
                                                      .FirstOrDefaultAsync(x => !x.IsDeleted &&
                                                      x.EmployeeID == request.EmployeeId && x.EmployeeTypeId != (int)EmployeeTypeStatus.Prospective);
                

                var financialYear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.IsDefault == true);

                if (financialYear != null)
                {
                    model.EmployeeCode = employeeDetail.EmployeeCode;
                    model.EmployeeName = employeeDetail.EmployeeName;
                    model.Year = financialYear.StartDate.Year;
                    model.LogoPath = _env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath;

                    var leaveReasons = await _dbContext.LeaveReasonDetail.Where(x=> x.IsDeleted == false).ToListAsync();

                    var assignedLeaveToEmployee = await _dbContext.AssignLeaveToEmployee
                                                .Include(x => x.LeaveReasonDetails)
                                                .Where(x => x.IsDeleted == false && x.FinancialYearId == financialYear.FinancialYearId && x.EmployeeId == request.EmployeeId)
                                                .OrderByDescending(a => a.LeaveId)
                                                .ToListAsync();

                    var appliedLeaves= await _dbContext.EmployeeApplyLeave
                                             .Where(x=> x.IsDeleted == false && x.EmployeeId == request.EmployeeId &&
                                             x.FinancialYearId == financialYear.FinancialYearId && x.ApplyLeaveStatusId == (int)ApplyLeaveStatus.Approve).ToListAsync();

                    for(int i=1; i<=12; i++) // loop for months
                    {
                        foreach (var reason in leaveReasons) //loop for leave reasons
                        {

                            if(appliedLeaves.Where(x=> x.FromDate.Month == i).Select(x=> x.LeaveReasonId).Contains(reason.LeaveReasonId))
                            {
                                var monthName = (Month)i;
                                EmployeeMonthLeavesModel leaveModel = new EmployeeMonthLeavesModel
                                {
                                    Month = monthName.ToString(),
                                    LeaveType = reason.ReasonName,
                                    LeaveHours = reason.Unit,
                                    AssignedHours = assignedLeaveToEmployee.FirstOrDefault(x=> x.LeaveReasonId == reason.LeaveReasonId).AssignUnit,
                                    AppliedLeave = appliedLeaves.Where(x=> x.LeaveReasonId == reason.LeaveReasonId && x.FromDate.Month == i).Select(x=> x.AppliedLeaveCount).DefaultIfEmpty(0).Sum(),
                                    BalanceLeave = assignedLeaveToEmployee.FirstOrDefault(x=> x.LeaveReasonId == reason.LeaveReasonId).AssignUnit - 
                                                              appliedLeaves.Where(x=> x.LeaveReasonId == reason.LeaveReasonId && x.FromDate.Month <= i).Select(x=> x.AppliedLeaveCount).DefaultIfEmpty(0).Sum()
                                };

                                model.EmployeeLeaves.Add(leaveModel);
                            }
                            else
                            {
                                var monthName = (Month)i;
                                EmployeeMonthLeavesModel leaveModel = new EmployeeMonthLeavesModel
                                {
                                    Month = monthName.ToString(),
                                    LeaveType = reason.ReasonName,
                                    LeaveHours = reason.Unit,
                                    AssignedHours = assignedLeaveToEmployee.FirstOrDefault(x=> x.LeaveReasonId == reason.LeaveReasonId).AssignUnit,
                                    AppliedLeave = appliedLeaves.Where(x=> x.LeaveReasonId == reason.LeaveReasonId && x.FromDate.Month < i).Select(x=> x.AppliedLeaveCount).DefaultIfEmpty(0).Sum(),
                                    BalanceLeave = assignedLeaveToEmployee.FirstOrDefault(x=> x.LeaveReasonId == reason.LeaveReasonId).AssignUnit - 
                                                              appliedLeaves.Where(x=> x.LeaveReasonId == reason.LeaveReasonId && x.FromDate.Month < i).Select(x=> x.AppliedLeaveCount).DefaultIfEmpty(0).Sum()
                                };

                                model.EmployeeLeaves.Add(leaveModel);
                            }
                        }
                    }

                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await _pdfExportService.ExportToPdf(model, "Pages/PdfTemplates/EmployeeLeaveReport.cshtml");
        }
    }
}