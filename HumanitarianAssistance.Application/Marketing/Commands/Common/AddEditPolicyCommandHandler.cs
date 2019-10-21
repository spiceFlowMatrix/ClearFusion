using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditPolicyCommandHandler : IRequestHandler<AddEditPolicyCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditPolicyCommandHandler(HumanitarianAssistanceDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditPolicyCommand request, CancellationToken cancellationToken)
        {
            long LatestPolicyId = 0;
            var policyCode = string.Empty;
            ApiResponse response = new ApiResponse();
            try
            {
                if (request.PolicyId == 0)
                {
                    var policy = _dbContext.PolicyDetails.Where(x => x.PolicyName == request.PolicyName && x.IsDeleted == false).FirstOrDefault();
                    if (policy == null)
                    {
                        var policyDetail = _dbContext.PolicyDetails.OrderByDescending(x => x.PolicyId)
                                                                                       .FirstOrDefault();
                        if (policyDetail == null)
                        {
                            LatestPolicyId = 1;
                            policyCode = LatestPolicyId.ToString().GetPolicyCode();
                        }
                        else
                        {
                            LatestPolicyId = Convert.ToInt32(policyDetail.PolicyId) + 1;
                            policyCode = LatestPolicyId.ToString().GetPolicyCode();
                        }
                        PolicyDetail obj = new PolicyDetail(); 
                        obj.CreatedById = request.CreatedById;
                        obj.MediumId = request.MediumId;
                        obj.ProducerId = request.ProducerId;
                        obj.MediaCategoryId = request.MediaCategoryId;
                        obj.PolicyName = request.PolicyName;
                        obj.CreatedDate = DateTime.Now;
                        obj.IsDeleted = false;
                        obj.PolicyCode = policyCode;
                        obj.Description = request.Description;
                        _mapper.Map(request, obj);
                        await _dbContext.PolicyDetails.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();
                        int totalCount = await _dbContext.PolicyDetails
                                      .Where(v => v.IsDeleted == false)
                                     .AsNoTracking()
                                     .CountAsync();
                        response.data.policyDetails = obj;
                        response.data.TotalCount = totalCount;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Policy created successfully";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Policy Name already exists. Please try again with other policy name.";
                    }
                }
                else
                {
                    var existRecord = await _dbContext.PolicyDetails.FirstOrDefaultAsync(x => x.IsDeleted == false && x.PolicyId == request.PolicyId);
                    if (existRecord != null)
                    { 
                        existRecord.IsDeleted = false;
                        existRecord.Description = request.Description;
                        existRecord.ModifiedById = request.ModifiedById;
                        existRecord.ModifiedDate = DateTime.Now;
                        existRecord.LanguageId = request.LanguageId;
                        existRecord.MediaCategoryId =request.MediaCategoryId;
                        existRecord.MediumId = request.MediumId;
                        existRecord.PolicyName = request.PolicyName;
                        existRecord.ProducerId = request.ProducerId;
                        _mapper.Map(request, existRecord);
                        await _dbContext.SaveChangesAsync();
                        response.data.policyDetails = existRecord;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Policy updated successfully";
                    }
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
