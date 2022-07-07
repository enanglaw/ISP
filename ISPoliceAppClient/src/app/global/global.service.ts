import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { forkJoin, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AbstractCreationDTO, OrganizationList, OrganizationTransaction, SubOrganization, SubOrganizationData, SubOrganizationDTO } from './global.model';
@Injectable({
  providedIn: 'root'
})
export class GlobalService {
  private genderApiURL = environment.apiUrl ;
  private religionApiURL = environment.apiUrl ;
  private maritalApiURL = environment.apiUrl ;
  private organizationCategoryApiURL = environment.apiUrl;
  private organizationApiURL = environment.apiUrl ;
  private leaderCategoryApiURL = environment.apiUrl ;

  constructor(private http: HttpClient) { }

   //#region gender
  
  getGenderById(id: number): Observable<AbstractCreationDTO> {
    return this.http.get<AbstractCreationDTO>(`${this.genderApiURL}/${id}`);
  }
  getGenderData(): Observable<AbstractCreationDTO[]> {  
    return this.http.get<AbstractCreationDTO[]>(this.genderApiURL + '/Gender');
  }
  
  deleteGender(id: number) {
    return this.http.delete(`${this.genderApiURL}/${id}`);
  }

  addGender(formData:AbstractCreationDTO): any {
    return this.http.post(this.genderApiURL +  '/Gender/Gender', formData);
  }
  addReligion(formData:AbstractCreationDTO): any {
    return this.http.post(this.religionApiURL +  '/Religion', formData);
  }
  editGender(id: number, genderData: OrganizationList) {
    var data = this.BuildGenderFormData(genderData);
    return this.http.put(`${this.genderApiURL}/${id}`, data);
  }
  private BuildGenderFormData(genderCreationDTO: OrganizationList): FormData {
    const formData = new FormData();

    formData.append('Name', genderCreationDTO.fullName);
   
    return formData;
  }
  //#endregion
 //#region Religion
  getReligionForDropdown(): Observable<AbstractCreationDTO[]>
  {
  return this.http.get<AbstractCreationDTO[]>(
    this.religionApiURL + '/Religion'
  );
  }
 

getReligionById(id: number): Observable<AbstractCreationDTO> {
  return this.http.get<AbstractCreationDTO>(`${this.religionApiURL}/${id}`);
}
getReligionData(): Observable<AbstractCreationDTO[]> {  
  return this.http.get<AbstractCreationDTO[]>(this.religionApiURL + '/Religion');
}
deleteReligion(id: number) {
  return this.http.delete(`${this.religionApiURL}/${id}`);
}


createReligion(genderData: OrganizationList) {
  var data = this.BuildGenderFormData(genderData);
  return this.http.post(this.religionApiURL, data);
}

editReligion(id: number, genderData: OrganizationList) {
  var data = this.BuildGenderFormData(genderData);
  return this.http.put(`${this.religionApiURL}/${id}`, data);
}
//#endregion
  //#region Marital
  getMaritalDropdown(): Observable<AbstractCreationDTO[]>
  {
  return this.http.get<AbstractCreationDTO[]>(
    this.maritalApiURL + '/MaritalStatus'
  );
}
  getMaritalById(id: number): Observable<AbstractCreationDTO>
  {
  return this.http.get<AbstractCreationDTO>(`${this.maritalApiURL}/${id}`);
  }

deleteMaritalStatus(id: number) {
  return this.http.delete(`${this.maritalApiURL}/${id}`);
}


createMaritalStatus(maritalStatusData: OrganizationList) {
  var data = this.BuildGenderFormData(maritalStatusData);
  return this.http.post(this.maritalApiURL, data);
}
addMaritalStatus(formData:AbstractCreationDTO): any {
  return this.http.post(this.maritalApiURL +  '/MaritalStatus', formData);
}
getMaritalStatusData(): Observable<AbstractCreationDTO[]> {  
  return this.http.get<AbstractCreationDTO[]>(this.maritalApiURL + '/MaritalStatus');
}
editMaritalStatus(id: number, maritalStatusData: OrganizationList) {
  var data = this.BuildGenderFormData(maritalStatusData);
  return this.http.put(`${this.maritalApiURL}/${id}`, data);
}
//#endMarital
  //#region Sub Organization

  getOrganizationDropdown(): Observable<any[]> {
    return this.http.get<any[]>(this.organizationApiURL + '/Organization/OrganizationDropdown');;
  }
  getSubOrganizationById(id: number): Observable<SubOrganizationDTO[]> {
   var subOrganization= this.http.get<SubOrganizationDTO>(`${this.organizationCategoryApiURL}/SubOrganization/${id}`)
     
   return forkJoin([subOrganization]) ;
  }
  getSubOrganizationData(): Observable<SubOrganizationDTO[]> {  
    return this.http.get<SubOrganizationDTO[]>(this.organizationCategoryApiURL + '/SubOrganization/SubOrganizationList');
  }
  
  deleteSubOrganization(id: number) {
    return this.http.delete(`${this.organizationCategoryApiURL+'/SubOrganization'}/${id}`);
  }

  addSubOrganization(formData: OrganizationList): any {
   
    return this.http.post(this.organizationCategoryApiURL +  '/SubOrganization', formData);
  }
  editSubOrganization(id: number, organizationData: OrganizationList): any {
    return this.http.put(`${this.organizationCategoryApiURL}/SubOrganization/${id}`, organizationData);
  }
   
  //#endregion sub Organization
}
