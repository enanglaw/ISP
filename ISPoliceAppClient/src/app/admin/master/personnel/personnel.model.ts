
export interface PersonnelList{
    id: number;
    name: string;
    image: string;
    personnelId: number;           
    currentRank: string;        
    personalNumber: string;        
    dateOffBirth: string;
    dateOfEnlistment: string;       
    presentPosting: string;       
    dateOfJoiningPresentPosting: string;
   genderId: number;
   genderName: string;
   maritalStatusId: number;
   maritalStatusName: string;
   religionId: number;
   religionName: string;
   fatherName: string;
   motherName: string;
   permanentAddress: string;
   presentAddress: string;
   contactNumber: string;      
   email: string;
    personnelPhotoUrl: string;
    personnelCaseDetails: PersonnelCaseDetails[];
    personnelEducationalBackgrounds: PersonnelEducationalBackgrounds[];
    personnelGallantryAwards: PersonnelGallantryAwards[];
    personnelPostings: PersonnelPostings[];
    personnelPreviousAllegations: PersonnelPreviousAllegations[];
    personnelWarningOrPunishments: PersonnelWarningOrPunishments[];
    personnelSpouses: PersonnelSpouses[];
    personnelChildrens: PersonnelChildrens[];
   
   

}


export interface PersonnelFamily{
   
    
    familyId: number;
   
}
export interface PersonnelBase{
   
    id: number;
    isActive: boolean | null;
    personnelId: number;
   
}
export interface PersonnelSpouses extends PersonnelBase {
    firstName: string;
    lastName: string;
}
export interface PersonnelChildrens extends PersonnelBase {
   
    firstName: string;
    lastName: string;
   
}
export interface PersonnelSiblings {
  
    name: string;
    gender: number;
    relation: number;
   
}
export interface PersonnelGender{
   
    genderName: string;
    genderId: number;
   
}
export interface PersonnelReligion{
   
    religionName: string;
    religionId: number;
   
}
export interface PersonnelMaritalStatus{
   
    maritalStatusName: string;
    maritalStatusId: number;
   
}


export interface PersonnelCaseDetails extends PersonnelBase {
    id: number;
    caseDate: Date;
    caseNumber: string;
    title: string;
    caseSection: string;
    createdDate: Date;
    currentStatus: string;
   
}

export interface PersonnelEducationalBackgrounds extends PersonnelBase {
    id: number;
    admissionYear: Date;
    institutionName: string;
    courseOfStudy: string;
    qualificationName: string;
    graduationYear: string;
  
}

export interface PersonnelGallantryAwards extends PersonnelBase{
    title: string;
    issueingAuthority: string;
    issuingDate: Date;
    awardDocumentUrl: string;
    id: number;
   
}
export interface PersonnelSpouses extends PersonnelBase{
    firstName: string;
    lastName: string;
    id: number;
   
}
export interface PersonnelChildrens extends PersonnelBase{
    firstName: string;
    lastName: string;
    id: number;
   
}
export interface PersonnelPostings extends PersonnelBase {
   
    id: number;
    to: Date;
    from: Date;
    place: string;
    post: string;
   
}
export interface PersonnelPreviousAllegations extends PersonnelBase {
   
    attachmentUrl: string;
    result: string;
    allegedDate: Date;
    description: string;
    id: number;
}

export interface PersonnelWarningOrPunishments  extends PersonnelBase{
   
     title:string
     attachmentUrl: string;
    warningDate: Date;
     id:  number
      
  
}
  
  
  export interface PersonnelAddress {
    addressLabel: string;
    addressText: string;
  }
  
  export interface PersonnelCaseHistory {
    caseId: number;
    caseStatusId: number;
  }
  
  export interface PersonnelMedia {
    mediaLabel: string;
    media: File;
  }
  
  export interface PersonnelDropdownDTO {
    personId: number;
    personName: string;
  }
  
  export interface PersonnelGridViewDTO {
    id :number;
    name :string;
    currentRank :string;
    personalNumber :string;
    dateOffBirth: string;
    dateOfEnlistment : string;
    presentPosting :string;
    dateOfJoiningPresentPosting :string;  
    permanentAddress :string;
    presentAddress :string;
   contactNumber :string;
      email: string;
      photoUrl: string;
    // personAddress: PersonAddress[];
  }
