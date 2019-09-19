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

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllEmployeeLeavePdfQueryHandler : IRequestHandler<GetAllEmployeeLeavePdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        public GetAllEmployeeLeavePdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
        }
        public async Task<byte[]> Handle(GetAllEmployeeLeavePdfQuery request, CancellationToken cancellationToken)
        {
            EmployeeLeaveReportPdfModel model = new EmployeeLeaveReportPdfModel();

            try
            {
                EmployeeDetail employeeDetail = await _dbContext.EmployeeDetail
                                                      .Include(x => x.EmployeeProfessionalDetail)
                                                      .FirstOrDefaultAsync(x => !x.IsDeleted &&
                                                      x.EmployeeID == request.EmployeeId && x.EmployeeTypeId != (int)EmployeeTypeStatus.Prospective);

                FinancialYearDetail financialyear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => !x.IsDeleted && x.IsDefault);

                List<PayrollMonthlyHourDetail> officeHoursList = await _dbContext.PayrollMonthlyHourDetail
                                                                                 .Where(x => !x.IsDeleted && x.OfficeId == employeeDetail.EmployeeProfessionalDetail.OfficeId &&
                                                                                 x.AttendanceGroupId == employeeDetail.EmployeeProfessionalDetail.AttendanceGroupId)
                                                                                 .ToListAsync();

                List<LeaveReasonDetail> leaveReasonDetails = await _dbContext.LeaveReasonDetail.Where(x => x.IsDeleted == false).ToListAsync();

                var assignedLeave = await _dbContext.AssignLeaveToEmployee
                                               .Include(x => x.LeaveReasonDetails)
                                               .Where(x => x.IsDeleted == false && x.FinancialYearId == financialyear.FinancialYearId && x.EmployeeId == request.EmployeeId)
                                               .OrderByDescending(a => a.LeaveId)
                                               .Select(x => new
                                                   {
                                                       x.LeaveId,
                                                       x.LeaveReasonId,
                                                       x.LeaveReasonDetails.ReasonName,
                                                       x.LeaveReasonDetails.Unit,
                                                       x.AssignUnit,
                                                       x.UsedLeaveUnit,
                                                       x.FinancialYearId,
                                                       x.Description
                                                   })
                                               .ToListAsync();

                var appliedLeave = await _dbContext.EmployeeApplyLeave
                                                      .Include(x => x.LeaveReasonDetails)
                                                      .Where(x => x.EmployeeId == request.EmployeeId)
                                                      .OrderByDescending(o => o.ApplyLeaveId)
                                                      .GroupBy(item => new
                                                      {
                                                          item.FromDate.Date.Month
                                                      })
                                                      .Select(x => new
                                                      {
                                                          Month = x.Key,
                                                          Leaves = x.GroupBy(y=> new 
                                                          {
                                                              y.LeaveReasonId
                                                          })
                                                          .Select(z=> new 
                                                          {
                                                              LeaveReasonId= z.Key,
                                                              Leaves=z.ToList()
                                                          })
                                                      })
                                                      .ToListAsync();

                model.EmployeeCode = employeeDetail.EmployeeCode;
                model.EmployeeName = employeeDetail.EmployeeName;
                model.Year = financialyear.StartDate.Year;

                int index = 0;

                foreach (var item in appliedLeave)
                {
                    var payrollMonthHour = officeHoursList.FirstOrDefault(x => x.PayrollMonth == item.Month.Month);

                    if(payrollMonthHour == null)
                    {
                        throw new Exception(String.Format(StaticResource.PayrollDailyHoursNotSaved, item.Month.Month, employeeDetail.EmployeeProfessionalDetail.OfficeId));
                    }

                    int workingHours = payrollMonthHour.Hours.Value;

                    foreach (var leave in item.Leaves)
                    {
                        EmployeeMonthLeavesModel leaveModel = new EmployeeMonthLeavesModel();
                        leaveModel.Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Month.Month);

                        leaveModel.LeaveType = leaveReasonDetails.FirstOrDefault(x => x.LeaveReasonId == leave.LeaveReasonId.LeaveReasonId).ReasonName;
                        leaveModel.LeaveHours = leave.Leaves.Count * workingHours;
                        leaveModel.AppliedLeave = leave.Leaves.Count;
                        leaveModel.AssignedHours = assignedLeave.FirstOrDefault(x => x.LeaveReasonId == leave.LeaveReasonId.LeaveReasonId).AssignUnit.Value * workingHours;
                        leaveModel.AssignedLeaveInDays = assignedLeave.FirstOrDefault(x => x.LeaveReasonId == leave.LeaveReasonId.LeaveReasonId).AssignUnit.Value;
                        leaveModel.BalanceLeave= index>0? model.EmployeeLeaves[index-1].BalanceLeave- leave.Leaves.Count:  leaveModel.AssignedLeaveInDays - leave.Leaves.Count;
                        leaveModel.Remarks = leave.Leaves.FirstOrDefault().Remarks;
                        model.EmployeeLeaves.Add(leaveModel);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return await _pdfExportService.ExportToPdf(model, "Pages/PdfTemplates/EmployeeLeaveReport.cshtml");
        }
    }
}