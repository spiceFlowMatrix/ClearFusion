using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Common
{
    public class PaginationModel
    {
        public int Take { get; set; }
        public int Skip { get; set; }
    }

    public class PagingModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
