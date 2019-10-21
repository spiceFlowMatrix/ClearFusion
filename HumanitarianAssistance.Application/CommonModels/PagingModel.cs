namespace HumanitarianAssistance.Application.CommonModels
{
    public class PagingModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}