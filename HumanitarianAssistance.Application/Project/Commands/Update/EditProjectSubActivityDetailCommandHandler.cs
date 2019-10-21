using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using HumanitarianAssistance.Application.Project.Models;
using System.Collections.Generic;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Domain.Entities.Project;
using AutoMapper;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
        public class EditProjectSubActivityDetailCommandHandler : IRequestHandler<EditProjectSubActivityDetailCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public EditProjectSubActivityDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(EditProjectSubActivityDetailCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                ProjectActivityDetail obj = await _dbContext.ProjectActivityDetail.FirstOrDefaultAsync(x => x.ActivityId == request.ActivityId && x.IsDeleted == false);
                if (obj != null)
                {
                    obj.ActivityDescription = request.ActivityDescription;
                    obj.ChallengesAndSolutions = request.ChallengesAndSolutions;
                    obj.EmployeeID = request.EmployeeID;
                    obj.IsCompleted = request.IsCompleted;
                    obj.BudgetLineId = request.BudgetLineId;
                    obj.Achieved = request.Achieved;
                    obj.Target = request.Target;
                    obj.ModifiedDate = request.ModifiedDate;
                    obj.IsDeleted = false;
                    obj.ModifiedById = request.ModifiedById;
                    obj.SubActivityTitle = request.SubActivityTitle;
                    await _dbContext.SaveChangesAsync();
                }
                response.data.ProjectActivityDetail = obj;
                response.StatusCode = StaticResource.successStatusCode;
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
