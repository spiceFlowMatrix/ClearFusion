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

export interface PhaseModel {
    JobPhaseId?: number;
    Phase?: string;
    CreatedById?: string;
    CreatedBy?: string;
    CreatedDate?: any;
    IsDeleted?: boolean;
    ModifiedBy?: string;
    ModifiedById?: string;
    ModifiedDate?: any;
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

    export interface UnitRateModel {
        UnitRateId?: number;
        UnitRate?: number;
        UnitRates?: number;
        CurrencyId?: number;
        MediumId?: number;
        TimeCategoryId?: number;
        NatureId?: number;
        QualityId?: number;
        ActivityTypeId?: number;
        ActivityName?: string;
        LanguageId?: number;
        MediaCategoryId?: number;
        StartDate?: any;
        EndDate?: any;
        ClientId?: number;
    }

    export interface UnitRatePaginationModel {
      pageIndex?: number;
      pageSize?: number;
    }

    export interface ProducerModel {
      ProducerId?: number;
      ProducerName?: string;
      CreatedById?: string;
      CreatedBy?: string;
      CreatedDate?: any;
      IsDeleted?: boolean;
      ModifiedBy?: string;
      ModifiedById?: string;
      ModifiedDate?: any;
      }

      
    export interface ChannelModel {
        ChannelId?: number;
        ChannelName?: string;
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
