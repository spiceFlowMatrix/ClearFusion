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

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditSecurityConsiderationCommandHandler: IRequestHandler<AddEditSecurityConsiderationCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddEditSecurityConsiderationCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ApiResponse> Handle(AddEditSecurityConsiderationCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (request.SecurityConsiderationId != null)
                {
                    bool securityPresent = _dbContext.SecurityConsiderationMultiSelect.Any(x => x.ProjectId == request.ProjectId && x.IsDeleted == false);

                    if (securityPresent)
                    {
                        var securityExist = _dbContext.SecurityConsiderationMultiSelect.Where(x => x.ProjectId == request.ProjectId && x.IsDeleted == false);


                        _dbContext.SecurityConsiderationMultiSelect.RemoveRange(securityExist);
                        await _dbContext.SaveChangesAsync();
                    }

                    List<SecurityConsiderationMultiSelect> securityList = new List<SecurityConsiderationMultiSelect>();

                    foreach (var item in request.SecurityConsiderationId)
                    {
                        SecurityConsiderationMultiSelect _data = new SecurityConsiderationMultiSelect();
                        _data.SecurityConsiderationId = item.Value;
                        _data.ProjectId = request.ProjectId;
                        _data.IsDeleted = false;
                        _data.CreatedById = request.CreatedById;
                        _data.CreatedDate = DateTime.UtcNow;

                        securityList.Add(_data);
                    }

                    //Add
                    _dbContext.SecurityConsiderationMultiSelect.AddRange(securityList);
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