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
    public class AddEditCountryByProjectIdCommandHandler: IRequestHandler<AddEditCountryByProjectIdCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public AddEditCountryByProjectIdCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddEditCountryByProjectIdCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (request.CountryId.Count != 0)
                {
                    var countryExist = await _dbContext.CountryMultiSelectDetails.Where(x => x.ProjectId == request.ProjectId && x.IsDeleted == false).ToListAsync();

                    if (countryExist.Any())
                    {
                        var provinceExist = _dbContext.ProvinceMultiSelect.Where(x => x.IsDeleted == false).ToList();
                        if (provinceExist.Any())
                        {
                            _dbContext.ProvinceMultiSelect.RemoveRange(provinceExist);
                            await _dbContext.SaveChangesAsync();
                        }
                        var district = _dbContext.DistrictMultiSelect.Where(x => x.IsDeleted == false).ToList();
                     
                        if (district.Any())
                        {
                            _dbContext.DistrictMultiSelect.RemoveRange(district);
                            await _dbContext.SaveChangesAsync();
                        }

                        _dbContext.CountryMultiSelectDetails.RemoveRange(countryExist);
                        await _dbContext.SaveChangesAsync();
                    }

                    List<CountryMultiSelectDetails> countryList = new List<CountryMultiSelectDetails>();

                    foreach (var item in request.CountryId)
                    {
                        CountryMultiSelectDetails _data = new CountryMultiSelectDetails();

                        _data.CountryId = item;
                        _data.ProjectId = request.ProjectId;
                        _data.IsDeleted = false;
                        _data.CreatedById = request.CreatedById;
                        _data.CreatedDate = DateTime.UtcNow;

                        countryList.Add(_data);
                    }

                    //Add
                    _dbContext.CountryMultiSelectDetails.AddRange(countryList);
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