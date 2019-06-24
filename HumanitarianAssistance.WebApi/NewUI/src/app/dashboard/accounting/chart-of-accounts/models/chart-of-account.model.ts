
export class ChartOfAccountModel {
    ChartOfAccountNewId?: number;
    AccountName?: string;
    ChartOfAccountNewCode?: string;
    ParentID?: number;
    AccountLevelId?: number;
    AccountHeadTypeId: number;
    AccountTypeId?: number;
    AccountFilterTypeId?: number;
    Children?: ChartOfAccountModel[] = [];
    _IsDeleted?: boolean;
    _IsLoading?: boolean;
    _IsError?: boolean;
}

export class AccountFilterTypeModel {
    AccountFilterTypeId: number;
    AccountFilterTypeName: string;
}

export class AccountTypeModel {
    AccountTypeId: number;
    AccountTypeName: string;
    AccountCategory: number;
    AccountHeadTypeId: number;
}

