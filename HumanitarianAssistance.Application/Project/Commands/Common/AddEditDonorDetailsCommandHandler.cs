using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditDonorDetailsCommandHandler: IRequestHandler<AddEditDonorDetailsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddEditDonorDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper= mapper;
        }

        public async Task<ApiResponse> Handle(AddEditDonorDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (request.DonorId == 0)
                {
                    DonorDetail obj = _mapper.Map<AddEditDonorDetailsCommand, DonorDetail>(request);
                    obj.Name = request.Name;
                    obj.ContactPerson = request.ContactPerson;
                    obj.ContactDesignation = request.ContactDesignation;
                    obj.ContactPersonEmail = request.ContactPersonEmail;
                    obj.ContactPersonCell = request.ContactPersonCell;
                    obj.IsDeleted = false;
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = DateTime.UtcNow;
                    
                    await _dbContext.DonorDetail.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();

                    response.data.DonorDetailById = obj;
                    response.data.TotalCount = await _dbContext.DonorDetail.Where(x => x.IsDeleted == false).AsNoTracking().CountAsync();
                }
                else
                {
                    var existRecord = await _dbContext.DonorDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.DonorId == request.DonorId);
                    if (existRecord != null)
                    {
                        existRecord.Name = request.Name;
                        existRecord.ContactPerson = request.ContactPerson;
                        existRecord.ContactDesignation = request.ContactDesignation;
                        existRecord.ContactPersonEmail = request.ContactPersonEmail;
                        existRecord.ContactPersonCell = request.ContactPersonCell;
                        existRecord.ModifiedById = request.ModifiedById;
                        existRecord.ModifiedDate = DateTime.UtcNow;
                        _dbContext.SaveChanges();
                        response.data.DonorDetailById = existRecord;
                        response.Message = "Success";
                    }
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.CommonId.Id = Convert.ToInt32(request.DonorId);
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