

export interface LeadersList{   
    id: number;
    name: string;
    designation: string;
    mobileNumber: string;
    address: string;    
    subOrganizationLeaderId: number;
    organizationLeaderId: number;
    organizationName: string;
    subOrganizationName: string;
    //leaderId:number, 
    leaderName: string;       
    alias: string;       
    placeOfBirth: string;       
    dateOfBirth: string;
    caste: string;       
    permanentAddress: string;     
    presentAddress: string;       
    nativeDistrict: string;
    properties: string;
    strinkingPersonalityTrait: string;
    presentPartyAffiliation: string;
    positionInTheParty: string;
    religionId: number; 
    religionName: string;
    genderId: number;
    genderName: string;
    leadersId: number;
    maritalStatusId: number;
    maritalStatusName: string;
    leaderPoliticalBackgrounds: PoliticalBackground[];
	leaderMedia:LeaderMedia[];
	leaderEvents:LeaderEvent[];
    
}
export interface DetailInfo{
    leaderId: number; 
    leaderName: string;       
    alias: string;       
    placeOfBirth: string;       
    dateOfBirth: string;
    caste: string;       
    permanentAddress: string;     
    presentAddress: string;       
    nativeDistrict: string;
    properties: string;
    strinkingPersonalityTrait: string;
    presentPartyAffiliation: string;
    positionInTheParty: string;
    religionId: number;  
    genderId: number;
    leadersId: number;
    maritalStatusId: number;
}
export interface PoliticalBackground{
    id: number;
    position: string;
    positionYear: Date;
    leaderId: number;
    isActive: boolean | null;
     }
export interface LeaderMedia{
    id: number;
    title: string;
    createdDate: Date;
    leaderMediaPath: string;
	leaderMediaUrl: string;
    leaderId: number;
    isActive: boolean | null;
     }
export interface LeaderEvent{
    id: number;
    title: string;
    eventDate: Date;
    description: string;
    leaderId: number
    isActive: boolean | null;
     }
export interface LeadersModel{
    id: number;
    name: string;
    designation: string;
    mobileNumber: string;
    address: string;    
    subOrganizationId: number,
    organizationId: number,
    organizationName: string,
    subOrganizationName:string,
   // detailInfo: DetailInfo[];
    politicalBackground: PoliticalBackground[];
	leaderMedia:LeaderMedia[];
	leaderEvent:LeaderEvent[];

}
export interface LeadersDTO{
    id: number;
    name: string;
    designation: string;
    mobileNumber: string;
    address: string;
    organizationId: number;
    subOrganizationId: number;
    organizationName: string;
    subOrganizationName: string;
    alias: string;
    caste: string;
    dateOfBirth: string;
genderId: number;
genderName: string;
groupName: string
leaderId: number
leaderEventCreations: string;
leaderMediaCreations: string;
leaderName: string;
leaderPoliticalBackgrounds: string;
maritalStatusId: number;
maritalStatusName: string;
nativeDistrict: string;
permanentAddress: string;
placeOfBirth: string;
positionInTheParty: string;
presentAddress: string;
presentPartyAffiliation: string;
properties: string;
religionId: number;
religionName: string;
strinkingPersonalityTrait: string;
politicalBackground: PoliticalBackground[];
leaderMedia:LeaderMedia[];
leaderEvent:LeaderEvent[];
}
export interface LeaderList{
    id: number;
    name: string;
    designation: string;
    mobileNumber: string;
    address: string;
    organizationId: number;
    subOrganizationId: number;
    organizationName: string;
    subOrganizationName: string;
    politicalBackground: PoliticalBackground[];
    leaderMedia:LeaderMedia[];
    leaderEvent:LeaderEvent[];
      
}
export interface LeaderDTO{
    id: number;
    name: string;
    designation: string;
    mobileNumber: string;
    address: string;
    organizationId: number;
    subOrganizationId: number;
    organizationName: string;
    subOrganizationName: string;
}
export interface SubOrganizationDTO
{
  id: number;
  name: string;
  description: string;
  isActive: boolean | null;
  organizationId: number;
  
}
export interface Organizations
{  
   
    organizationId: number;
    subOrganizationId: string;
  
    

}

export interface OrganizationModelList
{
    organizationId: number;
    fullName: string;
  
}
export interface SubOrganizationModelList
{
    subOrganizationId: number;
    name: string;
  
}