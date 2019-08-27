export interface FileManagementModel {
    FileName: string;
    File: any;
    FileType: string;
    FileLength: number;
}

export interface FileModel {
    FileType: string;
    FileName: string;
    FileSize: number;
    PageId: number;
    FilePath: string;
    RecordId: number;
    DocumentTypeId?: number;
    DocumentFileId?: number;
}

export interface UploadModel {
    PageId: number;
    EntityId: number;
    File: any;
    DocumentTypeId?: number;
    DocumentFileId?: number;
}
