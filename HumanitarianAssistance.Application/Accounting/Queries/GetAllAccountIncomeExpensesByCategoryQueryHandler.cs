using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllAccountIncomeExpensesByCategoryQueryHandler : IRequestHandler<GetAllAccountIncomeExpensesByCategoryQuery, ApiResponse>
    {
        ApiResponse response = new ApiResponse();
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IAccountBalanceServices _iAccountBalanceServices;

        public GetAllAccountIncomeExpensesByCategoryQueryHandler(HumanitarianAssistanceDbContext dbContext, IAccountBalanceServices iAccountBalanceServices)
        {
            _dbContext= dbContext;
            _iAccountBalanceServices = iAccountBalanceServices;

        }
        public async Task<ApiResponse> Handle(GetAllAccountIncomeExpensesByCategoryQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var inputLevelList = await _dbContext.ChartOfAccountNew
                    .Where(x => x.AccountHeadTypeId == request.id && x.AccountLevelId == (int)AccountLevels.InputLevel)
                    .Include(x => x.AccountType)
                    .ToListAsync();


                if (inputLevelList.Any(x => x.AccountTypeId == null))
                    throw new Exception("Some accounts do not have notes assigned to them!");

                var accountBalances = await _iAccountBalanceServices.GetAccountBalances(inputLevelList, request.currency, request.asOfDate, request.upToDate);

                var notes = inputLevelList.Select(x => x.AccountType).Distinct().ToList();
                List<NoteAccountBalancesModel> noteAccountBalances = new List<NoteAccountBalancesModel>();

                foreach (var note in notes)
                {
                    var currNoteBalances = accountBalances.Where(x => x.Key.AccountTypeId == note.AccountTypeId).ToDictionary(x => x.Key, x => x.Value);

                    var vmNoteBalances = _iAccountBalanceServices.GenerateBalanceViewModels(currNoteBalances);

                    var currNoteAccountBalances = new NoteAccountBalancesModel();

                    currNoteAccountBalances.NoteId = note.AccountTypeId;
                    currNoteAccountBalances.NoteName = note.AccountTypeName;
                    currNoteAccountBalances.AccountBalances = vmNoteBalances;
                    currNoteAccountBalances.NoteHeadId = note.AccountHeadTypeId;
                    noteAccountBalances.Add(currNoteAccountBalances);
                }

                response.data.NoteAccountBalances = noteAccountBalances;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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