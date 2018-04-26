using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service
{
    public class AccountNoteService : IAccountNoteDetails
    {
        IUnitOfWork uow;
        public async Task AddNoteDetails(AccountNoteDetail obj)
        {

            await uow.AccountNoteDetailRepository.AddAsyn(obj);
        }
        public AccountNoteService(IUnitOfWork _uow)
        {
            uow = _uow;
            
        }

       
    }
}
