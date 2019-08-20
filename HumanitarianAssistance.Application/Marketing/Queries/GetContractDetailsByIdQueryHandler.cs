using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetContractDetailsByIdQueryHandler : IRequestHandler<GetContractDetailsByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetContractDetailsByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetContractDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            ContractDetailsModel contractDetails = new ContractDetailsModel();
            try
            {
                var ContractDetail = await _dbContext.ContractDetails.AsNoTracking().AsQueryable().Where(x => x.IsDeleted == false && x.ContractId == request.contractId).SingleAsync();
                contractDetails.ActivityTypeId = ContractDetail.ActivityTypeId;
                contractDetails.ClientName = ContractDetail.ClientName;
                contractDetails.ContractCode = ContractDetail.ContractCode;
                contractDetails.ContractId = ContractDetail.ContractId;
                contractDetails.CurrencyId = ContractDetail.CurrencyId;
                contractDetails.EndDate = ContractDetail.EndDate;
                contractDetails.LanguageId = ContractDetail.LanguageId;
                contractDetails.MediaCategoryId = ContractDetail.MediaCategoryId;
                contractDetails.MediumId = ContractDetail.MediumId;
                contractDetails.UnitRateId = ContractDetail.UnitRateId;
                contractDetails.ClientId = ContractDetail.ClientId;
                contractDetails.NatureId = ContractDetail.NatureId;
                contractDetails.QualityId = ContractDetail.QualityId;
                contractDetails.StartDate = ContractDetail.StartDate;
                contractDetails.TimeCategoryId = ContractDetail.TimeCategoryId;
                contractDetails.UnitRate = ContractDetail.UnitRate;
                contractDetails.IsApproved = ContractDetail.IsApproved;
                contractDetails.IsDeclined = ContractDetail.IsDeclined;
                response.data.contractDetailsModel = contractDetails;
                response.StatusCode = 200;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

    }
}
