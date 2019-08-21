using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProjectActivityDetailCommandHandler : IRequestHandler<AddProjectActivityDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddProjectActivityDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddProjectActivityDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectActivityDetail obj = _mapper.Map<AddProjectActivityDetailCommand, ProjectActivityDetail>(request);
                obj.CreatedDate = DateTime.UtcNow;
                obj.IsDeleted = false;
                obj.CreatedById = request.CreatedById;
                await _dbContext.ProjectActivityDetail.AddAsync(obj);
                await _dbContext.SaveChangesAsync();

                if (request.ProvinceId != null)
                {
                    List<ProjectActivityProvinceDetail> activityProvienceList = new List<ProjectActivityProvinceDetail>();

                    var districts = _dbContext.DistrictDetail.Where(x => x.IsDeleted == false && request.ProvinceId.Contains(x.ProvinceID.Value)).ToList();

                    var selectedDistrict = districts.Where(x => request.DistrictID.Contains(x.DistrictID))
                                                                     .Select(x => new ProjectActivityProvinceDetail
                                                                     {
                                                                         DistrictID = x.DistrictID,
                                                                         ProvinceId = x.ProvinceID.Value
                                                                     }).ToList();

                    // var provincesWithNoDistrict= selectedDistrict.Where(x => !model.ProvinceId.Contains(x.ProvinceId));

                    var provincesWithNoDistrict = request.ProvinceId.Where(x => !selectedDistrict.Select(y => y.ProvinceId).Contains(x)).ToList();

                    foreach (var item in provincesWithNoDistrict)
                    {
                        ProjectActivityProvinceDetail projectActivityProvinceDetail = new ProjectActivityProvinceDetail();
                        projectActivityProvinceDetail.ProvinceId = item;
                        selectedDistrict.Add(projectActivityProvinceDetail);
                    }

                    foreach (var item in selectedDistrict)
                    {

                        item.ActivityId = obj.ActivityId;
                        item.CreatedById = request.CreatedById;
                        item.CreatedDate = request.CreatedDate;
                        item.IsDeleted = false;
                    }
                    // await _uow.ProjectActivityProvinceDetailRepository.A(obj);
                    await _dbContext.ProjectActivityProvinceDetail.AddRangeAsync(selectedDistrict);
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
