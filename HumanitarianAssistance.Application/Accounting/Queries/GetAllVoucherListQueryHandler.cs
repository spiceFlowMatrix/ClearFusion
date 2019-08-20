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

            string voucherNoValue = null;
            string referenceNoValue = null;
            string descriptionValue = null;
            string journalNameValue = null;
            string dateValue = null;

            if (!string.IsNullOrEmpty(request.FilterValue))
            {
                voucherNoValue = request.VoucherNoFlag ? request.FilterValue.ToLower().Trim() : null;
                referenceNoValue = request.ReferenceNoFlag ? request.FilterValue.ToLower().Trim() : null;
                descriptionValue = request.DescriptionFlag ? request.FilterValue.ToLower().Trim() : null;
                journalNameValue = request.JournalNameFlag ? request.FilterValue.ToLower().Trim() : null;
                dateValue = request.DateFlag ? request.FilterValue.ToLower().Trim() : null;
            }

            try
            {

                int totalCount = await _dbContext.VoucherDetail
                                       .Where(v => v.IsDeleted == false &&
                                               !string.IsNullOrEmpty(request.FilterValue) ? (
                                               v.VoucherNo.ToString().Trim().Contains(voucherNoValue) ||
                                               v.ReferenceNo.Trim().ToLower().Contains(referenceNoValue) ||
                                               v.Description.Trim().ToLower().Contains(descriptionValue) ||
                                               v.JournalDetails.JournalName.Trim().ToLower().Contains(journalNameValue) ||
                                               v.VoucherDate.ToString().Trim().Contains(dateValue)
                                               ) : true
                                       )
                                      .AsNoTracking()
                                      .CountAsync();

                var voucherList = await _dbContext.VoucherDetail
                                      .Where(v => v.IsDeleted == false &&
                                                 !string.IsNullOrEmpty(request.FilterValue) ? (
                                                   v.VoucherNo.ToString().Trim().Contains(voucherNoValue) ||
                                                   v.ReferenceNo.Trim().ToLower().Contains(referenceNoValue) ||
                                                   v.Description.Trim().ToLower().Contains(descriptionValue) ||
                                                   v.JournalDetails.JournalName.Trim().ToLower().Contains(journalNameValue) ||
                                                   v.VoucherDate.ToString().Trim().ToLower().Contains(dateValue)
                                                   ) : true
                                       )
                                      .OrderByDescending(x => x.CreatedDate)
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
                                      })
                                      .Skip(request.pageSize.Value * request.pageIndex.Value)
                                      .Take(request.pageSize.Value)
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