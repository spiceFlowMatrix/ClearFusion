using AutoMapper;
using HumanitarianAssistance.Application.CommonServices;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProjectBudgetLineDetailCommandHandler : IRequestHandler<AddEditProjectBudgetLineDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IProjectServices _iProjectServices;
        public AddEditProjectBudgetLineDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IProjectServices iProjectServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _iProjectServices = iProjectServices;
        }
        public async Task<ApiResponse> Handle(AddEditProjectBudgetLineDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectBudgetLineDetail obj = _mapper.Map<AddEditProjectBudgetLineDetailCommand, ProjectBudgetLineDetail>(request);

                if (request.BudgetLineId == null)
                {
                    obj.CreatedDate = request.CreatedDate;
                    obj.IsDeleted = false;
                    obj.CreatedById = request.CreatedById;

                    await _dbContext.ProjectBudgetLineDetail.AddAsync(obj);

                    if (obj.BudgetLineId != 0)
                    {

                        obj.BudgetCode = await _iProjectServices.GetProjectBudgetLineCode(obj);

                        await _dbContext.SaveChangesAsync();
                    }

                }
                else
                {
                    ProjectBudgetLineDetail projectBudgetLineDetail = _dbContext.ProjectBudgetLineDetail
                                                                                .FirstOrDefault(x => x.IsDeleted == false && x.BudgetLineId == request.BudgetLineId);
                    
                    projectBudgetLineDetail.BudgetCode = obj.BudgetCode;
                    projectBudgetLineDetail.BudgetName = obj.BudgetName;
                    projectBudgetLineDetail.CurrencyId = obj.CurrencyId;
                    projectBudgetLineDetail.InitialBudget = obj.InitialBudget;
                    projectBudgetLineDetail.ProjectJobId = obj.ProjectJobId;

                    obj.ModifiedById = request.ModifiedById;
                    obj.ModifiedDate = request.ModifiedDate;
                    obj.IsDeleted = false;

                    await _dbContext.SaveChangesAsync();
                }
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
