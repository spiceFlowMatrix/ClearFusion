﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Marketing
{
    public class ClientDetailModel : BaseModel
    {
        public long ClientId { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string FocalPoint { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PhysicialAddress { get; set; }
        public string History { get; set; }
        public string ClientBackground { get; set; }
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string type { get; set; }
        public int? Count { get; set; }
    }


    public class FilterClientModel
    {
        public long? ClientId { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public long? CategoryId { get; set; }
        public string Position { get; set; }
    }

    public class ClientPaginationModel
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
