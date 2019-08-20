using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetDetailOfNotesQueryHandler : IRequestHandler<GetDetailOfNotesQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetDetailOfNotesQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetDetailOfNotesQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            
            try
            {
                List<DetailOfNotesSPModel> spNotesDetail = await _dbContext.LoadStoredProc("get_detailofnote_pdf")
                                                              .WithSqlParam("to_currency_id", request.CurrencyId)
                                                              .WithSqlParam("till_date", request.TillDate.ToString())
                                                              .ExecuteStoredProc<DetailOfNotesSPModel>();

                List<DetailOfNotesSummaryModel> notesDetail = spNotesDetail.GroupBy(x => new { x.NoteId, x.NoteName })
                                               .Select(x => new DetailOfNotesSummaryModel
                                               {
                                                   NoteName = x.First().NoteName,
                                                   TotalDebits = Math.Round(x.Sum(y => y.Debit), 3),
                                                   TotalCredits = Math.Round(x.Sum(y => y.Credit), 3),
                                                   Balance = Math.Round(x.Sum(y => y.Debit) - x.Sum(y => y.Credit), 3),
                                                   AccountSummary = x.Select(s => new DetailOfNotesModel
                                                   {
                                                       AccountCode = s.AccountCode,
                                                       AccountName = s.AccountName,
                                                       Debit = Math.Round(s.Debit, 3),
                                                       Credit = Math.Round(s.Credit,3)
                                                   }).ToList()
                                               }).ToList();



                response.data.DetailsOfNotesFinalList = notesDetail;
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