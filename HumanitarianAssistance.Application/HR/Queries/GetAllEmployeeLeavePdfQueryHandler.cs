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
    public class GetAllEmployeeLeavePdfQueryHandler : IRequestHandler<GetAllEmployeeLeavePdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;
        public GetAllEmployeeLeavePdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService, IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
        }
        public async Task<byte[]> Handle(GetAllEmployeeLeavePdfQuery request, CancellationToken cancellationToken)
        {
            EmployeeLeaveReportPdfModel model = new EmployeeLeaveReportPdfModel();

            try
            {
                //Get Employee Details
                EmployeeDetail employeeDetail = await _dbContext.EmployeeDetail
                                                      .Include(x => x.EmployeeProfessionalDetail)
                                                      .FirstOrDefaultAsync(x => !x.IsDeleted &&
                                                      x.EmployeeID == request.EmployeeId && x.EmployeeTypeId != (int)EmployeeTypeStatus.Prospective);
                
                //Get Current Financial Year
                FinancialYearDetail financialyear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => !x.IsDeleted && x.IsDefault);

                if (financialyear == null)
                {
                    throw new Exception(StaticResource.defaultFinancialYearIsNotSet);
                }

                //Get All Payroll Hours for an office and Attendance Group
                List<PayrollMonthlyHourDetail> officeHoursList = await _dbContext.PayrollMonthlyHourDetail
                                                                                 .Where(x => !x.IsDeleted && x.OfficeId == employeeDetail.EmployeeProfessionalDetail.OfficeId &&
                                                                                 x.AttendanceGroupId == employeeDetail.EmployeeProfessionalDetail.AttendanceGroupId)
                                                                                 .ToListAsync();

                //Get All Leave Types
                List<LeaveReasonDetail> leaveReasonDetails = await _dbContext.LeaveReasonDetail.Where(x => x.IsDeleted == false).ToListAsync();

                if (leaveReasonDetails == null)
                {
                    throw new Exception(StaticResource.LeavesNotDefined);
                }

                //Get All leaves types assigned to an Employee
                var assignedLeave = await _dbContext.AssignLeaveToEmployee
                                               .Where(x => x.IsDeleted == false && x.FinancialYearId == financialyear.FinancialYearId && x.EmployeeId == request.EmployeeId)
                                               .OrderByDescending(a => a.LeaveId)
                                               .Select(x => new
                                                   {
                                                       x.LeaveReasonId,
                                                       x.AssignUnit,
                                                   })
                                               .ToListAsync();

                //Get All Leaves applied by employee 
                var appliedLeave = await _dbContext.EmployeeApplyLeave
                                                      .Include(x => x.LeaveReasonDetails)
                                                      .Where(x => x.EmployeeId == request.EmployeeId)
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
                model.LogoPath = _env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath;

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
                        EmployeeMonthLeavesModel leaveModel = new EmployeeMonthLeavesModel
                        {
                            LeaveReasonId = leave.LeaveReasonId.LeaveReasonId,
                            Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Month.Month),
                            LeaveMonthNumber = item.Month.Month,
                            LeaveType = leaveReasonDetails.FirstOrDefault(x => x.LeaveReasonId == leave.LeaveReasonId.LeaveReasonId).ReasonName,
                            LeaveHours = leaveReasonDetails.FirstOrDefault(x => x.LeaveReasonId == leave.LeaveReasonId.LeaveReasonId).Unit * workingHours,
                            AppliedLeave = leave.Leaves.Count * workingHours,
                            AssignedHours = assignedLeave.FirstOrDefault(x => x.LeaveReasonId == leave.LeaveReasonId.LeaveReasonId).AssignUnit.Value * workingHours,
                            AssignedLeaveInDays = assignedLeave.FirstOrDefault(x => x.LeaveReasonId == leave.LeaveReasonId.LeaveReasonId).AssignUnit.Value
                        };

                        if (model.EmployeeLeaves.Any(x => x.LeaveReasonId == leave.LeaveReasonId.LeaveReasonId))
                        {
                            int balanceLeave = model.EmployeeLeaves.OrderByDescending(x => x.LeaveMonthNumber).FirstOrDefault(x => x.LeaveReasonId == leave.LeaveReasonId.LeaveReasonId).BalanceLeave;
                            leaveModel.BalanceLeave = balanceLeave - (leave.Leaves.Count * workingHours);
                        }
                        else
                        {
                            leaveModel.BalanceLeave = (leaveModel.AssignedLeaveInDays - leave.Leaves.Count) * workingHours;
                        }
                        
                        leaveModel.Remarks = leave.Leaves.FirstOrDefault().Remarks;
                        model.EmployeeLeaves.Add(leaveModel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return await _pdfExportService.ExportToPdf(model, "Pages/PdfTemplates/EmployeeLeaveReport.cshtml");
        }
    }
}