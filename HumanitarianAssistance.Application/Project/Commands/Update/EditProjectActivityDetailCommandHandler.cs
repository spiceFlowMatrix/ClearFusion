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

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditProjectActivityDetailCommandHandler : IRequestHandler<EditProjectActivityDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditProjectActivityDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditProjectActivityDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var projectactivityDetail = await _dbContext.ProjectActivityDetail.FirstOrDefaultAsync(x => x.ActivityId == request.ActivityId && x.IsDeleted == false);
                if (projectactivityDetail != null)
                {
                    _mapper.Map(request, projectactivityDetail);

                    projectactivityDetail.ModifiedDate = request.ModifiedDate;
                    projectactivityDetail.ModifiedById = request.ModifiedById;
                    projectactivityDetail.IsDeleted = false;

                    await _dbContext.SaveChangesAsync();

                    if (request.ProvinceId.Any())
                    {
                        var projectActivityProvinceDetailExist = _dbContext.ProjectActivityProvinceDetail.Where(x => x.ActivityId == request.ActivityId && x.IsDeleted == false);

                        if (projectActivityProvinceDetailExist.Any())
                        {
                            _dbContext.ProjectActivityProvinceDetail.RemoveRange(projectActivityProvinceDetailExist);
                            _dbContext.SaveChanges();
                        }

                        List<ProjectActivityProvinceDetail> activityProvienceList = new List<ProjectActivityProvinceDetail>();


                        var districts = _dbContext.DistrictDetail.Where(x => x.IsDeleted == false && request.ProvinceId.Contains(x.ProvinceID.Value)).ToList();

                        var selectedDistrict = districts.Where(x => request.DistrictID.Contains(x.DistrictID))
                                                                         .Select(x => new ProjectActivityProvinceDetail
                                                                         {
                                                                             DistrictID = x.DistrictID,
                                                                             ProvinceId = x.ProvinceID.Value
                                                                         }).ToList();
                        var provincesWithNoDistrict = request.ProvinceId.Where(x => !selectedDistrict.Select(y => y.ProvinceId).Contains(x)).ToList();

                        foreach (var item in provincesWithNoDistrict)
                        {
                            ProjectActivityProvinceDetail projectActivityProvince = new ProjectActivityProvinceDetail();
                            projectActivityProvince.ProvinceId = item;
                            selectedDistrict.Add(projectActivityProvince);
                        }

                        foreach (var item in selectedDistrict)
                        {

                            item.ActivityId = projectactivityDetail.ActivityId;
                            item.ModifiedById = request.ModifiedById;
                            item.ModifiedDate = request.ModifiedDate;
                            item.IsDeleted = false;
                        }
                        await _dbContext.ProjectActivityProvinceDetail.AddRangeAsync(selectedDistrict);
                        await _dbContext.SaveChangesAsync();

                    }

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.ActivityNotFound;
                }
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
