export interface ProfileList{
    id: number;
    name: string;
    hs: string;
    image: string;
    profileTransaction:ProfileTransaction
    profileAlias: ProfileAlias[];
    profileAssociates: ProfileAssociates[];
    profileSpouses: ProfileSpouse[];
    profileChildrens: ProfileChildren[];
    profileSiblings: ProfileSiblings[];
    profileAbstracts: ProfileAbstract[];
    caseDetails: CaseDetail[];

}

export interface ProfileTransaction extends ProfileList{
    id:number;
    profileId: number;
    age: number;
    status: number;
    category: number;
    permanentAddres: string;
    presentAddress: string;
    photo: string;
    fatherName: string;
    motherName: string;
    martialStatus: number;
    spouseName: string;
    education: string;
    occupation: string;
    noOfGoondas: string;
    securityProceeding: string;
    dateOfInitiation: Date;
    lastAction: string;
    lastActionDate: string;
    bail: string;
    bailDate: string;
    entryDate: string;
    isActive: boolean | null;

    profileAssociates:ProfileAssociates[];
    profileAlias:ProfileAlias[];
    profileSpouse:ProfileSpouse[];
    profileChildren:ProfileChildren[];
    profileSiblings:ProfileSiblings[];
    profileAbstract:ProfileAbstract[];
    caseDetail:CaseDetail[];
} 
export interface BaseModel{
    id: number;
    profileId: number;
    isActive: boolean | null;
}
export interface ProfileAlias extends BaseModel {
    name: string;
}
export interface ProfileSpouse extends ProfileAlias{
}
export interface ProfileChildren extends BaseModel{
   
    name: string;
    gender: number;
   
}
export interface ProfileSiblings extends BaseModel{
  
    name: string;
    gender: number;
    relation: number;
   
}
export interface ProfileAssociates extends BaseModel{
 
    associatesId: number;
   
}

export interface ProfileAbstract extends BaseModel{
    distCity: number;
    distCityId: number;
    entryDate: Date;
    jurisdiction: string;
    murder: number;
    attmptMurder: number;
    ndps: number;
    robbery: number;
    chainSnatch: number;
    mobileSnatch: number;
    hbDay: number;
    hbNight: number;
    otherCase:number;
    techCase: number;
    totalCase: number;
   
}

export interface CaseDetail extends BaseModel{
   
    entryDate: Date;
    ps: number;
    cr: number;
    section: number;
    head: number;
    io: number;
    court: number;
    goondas: number;
    stage: number;
    reason: number;
    dsr:number;
}

