
export interface OrganizationList{
    [x: string]: any;
  
    Id: number;
    shortName: string;
    fullName: string;
    ideology: string;
    symbolUrl: string;
    flagUrl: string;
    organizationEvents:OrganizationEvent[]
   /* organizationMedia: OrganizationMedia[];*/
    subOrganizations: SubOrganization[];
    
}
export interface OrganizationDTO{
    id: number;
    shortName: string;
    fullName: string;
    ideology: string;
    symbolUrl: string;
    flagUrl: string;
    organizationEvent:OrganizationEvent[]
   /* organizationMedia: OrganizationMedia[];*/
    subOrganization: SubOrganization[];
    
}
export interface OrganizationAssociate{
    associateId: number;
    
    
}
export interface OrganizationDTO{
    organizationId: number;
    shortName: string;
    fullName: string;
    ideology: string;
    symbolUrl: string;
    flagUrl: string;
    
}
export interface OrganizationEvent{
    id: number;
    title: string;
    eventDate: Date;
    description: string;
    organizationId: number
    isActive: true;
}
export interface OrganizationMedia{
    id: number;
    organizationId: number;
    title: string;
    mediaUrl: string;
    isActive: true;
     
}
export interface SubOrganization
{  
    id: number;
    organizationId: number;
    name: string;
    description: string;
    isActive: true;

    
}
export interface OrganizationDTO
{
  
  OrganizationId: number;
  FullName:string;
  SubOrganizationList:SubOrganizationDTO[];

  
}
export interface SubOrganizationDTO
{
  id: number;
  name: string;
  description: string;
  isActive: boolean | null;
  organizationId: number;
  
}
export interface EventDTO{
    id: number;
    name:string,
    title: string;
    organizationName: string;
    organizationId: number;
    eventDate: Date;
  eventGroupId: number;
  eventGroupName: string;
}

export interface EventList
{
  subOrganizationId:number;
    organizationId: number;
    eventId: number;
  title: string;
  description: string;
  eventDate: Date
 
}
export interface OrganizationEventDTO
{
    id: number;
    title: string;
    eventDate:  Date,
    description: string;
  organizationId: number;
  organizationName: string;
  subOrganizationId: number;
  subOrganizationName: string;
  
}
export interface OrganizationModelList
{
    organizationId: number;
    fullName: string;
  
}
export interface SubOrganizationModelList
{
    organizationId: number;
    subOrganizationId: number;
    name: string;
  
}
export interface OrganizationList
{
 
  organizationId: number;
  fullName: string;
  shortName: string;
  subOrganization: SubOrganization[];
 
}
export interface SubOrganization{
    subOrganizationId: number;
  organizationId:number,
  name:string,
  description: string;
  }
  export interface SubOrganizationData{
  
    organizationId:number,
    name:string,
    description: string;
    }
  export interface OrganizationProfileList{
    organizationId: number;
    fullName: string;
    organiationProfileTransaction :OrganizationTransaction 
    subOrganizationDetails: SubOrganizationDetails[];

}


export interface OrganizationTransaction extends OrganizationProfileList{
    organizationId: number;
    fullName: string;
    subOrganizationDetails:SubOrganizationDetails[];
} 

export interface SubOrganizationDetails{
    id:number;
    organizationId: number;
    name: string;
    description: string;
    isActive: true;
} 
  