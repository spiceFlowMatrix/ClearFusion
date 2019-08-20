using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.Accounting.Commands.Update
{
    public class EditUsersCommandHandler : IRequestHandler<EditUsersCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public EditUsersCommandHandler(HumanitarianAssistanceDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ApiResponse> Handle(EditUsersCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    AppUser existUser = await _userManager.FindByIdAsync(request.Id);

                    existUser.FirstName = request.FirstName;
                    existUser.LastName = request.LastName;
                    existUser.Email = request.Email;
                    existUser.NormalizedEmail = request.Email;
                    existUser.NormalizedUserName = request.Email;
                    existUser.PhoneNumber = request.Phone;

                    var UserInfo = await _dbContext.UserDetails.FirstOrDefaultAsync(u => u.AspNetUserId == request.Id);

                    UserInfo.FirstName = request.FirstName;
                    UserInfo.LastName = request.LastName;
                    UserInfo.Password = request.Password;
                    UserInfo.Status = request.Status;
                    UserInfo.UserType = request.UserType;
                    UserInfo.ModifiedById = request.ModifiedById;
                    UserInfo.ModifiedDate = request.ModifiedDate;

                    var userOfficesList = await _dbContext.UserDetailOffices.Where(x => x.UserId == UserInfo.UserID).ToListAsync();

                    _dbContext.UserDetailOffices.RemoveRange(userOfficesList);
                    await _dbContext.SaveChangesAsync();

                    List<UserDetailOffices> lst = new List<UserDetailOffices>();
                    foreach (var item in request.OfficeId)
                    {
                        UserDetailOffices obj = new UserDetailOffices();
                        obj.OfficeId = item;
                        obj.UserId = UserInfo.UserID;
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = DateTime.UtcNow;
                        obj.IsDeleted = false;
                        lst.Add(obj);
                    }
                    await _dbContext.UserDetailOffices.AddRangeAsync(lst);
                    await _userManager.UpdateAsync(existUser);
                    _dbContext.UserDetails.Update(UserInfo);

                    await _dbContext.SaveChangesAsync();
                    tran.Commit();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong + ex.Message;
                }
            }
            return response;
        }

    }
}