using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HumanitarianAssistance.Service.Classes
{
    public class ProfessionService : IProfession
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public ProfessionService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<APIResponse> AddProfession(ProfessionModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                ProfessionDetails obj = _mapper.Map<ProfessionDetails>(model);
                await _uow.ProfessionDetailsRepository.AddAsyn(obj);
                await _uow.SaveAsync();
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

        public async Task<APIResponse> EditProfession(ProfessionModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var professioninfo = await _uow.ProfessionDetailsRepository.FindAsync(x => x.ProfessionId == model.ProfessionId && x.IsDeleted == false);
                if (professioninfo != null)
                {
                    professioninfo.ProfessionName = model.ProfessionName;
                    professioninfo.ModifiedById = model.ModifiedById;
                    professioninfo.ModifiedDate = model.ModifiedDate;
                    await _uow.ProfessionDetailsRepository.UpdateAsyn(professioninfo);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Sucess";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllProfession()
        {
            APIResponse response = new APIResponse();
            try
            {
                var professionlist = (from c in await _uow.ProfessionDetailsRepository.GetAllAsyn()
                                    where c.IsDeleted == false
                                    select new ProfessionModel
                                    {
                                        ProfessionId = c.ProfessionId,
                                        ProfessionName = c.ProfessionName
                                    }).ToList();
                response.data.ProfessionList = professionlist.OrderBy(x=> x.ProfessionId).ToList();
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
