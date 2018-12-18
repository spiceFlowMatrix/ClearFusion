using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using Microsoft.AspNetCore.Identity;

namespace HumanitarianAssistance.Service.Classes.AccountingNew
{
    public class FinancialReportService : IFinancialReportService
    {
        private IUnitOfWork _uow;
        private UserManager<AppUser> _userManager;
        private IMapper _mapper;

        public FinancialReportService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<APIResponse> GetNoteBalancesByHeadType(int headType)
        {
            APIResponse response = new APIResponse();

            // TODO: get all input-level accounts that match the headType
            try
            {
                var subLevelList =
                    await _uow.ChartOfAccountNewRepository.FindAllAsync(x =>
                        x.AccountLevelId == (int) AccountLevels.SubLevel);
                response.data.SubLevelAccountList = subLevelList.ToList();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            // TODO: fetch all vouchers that contain transactions towards accounts whose head type matches this headType
            // TODO: get exchange rate for each transaction. Use the currencyId from each transaction's voucher, and the toCurrencyId to get  

            return response;
        }

        public async Task<APIResponse> GetNoteBalanceById(int noteType)
        {
            APIResponse response = new APIResponse();

            return response;
        }
    }
}
