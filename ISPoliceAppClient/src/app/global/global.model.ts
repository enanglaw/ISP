
  
export interface AbstractCreationDTO
 {   
    Name: string;
 }
export interface AbstractUpdateDTO
 {   id:number,
    Name: string;
 }

export interface MaritalsCreationDTO
{
    maritalId: number;
    Name: string;
}
export interface LeadersCategoryCreationDTO
{
    leadersId: number;
    Name: string;
  }
  
export interface LeadersCategoryUpdateDTO
{
    leadersId: number;
    Name: string;
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
  subOrganizationName: string;
  organizationName: string;
  description: string;
  isActive: boolean | null;
  organizationId: number;

}
export interface OrganizationList
{
 
  organizationId: number;
  fullName: string;
  shortName: string;
  subOrganization: SubOrganization[];
 
}
export interface SubOrganization{
  id: number;
  subOrganizationName: string;
  organizationName: string;
  description: string;
  isActive: boolean | null;
  organizationId: number;
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
  
  
 
  