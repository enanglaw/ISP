/* export class InputData {
  public INPUT_DATE: string;
  public ZONE: string;
  public DISTRICT: string;
  public POLICE_STATION: string;
  public CATEGORY: string;
  public SUB_CATEGORY: string;
  public PRIMARY_PERSON: string;
  public SECONDARY_PERSONS: string;
  public CRIME_NUMBERS: string;
  public REQUIRES_ACTION: string;
  public MODUS_OPERANDI: string;
  public PRIORITY: string;
  public FULL_TEXT: string;
  public FILE_NAME: string;
  public FILE_PATH: string;
} */
export class InputData {
  uniquE_ID: number;
  inpuT_DATE: string;
  zone: string;
  district: string;
  policE_STATION: string;
  category: string;
  suB_CATEGORY: string;
  primarY_PERSON: string;
  secondarY_PERSONS: string;
  crimE_NUMBERS: string;
  requireS_ACTION: string;
  moduS_OPERANDI: string;
  priority: string;
  fulL_TEXT?: any;
  filE_NAME: string;
  filE_PATH: string;
}

export class AdvancedSearchInputData {
  mandatory: string;
  optional: string;
  stationId: number;
}

export class AdvanceSearchResultData {
  uniquE_ID: number;
  inpuT_DATE: string;
  zone: string;
  district: string;
  policE_STATION: string;
  category: string;
  suB_CATEGORY: string;
  primarY_PERSON: string;
  secondarY_PERSONS: string;
  crimE_NUMBERS: string;
  requireS_ACTION: string;
  moduS_OPERANDI: string;
  priority: string;
  fulL_TEXT?: any;
  filE_NAME: string;
  filE_PATH: string;
  referenceNo: string;
}

export interface DialogData {
  inputEntryId: string;
  reference: string;
}

export interface ReferenceDialogData {
  inputEntryId: string;
  searchText: string;
  stationId: number;
  reference: RefData[];
}

export interface RefData {
  inputEntryReferenceId: number;
  inputEntryId: number;
  referenceId: number;
  entryDate: Date;
  referenceNo: string;
}