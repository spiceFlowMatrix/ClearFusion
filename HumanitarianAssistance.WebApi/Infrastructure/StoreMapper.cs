using AutoMapper;
using HumanitarianAssistance.Application.Store.Commands.Common;
using HumanitarianAssistance.Application.Store.Commands.Create;
using HumanitarianAssistance.Application.Store.Commands.Update;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Infrastructure
{
    public class StoreMapper: Profile
    {
        public StoreMapper()
        {
            CreateMap<UnverifyPurchaseCommand, StoreItemPurchase>().ReverseMap();
            CreateMap<VerifyPurchaseCommand, StoreItemPurchase>().ReverseMap();
            CreateMap<AddInventoryCommand, StoreInventory>().ReverseMap();
            CreateMap<AddInventoryItemsCommand, StoreInventoryItem>().ReverseMap();
            CreateMap<AddInventoryItemsTypeCommand, InventoryItemType>().ReverseMap();
            CreateMap<AddItemOrderCommand, StorePurchaseOrder>().ReverseMap();
            //CreateMap<AddItemSpecificationsDetailsCommand, List<ItemSpecificationDetails>>().ReverseMap();
            CreateMap<AddItemSpecificationsMasterCommand, ItemSpecificationMaster>().ReverseMap();
            CreateMap<AddPurchaseCommand, StoreItemPurchase>().ReverseMap();
            CreateMap<AddPurchaseUnitTypeCommand, PurchaseUnitType>().ReverseMap();
            CreateMap<AddStoreSourceCodeCommand, StoreSourceCodeDetail>().ReverseMap();
            CreateMap<EditInventoryCommand, StoreInventory>().ReverseMap();
            CreateMap<EditInventoryItemsTypeCommand, InventoryItemType>().ReverseMap();
            CreateMap<EditItemOrderCommand, StorePurchaseOrder>().ReverseMap();
            CreateMap<EditItemSpecificationsDetailsCommand, ItemSpecificationDetails>().ReverseMap();
            CreateMap<EditItemSpecificationsMasterCommand, ItemSpecificationMaster>().ReverseMap();
            CreateMap<EditPurchaseCommand, StoreItemPurchase>().ReverseMap();
            //CreateMap<List<StoreSourceCodeDetail>, List<StoreSourceCodeDetailModel>>();


        }
    }
}
