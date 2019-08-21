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
    public class AddEditPolicyOrderScheduleCommandHandler : IRequestHandler<AddEditPolicyOrderScheduleCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditPolicyOrderScheduleCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddEditPolicyOrderScheduleCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var list = await _dbContext.PolicyOrderSchedules.Where(x => x.IsDeleted == false && x.PolicyId == request.PolicyId && ((x.StartDate <= request.StartDate && x.EndDate >= request.EndDate) || (x.StartDate <= request.StartDate && x.EndDate >= request.StartDate) || (x.StartDate <= request.EndDate && x.EndDate >= request.EndDate))).ToListAsync();
                try
                {
                    if (list.Count != 0)
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Order schedule could not be requested as the dates are already taken.";
                    }

                    else
                    {
                        if (request.Id == 0)
                        {
                            PolicyOrderSchedule obj = new PolicyOrderSchedule();                            
                            obj.PolicyId = request.PolicyId;
                            obj.CreatedDate = DateTime.UtcNow;
                            obj.StartDate = request.StartDate;
                            obj.EndDate = request.EndDate;
                            obj.IsDeleted = false;
                            obj.RequestSchedule = true;
                            _mapper.Map(request,obj);
                            await _dbContext.PolicyOrderSchedules.AddAsync(obj);
                            await _dbContext.SaveChangesAsync();
                            response.StatusCode = 200;
                            //response.data.policyTimeScheduleDetails = obj;
                            response.Message = "Schedule order requested successfully";
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong + ex.Message;
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
