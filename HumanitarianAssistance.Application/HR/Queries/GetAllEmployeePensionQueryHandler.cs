using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Domain.Entities;
using System;
using HumanitarianAssistance.Common.Helpers;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllEmployeePensionQueryHandler: IRequestHandler<GetAllEmployeePensionQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmployeePensionQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllEmployeePensionQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {

                List<PensionPaymentModel> PensionPaymentList = new List<PensionPaymentModel>();

                //Get Employees total pension from approved payroll
                var salaryTaxReportList = from eptypes in _dbContext.EmployeePaymentTypes.Include(x => x.EmployeeDetail)
                                          join ed in _dbContext.EmployeeDetail on eptypes.EmployeeID equals ed.EmployeeID
                                          join epd in _dbContext.EmployeeProfessionalDetail on ed.EmployeeID equals epd.EmployeeId
                                          where eptypes.IsDeleted == false && ed.IsDeleted== false && eptypes.OfficeId == request.OfficeId && eptypes.IsApproved == true &&
                                          epd.EmployeeTypeId != (int)EmployeeTypeStatus.Prospective
                                          group eptypes by eptypes.EmployeeID
                                                                  into eGroup
                                          select new
                                          {
                                              Key = eGroup.Key,
                                              EmployeePension = eGroup.OrderBy(x => x.EmployeeID)
                                          };

                List<PensionPaymentHistory> pensionPaymentList = await _dbContext.PensionPaymentHistory.Include(x => x.EmployeeDetail).ThenInclude(x => x.EmployeeProfessionalDetail).Where(x => x.EmployeeDetail.EmployeeProfessionalDetail.OfficeId == request.OfficeId && x.IsDeleted == false).ToListAsync();

                foreach (var item in salaryTaxReportList)
                {
                    PensionPaymentModel PensionPayment = new PensionPaymentModel();
                    PensionPayment.EmployeeId = item.Key;
                    PensionPayment.EmployeeName = item.EmployeePension.FirstOrDefault().EmployeeDetail.EmployeeName;
                    PensionPayment.EmployeeCode = item.EmployeePension.FirstOrDefault().EmployeeDetail.EmployeeCode;
                    PensionPayment.TotalPensionAmount = Convert.ToDecimal(item.EmployeePension.Sum(x => x.PensionAmount));
                    PensionPayment.PensionAmountPaid = pensionPaymentList.Where(x => x.EmployeeId == item.Key).Sum(x => x.PaymentAmount);
                    PensionPayment.CurrencyId = item.EmployeePension.FirstOrDefault().CurrencyId;
                    PensionPaymentList.Add(PensionPayment);
                }

                var employeesWithNoPension = (from ed in _dbContext.EmployeeDetail

                                              join epd in _dbContext.EmployeeProfessionalDetail on ed.EmployeeID equals epd.EmployeeId into edr
                                              from edresult in edr.DefaultIfEmpty()
                                              where ed.IsDeleted == false && edresult.OfficeId == request.OfficeId
                                              && edresult.EmployeeTypeId != (int)EmployeeTypeStatus.Prospective
                                              && !PensionPaymentList.Select(x => x.EmployeeId).Contains(ed.EmployeeID)
                                              select new
                                              {
                                                  EmployeeId = ed.EmployeeID,
                                                  EmployeeName = ed.EmployeeName,
                                                  EmployeeCode = ed.EmployeeCode,
                                              });

                foreach (var item in employeesWithNoPension)
                {
                    PensionPaymentModel PensionPayment = new PensionPaymentModel();
                    PensionPayment.EmployeeId = item.EmployeeId;
                    PensionPayment.EmployeeName = item.EmployeeName;
                    PensionPayment.EmployeeCode = item.EmployeeCode;
                    PensionPayment.TotalPensionAmount = 0;
                    PensionPayment.PensionAmountPaid = 0;
                    PensionPayment.CurrencyId = _dbContext.EmployeePayroll.FirstOrDefault(x => x.IsDeleted == false && x.EmployeeID == item.EmployeeId).CurrencyId;
                    PensionPaymentList.Add(PensionPayment);
                }

                response.data.PensionPayment = PensionPaymentList;

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