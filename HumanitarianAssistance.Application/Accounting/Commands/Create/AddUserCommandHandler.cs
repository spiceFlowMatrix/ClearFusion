using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.Accounting.Commands.Create
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public AddUserCommandHandler(HumanitarianAssistanceDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ApiResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    AppUser newUser = new AppUser
                    {
                        UserName = request.Email,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        PhoneNumber = request.Phone
                    };

                    AppUser existUser = await _userManager.FindByNameAsync(request.Email);

                    if (existUser == null)
                    {
                        IdentityResult objNew = await _userManager.CreateAsync(newUser, request.Password);

                        UserDetails user = new UserDetails();

                        user.FirstName = request.FirstName;
                        user.LastName = request.LastName;
                        user.Password = request.Password;
                        user.Status = request.Status;
                        user.Username = request.Email;
                        user.CreatedById = request.CreatedById;
                        user.CreatedDate = request.CreatedDate;
                        user.UserType = request.UserType;
                        user.AspNetUserId = newUser.Id;

                        await _dbContext.UserDetails.AddAsync(user);
                        await _dbContext.SaveChangesAsync();

                        List<UserDetailOffices> lst = new List<UserDetailOffices>();
                        foreach (var item in request.OfficeId)
                        {
                            UserDetailOffices obj = new UserDetailOffices();
                            obj.OfficeId = item;
                            obj.UserId = user.UserID;
                            obj.CreatedById = request.CreatedById;
                            obj.CreatedDate = request.CreatedDate;
                            obj.IsDeleted = false;
                            lst.Add(obj);
                        }

                        await _dbContext.UserDetailOffices.AddRangeAsync(lst);
                        await _dbContext.SaveChangesAsync();
                        tran.Commit();

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = StaticResource.SuccessText;
                    }
                    else
                    {
                        response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                        response.Message = StaticResource.UserAlreadyExist;
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = ex.Message;
                }
            }

            return response;
        }
    }
}