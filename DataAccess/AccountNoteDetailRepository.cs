using DataAccess.DbEntities;
using HumanitarianAssistance.Entities;
using HumanitarianAssistance.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
   public class AccountNoteDetailRepository : GenericRepository<AccountNoteDetail>
    {
        AccountNoteDetailRepository(ApplicationDbContext _dbcontext): base(_dbcontext)
        {
                
        }
    }

    
}
