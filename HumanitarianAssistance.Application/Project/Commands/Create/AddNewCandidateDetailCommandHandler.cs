using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
        public class AddNewCandidateDetailCommandHandler : IRequestHandler<AddNewCandidateDetailCommand, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddNewCandidateDetailCommandHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle (AddNewCandidateDetailCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
             try {
                ProjectHiringRequestDetail hiringRequestDeatil = new ProjectHiringRequestDetail () {
                    CreatedById = request.CreatedById,
                    CreatedDate = request.CreatedDate,
                    IsDeleted = false,                    
                };
                await _dbContext.ProjectHiringRequestDetail.AddAsync (hiringRequestDeatil);
                await _dbContext.SaveChangesAsync ();

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}