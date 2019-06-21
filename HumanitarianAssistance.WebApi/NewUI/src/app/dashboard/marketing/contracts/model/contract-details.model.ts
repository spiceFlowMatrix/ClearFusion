export interface ContractDetailsModel {
    ContractId?: number;
    ContractCode?: string;
    ClientName?: string;
    UnitRate?: number;
    CurrencyId?: number;
    StartDate?: any;
    EndDate?: any;
    LanguageId?: number;
    MediumId?: number;
    NatureId?: number;
    TimeCategoryId?: number;
    MediaCategoryId?: number;
    QualityId?: number;
    IsCompleted?: boolean;
    ClientId?: number;
    ActivityTypeId?: number;
    UnitRateId?: number;
    IsApproved?: boolean;
    IsDeclined?: boolean;
    Activity?: string;
    Type?: string;
    Count?: number;
}

export interface TimeCategoryModel {
    TimeCategoryId?: number;
    TimeCategoryName?: string;
    CreatedById?: string;
    CreatedBy?: string;
    CreatedDate?: any;
    IsDeleted?: boolean;
    ModifiedBy?: string;
    ModifiedById?: string;
    ModifiedDate?: any;
}

export interface ActivityTypeModel {
    ActivityTypeId?: number;
    ActivityName?: string;
    CreatedById?: string;
    CreatedBy?: string;
    CreatedDate?: any;
    IsDeleted?: boolean;
    ModifiedBy?: string;
    ModifiedById?: string;
    ModifiedDate?: any;
}

export interface MediumModel {
    MediumId?: number;
    MediumName?: string;
    CreatedById?: string;
    CreatedBy?: string;
    CreatedDate?: any;
    IsDeleted?: boolean;
    ModifiedBy?: string;
    ModifiedById?: string;
    ModifiedDate?: any;
}

export interface NatureModel {
    NatureId?: number;
    NatureName?: string;
    CreatedById?: string;
    CreatedBy?: string;
    CreatedDate?: any;
    IsDeleted?: boolean;
    ModifiedBy?: string;
    ModifiedById?: string;
    ModifiedDate?: any;
}

export interface QualityModel {
    QualityId?: number;
    QualityName?: string;
    CreatedById?: string;
    CreatedBy?: string;
    CreatedDate?: any;
    IsDeleted?: boolean;
    ModifiedBy?: string;
    ModifiedById?: string;
    ModifiedDate?: any;
}

export interface MediaCategoryModel {
    MediaCategoryId?: number;
    CategoryName?: string;
    CreatedById?: string;
    CreatedBy?: string;
    CreatedDate?: any;
    IsDeleted?: boolean;
    ModifiedBy?: string;
    ModifiedById?: string;
    ModifiedDate?: any;
}

export interface LanguageModel {
    LanguageId?: number;
    LanguageName?: string;
    CreatedById?: string;
    CreatedBy?: string;
    CreatedDate?: any;
    IsDeleted?: boolean;
    ModifiedBy?: string;
    ModifiedById?: string;
    ModifiedDate?: any;
}

export interface CurrencyModel {
    CurrencyId?: number;
    CurrencyCode?: number;
    CurrencyRate?: number;
    CurrencyName?: string;
    CreatedById?: string;
    CreatedBy?: string;
    CreatedDate?: any;
    IsDeleted?: boolean;
    ModifiedBy?: string;
    ModifiedById?: string;
    ModifiedDate?: any;
    Status?: boolean;
    SalaryTaxFlag?: boolean;
}

export interface ApproveContractModel {
    ContractId?: number;
    Type?: string;
}
