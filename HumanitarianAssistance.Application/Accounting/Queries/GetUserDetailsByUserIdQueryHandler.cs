using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetUserDetailsByUserIdQueryHandler : IRequestHandler<GetUserDetailsByUserIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public GetUserDetailsByUserIdQueryHandler(HumanitarianAssistanceDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ApiResponse> Handle(GetUserDetailsByUserIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                var userdetailslist = await _dbContext.UserDetails.FirstOrDefaultAsync(x => x.AspNetUserId == request.UserId);
                UserDetailsModel obj = new UserDetailsModel();
                if (userdetailslist != null)
                {
                    var existUser = await _userManager.FindByIdAsync(request.UserId);
                    var User = _dbContext.UserDetails.AsNoTracking().FirstOrDefault(x => x.IsDeleted == false && x.AspNetUserId == request.UserId).UserID;
                    var Offices = _dbContext.UserDetailOffices.Where(x => x.IsDeleted == false && x.UserId == User).Select(x => x.OfficeId).ToList();
                    obj.UserID = userdetailslist.UserID;
                    obj.Username = userdetailslist.Username;
                    obj.FirstName = userdetailslist.FirstName;
                    obj.LastName = userdetailslist.LastName;
                    obj.UserType = userdetailslist.UserType;
                    obj.OfficeId = Offices;
                    obj.DepartmentId = userdetailslist.DepartmentId;
                    obj.Phone = existUser.PhoneNumber;
                    obj.Email = existUser.Email;
                    obj.Id = request.UserId;
                    obj.Status = userdetailslist.Status;
                }
                response.data.UserDetails = obj;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}