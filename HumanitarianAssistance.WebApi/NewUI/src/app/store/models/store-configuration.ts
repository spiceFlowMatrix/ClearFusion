export interface UnitType {
    UnitTypeId?: number;
    UnitTypeName?: string;
}
export interface SourceCodeType {
    CodeTypeId?: number;
    CodeTypeName?: string;
}
export interface SourceCode {
    SourceCodeId?: number;
    Code?: string;
    Description?: string;
    Address?: string;
    Phone?: string;
    Fax?: string;
    EmailAddress?: string;
    Guarantor?: string;
    CodeTypeId?: number;
}
export interface InventoryModel {
    Id?: number;
    InventoryId?: number;
    ItemGroupId?: number;
    Code?: string;
    Name?: string;
    Description?: string;
    InventoryDebitAccount?: string;
    InventoryCreditAccount?: string;
    AssetType?: number;
    addText?: string;
    editText?: string;
    IsTransportCategory?: boolean;
    ItemTypeCategory?: number;
    // addText:"add item category";
    // editText:"edit master inventory";
    children?: ItemGroup[];
}

export interface ItemGroup {
    InventoryId?: number;
    children?: InventoryItem[];
    Code?: string;
    Id?: number;
    Name?: string;
    addText?: string;
    editText?: string;
    Description?: string;
    ItemTypeCategory?: number;
    // addText:"add item";
    // editText:"edit item category";

}
export interface InventoryItem {
    Code?: string;
    ItemGroupId?: number;
    Id?: number;
    InventoryId?: number;
    Name?: string;
    addText?: string;
    editText?: string;
    Description?: string;
    ItemTypeCategory?: number;
    // addText:"";
    // editText:"edit item ";
}


export class MasterInventoryModel {
    InventoryId?: any;
    InventoryCode?: any;
    InventoryName?: any;
    InventoryDescription?: any;
    InventoryDebitAccount?: any;
    InventoryCreditAccount?: any;
    IsTransportCategory?: boolean;
    AssetType?: any;
}

export class MasterItemGroupModel {
    ItemGroupId?: any;
    ItemGroupName?: any;
    ItemGroupCode?: any;
    Description?: any;
    InventoryId?: any;
    IsTransportCategory?: boolean;
    ItemTypeCategory?: number;
}

export class MasterInventoryItemModel {
    ItemId?: any;
    ItemName?: any;
    ItemCode?: any;
    Description?: any;
    ItemType?: any;
    ItemGroupId?: any;
    ItemInventory?: any;
    AssetType?:number;
    ItemTypeCategory?: number;
    // Voucher: any;
}