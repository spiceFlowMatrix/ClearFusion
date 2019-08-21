using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddOfficeDetailCommandHandler : IRequestHandler<AddOfficeDetailCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;

        public AddOfficeDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddOfficeDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var existoffice = await _dbContext.OfficeDetail.FirstOrDefaultAsync(o => o.OfficeCode == request.OfficeCode); //use only OfficeCode

                if (existoffice == null)
                {
                    OfficeDetail obj = new OfficeDetail
                    {
                        OfficeCode = request.OfficeCode,
                        OfficeName = request.OfficeName,
                        SupervisorName = request.SupervisorName,
                        PhoneNo = request.PhoneNo,
                        FaxNo = request.FaxNo,
                        OfficeKey = request.OfficeKey,
                        IsDeleted = false,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow
                    };

                    await _dbContext.OfficeDetail.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                    response.Message = StaticResource.MandateNameAlreadyExist;
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