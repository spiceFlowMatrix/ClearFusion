using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllStoreSourceCodeQueryHandler : IRequestHandler<GetAllStoreSourceCodeQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public GetAllStoreSourceCodeQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(GetAllStoreSourceCodeQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            List<StoreSourceCodeDetailModel> obj = new List<StoreSourceCodeDetailModel>();
            try
            {
                List<StoreSourceCodeDetail> StoreSourceCodeDetailList = new List<StoreSourceCodeDetail>();

                //Get Store Source Code Detail based on source code type selected
                if (request.typeId != null)
                {
                    obj= await _dbContext.StoreSourceCodeDetail.Where(x => x.IsDeleted == false && x.CodeTypeId == request.typeId).Select(y => new StoreSourceCodeDetailModel
                    {
                        Address = y.Address,
                        EmailAddress = y.EmailAddress,
                        SourceCodeId = y.SourceCodeId,
                        CodeTypeId = y.CodeTypeId,
                        Code = y.Code,
                        Description = y.Description,
                        Phone = y.Phone,
                        Fax = y.Fax,
                        Guarantor = y.Guarantor
                    }).ToListAsync();
                }
                else //Source Code Type is empty so Get all Store Source Code Detail
                {
                    obj = await _dbContext.StoreSourceCodeDetail.Where(x => x.IsDeleted == false).Select(y => new StoreSourceCodeDetailModel
                    {
                        Address = y.Address,
                        EmailAddress = y.EmailAddress,
                        SourceCodeId=y.SourceCodeId,
                        CodeTypeId=y.CodeTypeId,
                        Code=y.Code,
                        Description=y.Description,
                        Phone=y.Phone,
                        Fax=y.Fax,
                        Guarantor=y.Guarantor
                    }).ToListAsync();
                }

                //List<StoreSourceCodeDetailModel> obj = _mapper.Map<List <StoreSourceCodeDetailModel>>(StoreSourceCodeDetailList);
                response.data.SourceCodeDatalist = obj;
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
