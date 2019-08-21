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
    public class AddEditJobDetailCommandHandler : IRequestHandler<AddEditJobDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditJobDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditJobDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            long LatestJobId = 0;
            var jobcode = string.Empty;
            try
            {
                if (request.JobId == 0)
                {
                    var jobList = _dbContext.JobDetails.Where(x => x.JobName == request.JobName && x.IsDeleted == false).FirstOrDefault();
                    if (jobList == null)
                    {
                        var jobDetail = _dbContext.JobDetails.OrderByDescending(x => x.ContractId).FirstOrDefault();
                        if (jobDetail == null)
                        {
                            LatestJobId = 1;
                            jobcode = ProjectUtility.GetJobCode(LatestJobId.ToString());
                        }
                        else
                        {
                            LatestJobId = Convert.ToInt32(jobDetail.ContractId) + 1;
                            jobcode = ProjectUtility.GetJobCode(LatestJobId.ToString());
                        }
                        JobDetails obj = new JobDetails();                        
                        obj.ContractId = request.ContractId;
                        obj.Description = request.Description;
                        obj.EndDate = request.EndDate;
                        obj.IsActive = true;
                        obj.IsApproved = request.IsApproved;
                        obj.JobCode = request.JobCode;
                        obj.IsDeleted = false;
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = request.CreatedDate;
                        obj.JobPhaseId = request.JobPhaseId;
                        obj.JobName = request.JobName;
                        obj.JobCode = jobcode;
                        obj.IsDeleted = false;
                        _mapper.Map(request,obj);
                        await _dbContext.JobDetails.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();

                        JobPriceDetailsModel obj1 = new JobPriceDetailsModel();
                        obj1.JobId = obj.JobId;
                        obj1.Discount = request.Discount;
                        obj1.DiscountPercent = request.DiscountPercent;
                        obj1.FinalPrice = request.FinalPrice;
                        obj1.FinalRate = request.FinalRate;
                        obj1.TotalPrice = request.TotalPrice;
                        obj1.UnitRate = request.UnitRate;
                        obj1.Units = request.Units;
                        obj1.Minutes = request.Minutes;

                        JobPriceDetails priceDetails = new JobPriceDetails();                        
                        priceDetails.JobId = obj1.JobId;
                        priceDetails.Minutes = obj1.Minutes;
                        priceDetails.Discount = obj1.Discount;
                        priceDetails.DiscountPercent = obj1.DiscountPercent;
                        priceDetails.FinalPrice = obj1.FinalPrice;
                        priceDetails.FinalRate = obj1.FinalRate;
                        priceDetails.TotalPrice = obj1.TotalPrice;
                        priceDetails.UnitRate = obj1.UnitRate;
                        priceDetails.Units = obj1.Units;
                        priceDetails.CreatedById = request.CreatedById;
                        priceDetails.CreatedDate = request.CreatedDate;
                        priceDetails.IsDeleted = false;
                        _mapper.Map(obj1, priceDetails);
                        await _dbContext.JobPriceDetails.AddAsync(priceDetails);
                        await _dbContext.SaveChangesAsync();

                        JobPriceModel details = new JobPriceModel();
                        details.ContractId = request.ContractId;
                        details.Discount = request.Discount;
                        details.DiscountPercent = request.DiscountPercent;
                        details.EndDate = request.EndDate;
                        details.FinalPrice = request.FinalPrice;
                        details.FinalRate = request.FinalRate;
                        details.JobCode = obj.JobCode;
                        details.JobName = request.JobName;
                        details.TotalPrice = request.TotalPrice;
                        details.UnitRate = request.UnitRate;
                        details.JobId = obj.JobId;
                        details.JobPriceId = obj1.JobPriceId;
                        details.Minutes = obj1.Minutes;
                        details.IsApproved = obj.IsApproved;
                        int count = _dbContext.JobDetails.Where(x => x.IsDeleted == false).ToList().Count();
                        response.data.JobPriceDetail = details;
                        response.data.jobListTotalCount = count;
                        response.Message = "Job Created Successfully";
                        response.StatusCode = StaticResource.successStatusCode;
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Job Name already exists. Please try again with other job name.";
                    }

                }
                else
                {
                    var existRecord = await _dbContext.JobDetails.FirstOrDefaultAsync(x => x.IsDeleted == false && x.JobId == request.JobId);
                    if (existRecord != null)
                    {
                        _mapper.Map(request, existRecord);
                        existRecord.IsDeleted = false;
                        existRecord.ModifiedById = request.ModifiedById;
                        existRecord.ModifiedDate = request.ModifiedDate;
                        await _dbContext.SaveChangesAsync();
                        var existRecords = await _dbContext.JobPriceDetails.FirstOrDefaultAsync(x => x.IsDeleted == false && x.JobId == request.JobId);
                        JobPriceDetailsModel obj2 = new JobPriceDetailsModel();
                        obj2.JobId = request.JobId;
                        obj2.Discount = request.Discount;
                        obj2.DiscountPercent = request.DiscountPercent;
                        obj2.FinalPrice = request.FinalPrice;
                        obj2.FinalRate = request.FinalRate;
                        obj2.TotalPrice = request.TotalPrice;
                        obj2.UnitRate = request.UnitRate;
                        obj2.Minutes = request.Minutes;
                        obj2.JobPriceId = existRecords.JobPriceId;
                        _mapper.Map(obj2, existRecords);

                        existRecords.IsDeleted = false;
                        existRecords.ModifiedById = request.ModifiedById;
                        existRecords.ModifiedDate = request.ModifiedDate;
                        await _dbContext.SaveChangesAsync();
                        response.Message = "Job Updated Successfully";
                        response.StatusCode = StaticResource.successStatusCode;
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
