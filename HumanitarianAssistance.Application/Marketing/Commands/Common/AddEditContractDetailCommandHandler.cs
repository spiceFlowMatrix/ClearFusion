using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditContractDetailCommandHandler : IRequestHandler<AddEditContractDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditContractDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditContractDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            ContractDetails contractDetails = new ContractDetails();
            long LatestContractId = 0;
            var contractcode = string.Empty;
            ClientDetails client = await _dbContext.ClientDetails.FirstOrDefaultAsync(x => x.ClientId == request.ClientId && x.IsDeleted == false);
            request.ClientName = client.ClientName;
            ContractDetailsModel conDetails = new ContractDetailsModel();
            try
            {
                if (request.ContractId == 0)
                {
                    var ContractDetail = _dbContext.ContractDetails
                                                               .OrderByDescending(x => x.ContractId)
                                                               .FirstOrDefault();
                    if (ContractDetail == null)
                    {
                        LatestContractId = 1;
                        contractcode = ProjectUtility.GetContractCode(LatestContractId.ToString());
                    }
                    else
                    {
                        LatestContractId = Convert.ToInt32(ContractDetail.ContractId) + 1;
                        contractcode = ProjectUtility.GetContractCode(LatestContractId.ToString());
                    }
                    ContractDetails obj = new ContractDetails();
                    obj.ContractId = request.ContractId;
                    obj.ActivityTypeId = request.ActivityTypeId;
                    obj.ClientName = request.ClientName;
                    obj.ContractCode = contractcode;
                    obj.IsDeleted = false;
                    obj.CreatedById = request.CreatedById;
                    obj.UnitRateId = request.UnitRateId == 0 ? null : request.UnitRateId;
                    obj.UnitRate = request.UnitRate;
                    obj.CreatedDate = DateTime.Now;
                    obj.CurrencyId = request.CurrencyId;
                    obj.StartDate = request.StartDate;
                    obj.EndDate = request.EndDate;
                    obj.IsCompleted = false;
                    obj.LanguageId = request.LanguageId;
                    obj.MediaCategoryId = request.MediaCategoryId;
                    obj.MediumId = request.MediumId;
                    obj.NatureId = request.NatureId;
                    obj.QualityId = request.QualityId;
                    obj.TimeCategoryId = request.TimeCategoryId;
                    _mapper.Map(request, obj);
                    await _dbContext.ContractDetails.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();

                    conDetails.ActivityTypeId = obj.ActivityTypeId;
                    conDetails.ClientName = obj.ClientName;
                    conDetails.ContractCode = obj.ContractCode;
                    conDetails.ContractId = obj.ContractId;
                    conDetails.CurrencyId = obj.CurrencyId;
                    conDetails.EndDate = obj.EndDate;
                    conDetails.LanguageId = obj.LanguageId;
                    conDetails.MediaCategoryId = obj.MediaCategoryId;
                    conDetails.MediumId = obj.MediumId;
                    conDetails.NatureId = obj.NatureId;
                    conDetails.QualityId = obj.QualityId;
                    conDetails.StartDate = obj.StartDate;
                    conDetails.TimeCategoryId = obj.TimeCategoryId;
                    conDetails.UnitRate = obj.UnitRate;
                    conDetails.IsApproved = obj.IsApproved;
                    conDetails.Count = _dbContext.ContractDetails.Count(x => x.IsDeleted == false);
                    if (obj.IsDeclined == true)
                    {
                        conDetails.IsDeclined = obj.IsDeclined;
                    }
                    if (obj.IsApproved == true)
                    {
                        conDetails.IsApproved = obj.IsApproved;
                    }
                    response.data.contractDetailsModel = conDetails;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Contract added successfully";
                }
                else
                {
                    ContractDetails existRecord = await _dbContext.ContractDetails.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ContractId == request.ContractId);
                    if (existRecord != null)
                    {
                        existRecord.IsCompleted = true;
                        existRecord.IsDeleted = false;
                        existRecord.UnitRateId = request.UnitRateId == 0 ? null : request.UnitRateId;
                        existRecord.UnitRate = request.UnitRate;
                        existRecord.ModifiedById = request.ModifiedById;
                        existRecord.ModifiedDate = request.ModifiedDate;
                        _mapper.Map(request, existRecord);
                        await _dbContext.SaveChangesAsync();
                        response.data.contractDetailsModel = existRecord;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Contract updated successfully";
                    }
                }
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
