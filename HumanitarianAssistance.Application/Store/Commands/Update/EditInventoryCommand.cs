﻿using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditInventoryCommand : BaseModel, IRequest<ApiResponse>
    {
        public long InventoryId { get; set; }
        public string InventoryCode { get; set; }
        public string InventoryName { get; set; }
        public string InventoryDescription { get; set; }
        public int AssetType { get; set; }
        public long InventoryDebitAccount { get; set; }
        public long? InventoryCreditAccount { get; set; }
        public bool? IsTransportCategory { get; set; }
    }
}
