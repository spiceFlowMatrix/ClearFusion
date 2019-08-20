using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditSelectedDistrictsCommandHandler: IRequestHandler<AddEditSelectedDistrictsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddEditSelectedDistrictsCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ApiResponse> Handle(AddEditSelectedDistrictsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (request.DistrictID != null)
                {
                    var districtExist = _dbContext.DistrictMultiSelect.Where(x => x.ProjectId == request.ProjectId && x.IsDeleted == false);

                    if (districtExist.Any())
                    {
                        _dbContext.DistrictMultiSelect.RemoveRange(districtExist);
                        await _dbContext.SaveChangesAsync();
                    }

                    List<DistrictMultiSelect> districtList = new List<DistrictMultiSelect>();

                    var selectedDistricts = await _dbContext.DistrictDetail.Where(x => request.DistrictID.Contains(x.DistrictID)).ToListAsync();

                    foreach (var item in selectedDistricts)
                    {

                        DistrictMultiSelect _data = new DistrictMultiSelect();

                        _data.DistrictID = item.DistrictID;
                        _data.ProjectId = request.ProjectId;
                        _data.IsDeleted = false;
                        _data.CreatedById = request.CreatedById;
                        _data.ProvinceId = item.ProvinceID.Value;
                        _data.CreatedDate = DateTime.UtcNow;

                        districtList.Add(_data);
                    }

                    //Add
                    _dbContext.DistrictMultiSelect.AddRange(districtList);
                    await _dbContext.SaveChangesAsync();
                }



                //response.CommonId.Id = Convert.ToInt32(_detail.SecurityConsiderationId);
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