using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditSalaryTaxReportContentDetailsCommandHandler : IRequestHandler<EditSalaryTaxReportContentDetailsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditSalaryTaxReportContentDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditSalaryTaxReportContentDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var existrecord = await _dbContext.SalaryTaxReportContent.FirstOrDefaultAsync(x => x.SalaryTaxReportContentId == request.SalaryTaxReportContentId);
                if (existrecord != null)
                {
                    existrecord.EmployerAuthorizedOfficerName = request.EmployerAuthorizedOfficerName;
                    existrecord.PositionAuthorizedOfficer = request.PositionAuthorizedOfficer;
                    existrecord.OfficeId = request.OfficeId;
                    existrecord.ModifiedById = request.ModifiedById;
                    existrecord.ModifiedDate = request.ModifiedDate;
                    existrecord.IsDeleted = request.IsDeleted;

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
