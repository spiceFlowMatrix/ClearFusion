using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAdvancesByOfficeIdQueryHandler: IRequestHandler<GetAdvancesByOfficeIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAdvancesByOfficeIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<ApiResponse> Handle(GetAdvancesByOfficeIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            
            try
            {
                List<AdvanceModel> list = await _dbContext.Advances
                                     .Include(x=> x.EmployeeDetail)
                                     .ThenInclude(x=> x.EmployeeProfessionalDetail)
                                     .Where(x => x.OfficeId == request.OfficeId &&
                                                 x.IsDeleted == false &&
                                                 x.AdvanceDate.Month <= request.Month &&
                                                 x.AdvanceDate.Year <= request.Year &&
                                                 x.IsDeducted == false)
                                                 .Select(x => new AdvanceModel
                                                 {
                                                     AdvancesId = x.AdvancesId,
                                                     AdvanceDate = x.AdvanceDate,
                                                     EmployeeId = x.EmployeeId,
                                                     EmployeeCode = x.EmployeeDetail.EmployeeCode,
                                                     EmployeeName = x.EmployeeDetail.EmployeeName,
                                                     CurrencyId = Convert.ToInt32(x.CurrencyId),
                                                     VoucherReferenceNo = Convert.ToInt64(x.VoucherReferenceNo),
                                                     Description = x.Description,
                                                     ModeOfReturn = x.ModeOfReturn,
                                                     ApprovedBy = Convert.ToInt32(x.ApprovedBy),
                                                     RequestAmount = x.RequestAmount,
                                                     AdvanceAmount = x.AdvanceAmount,
                                                     OfficeId = Convert.ToInt32(x.OfficeId),
                                                     IsApproved = Convert.ToBoolean(x.IsApproved),
                                                     IsDeducted = x.IsDeducted,
                                                     NumberOfInstallments = x.NumberOfInstallments,
                                                     DepartmentId = x.EmployeeDetail.EmployeeProfessionalDetail.DepartmentId
                                                 }).ToListAsync();

                if (list.Count > 0)
                {
                    response.data.AdvanceList = list;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "No record found";
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