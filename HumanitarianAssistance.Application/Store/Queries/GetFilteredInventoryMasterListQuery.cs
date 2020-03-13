using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetFilteredInventoryMasterListQuery : IRequest<object>
    {
        public string FilterValue { get; set; }
        public int AssetType { get; set; }

    }
}