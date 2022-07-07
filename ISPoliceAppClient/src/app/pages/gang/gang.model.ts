export interface GangDTO {
  gangId: number;
  gangName: string;
  leader: string;
  activityArea: string;
  modusOperandi: string;
  createdBy: number;
  createdOn: Date;
  updatedBy: number;
  updatedOn: Date;
  gangActivity: GangActivity[];
  gangMedia: GangMedia[];
  gangMember: GangMember[];
}

export interface GangViewDTO {
  gangId: number;
  gangName: string;
  leader: string;
  activityArea: string;
  modusOperandi: string;
  // gangActivity: GangActivity[];
  // gangMedia: GangMedia[];
  // gangMember: GangMember[];
}

export interface GangCreationDTO {
  gangName: string;
  leader: string;
  activityArea: string;
  modusOperandi: string;
  // createdBy?: number;
  // createdOn: Date;
  // updatedBy?: number;
  // updatedOn: Date;
  gangActivityIds: number[];
  gangMedia: File[];
  gangMember: GangMember[];
}

export interface GangActivity {
  gangActivityId: number;
  gangId: number;
  activityTypeId: number;
  createdBy: number;
  createdOn: Date;
  updatedBy: number;
  updatedOn: Date;
  activityType: ActivityType;
}

export interface ActivityType {
  activityTypeId: number;
  activityTypeName: string;
  // isActive: boolean;
  // createdBy: number;
  // createdOn: Date;
  // updatedBy: number;
  // updatedOn: Date;
}

export interface GangMedia {
  gangMediaId: number;
  gangId: number;
  documentName: string;
  documentPath: string;
  createdBy: number;
  createdOn: Date;
  updatedBy: number;
  updatedOn: Date;
}

export interface GangMember {
  // gangMemberId: number;
  // gangId: number;
  memberName: string;
  isLeader: boolean;
  isProminant: boolean;
  // createdBy: number;
  // createdOn: Date;
  // updatedBy: number;
  // updatedOn: Date;
}

export interface GangDropdownDTO {
  gangId: number;
  gangName: string;
}

export interface Activity {
  gangActivityId: number;
  activityTypeId: number;
  activityTypeName: string;
}

export interface Media {
  gangMediaId: number;
  documentName: string;
  documentPath: string;
}

export interface Member {
  gangMemberId: number;
  memberName: string;
  isLeader: boolean;
  isProminant: boolean;
}

export interface GangDetailViewDTO {
  gangId: number;
  gangName: string;
  leader: string;
  activityArea: string;
  modusOperandi: string;
  activities: Activity[];
  members: Member[];
  medias: Media[];
}

export interface GangDialogData {
  gangId: number;
}
