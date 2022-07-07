import { RefData } from './../../search/input-data.model';
export interface SubCategoryDTO {
  subCategoryId: number;
  subCategoryName: string;
}

export interface CategoryDTO {
  categoryId: number;
  categoryName: string;
  subCategory: SubCategoryDTO[];
}

export interface StationDTO {
  stationid: number;
  stationName: string;
}

export interface DistrictDTO {
  districtId: number;
  district: string;
  station: StationDTO[];
}

export interface ZoneDTO {
  zoneId: number;
  zone: string;
  district: DistrictDTO[];
}

export interface InputEntry1DTO {
  inputEntryId: number;
  entryDate: Date;
  categoryId: number;
  subCategoryId: number;
  zoneId: number;
  districtId: number;
  stationId: number;
  inputData: string;
  inputDocument: File;
  referenceNo: string;
  referenceDocument: File;
  referenceIds: RefData[];
  /*   offenderName: string;
  offenderParentName: string;
  primaryAddress: string;
  historySheetNo: string;
  currentActivity: string;
  offenderStatusId: number;
  modusOperandi: string;
  gangId: number;
  photoPath: string;
  photoUrl: string;
  departmentalAlertEnglish: string;
  departmentalAlertTamil: string;
  createdBy: number;
  createdOn: Date;
  updatedBy: number;
  updatedOn: Date;
  category: Category;
  district: District;
  gang: Gang;
  offenderStatus: OffenderStatus;
  station: Station;
  subCategory: SubCategory;
  zone: Zone2;
  inputEntryAddress: InputEntryAddress[];
  inputEntryAliasName: InputEntryAliasName[];
  inputEntryCaseHistory: InputEntryCaseHistory[];
  inputEntryMedia: InputEntryMedia[];
  inputEntryOffenderType: InputEntryOffenderType[];
  inputEntryRivalGang: InputEntryRivalGang2[];
 */
}

export interface InputEntry1EditDTO extends InputEntry1DTO {
  isDocumentDeleted: boolean;
  isReferenceDeleted: boolean;
  deletedReferences: number[];
}

export interface InputEntryStage2CreationDTO {
  inputEntryId: number;
  personId: number;
  secondaryPersons: number[];
  repeatOffence: boolean;
  departmentalAlertEnglish: string;
  deptAlertEnglishDocument: File;
  departmentalAlertTamil: string;
  deptAlertTamilDocument: File;
  inputEntryMedia: InputEntryMedia[];
  /* entryDate: Date;
  categoryId: number;
  subCategoryId: number;
  zoneId: number;
  districtId: number;
  stationId: number;
  inputData: string;
  inputDocument: File;
  offenderName: string;
  offenderParentName: string;
  primaryAddress: string;
  inputEntryOffenderType: number[];
  historySheetNo: string;
  currentActivity: string;
  offenderStatusId: number;
  modusOperandi: string;
  gangId: number;
  photoDocument: File;
  inputEntryAddress: InputEntryAddress[];
  inputEntryAliasName: InputEntryAliasName[];
  inputEntryCaseHistory: InputEntryCaseHistory[];
  inputEntryRivalGang: number[];
  inputEntryOffenderType: InputEntryOffenderType[]; */
}

export interface InputEntryAliasName {
  aliasName: string;
}

export interface InputEntryAddress {
  addressLabel: string;
  addressText: string;
}

export interface InputEntryCaseHistory {
  caseId: number;
  caseStatusId: number;
}

export interface InputEntryMedia {
  mediaLabel: string;
  media: File;
}

export interface InputEntryStage1ViewDTO {
  inputEntryId: number;
  entryDate: Date;
  categoryId: number;
  categoryName: string;
  subCategoryId: number;
  subCategoryName: string;
  zoneId: number;
  zone: string;
  districtId: number;
  district: string;
  stationId: number;
  stationName: string;
  inputData: string;
  inputDocumentUrl: string;
  inputDocumentPath: string;
  referenceNo: string;
  referenceDocumentPath: string;
  referenceDocumentUrl: string;
  inputReference: string[];
  referenceIds: RefData[];
}

export interface InputEntryStage1EditDTO {
  inputEntry: InputEntryStage1ViewDTO;
  category: CategoryDTO[];
  zone: ZoneDTO[];
}

export class InputEntrySearchDataDTO {
  entryDate: Date;
  zoneId?: number;
  districtId?: number;
  stationId?: number;
  categoryId?: number;
  subCategoryId?: number;
  offenderName: string;
  secondaryPerson: string;
  crimeNumber: string;
  repeatOffence: string;
  modusOperandi: string;
  inputData: string;
}

export interface InputEntrySearchResultDTO {
  inputEntryId: number;
  entryDate: Date;
  categoryId: number;
  category: CategoryData;
  subCategoryId: number;
  subCategory: SubCategoryData;
  zoneId: number;
  zone: ZoneData;
  districtId: number;
  district: DistrictData;
  stationId: number;
  station: StationData;
  inputData: string;
  inputDataDocumentUrl: string;
  inputDataDocumentPath: string;
  offenderName: string;
  offenderParentName: string;
  modusOperandi: string;
  priority: string;
  reference: string;
}

export interface CategoryData {
  name: string;
}

export interface SubCategoryData {
  name: string;
}

export interface ZoneData {
  name: string;
}

export interface DistrictData {
  name: string;
}

export interface StationData {
  name: string;
}

export interface CaseDTO {
  caseId: number;
  caseName: string;
}

export interface CaseStatusDTO {
  caseStatusId: number;
  caseStatus: string;
}

export interface PersonTypeDTO {
  personTypeId: number;
  personTypeName: string;
}

export interface PersonStatusDTO {
  statusId: number;
  statusName: string;
}

export class InputEntryPrintSearchDataDTO {
  entryDate: Date;
  zones?: number[];
  categoryId?: number;
  keyword: string;
}

export interface InputDialogData {
  inputEntryId: number;
}