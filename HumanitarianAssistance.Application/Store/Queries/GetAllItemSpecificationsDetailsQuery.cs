﻿using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllItemSpecificationsDetailsQuery : IRequest<ApiResponse>
    {
        public long ItemId { get; set; }
        public int ItemTypeId { get; set; }
        public int OfficeId { get; set; }  
    }
}
