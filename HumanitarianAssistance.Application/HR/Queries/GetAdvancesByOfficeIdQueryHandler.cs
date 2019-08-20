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
                                     .Join(_dbContext.EmployeeDetail,
                                                                x => x.EmployeeId, //Primary
                                                                y => y.EmployeeID, //Foreign
                                                                (x, y) => new
                                                                {
                                                                    Adv = x,
                                                                    Emp = y
                                                                })
                                     .Where(x => x.Adv.OfficeId == request.OfficeId &&
                                                 x.Adv.IsDeleted == false &&
                                                 x.Adv.AdvanceDate.Month <= request.Month &&
                                                 x.Adv.AdvanceDate.Year <= request.Year &&
                                                 x.Adv.IsDeducted == false)
                                                 .Select(x => new AdvanceModel
                                                 {
                                                     AdvancesId = x.Adv.AdvancesId,
                                                     AdvanceDate = x.Adv.AdvanceDate,
                                                     EmployeeId = x.Adv.EmployeeId,
                                                     EmployeeCode = x.Emp.EmployeeCode,
                                                     EmployeeName = x.Emp.EmployeeName,
                                                     CurrencyId = x.Adv.CurrencyId,
                                                     VoucherReferenceNo = x.Adv.VoucherReferenceNo,
                                                     Description = x.Adv.Description,
                                                     ModeOfReturn = x.Adv.ModeOfReturn,
                                                     ApprovedBy = x.Adv.ApprovedBy,
                                                     RequestAmount = x.Adv.RequestAmount,
                                                     AdvanceAmount = x.Adv.AdvanceAmount,
                                                     OfficeId = x.Adv.OfficeId,
                                                     IsApproved = x.Adv.IsApproved,
                                                     IsDeducted = x.Adv.IsDeducted,
                                                     NumberOfInstallments = x.Adv.NumberOfInstallments,
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