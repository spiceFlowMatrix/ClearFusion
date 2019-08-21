using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{

    public class GetAllUserDetailsQueryHandler : IRequestHandler<GetAllUserDetailsQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllUserDetailsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllUserDetailsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                List<UserDetailsModel> list = new List<UserDetailsModel>();
                var userdetailslist = await _dbContext.UserDetails.Where(x => x.IsDeleted == false).ToListAsync();
                foreach (var item in userdetailslist)
                {
                    UserDetailsModel obj = new UserDetailsModel();
                    var officeList = await _dbContext.UserDetailOffices.Where(x => x.UserId == item.UserID).ToListAsync();
                    obj.UserID = item.UserID;
                    obj.Id = item.AspNetUserId;
                    obj.Username = item.Username;
                    obj.FirstName = item.FirstName;
                    obj.LastName = item.LastName;
                    obj.UserType = item.UserType;
                    obj.Status = item.Status;
                    List<int> UserOfficesList = new List<int>();
                    foreach (var element in officeList)
                    {
                        UserOfficesList.Add(element.OfficeId);
                    }
                    obj.OfficeId = UserOfficesList;
                    list.Add(obj);
                }
                response.data.UserDetailsList = list;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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