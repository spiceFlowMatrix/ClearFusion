using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeePensionHistoryQueryHandler: IRequestHandler<GetEmployeePensionHistoryQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeePensionHistoryQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeePensionHistoryQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {

                List<PensionPaymentHistoryModel> PensionPaymentHistoryList = new List<PensionPaymentHistoryModel>();

                //Get Employees total pension from approved payroll
                List<PensionPaymentHistory> pensionPaymentList = await _dbContext.PensionPaymentHistory.Include(x => x.EmployeeDetail)
                                                                                                       .ThenInclude(x => x.EmployeeProfessionalDetail)
                                                                                                       .OrderByDescending(x=> x.PaymentDate)
                                                                                                       .Where(x => x.EmployeeDetail.EmployeeProfessionalDetail.OfficeId == request.OfficeId && 
                                                                                                                   x.EmployeeId == request.EmployeeId && x.IsDeleted == false)
                                                                                                       .ToListAsync();

                foreach (var item in pensionPaymentList)
                {
                    PensionPaymentHistoryModel pensionHistory = new PensionPaymentHistoryModel();
                    pensionHistory.Employee = $"{item.EmployeeDetail.EmployeeCode}-{item.EmployeeDetail.EmployeeName}";
                    pensionHistory.PensionPaid = item.PaymentAmount;
                    pensionHistory.PaymentDate = item.PaymentDate;
                    pensionHistory.VoucherNo = item.VoucherNo;
                    pensionHistory.VoucherReferenceNo = item.VoucherReferenceNo;

                    PensionPaymentHistoryList.Add(pensionHistory);
                }

                response.data.PensionPaymentHistory = PensionPaymentHistoryList;

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