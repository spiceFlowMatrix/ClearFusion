using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddQualificationDetailsCommandHandler : IRequestHandler<AddQualificationDetailsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddQualificationDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddQualificationDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                QualificationDetails existrecord = await _dbContext.QualificationDetails.FirstOrDefaultAsync(x => x.QualificationName == request.QualificationName);
                if (existrecord == null)
                {
                    QualificationDetails obj = _mapper.Map<QualificationDetails>(request);
                    obj.IsDeleted = false;
                    await _dbContext.QualificationDetails.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                    response.Message = "Qualification Name already exist.";
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