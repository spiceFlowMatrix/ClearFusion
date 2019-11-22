using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Create {

    public class AddInterviewDetailsCommandHandler : IRequestHandler<AddInterviewDetailsCommand, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddInterviewDetailsCommandHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle (AddInterviewDetailsCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}