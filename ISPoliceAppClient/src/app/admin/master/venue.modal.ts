export interface VenuePermissionCreationDTO {
  venueId: number;
  venuePermissionTypeId: number;
  venueDate: Date;
  organizationName: string;
  reason: string;
  expectedStrength: number;
  leadBy: string;
  applicantName: string;
  address: string;
  phone: string;
  requestApplication: File;
  isDeskApproved: boolean;
  isDeskApprovedOn: Date;
  isDeskNotes: string;
  isDeskAdvisory: string;
  acApproved: boolean;
  acApprovedOn: Date;
  acNotes: string;
  acAdvisory: string;
  acApprovalLetter: File;
  dcApproved: boolean;
  dcApprovedOn: Date;
  dcNotes: string;
  dcAdvisory: string;
  dcApprovalLetter: File;
  copApproved: boolean;
  copApprovedOn: Date;
  copNotes: string;
  copAdvisory: string;
  copApprovalLetter: File;
}

export interface VenuePermissionTypeCreationDTO {
  venuePermissionTypeId: number;
  venuePermissionTypeName: string;
  venueId: number;
  isActive: boolean;
}

export interface VenuePermissionTypeDTO {
  venuePermissionTypeId: number;
  venuePermissionTypeName: string;
}

export interface VenuePermissionGridDTO {
  venuePermissionTypeId: number;
  venuePermissionTypeName: string;
  venueName: string;
  isActive: boolean;
}

export interface Venue {
  venueId: number;
  venueName: string;
}

export interface VenueCreationDTO {
  venueId: number;
  venueName: string;
  isActive: boolean;
}

export interface VenueDropdownDTO {
  venueId: number;
  venueName: string;
  venuePermissionTypes: string[];
  venuePermissionType: VenuePermissionTypeDTO[];
}

export interface VenueGridDTO {
  venueId: number;
  venueName: string;
  isActive: boolean;
  venuePermissionType: VenuePermissionTypeDTO[];
}

export interface VenuePermissionViewDTO {
  venuePermissionId: number;
  venueId: number;
  venueName: string;
  venueDate: Date;
  venuePermissionTypeId: number;
  venuePermissionTypeName: string;
  organizationName: string;
  reason: string;
  expectedStrength: number;
  leadBy: string;
  applicantName: string;
  phone: string;
  requestApplicationUrl: string;
  requestApplicationFile: string;
  actualStrength: number;
  notes: string;
  venuePermissionStatus: VenuePermissionStatusDTO[];
  venuePermissionCase: VenuePermissionCaseViewDTO[];
}

export interface VenuePermissionStatusDTO {
  venuePermissionStatusId: number;
  venuePermissionId: number;
  permissionStatus: string;
  statusCode: number;
  statusOn: Date;
  statusNotes: string;
  supportDocumentPath: string;
  supportDocumentUrl: string;
  createdBy: number;
  createdOn: Date;
}

export interface VenuePermissionStatusCreationDTO {
  venuePermissionStatusId: number;
  venuePermissionId: number;
  statusCode: number;
  statusOn: Date;
  statusNotes: string;
  supportDocument: File;
  isDocumentDeleted: boolean;
}

export interface VenuePermissionStatusViewDTO {
  venuePermissionStatusId: number;
  venuePermissionId: number;
  statusCode: number;
  statusOn: Date;
  statusNotes: string;
  supportDocumentPath: string;
  supportDocumentUrl: string;
}

export interface VenuePermissionAfterEventDetailCreationDTO {
  venuePermissionId: number;
  actualStrength: number;
  notes: string;
  venuePermissionCaseHistory: VenuePermissionCaseDTO[];
}

export interface VenuePermissionCaseDTO {
  venuePermissionCaseId: number;
  venuePermissionId: number;
  personName: string;
  crimeNumber: string;
  sectionNo: string;
}

export interface VenuePermissionCaseViewDTO {
  venuePermissionCaseId: number;
  venuePermissionId: number;
  personName: string;
  crimeNumber: string;
  sectionNo: string;
}
