using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllVoucherListQueryHandler : IRequestHandler<GetAllVoucherListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllVoucherListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllVoucherListQuery request, CancellationToken cancellationToken)
        {

            ApiResponse response = new ApiResponse();

            try
            {

                var query = _dbContext.VoucherDetail
                                       .Where(v => v.IsDeleted == false && request.OfficeIds.Contains(v.OfficeId))
                                      .OrderByDescending(x => x.CreatedDate)
                                      .AsQueryable();

                if (request.CurrencyId.HasValue)
                {
                    query = query.Where(x => x.CurrencyId == request.CurrencyId);
                }
                if (request.JournalId.HasValue)
                {
                    query = query.Where(x => x.JournalCode == request.JournalId);
                }
                if (request.StartDate.HasValue && request.EndDate.HasValue)
                {
                    query = query.Where(x => x.VoucherDate.Date >= request.StartDate.Value.Date && x.VoucherDate.Date <= request.EndDate.Value.Date);
                }
                if (!string.IsNullOrEmpty(request.FilterValue))
                {
                    query = query.Where(x =>  x.ReferenceNo.Trim().ToLower().Contains(request.FilterValue.Trim().ToLower()) ||
                                    x.Description.Trim().ToLower().Contains(request.FilterValue.Trim().ToLower()));
                }           
                if(request.OperationalType.HasValue) 
                {
                    query = query.Where(x => x.OperationalType == request.OperationalType);
                }

                int totalCount = await query.CountAsync();
                var voucherList = await query
                                      .Select(x => new VoucherDetailModel
                                      {
                                          VoucherNo = x.VoucherNo,
                                          CurrencyCode = x.CurrencyDetail.CurrencyCode,
                                          CurrencyId = x.CurrencyDetail.CurrencyId,
                                          VoucherDate = x.VoucherDate,
                                          ChequeNo = x.ChequeNo,
                                          ReferenceNo = x.ReferenceNo,
                                          Description = x.Description,
                                          JournalName = x.JournalDetails.JournalName,
                                          JournalCode = x.JournalDetails.JournalCode,
                                          VoucherTypeId = x.VoucherTypeId,
                                          OfficeId = x.OfficeId,
                                          ProjectId = x.ProjectId,
                                          BudgetLineId = x.BudgetLineId,
                                          OfficeName = x.OfficeDetails.OfficeName,
                                          IsVoucherVerified = x.IsVoucherVerified,
                                          OperationalType = x.OperationalType
                                      }).OrderByDescending(x=> x.CreatedDate)
                                      .Skip(request.PageSize.Value * request.PageIndex.Value)
                                      .Take(request.PageSize.Value)
                                      .AsNoTracking()
                                      .ToListAsync();

                response.data.VoucherDetailList = voucherList;
                response.data.TotalCount = totalCount;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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