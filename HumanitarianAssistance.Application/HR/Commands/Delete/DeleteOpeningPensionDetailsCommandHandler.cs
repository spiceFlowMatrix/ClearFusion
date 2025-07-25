using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Delete {
    public class DeleteOpeningPensionDetailsCommandHandler : IRequestHandler<DeleteOpeningPensionDetailsCommand, ApiResponse> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteOpeningPensionDetailsCommandHandler (HumanitarianAssistanceDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle (DeleteOpeningPensionDetailsCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                var existRecord = await _dbContext.MultiCurrencyOpeningPension.FirstOrDefaultAsync (x => x.IsDeleted == false && x.PensionId == request.PensionId);
                if (existRecord != null) {
                    existRecord.IsDeleted = true;
                    existRecord.ModifiedById = request.ModifiedById;
                    existRecord.ModifiedDate = request.ModifiedDate;
                    await _dbContext.SaveChangesAsync ();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                } else {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}