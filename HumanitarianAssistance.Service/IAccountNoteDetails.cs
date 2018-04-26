using DataAccess.DbEntities;
using HumanitarianAssistance.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service
{
    public interface IAccountNoteDetails
    {
        Task AddNoteDetails(AccountNoteDetail obj);
    }
}
