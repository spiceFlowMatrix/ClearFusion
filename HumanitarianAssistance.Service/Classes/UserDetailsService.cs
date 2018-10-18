using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Entities;

namespace HumanitarianAssistance.Service.Classes
{
	public class UserDetailsService : IUserDetails
	{
		IUnitOfWork _uow;
		IMapper _mapper;
		UserManager<AppUser> _userManager;
		RoleManager<IdentityRole> _roleManager;
		public UserDetailsService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			this._uow = uow;
			this._mapper = mapper;
			this._userManager = userManager;
			this._roleManager = roleManager;
		}
		public async Task<APIResponse> GetDepartmentsByOfficeCodeAsyn(string officeCode)
		{
			APIResponse response = new APIResponse();
			try
			{
				var list = await _uow.DepartmentRepository.FindAllAsync(x => x.OfficeCode == officeCode && x.IsDeleted == false);
				var departmentlist = list.Where(x => x.IsDeleted == false).
					Select(x => new DepartmentModel
					{
						OfficeCode = x.OfficeCode,
						DepartmentId = x.DepartmentId,
						DepartmentName = x.DepartmentName,
					}).ToList();
				response.data.Departments = departmentlist;
				response.StatusCode = StaticResource.successStatusCode;
				response.Message = "Success";
			}
			catch (Exception)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong;
			}
			return response;
		}

		public async Task<APIResponse> AddUser(UserDetailsModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var newUser = new AppUser { UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, PhoneNumber = model.Phone };

				var existUser = await _userManager.FindByNameAsync(model.Email);

				if (existUser == null)
				{
					var objNew = await _userManager.CreateAsync(newUser, model.Password);
					//await _userManager.AddClaimAsync(newUser, new Claim("OfficeCode", model.OfficeCode));
					//await _userManager.AddClaimAsync(newUser, new Claim("DepartmentId", model.DepartmentId.ToString()));

					var id = newUser.Id;
					//UserDetails user = _mapper.Map<UserDetails>(model);
					UserDetails user = new UserDetails();
					user.FirstName = model.FirstName;
					user.LastName = model.LastName;
					user.Password = model.Password;
					user.Status = model.Status;
					user.Username = model.Email;
					user.CreatedById = model.CreatedById;
					user.CreatedDate = model.CreatedDate;
					user.UserType = model.UserType;
					user.AspNetUserId = id;
					await _uow.UserDetailsRepository.AddAsyn(user);

					List<UserDetailOffices> lst = new List<UserDetailOffices>();
					foreach (var item in model.OfficeId)
					{
						UserDetailOffices obj = new UserDetailOffices();
						obj.OfficeId = item;
						obj.UserId = user.UserID;
						obj.CreatedById = model.CreatedById;
						obj.CreatedDate = model.CreatedDate;
						obj.IsDeleted = false;
						lst.Add(obj);
					}
					await _uow.GetDbContext().UserDetailOffices.AddRangeAsync(lst);

					await _uow.SaveAsync();
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
				else
				{
					response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
					response.Message = StaticResource.UserAlreadyExist;
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong + ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> EditUser(UserDetailsModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				//using (var context = _uow.GetDbContext())
				//{
				//	using (var transaction = context.Database.BeginTransaction())
				//	{
						var existUser = await _userManager.FindByIdAsync(model.Id);
						//string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);
						//var passchangeResult = await _userManager.ResetPasswordAsync(existUser, token, model.Password);
						existUser.FirstName = model.FirstName;
						existUser.LastName = model.LastName;
						existUser.Email = model.Email;
						existUser.NormalizedEmail = model.Email;
						existUser.NormalizedUserName = model.Email;
						existUser.PhoneNumber = model.Phone;						

						var UserInfo = await _uow.UserDetailsRepository.FindAsync(u => u.AspNetUserId == model.Id);
						UserInfo.FirstName = model.FirstName;
						UserInfo.LastName = model.LastName;
						//UserInfo.OfficeId = model.OfficeId;
						//UserInfo.DepartmentId = model.DepartmentId;
						UserInfo.Password = model.Password;
						UserInfo.Status = model.Status;
						UserInfo.UserType = model.UserType;
						UserInfo.ModifiedById = model.ModifiedById;
						UserInfo.ModifiedDate = model.ModifiedDate;						

						var userOfficesList = await _uow.UserOfficesRepository.FindAllAsync(x=>x.UserId == UserInfo.UserID);
						_uow.GetDbContext().UserDetailOffices.RemoveRange(userOfficesList);

						List<UserDetailOffices> lst = new List<UserDetailOffices>();
						foreach (var item in model.OfficeId)
						{
							UserDetailOffices obj = new UserDetailOffices();
							obj.OfficeId = item;
							obj.UserId = UserInfo.UserID;
							obj.CreatedById = model.CreatedById;
							obj.CreatedDate = DateTime.Now;
							obj.IsDeleted = false;
							lst.Add(obj);
						}
						await _uow.GetDbContext().UserDetailOffices.AddRangeAsync(lst);
						await _userManager.UpdateAsync(existUser);
						await _uow.UserDetailsRepository.UpdateAsyn(UserInfo);
						await _uow.SaveAsync();
						//transaction.Commit();
						response.StatusCode = StaticResource.successStatusCode;
						response.Message = "Success";
				//	}
				//}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong + ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> GetAllUserDetails()
		{
			APIResponse response = new APIResponse();
			try
			{
				//var userdetailslist = (from ud in await _uow.UserDetailsRepository.GetAllAsyn()
				//					   join od in await _uow.OfficeDetailRepository.GetAllAsyn() on ud.OfficeId equals od.OfficeId
				//					   join d in await _uow.DepartmentRepository.GetAllAsyn() on ud.DepartmentId equals d.DepartmentId									   
				//					   where ud.IsDeleted == false
				//					   select new UserDetailsModel
				//					   {
				//						   UserID = ud.UserID,
				//						   Username = ud.Username,
				//						   FirstName = ud.FirstName,
				//						   LastName = ud.LastName,
				//						   UserType = ud.UserType,
				//						   Status = ud.Status,
				//						   //OfficeId = od.OfficeId,
				//						   OfficeName = od.OfficeName,
				//						   DepartmentId = d.DepartmentId,
				//						   DepartmentName = d.DepartmentName,
				//						   Id = ud.AspNetUserId,
				//						   UserOfficesList = _uow.UserOfficesRepository.FindAll(x=>x.UserId == ud.UserID && x.IsDeleted == false).Select(x=> new UserOfficesModel {
				//							   OfficeId = x.OfficeId										   
				//						   }).ToList()
				//					   }).ToList();

				List<UserDetailsModel> list = new List<UserDetailsModel>();
				var userdetailslist = await _uow.UserDetailsRepository.FindAllAsync(x => x.IsDeleted == false);
				foreach (var item in userdetailslist)
				{
					UserDetailsModel obj = new UserDetailsModel();
					var officeList = await _uow.UserOfficesRepository.FindAllAsync(x=>x.UserId == item.UserID);
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
				response.Message = "Success";
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong + ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> GetUserDetailsByUserId(string UserId)
		{
			APIResponse response = new APIResponse();
			try
			{

				//var userdetailslist = (from ud in await _uow.UserDetailsRepository.GetAllAsyn()
				//					   join od in await _uow.OfficeDetailRepository.GetAllAsyn() on ud.OfficeId equals od.OfficeId
				//					   join d in await _uow.DepartmentRepository.GetAllAsyn() on ud.DepartmentId equals d.DepartmentId
				//					   where ud.AspNetUserId == UserId
				//					   select new UserDetailsModel
				//					   {
				//						   UserID = ud.UserID,
				//						   Username = ud.Username,
				//						   FirstName = ud.FirstName,
				//						   LastName = ud.LastName,
				//						   UserType = ud.UserType,
				//						   Password = ud.Password,
				//						   Status = ud.Status,
				//						   OfficeId = od.OfficeId.,
				//						   OfficeName = od.OfficeName,
				//						   DepartmentId = d.DepartmentId,
				//						   DepartmentName = d.DepartmentName,
				//						   Id = ud.AspNetUserId
				//					   }).ToList();

				//var existUser = await _userManager.FindByIdAsync(UserId);
				//userdetailslist[0].Email = existUser.Email;
				//userdetailslist[0].Phone = existUser.PhoneNumber;
				//response.data.UserDetailsList = userdetailslist.ToList();

				var userdetailslist = await _uow.UserDetailsRepository.FindAsync(x=>x.AspNetUserId == UserId);
				UserDetailsModel obj = new UserDetailsModel();
				if (userdetailslist != null)
				{
					var existUser = await _userManager.FindByIdAsync(UserId);
					var User = _uow.GetDbContext().UserDetails.AsNoTracking().Where(x => x.IsDeleted == false && x.AspNetUserId == UserId).Select(x => x.UserID).FirstOrDefault();
					var Offices = _uow.GetDbContext().UserDetailOffices.Where(x => x.IsDeleted == false && x.UserId == User).Select(x => x.OfficeId).ToList();
					obj.UserID = userdetailslist.UserID;
					obj.Username = userdetailslist.Username;
					obj.FirstName = userdetailslist.FirstName;
					obj.LastName = userdetailslist.LastName;
					obj.UserType = userdetailslist.UserType;
					obj.OfficeId = Offices;
					obj.DepartmentId = userdetailslist.DepartmentId;
					obj.Phone = existUser.PhoneNumber;
					obj.Email = existUser.Email;
					obj.Id = UserId;
					obj.Status = userdetailslist.Status;
				}
				response.data.UserDetails = obj;
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

		public async Task<APIResponse> ChangePassword(ChangePasswordModel model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var existUser = await _userManager.FindByNameAsync(model.Username);
				var Result = await _userManager.ChangePasswordAsync(existUser, model.CurrentPassword, model.NewPassword);
				if (Result.Succeeded == true)
				{
					//var UserInfo = await _uow.UserDetailsRepository.GetAsync(model.UserId);
					//UserInfo.Password = model.NewPassword;
					//UserInfo.ModifiedById = model.ModifiedById;
					//UserInfo.ModifiedDate = model.ModifiedDate;
					//await _uow.UserDetailsRepository.UpdateAsyn(UserInfo, model.UserId);
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong + ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> ResetPassword(ResetPassword model)
		{
			APIResponse response = new APIResponse();
			try
			{
				var existUser = await _userManager.FindByNameAsync(model.Username);
				string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);
				var passchangeResult = await _userManager.ResetPasswordAsync(existUser, token, model.NewPassword);
				if (passchangeResult.Succeeded == true)
				{
					//var UserInfo = await _uow.UserDetailsRepository.GetAsync(model.UserId);
					//UserInfo.Password = model.NewPassword;
					//UserInfo.ModifiedById = model.ModifiedById;
					//UserInfo.ModifiedDate = model.ModifiedDate;
					//await _uow.UserDetailsRepository.UpdateAsyn(UserInfo, model.UserId);
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong + ex.Message;
			}
			return response;
		}

		public async Task<APIResponse> GetUserRolesByUserId(string userid)
		{
			APIResponse response = new APIResponse();
			try
			{
				List<Roles> currentRoles = new List<Roles>();
				var user = await _userManager.FindByIdAsync(userid);
				await Task.Run(async () =>
				{
					var roles = _roleManager.Roles.Select(x => new Roles
					{
						RoleName = x.Name,
						Id = x.Id
					}).ToList();

					foreach (var rol in roles)
					{
						var isExist = _userManager.IsInRoleAsync(user, rol.RoleName).Result;
						if (isExist)
						{
							currentRoles.Add(rol);
						}
					}

					foreach (var rol in currentRoles)
					{



						var permissionlist = (from pir in await _uow.PermissionsInRolesRepository.GetAllAsyn()
											  join p in await _uow.PermissionRepository.GetAllAsyn() on pir.PermissionId equals p.Id
											  where pir.RoleId == rol.Id && pir.IsGrant == true
											  select new PermissionsModel
											  {
												  Id = pir.PermissionId,
												  Name = p.Name
											  }).ToList();

						rol.PermissionsList = permissionlist;
					}

					response.data.RoleList = currentRoles;
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Role List";
				});
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
