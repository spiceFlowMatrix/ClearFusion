export interface NoteAccountBalanceModel {
  NoteId?: number;
  NoteName?: string;
  NoteHeadId?: number;
  NoteBalance: number;
  AccountBalances: [];
  _index?: number;
  _IsDeleted?: boolean;
  _IsLoading?: boolean;
  _IsError?: boolean;
}

export interface IDetailsOfNotesAccountList {
  AccountCode: string;
  AccountName: string;
  Debit: number;
  Credit: number;
}

export interface IDetailsOfNotesFinalList {
  NoteName: string;
  TotalDebits: number;
  TotalCredits: number;
  Balance: number;
  AccountSummary: IDetailsOfNotesAccountList[];
}
