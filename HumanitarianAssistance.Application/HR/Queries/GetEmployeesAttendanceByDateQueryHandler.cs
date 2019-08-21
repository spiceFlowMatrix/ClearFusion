using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeesAttendanceByDateQueryHandler: IRequestHandler<GetEmployeesAttendanceByDateQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly  IMapper _mapper;

        public GetEmployeesAttendanceByDateQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(GetEmployeesAttendanceByDateQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {

                int month = DateTime.Parse(request.SelectedDate).Month;

                List<EmployeeAttendance> empattendancelist = await _dbContext.EmployeeAttendance.Where(x => x.Date.Date == DateTime.Parse(request.SelectedDate).Date).ToListAsync();  //marked or not

                List<EmployeeDetail> activelist = await _dbContext.EmployeeDetail
                                                                    .Include(x => x.EmployeeProfessionalDetail) // use to get officeId 
                                                                    .Include(x => x.EmployeeAttendance)
                                                                    .Where(x => x.EmployeeProfessionalDetail.OfficeId == request.OfficeId &&
                                                                                x.EmployeeProfessionalDetail.HiredOn.Value.Date <= DateTime.Parse(request.SelectedDate).Date &&
                                                                                x.EmployeeProfessionalDetail.EmployeeTypeId == (int)EmployeeTypeStatus.Active &&
                                                                                x.IsDeleted == false && x.EmployeeProfessionalDetail.AttendanceGroupId== request.AttendanceGroupId)
                                                                    .OrderBy(x => x.EmployeeID)
                                                                    .ToListAsync();                 //EmployeeTypeId is moved to EmployeeProfessionalDetails table

                IList<EmployeeAttendanceModel> empAttModel = new List<EmployeeAttendanceModel>();
                if (request.AttendanceStatus)
                {
                    empAttModel = activelist.SelectMany(x => x.EmployeeAttendance)
                                            .Where(x => x.Date.Date == DateTime.Parse(request.SelectedDate).Date)
                                            .Select(x => new EmployeeAttendanceModel
                                            {
                                                AttendanceId = x.AttendanceId,
                                                EmployeeId = x.EmployeeId,
                                                Date = x.Date,
                                                InTime = x.InTime.Value,
                                                OutTime = x.OutTime,
                                                AttendanceTypeId = x.AttendanceTypeId,
                                                EmployeeName = x.EmployeeDetails.EmployeeName,
                                                EmployeeCode = x.EmployeeDetails.EmployeeCode,
                                                LeaveStatus = x.AttendanceId == (int)AttendanceType.L ? true : false,
                                                OfficeId = x.EmployeeDetails.EmployeeProfessionalDetail.OfficeId
                                            }).ToList();


                    if (empAttModel.Count == 0 || empAttModel == null)
                        response.data.AttendanceStatus = false;  //attendance already marked
                    else
                        response.data.AttendanceStatus = true; //not marked

                }
                else
                {
                    int count = 0;

                    //Get Default In-time and out-time for an office and for current year and current month
                    PayrollMonthlyHourDetail xPayrollMonthlyHourDetail = _dbContext.PayrollMonthlyHourDetail.FirstOrDefault(x => x.IsDeleted == false && x.OfficeId == request.OfficeId && x.PayrollYear == DateTime.Now.Year
                                                                                                                            && x.PayrollMonth == month && x.AttendanceGroupId== request.AttendanceGroupId);
                    if (xPayrollMonthlyHourDetail != null)
                    {

                        foreach (var item in activelist)
                        {
                            EmployeeAttendanceModel obj = new EmployeeAttendanceModel();
                            count = empattendancelist.Where(x => x.EmployeeId == item.EmployeeID).ToList().Count();
                            if (count == 0)
                            {
                                obj.EmployeeId = item.EmployeeID;
                                obj.EmployeeName = item.EmployeeName;
                                obj.EmployeeCode = item.EmployeeCode;
                                obj.AttendanceTypeId = (int)AttendanceType.P;
                                obj.Date = DateTime.UtcNow;
                                obj.InTime = xPayrollMonthlyHourDetail.InTime;
                                obj.OutTime = xPayrollMonthlyHourDetail.OutTime;
                                obj.LeaveStatus = false;
                                obj.OfficeId = request.OfficeId;
                                empAttModel.Add(obj);
                            }
                        }

                        if (empAttModel.Count == 0 || empAttModel == null)
                            response.data.AttendanceStatus = false;  //attendance already marked
                        else
                            response.data.AttendanceStatus = true; //not marked

                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;

                        response.Message = "Intime and Outtime not set for office";

                    }
                }

                response.data.EmployeeAttendanceList = empAttModel;
                response.StatusCode = response.StatusCode == 0 ? StaticResource.successStatusCode : response.StatusCode;
                
                //if message is empty then success else there is already some error message
                response.Message = string.IsNullOrEmpty(response.Message) ? "Success" : response.Message;
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