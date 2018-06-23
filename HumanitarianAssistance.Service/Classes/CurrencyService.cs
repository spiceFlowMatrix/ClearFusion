using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes
{
    public class CurrencyService : ICurrency
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public CurrencyService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<APIResponse> AddCurrency(CurrencyModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existcurrency = await _uow.CurrencyDetailsRepository.FindAsync(c => c.CurrencyCode == model.CurrencyCode);
                if (existcurrency == null)
                {
                    CurrencyDetails obj = _mapper.Map<CurrencyDetails>(model);
                    obj.CreatedById = model.CreatedById;
                    obj.CreatedDate = DateTime.UtcNow;
                    obj.IsDeleted = false;
                    await _uow.CurrencyDetailsRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                    response.Message = StaticResource.MandateNameAlreadyExist;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EditCurrency(CurrencyModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var currencyInfo = await _uow.CurrencyDetailsRepository.FindAsync(c => c.CurrencyId == model.CurrencyId);
                currencyInfo.CurrencyCode = model.CurrencyCode;
                currencyInfo.CurrencyName = model.CurrencyName;
                currencyInfo.CurrencyRate = model.CurrencyRate;
                currencyInfo.ModifiedById = model.ModifiedById;
                currencyInfo.ModifiedDate = model.ModifiedDate;
				currencyInfo.Status = model.Status;
                await _uow.CurrencyDetailsRepository.UpdateAsyn(currencyInfo, currencyInfo.CurrencyCode);
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

        public async Task<APIResponse> GetAllCurrency()
        {
            APIResponse response = new APIResponse();
            try
            {
                var currencylist = (from c in await _uow.CurrencyDetailsRepository.GetAllAsyn() orderby c.CurrencyName ascending
                                       select new CurrencyModel
                                       {
                                           CurrencyId = c.CurrencyId,
                                           CurrencyCode = c.CurrencyCode,
                                           CurrencyName = c.CurrencyName,
                                           CurrencyRate = c.CurrencyRate,
                                           CreatedById = c.CreatedById,
                                           CreatedDate = c.CreatedDate,
                                           ModifiedById = c.ModifiedById,
                                           ModifiedDate = c.ModifiedDate,
										   Status = c.Status
                                       }).ToList();
                response.data.CurrencyList = currencylist;
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

        public async Task<APIResponse> GetCurrencyByCurrencyCode(string CurrencyCode)
        {
            APIResponse response = new APIResponse();
            try
            {
                CurrencyModel obj = new CurrencyModel();
                var currencylist = (from c in await _uow.CurrencyDetailsRepository.GetAllAsyn()
                                    where c.CurrencyCode == CurrencyCode
                                    select new CurrencyModel
                                    {
                                        CurrencyId = c.CurrencyId,
                                        CurrencyCode = c.CurrencyCode,
                                        CurrencyName = c.CurrencyName,
                                        CurrencyRate = c.CurrencyRate,
                                        CreatedById = c.CreatedById,
                                        CreatedDate = c.CreatedDate,
                                        ModifiedById = c.ModifiedById,
                                        ModifiedDate = c.ModifiedDate
                                    }).ToList();
                response.data.CurrencyList = currencylist;
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
