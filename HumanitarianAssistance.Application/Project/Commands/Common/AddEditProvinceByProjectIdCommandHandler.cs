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
    public class AddEditProvinceByProjectIdCommandHandler : IRequestHandler<AddEditProvinceByProjectIdCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public AddEditProvinceByProjectIdCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddEditProvinceByProjectIdCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (request.ProvinceId.Count != 0)
                {
                    var provinceExist = await _dbContext.ProvinceMultiSelect.Where(x => x.ProjectId == request.ProjectId && x.IsDeleted == false).ToListAsync();

                    var noExistProvinceId = provinceExist.Where(x => !request.ProvinceId.Contains(x.ProvinceId)).Select(x => x.ProvinceId).ToList();

                    if (provinceExist.Any())
                    {
                        var districtExist = await _dbContext.DistrictMultiSelect.Where(x => noExistProvinceId.Contains(x.ProvinceId) && x.IsDeleted == false).ToListAsync();

                        if (districtExist.Any())
                        {
                            _dbContext.DistrictMultiSelect.RemoveRange(districtExist);
                            await _dbContext.SaveChangesAsync();

                        }
                        _dbContext.ProvinceMultiSelect.RemoveRange(provinceExist);
                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    var districtExist = _dbContext.DistrictMultiSelect.Where(x => x.IsDeleted == false).ToList();
                    if (districtExist.Any())
                    {
                        _dbContext.DistrictMultiSelect.RemoveRange(districtExist);
                        await _dbContext.SaveChangesAsync();
                    }
                }

                List<ProvinceMultiSelect> provinceList = new List<ProvinceMultiSelect>();

                foreach (var item in request.ProvinceId)
                {
                    ProvinceMultiSelect _data = new ProvinceMultiSelect();

                    _data.ProvinceId = item;
                    _data.ProjectId = request.ProjectId;
                    _data.IsDeleted = false;
                    _data.CreatedById = request.CreatedById;
                    _data.CreatedDate = DateTime.UtcNow;

                    provinceList.Add(_data);
                    await _dbContext.SaveChangesAsync();
                }
                //Add
                _dbContext.ProvinceMultiSelect.AddRange(provinceList);
                await _dbContext.SaveChangesAsync();
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