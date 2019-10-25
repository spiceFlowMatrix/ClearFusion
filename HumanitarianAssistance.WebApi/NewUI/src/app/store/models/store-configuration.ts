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