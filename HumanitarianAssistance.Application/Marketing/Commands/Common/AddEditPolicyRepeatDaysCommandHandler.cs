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
    public class AddEditPolicyRepeatDaysCommandHandler : IRequestHandler<AddEditPolicyRepeatDaysCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditPolicyRepeatDaysCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditPolicyRepeatDaysCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request.Id == 0)
                {
                    var detail = _dbContext.PolicyDaySchedules.OrderByDescending(x => x.Id).FirstOrDefault();
                    PolicyDaySchedule obj = new PolicyDaySchedule();                    
                    if (request.RepeatDays != null && request.RepeatDays.Count > 0)
                    {
                        foreach (var items in request.RepeatDays)
                        {
                            if (items.Value == "MON")
                            {
                                obj.Monday = items.status;
                            }
                            if (items.Value == "TUE")
                            {
                                obj.Tuesday = items.status;
                            }
                            if (items.Value == "WED")
                            {
                                obj.Wednesday = items.status;
                            }
                            if (items.Value == "THU")
                            {
                                obj.Thursday = items.status;
                            }
                            if (items.Value == "FRI")
                            {
                                obj.Friday = items.status;
                            }
                            if (items.Value == "SAT")
                            {
                                obj.Saturday = items.status;
                            }
                            if (items.Value == "SUN")
                            {
                                obj.Sunday = items.status;
                            }
                        }
                    }
                    obj.PolicyId = request.PolicyId;
                    obj.CreatedDate = DateTime.Now;
                    obj.IsDeleted = false;
                    _mapper.Map(request,obj);
                    await _dbContext.PolicyDaySchedules.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = 200;
                    response.data.policyDayScheduleDetails = obj;
                    response.Message = "Repeat Days updated successfully";
                }
                else
                {
                    var existRecord = await _dbContext.PolicyDaySchedules.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.Id);
                    if (existRecord != null)
                    {
                        _mapper.Map(request, existRecord);
                        existRecord.IsDeleted = false;
                        existRecord.ModifiedById = request.ModifiedById;
                        existRecord.ModifiedDate = DateTime.Now;
                        if (request.RepeatDays != null && request.RepeatDays.Count > 0)
                        {
                            foreach (var items in request.RepeatDays)
                            {
                                if (items.Value == "MON")
                                {
                                    existRecord.Monday = items.status;
                                }
                                if (items.Value == "TUE")
                                {
                                    existRecord.Tuesday = items.status;
                                }
                                if (items.Value == "WED")
                                {
                                    existRecord.Wednesday = items.status;
                                }
                                if (items.Value == "THU")
                                {
                                    existRecord.Thursday = items.status;
                                }
                                if (items.Value == "FRI")
                                {
                                    existRecord.Friday = items.status;
                                }
                                if (items.Value == "SAT")
                                {
                                    existRecord.Saturday = items.status;
                                }
                                if (items.Value == "SUN")
                                {
                                    existRecord.Sunday = items.status;
                                }
                            }
                        }
                        await _dbContext.SaveChangesAsync();
                        response.data.policyDayScheduleDetails = existRecord;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Repeat Days updated successfully";
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
