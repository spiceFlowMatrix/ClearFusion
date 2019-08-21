using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditPlayoutMinutesCommandHandler : IRequestHandler<AddEditPlayoutMinutesCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddEditPlayoutMinutesCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddEditPlayoutMinutesCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                bool IsExist = await _dbContext.PlayoutMinutes.AnyAsync(x => x.IsDeleted == false && x.ScheduleId == request.ScheduleId);
                if (!IsExist)
                {
                    PlayoutMinutes objPlayoutMinutes = new PlayoutMinutes
                    {
                        TotalMinutes = request.TotalMinutes,
                        DroppedMinutes = request.DroppedMinutes,
                        ScheduleId = request.ScheduleId,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false
                    };
                    await _dbContext.PlayoutMinutes.AddAsync(objPlayoutMinutes);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Playout Minutes Added Successfully";
                }
                else
                {
                    var objPlayoutMinutes = await _dbContext.PlayoutMinutes.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ScheduleId == request.ScheduleId);
                    objPlayoutMinutes.IsDeleted = false;
                    objPlayoutMinutes.ScheduleId = request.ScheduleId;
                    objPlayoutMinutes.ModifiedById = request.ModifiedById;
                    objPlayoutMinutes.ModifiedDate = DateTime.Now;
                    objPlayoutMinutes.TotalMinutes = request.TotalMinutes;
                    objPlayoutMinutes.DroppedMinutes = request.DroppedMinutes;
                    await _dbContext.SaveChangesAsync();
                    response.data.playoutMinutesDetails = objPlayoutMinutes;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Playout minutes Updated successfully";
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
