using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddSalaryTaxReportContentDetailsCommandHandler : IRequestHandler<AddSalaryTaxReportContentDetailsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddSalaryTaxReportContentDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddSalaryTaxReportContentDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    SalaryTaxReportContent obj = new SalaryTaxReportContent()
                    {
                        EmployerAuthorizedOfficerName = request.EmployerAuthorizedOfficerName,
                        PositionAuthorizedOfficer = request.PositionAuthorizedOfficer,
                        OfficeId = request.OfficeId,
                        CreatedById = request.CreatedById,
                        CreatedDate = request.CreatedDate,
                        IsDeleted = false
                    };
                    await _dbContext.SalaryTaxReportContent.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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
