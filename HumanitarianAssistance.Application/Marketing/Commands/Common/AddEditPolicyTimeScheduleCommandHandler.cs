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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditPolicyTimeScheduleCommandHandler : IRequestHandler<AddEditPolicyTimeScheduleCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        IMapper _mapper;
        public AddEditPolicyTimeScheduleCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditPolicyTimeScheduleCommand request, CancellationToken cancellationToken)
        {
            long LatestId = 0;
            var Code = string.Empty;
            ApiResponse response = new ApiResponse();
            try
            {
                var ifExists = await _dbContext.PolicyTimeSchedules.Where(x => x.PolicyId == request.PolicyId && x.StartTime == TimeSpan.Parse(request.StartTime) && x.EndTime == TimeSpan.Parse(request.EndTime) && x.IsDeleted == false).FirstOrDefaultAsync();
                if (ifExists != null)
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Time slot for the policy already exists";
                }
                else
                {
                    if (request.Id == 0)
                    {
                        var detail = _dbContext.PolicyTimeSchedules.OrderByDescending(x => x.Id)
                                                                                       .FirstOrDefault();
                        if (detail == null)
                        {
                            LatestId = 1;
                            Code = LatestId.ToString().GetPolicyCode();
                        }
                        else
                        {
                            LatestId = Convert.ToInt32(detail.Id) + 1;
                            Code = LatestId.ToString().GetPolicyCode();
                        }
                        PolicyTimeSchedule obj = new PolicyTimeSchedule();                        
                        obj.StartTime = TimeSpan.Parse(request.StartTime);
                        obj.EndTime = TimeSpan.Parse(request.EndTime);
                        obj.TimeScheduleCode = Code;
                        obj.PolicyId = request.PolicyId;
                        obj.CreatedDate = DateTime.Now;
                        obj.IsDeleted = false;
                        _mapper.Map(request, obj);
                        await _dbContext.PolicyTimeSchedules.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();
                        response.StatusCode = 200;
                        response.data.policyTimeScheduleDetails = obj;
                        response.Message = "Time slot created successfully";

                    }
                    else
                    {
                        var existRecord = await _dbContext.PolicyTimeSchedules.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.Id);
                        if (existRecord != null)
                        {
                            _mapper.Map(request, existRecord);
                            existRecord.IsDeleted = false;
                            existRecord.StartTime = TimeSpan.Parse(request.StartTime);
                            existRecord.ModifiedById = request.ModifiedById;
                            existRecord.ModifiedDate = DateTime.Now;
                            existRecord.EndTime = TimeSpan.Parse(request.EndTime);
                            await _dbContext.SaveChangesAsync();
                            response.data.policyTimeScheduleDetails = existRecord;
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = "Time slot updated successfully";
                        }
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
