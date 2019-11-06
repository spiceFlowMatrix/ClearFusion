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
    Code?: string;
    Name?: string;
    Description?: string;
    InventoryDebitAccount?: string;
    InventoryCreditAccount?: string;
    AssetType?: number;
    addText?: string;
    editText?: string;
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
    // addText:"add item";
    // editText:"edit item category";

}
export interface InventoryItem {
    Code?: string;
    ItemGroupId?: number;
    Id?: number;
    ItemInventory?: number;
    Name?: string;
    addText?: string;
    editText?: string;
    // addText:"";
    // editText:"edit item ";
} 