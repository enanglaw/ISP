export interface PersonCreationDTO {
  personName: string;
  parentName: string;
  primaryAddress: string;
  historySheetNo: string;
  currentActivity: string;
  statusId: number;
  modusOperandi: string;
  gangId: number;
  gangMemberType: number;
  photoDocument: File;
  personAliasName: PersonAliasName[];
  personAddress: PersonAddress[];
  personCaseHistory: PersonCaseHistory[];
  personPersonType: number[];
  personRivalGang: number[];
  personMedia: PersonMedia[];
}

export interface PersonAliasName {
  aliasName: string;
}

export interface PersonAddress {
  addressLabel: string;
  addressText: string;
}

export interface PersonCaseHistory {
  caseId: number;
  caseStatusId: number;
}

export interface PersonMedia {
  mediaLabel: string;
  media: File;
}

export interface PersonDropdownDTO {
  personId: number;
  personName: string;
}

export interface PersonGridViewDTO {
  personId: number;
  personName: string;
  personAliasNames: string[];
  primaryAddress: string;
  parentName: string;
  historySheetNo: string;
  currentActivity: string;
  status: string;
  modusOperandi: string;
  gang: string;
  gangMemberType: string;
  photoUrl: string;
  // personAddress: PersonAddress[];
}
