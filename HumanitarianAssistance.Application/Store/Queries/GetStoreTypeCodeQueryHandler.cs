using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Enums;
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
        public class GetStoreTypeCodeQueryHandler : IRequestHandler<GetStoreTypeCodeQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetStoreTypeCodeQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<ApiResponse> Handle(GetStoreTypeCodeQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                string storeCode = string.Empty;

                int codeNumber = 0;

                //Getting latest created record of StoreSourceCodeDetail based on source code type selected
                StoreSourceCodeDetail storeSourceCodeDetail = await _dbContext.StoreSourceCodeDetail.OrderByDescending(x => x.SourceCodeId).FirstOrDefaultAsync(x => x.IsDeleted == false && x.CodeTypeId == request.CodeTypeId);

                if (storeSourceCodeDetail != null)
                {
                    //retreiving the number in code
                    if (int.TryParse(storeSourceCodeDetail.Code.Substring(1), out codeNumber))
                    {

                        //generating a new code for new entry in StoreSourceCodeDetail table based on source code type selected
                        switch (request.CodeTypeId)
                        {
                            case (int)SourceCode.Organizations:
                                storeCode = "O" + String.Format("{0:D5}", ++codeNumber);
                                break;
                            case (int)SourceCode.Suppliers:
                                storeCode = "S" + String.Format("{0:D5}", ++codeNumber);
                                break;
                            case (int)SourceCode.RepairShops:
                                storeCode = "R" + String.Format("{0:D5}", ++codeNumber);
                                break;
                            case (int)SourceCode.LocationsStores:
                                storeCode = "L" + String.Format("{0:D5}", ++codeNumber);
                                break;
                            case (int)SourceCode.IndividualOthers:
                                storeCode = "I" + String.Format("{0:D5}", ++codeNumber);
                                break;
                            case (int)SourceCode.Test:
                                storeCode = "T" + String.Format("{0:D5}", ++codeNumber);
                                break;
                        }

                        response.data.StoreSourceCode = storeCode;
                    }

                }
                else//record is not present
                {
                    switch (request.CodeTypeId)
                    {
                        case (int)SourceCode.Organizations:
                            storeCode = "O" + String.Format("{0:D5}", ++codeNumber);
                            break;
                        case (int)SourceCode.Suppliers:
                            storeCode = "S" + String.Format("{0:D5}", ++codeNumber);
                            break;
                        case (int)SourceCode.RepairShops:
                            storeCode = "R" + String.Format("{0:D5}", ++codeNumber);
                            break;
                        case (int)SourceCode.LocationsStores:
                            storeCode = "L" + String.Format("{0:D5}", ++codeNumber);
                            break;
                        case (int)SourceCode.IndividualOthers:
                            storeCode = "I" + String.Format("{0:D5}", ++codeNumber);
                            break;
                        case (int)SourceCode.Test:
                            storeCode = "T" + String.Format("{0:D5}", ++codeNumber);
                            break;
                    }

                    response.data.StoreSourceCode = storeCode;
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
