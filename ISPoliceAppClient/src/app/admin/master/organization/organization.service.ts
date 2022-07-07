import { HttpClient, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { forkJoin, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { EventDTO, EventList, OrganizationDTO, OrganizationEventDTO, OrganizationList, SubOrganizationDTO } from './organization.model';

@Injectable({
  providedIn: 'root'
})
export class OrganizationService {

  organizationApiURL = environment.apiUrl;
  eventApiURL=environment.apiUrl;
  private organizationCategoryApiURL = environment.apiUrl;
 
  UploadUrl = environment.apiUrl ;
  downloadUrl = environment.apiUrl ;


  constructor(private http: HttpClient) { }
  getOrganizationeGrid(): Observable<OrganizationDTO[]> {
    return this.http.get<OrganizationDTO[]>(this.organizationApiURL + '/Organization');
  }

  uploadMedia(formData): Observable<any>{
        return this.http.post(`${this.organizationApiURL}/Organization/Upload`, formData, {reportProgress: true, observe: 'events'});
  }
  uploadFlag(formData): Observable<any>{
    return this.http.post(`${this.organizationApiURL}/Organization/Upload`, formData, {reportProgress: true, observe: 'events'});
  }
  uploadSymbol(formData): Observable<any>{
    return this.http.post(`${this.organizationApiURL}/Organization/Upload`, formData, {reportProgress: true, observe: 'events'});
}


  addOrganization(formData): any {
   
    return this.http.post(this.organizationApiURL + '/Organization', formData);
  }

  getOrganizationById(id: number): Observable<any[]> {
    var organization = this.http.get(`${this.organizationApiURL+"/Organization/GetOrganization"}/${id}`);;
   
    return forkJoin([organization]);
  }

 
  editOrganization(id: number, obj: OrganizationList) {
   
    return this.http.put<OrganizationList>(`${this.organizationApiURL+"/Organization"}/${id}`, obj);
  }
  deleteOrganization(id:number) {
    return this.http.delete(`${this.organizationApiURL+"/Organization"}/${id}`)
  }
  downloadOrganizationSymbol( id: number) {
    return this.http.request(new HttpRequest(
      'GET', `${this.downloadUrl}${id}`,
      null,
      {
        reportProgress: true,
        responseType: 'blob'
      }));
  }
  downloadOrganizationFlag( id: number) {
    return this.http.request(new HttpRequest(
      'GET', `${this.downloadUrl}${id}`,
      null,
      {
        reportProgress: true,
        responseType: 'blob'
      }));
  }


  getOrganizationDropdown(): Observable<any[]> {
    return this.http.get<any[]>(this.organizationApiURL + '/Organization/SubOrganizationDropdown');;
  }
  getSubOrganizationById(id: number): Observable<any[]> {
   var subOrganization= this.http.get(`${this.organizationCategoryApiURL}/SubOrganization/${id}`)
     
   return forkJoin([subOrganization]) ;
  }
  getOrganizationEventById(id: number): Observable<any[]> {
    var subOrganization= this.http.get(`${this.organizationCategoryApiURL}/OrganizationEvent/GetOrganizationEvent/${id}`)
      
    return forkJoin([subOrganization]) ;
   }
  
  getSubOrganizationData(): Observable<SubOrganizationDTO[]> {  
    return this.http.get<SubOrganizationDTO[]>(this.organizationCategoryApiURL + '/SubOrganization/SubOrganizationList');
  }
  
  deleteSubOrganization(id: number) {
    return this.http.delete(`${this.organizationCategoryApiURL+'/OrganizationEvent'}/${id}`);
  }

  addSubOrganization(formData:OrganizationList): any {
    return this.http.post(this.organizationCategoryApiURL +  '/SubOrganization', formData);
  }
  editSubOrganization(id: number, organizationData: OrganizationList): any {
    return this.http.put(`${this.organizationCategoryApiURL}/SubOrganization/${id}`, organizationData);
  }

  deleteOrganizationEvent(id: number) {
    return this.http.delete(`${this.organizationCategoryApiURL+'/OrganizationEvent'}/${id}`);
  }
  
  assignEvent(formData): any {   
    return this.http.post(this.eventApiURL +  '/OrganizationEvent/PostOrganizationEvent', formData);
  }
  editOrganizationEvent(id: number, organizationData: OrganizationEventDTO): any {
    return this.http.put(`${this.organizationCategoryApiURL}/OrganizationEvent/${id}`, organizationData);
  }
 
  getSubOrganizationEventData(): Observable<EventDTO[]> {  
    return this.http.get<EventDTO[]>(this.organizationCategoryApiURL + '/SubOrganizationEvent');
  }
  getSubOrganizationEventDataById(id: number): Observable<EventDTO[]> {  
    return this.http.get<EventDTO[]>(`${this.organizationCategoryApiURL}/SubOrganizationEvent/${id}`);
  }
  getOrganizationEventDataById(id:number): Observable<EventDTO[]> { 
    return this.http.get<EventDTO[]>(`${this.organizationCategoryApiURL}/OrganizationEvent/${id}` );
  }
  getOrganizationEventData(): Observable<EventDTO[]> {  
    return this.http.get<EventDTO[]>(this.organizationCategoryApiURL + '/OrganizationEvent');
  }
  deleteSubOrganizationEvent(id: number) {
    return this.http.delete(`${this.organizationCategoryApiURL+'/OrganizationEvent'}/${id}`);
  }
  deleteOrganizationEvents(id: number) {
    return this.http.delete(`${this.organizationCategoryApiURL+'/OrganizationEvent'}/${id}`);
  }
}
