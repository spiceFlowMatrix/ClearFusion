using System.Collections.Generic;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Commands.Common;
using HumanitarianAssistance.Application.Accounting.Commands.Create;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Domain.Entities.Accounting;

namespace HumanitarianAssistance.Application.CommonServicesInterface
{
    public interface IAccountingServices
    {
        Task<VoucherDetailEntityModel> AddVoucherDetail(AddVoucherDetailCommand request);
        bool CheckExchangeRateIsPresent(List<int> currencyList, List<ExchangeRateDetail> exchangeRates);
        Task<bool> DeleteVoucher(long voucherId);
        bool AddEditTransactionList(AddEditTransactionListCommand request);
        Task<bool> DeleteTransaction(long voucherId, string userId);
        Task<bool> ReverseEmployeeSalaryVoucher(ReverseEmployeeSalaryVoucherCommand request);
        Task<bool> AddRole(string RoleName);
    }
}