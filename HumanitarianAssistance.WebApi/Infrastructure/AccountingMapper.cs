using AutoMapper;
using HumanitarianAssistance.Application.Accounting.Commands.Create;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Domain.Entities.Accounting;

namespace HumanitarianAssistance.WebApi.Infrastructure
{
    public class AccountingMapper : Profile
    {
        public AccountingMapper()
        {
            // Mapping
            CreateMap<VoucherDetail, VoucherDetailEntityModel>().ReverseMap();
            CreateMap<VoucherDetail, VoucherDetailModel>().ReverseMap();
            CreateMap<VoucherDetail, AddVoucherDetailCommand>().ReverseMap();

            
        }
    }
}
